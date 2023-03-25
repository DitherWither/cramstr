using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Shared;

public class FlashCardSet
{
    /// <summary>
    ///     A random number generator
    /// </summary>
    [JsonIgnore] [BsonIgnore] private readonly Random _rnd = new();

    public FlashCardSet(List<FlashCard> flashCards)
    {
        FlashCards = flashCards;
    }

    /// <summary>
    ///     The id for the entry in the database
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [JsonInclude]
    [JsonPropertyName("_id")]
    public string? Id { get; set; }


    /// <summary>
    ///     The title of the flashcard set
    /// </summary>
    [DataMember]
    [BsonElement("title")]
    [JsonInclude]
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    /// <summary>
    ///     The description of the flashcard set
    /// </summary>
    [DataMember]
    [BsonElement("description")]
    [JsonInclude]
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    ///     An array storing all the flashcards
    ///     Is an empty array by default
    /// </summary>
    [DataMember]
    [BsonElement("flashCards")]
    [JsonInclude]
    [JsonPropertyName("flashCards")]
    public List<FlashCard> FlashCards { get; set; }


    public List<FlashCard> GetQuiz(int length, bool useCorrectnessScore = true)
    {
        if (useCorrectnessScore == false)
            return GetRandomQuiz(length);
        return GetScoreSortedQuiz(length);
    }

    private List<FlashCard> GetRandomQuiz(int length)
    {
        // TODO: Test this
        return (List<FlashCard>)FlashCards.OrderBy(_ => _rnd.Next())
            .Take(length);
    }

    private List<FlashCard> GetScoreSortedQuiz(int length)
    {
        // TODO: Test this
        return (List<FlashCard>)FlashCards
            .OrderBy(x => x.CorrectnessScore)
            .Take(length *
                  2) // The reason for taking twice the length is to introduce some randomness to the quiz, otherwise the quiz would end up being too same
            .OrderBy(_ => _rnd.Next())
            .Take(length);
    }
}