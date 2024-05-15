using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using System.Data.Entity;
using System.Drawing;

namespace NZWalks.API.Repository
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDBContext dBContext;

        public SQLRegionRepository(NZWalksDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public async Task<RegionDomain> CreateAsync(RegionDomain region)
        {
            await dBContext.Regions.AddAsync(region);
            await dBContext.SaveChangesAsync();
            return region;
        }

        public async Task<RegionDomain?> DeleteAsync(Guid id)
        {
            var existingRegion= await dBContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if(existingRegion == null)
            {
                return null;
            }
             dBContext.Regions.Remove(existingRegion);
             await dBContext.SaveChangesAsync();
             return existingRegion;
        }

        public async Task<List<RegionDomain>> GetAllAsync()
        {
            return await dBContext.Regions.ToListAsync();
        }

        public async Task<RegionDomain?> GetByIdAsync(Guid id)
        {
           return await dBContext.Regions.FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<RegionDomain?> UpdateAsync(Guid id, RegionDomain region)
        {
            var existingRegion = await dBContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
            {
                return null;
            }
            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageURL= region.RegionImageURL;

            await dBContext.SaveChangesAsync();
            return existingRegion;
        }
    }
}
