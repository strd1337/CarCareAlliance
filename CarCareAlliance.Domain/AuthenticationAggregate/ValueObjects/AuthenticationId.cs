﻿using CarCareAlliance.Domain.Common.Models;

namespace CarCareAlliance.Domain.AuthenticationAggregate.ValueObjects
{
    public sealed class AuthenticationId : AggregateRootId<Guid>
    {
        public override Guid Value { get; protected set; }

        private AuthenticationId(Guid value)
        {
            Value = value;
        }

        public static AuthenticationId CreateUnique() => new(Guid.NewGuid());

        public static AuthenticationId Create(Guid value)
            => new(value);

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
