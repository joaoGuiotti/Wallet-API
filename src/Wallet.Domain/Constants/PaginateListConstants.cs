using Wallet.Domain.Repository;

namespace Wallet.Domain.Constants
{
    public static class PaginateListConstants
    {
        public const int DefaultPage = 1;
        public const int DefaultPerPage = 15;
        public const string DefaultSearchTerm = "";
        public const string DefaultSort = "";
        public const ESearchOrder DefaultDir = ESearchOrder.Asc;
    }
}
