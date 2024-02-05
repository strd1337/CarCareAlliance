using CarCareAlliance.Domain.Common.Models;
using CarCareAlliance.Domain.ExpenseHistoryAggregate.ValueObjects;
using CarCareAlliance.Domain.PhotoAggregate.ValueObjects;
using CarCareAlliance.Domain.UserProfileAggregate.ValueObjects;

namespace CarCareAlliance.Domain.ExpenseHistoryAggregate
{
    public sealed class ExpenseHistory : AggregateRoot<ExpenseHistoryId, Guid>
    {
        public DateTime Date { get; private set; }
        public float Amount { get; private set; }
        public string Description { get; private set; }
        public UserProfileId UserProfileId { get; private set; }
        public PhotoId ReceiptImage { get; private set; }

        private ExpenseHistory(
            ExpenseHistoryId id,
            DateTime date,
            float amount,
            string description,
            UserProfileId userProfileId,
            PhotoId receiptImage) : base(id)
        {
            Date = date;
            Amount = amount;
            Description = description;
            UserProfileId = userProfileId;
            ReceiptImage = receiptImage;
        }

        public static ExpenseHistory Create(
            DateTime date,
            float amount,
            string description,
            UserProfileId userProfileId,
            PhotoId receiptImage)
        {
            return new ExpenseHistory(
                ExpenseHistoryId.CreateUnique(),
                date,
                amount,
                description,
                userProfileId,
                receiptImage);
        }

#pragma warning disable CS8618
        private ExpenseHistory()
        {
        }
#pragma warning restore CS8618
    }
}
