using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travellin.Core.Dtos.Violations;

namespace Travellin.Core.Interfaces
{
    public interface IViolationService
    {
        Task<ViolationDto> CreateAsync(CreateViolationDto dto, Guid reporterId);
        Task<IEnumerable<ViolationDto>> GetAllAsync(string status);
        Task<ViolationDto?> GetByIdAsync(int id);
        Task<bool> UpdateStatusAsync(int id, UpdateViolationStatusDto dto);
        Task<IEnumerable<ViolationDto>> GetByReporterAsync(Guid reporterId);
        Task CreateAsync(CreateViolationDto dto, string v);
    }
}