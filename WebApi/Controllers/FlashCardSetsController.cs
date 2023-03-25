using Microsoft.AspNetCore.Mvc;
using Shared;
using WebApi.Services;

namespace WebApi.Controllers;

/// <summary>
///     The controller for the flashcard sets.
///     It is responsible for handling the HTTP requests and responses for the flashcard sets.
///     Its routes are at /api/FlashCardSets/[controller]
/// </summary>
[ApiController]
[Route("/api/[controller]")]
public class FlashCardSetsController
{
    private readonly FlashCardSetService _flashCardSetService;

    /// <summary>
    ///     The constructor for the flashcard sets controller.
    /// </summary>
    /// <param name="flashCardSetService">
    ///     The flashcard set service. This is injected by the dependency injection framework.
    /// </param>
    public FlashCardSetsController(FlashCardSetService flashCardSetService)
    {
        _flashCardSetService = flashCardSetService;
    }

    /// <summary>
    ///     Route for getting all the flashcard sets
    /// </summary>
    /// <returns>
    ///     A list of all the flashcard sets
    /// </returns>
    [HttpGet]
    public async Task<ActionResult<List<FlashCardSet>>> GetAll()
    {
        return await _flashCardSetService.GetAllAsync();
    }

    /// <summary>
    ///     Route for getting a flashcard set by its id
    /// </summary>
    /// <param name="id">
    ///     The id of the flashcard set
    /// </param>
    /// <returns>
    ///     The flashcard set with the given id or a 404 if it doesn't exist
    /// </returns>
    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<FlashCardSet>> GetById(string id)
    {
        var flashCardSet = await _flashCardSetService.GetByIdAsync(id);

        // If the flashcard set is null, return a 404
        if (flashCardSet == null)
            return new NotFoundResult();

        // Otherwise, return the flashcard set
        return flashCardSet;
    }

    /// <summary>
    ///     Route for creating a flashcard set
    /// </summary>
    /// <param name="flashCardSet">
    ///     The flashcard set to create in the database
    /// </param>
    /// <returns>
    ///     The created flashcard set
    /// </returns>
    [HttpPost]
    public async Task<FlashCardSet> Create(FlashCardSet flashCardSet)
    {
        Console.WriteLine(
            $"The first flashcard's question is: {flashCardSet.FlashCards[0].Question}");

        // Create the flashcard set
        await _flashCardSetService.CreateAsync(flashCardSet);


        // Return the new flashcard set. This is done so that the assigned id is returned
        return flashCardSet;
    }

    /// <summary>
    ///     Route for updating a flashcard set by its id
    /// </summary>
    /// <param name="id">
    ///     The id of the flashcard set to update, must exit in the database
    /// </param>
    /// <param name="flashCardSet">
    ///     The new flashcard set, if the id is different, the id of the route parameter will be used
    /// </param>
    /// <returns>
    ///     A 204 if the flashcard set was updated, otherwise a 404
    /// </returns>
    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id,
        FlashCardSet flashCardSet)
    {
        // If a flashcard set with the given id doesn't exist, return a 404
        if (await _flashCardSetService.GetByIdAsync(id) == null)
            return new NotFoundResult();

        // Otherwise, update the flashcard set
        flashCardSet.Id = id;
        await _flashCardSetService.UpdateAsync(id, flashCardSet);

        return new NoContentResult();
    }

    /// <summary>
    ///     Route for deleting a flashcard set by its id, if it exists, returns a 204, otherwise a 404
    /// </summary>
    /// <param name="id">
    ///     The id of the flashcard set to delete
    /// </param>
    /// <returns>
    ///     A 204 if the flashcard set was deleted, otherwise a 404
    /// </returns>
    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        // If a flashcard set with the given id doesn't exist, return a 404
        if (await _flashCardSetService.GetByIdAsync(id) == null)
            return new NotFoundResult();

        // Otherwise, delete the flashcard set
        await _flashCardSetService.RemoveAsync(id);

        return new NoContentResult();
    }
}