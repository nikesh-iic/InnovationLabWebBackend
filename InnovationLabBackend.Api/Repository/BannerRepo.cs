using InnovationLabBackend.Api.DbContext;
using InnovationLabBackend.Api.Dtos.Banner;
using InnovationLabBackend.Api.Interfaces;
using InnovationLabBackend.Api.Models;
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
    }
}
