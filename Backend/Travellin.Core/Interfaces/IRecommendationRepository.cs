using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Travellin.Core.Entities;

namespace Travellin.Core.Interfaces
{
    public interface IRecommendationRepository: IGenericRepository<Recommendations, int>
    {
        Task<IEnumerable<Recommendations>> GetUserRecommendationsAsync(string userId, int limit = 10);

    }
}
