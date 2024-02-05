using CarCareAlliance.Domain.Common.Models;

namespace CarCareAlliance.Domain.ExpenseHistoryAggregate.ValueObjects
{
    public sealed class ExpenseHistoryId : AggregateRootId<Guid>
    {
        public override Guid Value { get; protected set; }

        private ExpenseHistoryId(Guid value)
        {
            Value = value;
        }

        public static ExpenseHistoryId CreateUnique() => new(Guid.NewGuid());

        public static ExpenseHistoryId Create(Guid value)
            => new(value);

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
