﻿using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.Vehicles.Common;

namespace CarCareAlliance.Application.Vehicles.Queries.Get
{
    public record VehicleGetQuery(
        Guid VehicleId) : IQuery<VehicleGetResult>;
}
