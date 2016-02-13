using System.Collections.Generic;
using InsuranceV2.Common.Models;

namespace InsuranceV2.Infrastructure.Repositories
{
    public interface IInsureeRepository : IRepository<Insuree, int>
    {
        IEnumerable<Insuree> FindByLastName(string lastName);
    }
}