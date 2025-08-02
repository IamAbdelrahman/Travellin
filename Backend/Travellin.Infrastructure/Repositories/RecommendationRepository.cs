using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Travellin.Core.Entities;
using Travellin.Core.Interfaces;
using Travellin.Infrastructure.Data;
namespace Travellin.Infrastructure.Repositories
{
    public class RecommendationRepository: GenericRepository<Recommendations, int>, IRecommendationRepository
    {
        public RecommendationRepository(TravellinDbContext dbContext) : base(dbContext) { }
        public async Task<IEnumerable<Recommendations>> GetUserRecommendationsAsync(string userId, int limit = 10)
        {
            return await _dbContext.Recommendations
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.Score)
                .ThenByDescending(r => r.CreatedAt)
                .Take(limit)
                .ToListAsync();
        }

    }
}
