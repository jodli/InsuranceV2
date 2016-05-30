using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using InsuranceV2.Common;
using InsuranceV2.Common.Exceptions;
using InsuranceV2.Common.Models;
using InsuranceV2.Infrastructure.Database.Configurations;

namespace InsuranceV2.Infrastructure.Database.Context
{
    public class InsuranceAppContext : DbContext
    {
        public InsuranceAppContext() : base("Default")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Insuree> Insurees { get; set; }

        public override int SaveChanges()
        {
            var orphanedObjects = ChangeTracker.Entries().Where(
                e =>
                    (e.State == EntityState.Modified || e.State == EntityState.Added) &&
                    e.Entity.GetType().GetInterfaces().Any(x =>
                        x.IsGenericType && x.GetGenericTypeDefinition() == typeof (IHasOwner<>)) &&
                    e.Reference("Owner").CurrentValue == null);

            foreach (var orphanedObject in orphanedObjects)
            {
                orphanedObject.State = EntityState.Deleted;
            }

            try
            {
                var modified = ChangeTracker.Entries().Where(
                    e => e.State == EntityState.Modified || e.State == EntityState.Added);

                foreach (var entry in modified)
                {
                    var changedOrAddedItem = entry.Entity as IDateTracking;
                    if (changedOrAddedItem != null)
                    {
                        if (entry.State == EntityState.Added)
                        {
                            changedOrAddedItem.DateCreated = DateTime.Now;
                        }
                        changedOrAddedItem.DateModified = DateTime.Now;
                    }
                }
                return base.SaveChanges();
            }
            catch (DbEntityValidationException validationException)
            {
                var errors = validationException.EntityValidationErrors;
                var result = new StringBuilder();
                var allErrors = new List<ValidationResult>();

                foreach (var error in errors)
                {
                    foreach (var validationError in error.ValidationErrors)
                    {
                        result.Append(
                            $"\r\n Entity of type {error.Entry.Entity.GetType()} has validation error \"{validationError.ErrorMessage}\" for property {validationError.PropertyName}.\r\n");

                        var domainEntity = error.Entry.Entity as DomainEntity<int>;
                        if (domainEntity != null)
                        {
                            result.Append(domainEntity.IsTransient()
                                ? $" This entity was added in this session.\r\n"
                                : $" The Id of the entity is {domainEntity.Id}.\r\n");
                        }
                        allErrors.Add(new ValidationResult(validationError.ErrorMessage,
                            new[] {validationError.PropertyName}));
                    }
                }
                throw new ModelValidationException(result.ToString(), validationException, allErrors);
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new InsureeConfiguration());
            modelBuilder.Configurations.Add(new AddressConfiguration());
            modelBuilder.Configurations.Add(new EmailAddressConfiguration());
            modelBuilder.Configurations.Add(new PhoneNumberConfiguration());
        }
    }
}