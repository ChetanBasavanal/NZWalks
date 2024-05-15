using NZWalks.API.Models.Domain;
using System.Drawing;
using System.Runtime.InteropServices;

namespace NZWalks.API.Repository
{
    public interface IRegionRepository
    {
         Task< List<RegionDomain> > GetAllAsync();
        Task<RegionDomain?>GetByIdAsync(Guid id);  //Making it as nullable, since there is possiblity returing null.

        Task<RegionDomain> CreateAsync(RegionDomain region);

        Task<RegionDomain?> UpdateAsync(Guid id,RegionDomain region); //Making it as nullable, since there is possiblity returing null.
        Task<RegionDomain?> DeleteAsync(Guid id);
        
    }
}
