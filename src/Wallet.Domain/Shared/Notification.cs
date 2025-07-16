namespace Wallet.Domain.Shared;

public record NotificationError(string Context, string Message);

public class Notification
{
    private readonly Dictionary<string, List<string>> _errors = new();

    public IReadOnlyList<NotificationError> GetErrors()
    {
        return _errors
            .SelectMany(pair => pair.Value.Select(message => new NotificationError(pair.Key, message)))
            .ToList();
    }

    public void AddError(string context, string message)
    {
        var error = new NotificationError(context, message);
        if (!_errors.ContainsKey(error.Context))
        {
            _errors[error.Context] = new List<string>();
        }

        _errors[error.Context].Add(error.Message);
    }

    public bool HasErrors() => _errors.Any();

    public IReadOnlyList<string> GetMessages(string? context = null)
    {
        if (string.IsNullOrEmpty(context))
        {
            return _errors.Values.SelectMany(x => x).ToList();
        }

        return _errors.TryGetValue(context, out var messages) ? messages : new List<string>();
    }

    public string GetMessagesNormalized()
    {
        return string.Join(", ",
            _errors.Select(kvp =>
                string.Join(", ", kvp.Value.Select(msg => $"{kvp.Key}: {msg}"))
            )
        );
    }

    public void CopyErrors(Notification other)
    {
        if (!other.HasErrors()) return;

        foreach (var (context, messages) in other._errors)
        {
            foreach (var message in messages)
            {
                AddError(message, context);
            }
        }
    }

}
