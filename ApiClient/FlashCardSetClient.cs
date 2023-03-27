using System.Net.Http.Json;
using Shared;

namespace ApiClient;

/// <summary>
///     The client for the flashcard set.
///     Parallel to WebApi.FlashCardSetController.
/// </summary>
/// <remarks>
///     TODO: Add error handling instead of just returning null.
/// </remarks>
public static class FlashCardSetClient
{
    public static async Task<List<FlashCardSet>?> GetAllAsync()
    {
        return await ApiClient.HttpClient.GetFromJsonAsync<List<FlashCardSet>>(
            "api/FlashCardSets");
    }

    public static async Task<FlashCardSet?> GetByIdAsync(string id)
    {
        return await ApiClient.HttpClient.GetFromJsonAsync<FlashCardSet>(
            $"api/FlashCardSets/{id}");
    }

    public static async Task CreateAsync(FlashCardSet flashCardSet)
    {
        await ApiClient.HttpClient.PostAsJsonAsync("api/FlashCardSets",
            flashCardSet);
    }

    public static async Task UpdateAsync(string id, FlashCardSet flashCardSet)
    {
        await ApiClient.HttpClient.PutAsJsonAsync($"api/FlashCardSets/{id}",
            flashCardSet);
    }

    public static async Task RemoveAsync(string id)
    {
        await ApiClient.HttpClient.DeleteAsync($"api/FlashCardSets/{id}");
    }
}