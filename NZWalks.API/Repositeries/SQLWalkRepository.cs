using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using System.Globalization;

namespace NZWalks.API.Repositeries
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDBContext nZWalksDBContext;

        public SQLWalkRepository(NZWalksDBContext nZWalksDBContext)
        {
            this.nZWalksDBContext = nZWalksDBContext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await nZWalksDBContext.Walks.AddAsync(walk);
            await nZWalksDBContext.SaveChangesAsync();
            return walk;
        }

        public async Task<List<Walk>> GetAllAsync(string? name = null, string? sortBy = null, bool isAscending = true)
        {
            //    return await nZWalksDBContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
            var walks = nZWalksDBContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            //Filtering
            if (!string.IsNullOrWhiteSpace(name))
            {
                walks = walks.Where(x => x.Name.Contains(name));
            }

            //Sorting

            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }

            return await walks.ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(int id)
        {
            return await nZWalksDBContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> UpdateAsync(int id, Walk walk)
        {
            var existingWalk = await nZWalksDBContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (existingWalk == null)
            {
                return null;
            }
            existingWalk.Name = walk.Name;
            existingWalk.Description = walk.Description;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.WalkImgUrl = walk.WalkImgUrl;
            existingWalk.DifficultyId = walk.DifficultyId;
            existingWalk.RegionId = walk.RegionId;
            await nZWalksDBContext.SaveChangesAsync();
            return existingWalk;

        }

        public async Task<Walk?> DeleteAsync(int id)
        {
            var existingWalk = await nZWalksDBContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (existingWalk == null)
            {
                return null;
            }

            nZWalksDBContext.Remove(existingWalk);
            await nZWalksDBContext.SaveChangesAsync();
            return existingWalk;
        }
    }
}
