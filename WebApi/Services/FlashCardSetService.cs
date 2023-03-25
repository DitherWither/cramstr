using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Shared;
using WebApi.Models;

namespace WebApi.Services;

/// <summary>
///     The service for the flashcard set.
///     It is responsible for all the database operations.
///     It is injected into the controllers.
/// </summary>
public class FlashCardSetService
{
    /// <summary>
    ///     The collection of flashcard sets. This is the main collection.
    /// </summary>
    private readonly IMongoCollection<FlashCardSet> _flashCardSetsCollection;

    /// <summary>
    ///     The constructor for the flashcard set service.
    ///     It initializes the collection.
    /// </summary>
    /// <param name="cramstrDatabaseSettings">
    ///     The settings for the database. This is injected by the dependency injection framework.
    /// </param>
    public FlashCardSetService(
        IOptions<CramstrDatabaseSettings> cramstrDatabaseSettings)
    {
        Console.WriteLine(
            $"Connection String: {cramstrDatabaseSettings.Value.ConnectionString}");

        // Get a client to the database
        var mongoClient =
            new MongoClient(cramstrDatabaseSettings.Value.ConnectionString);

        // Get the database
        var database =
            mongoClient.GetDatabase(cramstrDatabaseSettings.Value.DatabaseName);

        // Initialize the collection
        _flashCardSetsCollection =
            database.GetCollection<FlashCardSet>(cramstrDatabaseSettings.Value
                .FlashCardSetCollectionName);
    }

    /// <summary>
    ///     Get all the flashcard sets in the database asynchronously.
    /// </summary>
    /// <returns>
    ///     A list of all the flashcard sets in the database.
    /// </returns>
    public async Task<List<FlashCardSet>> GetAllAsync()
    {
        return await _flashCardSetsCollection.Find(_ => true).ToListAsync();
    }

    /// <summary>
    ///     Get a flashcard set by its id asynchronously.
    /// </summary>
    /// <param name="id">
    ///     The id of the flashcard set.
    /// </param>
    /// <returns>
    ///     The flashcard set with the given id.
    /// </returns>
    public async Task<FlashCardSet?> GetByIdAsync(string id)
    {
        return await _flashCardSetsCollection.Find(x => x.Id == id)
            .FirstOrDefaultAsync();
    }

    /// <summary>
    ///     Create a flashcard set asynchronously.
    /// </summary>
    /// <param name="flashCardSet">
    ///     The flashcard set to create.
    /// </param>
    public async Task CreateAsync(FlashCardSet flashCardSet)
    {
        flashCardSet.Id = null; // Make sure the id is null

        // Create the flashcard set with a new id
        await _flashCardSetsCollection.InsertOneAsync(flashCardSet);
    }

    /// <summary>
    ///     Update a flashcard set asynchronously.
    /// </summary>
    /// <param name="id">
    ///     The id of the flashcard set to update.
    /// </param>
    /// <param name="flashCardSet">
    ///     The new flashcard set.
    /// </param>
    public async Task UpdateAsync(string id, FlashCardSet flashCardSet)
    {
        await _flashCardSetsCollection.ReplaceOneAsync(x => x.Id == id,
            flashCardSet);
    }

    /// <summary>
    ///     Remove a flashcard set asynchronously.
    /// </summary>
    /// <param name="id">
    ///     The id of the flashcard set to remove.
    /// </param>
    public async Task RemoveAsync(string id)
    {
        await _flashCardSetsCollection.DeleteOneAsync(x => x.Id == id);
    }
}