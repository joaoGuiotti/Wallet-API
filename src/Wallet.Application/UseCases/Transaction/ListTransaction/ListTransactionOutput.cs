using System;
using System.Collections.Generic;
using System.Linq;
using Wallet.Application.Common;
using Wallet.Application.UseCases.Transaction.Common;
using Wallet.Domain.Repository;
using DomainEntity = Wallet.Domain.Entities;

namespace Wallet.Application.UseCases.Transaction.ListTransaction
{
    public class ListTransactionOutput : PaginatedListOutput<TransactionModelOutput>
    {
        public ListTransactionOutput(int page, int perPage, int total, IReadOnlyList<TransactionModelOutput> items)
            : base(page, perPage, total, items) { }

        public static ListTransactionOutput FromSearchOutput(
            SearchOutput<DomainEntity.Transaction> search
        )
            => new(
                search.CurrentPage,
                search.PerPage,
                search.Total,
                search.Items
                    .Select(TransactionModelOutput.FromTransaction)
                    .ToList().AsReadOnly()
            );
    }
}
