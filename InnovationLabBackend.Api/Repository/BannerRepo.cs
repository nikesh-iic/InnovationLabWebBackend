using InnovationLabBackend.Api.DbContext;
using InnovationLabBackend.Api.Dtos.Banner;
using InnovationLabBackend.Api.Dtos.Banners;
using InnovationLabBackend.Api.Interfaces;
using InnovationLabBackend.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace InnovationLabBackend.Api.Repository
{
    public class BannerRepo(InnovationLabDbContext context) : IBannerRepo
    {
        private readonly InnovationLabDbContext _dbContext = context;

        public async Task<Banner> CreateBannerAsync(Banner banner)
        {
            await _dbContext.Banners.AddAsync(banner);
            await _dbContext.SaveChangesAsync();
            return banner;
        }

        public async Task<IEnumerable<BannerGetDTO>> GetAllBannerAsync(
             Enums.BannerType? type = null,
            DateTimeOffset? startDate = null,
            DateTimeOffset? endDate = null,
            DateTimeOffset? createdAfter = null
            )

        {
            var query =  _dbContext.Banners.AsNoTracking().AsQueryable();

            if (type.HasValue)
                query = query.Where(b => b.Type == type.Value);

            if (startDate.HasValue)
                query = query.Where(b => b.ScheduledStart >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(b => b.ScheduledEnd <= endDate.Value);

            if (createdAfter.HasValue)
                query = query.Where(b => b.CreatedAt >= createdAfter.Value); // yes lai ali proper banauna sakinxa

            return await query
                .Select(b => new BannerGetDTO
                {
                    Id = b.Id,
                    Url = b.Url,
                    Type = b.Type.ToString(),
                    Title = b.Title,
                    SubTitle = b.SubTitle,
                    Caption = b.Caption,
                    Current = b.Current,
                    Version = b.Version,
                    ParentId = b.ParentId,
                    CreatedAt = b.CreatedAt,
                    ScheduledStart = b.ScheduledStart,
                    ScheduledEnd = b.ScheduledEnd
                })
                .ToListAsync();
        }

        public async Task<BannerGetDTO> GetBannerByIdAsync(Guid id)
        {
            var result= await _dbContext.Banners.AsNoTracking()
                .Where(u => u.Id == id)
                .Select(b=> new BannerGetDTO
                {
                    Id = b.Id,
                    Url = b.Url,
                    Type = b.Type.ToString(),
                    Title = b.Title,
                    SubTitle = b.SubTitle,
                    Caption = b.Caption,
                    Current = b.Current,
                    Version = b.Version,
                    ParentId = b.ParentId,
                    CreatedAt = b.CreatedAt,
                    ScheduledStart = b.ScheduledStart,
                    ScheduledEnd = b.ScheduledEnd
                }).FirstOrDefaultAsync();

            if (result == null)
                throw new NotFoundException($"Banner with ID {id} not found.");
            return result;

        }
    }

    [Serializable]
    internal class NotFoundException : Exception
    {
        public NotFoundException()
        {
        }

        public NotFoundException(string? message) : base(message)
        {
        }

        public NotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
