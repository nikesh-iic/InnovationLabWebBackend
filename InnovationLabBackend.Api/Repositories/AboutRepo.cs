﻿using InnovationLabBackend.Api.DbContext;
using InnovationLabBackend.Api.Interfaces;
using InnovationLabBackend.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace InnovationLabBackend.Api.Repositories
{
    public class AboutRepo(InnovationLabDbContext dbContext) : IAboutRepo
    {
        private readonly InnovationLabDbContext _dbContext = dbContext;

        private async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() != 0;
        }

        // About
        public async Task<About?> GetAboutAsync()
        {
            var about = await _dbContext.About.FirstOrDefaultAsync();
            return about;
        }

        public async Task<About> CreateOrUpdateAboutAsync(About about)
        {
            var existingAbout = await _dbContext.About.FirstOrDefaultAsync();

            if (existingAbout == null)
            {
                await _dbContext.About.AddAsync(about);
            }
            else
            {
                existingAbout.Mission = about.Mission;
                existingAbout.Vision = about.Vision;
                existingAbout.ParentOrgName = about.ParentOrgName;
                existingAbout.ParentOrgDescription = about.ParentOrgDescription;
                existingAbout.ParentOrgLogoUrl = about.ParentOrgLogoUrl;
                existingAbout.ParentOrgWebsiteUrl = about.ParentOrgWebsiteUrl;
                existingAbout.UpdatedAt = DateTimeOffset.UtcNow;

                about = existingAbout;
            }

            await SaveChangesAsync();
            return about;
        }

        // Core Values
        public async Task<List<CoreValue>> GetCoreValuesAsync()
        {
            var coreValues = await _dbContext.CoreValues
                .Where(cv => !cv.IsDeleted)
                .OrderBy(cv => cv.Order)
                .ToListAsync();

            return coreValues;
        }

        public async Task<CoreValue?> GetCoreValueByIdAsync(Guid id)
        {
            var coreValue = await _dbContext.CoreValues
                .FirstOrDefaultAsync(cv => cv.Id == id && !cv.IsDeleted);

            return coreValue;
        }

        public async Task<CoreValue> CreateCoreValueAsync(CoreValue coreValue)
        {
            await _dbContext.CoreValues.AddAsync(coreValue);
            await SaveChangesAsync();
            return coreValue;
        }

        public async Task UpdateCoreValueAsync(CoreValue coreValue)
        {
            coreValue.UpdatedAt = DateTimeOffset.UtcNow;
            await SaveChangesAsync();
        }

        public async Task DeleteCoreValueAsync(CoreValue coreValue)
        {
            coreValue.IsDeleted = true;
            coreValue.DeletedAt = DateTimeOffset.UtcNow;
            await SaveChangesAsync();
        }

        // Journey
        public async Task<List<JourneyItem>> GetJourneyItemsAsync()
        {
            var journeyItems = await _dbContext.JourneyItems
                .Where(ji => !ji.IsDeleted)
                .OrderByDescending(ji => ji.Date)
                .ThenBy(ji => ji.Order)
                .ToListAsync();

            return journeyItems;
        }

        public async Task<JourneyItem?> GetJourneyItemByIdAsync(Guid id)
        {
            var journeyItem = await _dbContext.JourneyItems
                .FirstOrDefaultAsync(ji => ji.Id == id && !ji.IsDeleted);

            return journeyItem;
        }

        public async Task<JourneyItem> CreateJourneyItemAsync(JourneyItem journeyItem)
        {
            await _dbContext.JourneyItems.AddAsync(journeyItem);
            await SaveChangesAsync();
            return journeyItem;
        }

        public async Task UpdateJourneyItemAsync(JourneyItem journeyItem)
        {
            journeyItem.UpdatedAt = DateTimeOffset.UtcNow;
            await SaveChangesAsync();
        }

        public async Task DeleteJourneyItemAsync(JourneyItem journeyItem)
        {
            journeyItem.IsDeleted = true;
            journeyItem.DeletedAt = DateTimeOffset.UtcNow;
            await SaveChangesAsync();
        }
    }
}
