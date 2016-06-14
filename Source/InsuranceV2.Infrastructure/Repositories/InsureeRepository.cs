using System.Collections.Generic;
using System.Linq;
using InsuranceV2.Common.Models;
using InsuranceV2.Infrastructure.Database.Context;

namespace InsuranceV2.Infrastructure.Repositories
{
    public class InsureeRepository : Repository<Insuree>, IInsureeRepository
    {
        public IEnumerable<Insuree> FindByLastName(string lastName)
        {
            return DataContextFactory.GetDataContext().Set<Insuree>().Where(x => x.LastName == lastName).ToList();
        }
    }
}