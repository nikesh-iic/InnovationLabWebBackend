﻿using InnovationLabBackend.Api.Models;

namespace InnovationLabBackend.Api.Interfaces
{
    public interface IAboutRepo
    {
        // About
        Task<Models.About?> GetAboutAsync();
        Task<Models.About> CreateOrUpdateAboutAsync(Models.About about);

        // Core Values
        Task<List<CoreValue>> GetCoreValuesAsync();
        Task<CoreValue?> GetCoreValueByIdAsync(Guid id);
        Task<CoreValue> CreateCoreValueAsync(CoreValue coreValue);
        Task UpdateCoreValueAsync(CoreValue coreValue);
        Task DeleteCoreValueAsync(CoreValue coreValue);

        // Journey
        Task<List<JourneyItem>> GetJourneyItemsAsync();
        Task<JourneyItem?> GetJourneyItemByIdAsync(Guid id);
        Task<JourneyItem> CreateJourneyItemAsync(JourneyItem journeyItem);
        Task UpdateJourneyItemAsync(JourneyItem journeyItem);
        Task DeleteJourneyItemAsync(JourneyItem journeyItem);
    }
}
