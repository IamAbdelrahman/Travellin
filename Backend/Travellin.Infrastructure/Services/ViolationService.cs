using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Travellin.Core.Dtos.Violations;
using Travellin.Core.Entities;
using Travellin.Core.Interfaces;
using Travellin.Infrastructure.Data;

namespace Travellin.Core.Services
{
    public class ViolationService : IViolationService
    {
        private readonly TravellinDbContext _context;

        public ViolationService(TravellinDbContext context)
        {
            _context = context;
        }

        public async Task<ViolationDto> CreateAsync(CreateViolationDto dto, Guid reporterId)
        {
            var violation = new Violation
            {
                ReportedById = reporterId,
                ReportedPropertyId = dto.ReportedPropertyId,
                ReportedUserId = dto.ReportedUserId,
                Name = dto.ViolationType,
                Description = dto.Description,
                Status = "Pending"
            };

            _context.Violations.Add(violation);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(violation.Id);
        }

        public async Task<IEnumerable<ViolationDto>> GetAllAsync(string status)
        {
            return await _context.Violations
                .Include(v => v.ReportedBy)
                .Include(v => v.ReportedProperty)
                .Include(v => v.ReportedUser)
                .Where(v => v.Status == status)
                .Select(v => MapToDto(v))
                .ToListAsync();
        }

        public async Task<ViolationDto?> GetByIdAsync(int id)
        {
            var violation = await _context.Violations
                .Include(v => v.ReportedBy)
                .Include(v => v.ReportedProperty)
                .Include(v => v.ReportedUser)
                .FirstOrDefaultAsync(v => v.Id == id);

            return violation != null ? MapToDto(violation) : null;
        }

        public async Task<bool> UpdateStatusAsync(int id, UpdateViolationStatusDto dto)
        {
            var violation = await _context.Violations.FindAsync(id);
            if (violation == null) return false;

            violation.Status = dto.Status;
            violation.AdminNotes = dto.AdminNotes;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ViolationDto>> GetByReporterAsync(Guid reporterId)
        {
            return await _context.Violations
                .Where(v => v.ReportedById == reporterId)
                .Select(v => MapToDto(v))
                .ToListAsync();
        }

        private static ViolationDto MapToDto(Violation v)
        {
            return new ViolationDto
            {
                Id = v.Id,
                ReportedById = v.ReportedById,
                ReportedByName = v.ReportedBy.UserName,
                ReportedPropertyId = v.ReportedPropertyId,
                ReportedPropertyTitle = v.ReportedProperty?.Title,
                ReportedUserId = v.ReportedUserId,
                ReportedUserName = v.ReportedUser?.UserName,
                ViolationType = v.Name,
                Description = v.Description,
                Status = v.Status,
                AdminNotes = v.AdminNotes
            };
        }

        public Task CreateAsync(CreateViolationDto dto, string v)
        {
            throw new NotImplementedException();
        }
    }
}