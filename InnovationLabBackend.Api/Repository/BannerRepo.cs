using InnovationLabBackend.Api.DbContext;
using InnovationLabBackend.Api.Dtos.Banner;
using InnovationLabBackend.Api.Dtos.Banners;
using InnovationLabBackend.Api.Interfaces;
using InnovationLabBackend.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Linq;
using System.Reflection;

namespace InnovationLabBackend.Api.Repository
{
    public class BannerRepo(InnovationLabDbContext context) : IBannerRepo
    {
        private readonly InnovationLabDbContext _dbContext = context;

        public async Task ActivateBanner(Guid id)
        {
           var currentBanner= await _dbContext.Banners.FindAsync(id);
            if (currentBanner == null)
                throw new NotFoundException("Banner with this id is not found");
           var otherBanners = await _dbContext.Banners
                .Where(b => b.Current && b.Id != id)
                .ToListAsync();

            foreach (var b in otherBanners)
            {
                b.Current = false;
            }

            currentBanner.Current = true;
            currentBanner.ScheduledStart = DateTimeOffset.UtcNow;
            await _dbContext.SaveChangesAsync();
        }


        public async Task<Banner> CreateBannerAsync(Banner banner)
        {
            await _dbContext.Banners.AddAsync(banner);
            await _dbContext.SaveChangesAsync();
            return banner;
        }

        public Task DeleteBannerAsync(Guid id)
        {
            var banner = _dbContext.Banners.FirstOrDefault(b => b.Id == id);
            if (banner == null)
            {
                throw new NotFoundException($"Banner with ID {id} not found.");
            }

            _dbContext.Banners.Remove(banner);
            return _dbContext.SaveChangesAsync();
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

        public async Task<Banner> ScheduleBannerDate(Guid id, DateScheduleDTO dateScheduleDTO)
        {
           var banner= await _dbContext.Banners.FindAsync(id);
            if (banner == null)
                throw new NotFoundException("Banner with this id not found");
            if (dateScheduleDTO.ScheduledEnd.HasValue)
                banner.ScheduledEnd = dateScheduleDTO.ScheduledEnd;

            if (dateScheduleDTO.ScheduledStart.HasValue)
                banner.ScheduledStart = dateScheduleDTO.ScheduledStart;

            await _dbContext.SaveChangesAsync();
            return banner;

        }

        public async Task<Banner> updateBannerAsync(Guid id, BannerUpdateDTO bannerUpdateDTO)
        {
            var banner = await _dbContext.Banners.FindAsync(id);
            if (banner == null)
            {
                throw new NotFoundException("Banner with this id does not exist to update");
            }

            if (bannerUpdateDTO.Url != null)
                banner.Url = bannerUpdateDTO.Url;

            if (bannerUpdateDTO.Title != null)
                banner.Title = bannerUpdateDTO.Title;

            if (bannerUpdateDTO.SubTitle != null)
                banner.SubTitle = bannerUpdateDTO.SubTitle;

            if (bannerUpdateDTO.ScheduledEnd.HasValue)
                banner.ScheduledEnd = bannerUpdateDTO.ScheduledEnd;

            if (bannerUpdateDTO.ScheduledStart.HasValue)
                banner.ScheduledStart = bannerUpdateDTO.ScheduledStart;

            if (bannerUpdateDTO.Caption != null)
                banner.Caption = bannerUpdateDTO.Caption;

            if (bannerUpdateDTO.Type.HasValue)
                banner.Type = bannerUpdateDTO.Type.Value;

            await _dbContext.SaveChangesAsync();
            return banner;
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
