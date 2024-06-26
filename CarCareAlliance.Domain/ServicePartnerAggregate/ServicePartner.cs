﻿using CarCareAlliance.Domain.Common.Models;
using CarCareAlliance.Domain.MechanicAggregate;
using CarCareAlliance.Domain.MechanicAggregate.ValueObjects;
using CarCareAlliance.Domain.PhotoAggregate.ValueObjects;
using CarCareAlliance.Domain.ReviewAggregate.ValueObjects;
using CarCareAlliance.Domain.ServicePartnerAggregate.Entities;
using CarCareAlliance.Domain.ServicePartnerAggregate.ValueObjects;
using CarCareAlliance.Domain.WorkScheduleAggregate.ValueObjects;

namespace CarCareAlliance.Domain.ServicePartnerAggregate
{
    public sealed class ServicePartner : AggregateRoot<ServicePartnerId, Guid>
    {
        private readonly List<ServiceCategory> serviceCategories = [];
        private readonly List<PhotoId> photoIds = [];
        private readonly List<ReviewId> reviewIds = [];
        private readonly List<MechanicProfileId> mechanicProfileIds = [];
        private readonly List<WorkScheduleId> workScheduleIds = [];

        public string Name { get; private set; }
        public string Description { get; private set; }
        public PhotoId? LogoId { get; private set; }
        public ServiceLocation ServiceLocation { get; private set; }

        public IReadOnlyList<PhotoId> PhotoIds => photoIds.AsReadOnly();
        public IReadOnlyList<ReviewId> ReviewIds => reviewIds.AsReadOnly();

        public IReadOnlyList<WorkScheduleId> WorkScheduleIds
           => workScheduleIds.AsReadOnly();

        public IReadOnlyList<ServiceCategory> ServiceCategories 
            => serviceCategories.AsReadOnly();

        public IReadOnlyList<MechanicProfileId> MechanicProfileIds 
            => mechanicProfileIds.AsReadOnly();

        private ServicePartner(
            ServicePartnerId id,
            string name,
            string description,
            ServiceLocation serviceLocation,
            List<ServiceCategory> serviceCategories) : base(id)
        {
            Name = name;
            Description = description;
            ServiceLocation = serviceLocation;
            this.serviceCategories = serviceCategories;
        }

        public static ServicePartner Create(
            string name,
            string description,
            ServiceLocation serviceLocation,
            List<ServiceCategory> serviceCategories)
        {
            return new ServicePartner(
                ServicePartnerId.CreateUnique(),
                name,
                description,
                serviceLocation,
                serviceCategories);
        }

        public void AddServiceCategory(ServiceCategory category)
        {
            serviceCategories.Add(category);
        }

        public void AddWorkSchedule(WorkScheduleId workScheduleId)
        {
            workScheduleIds.Add(workScheduleId);
        }

        public void UpdateLogo(PhotoId photoId)
        {
            LogoId = photoId;
        }

        public void AddMechanicProfile(MechanicProfile mechanic)
        {
            mechanicProfileIds.Add(MechanicProfileId.Create(mechanic.Id.Value));
        }

        public void UpdateLocation(ServiceLocation location)
        {
            ServiceLocation = location;
        }

        public void Update(string name, string description)
        {
            Name = name;
            Description = description;
        }

#pragma warning disable CS8618
        private ServicePartner()
        {
        }
#pragma warning restore CS8618
    }
}