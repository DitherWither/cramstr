using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace Shared;

/// <summary>
///     A single flashcard. Stores the question and the answer for now.
/// </summary>
public class FlashCard
{
    /// <summary>
    ///     The question, fairly self explanatory.
    /// </summary>
    /// <remarks>
    ///     Should be displayed first.
    ///     Should default to an empty string
    /// </remarks>
    [BsonElement("question")]
    [JsonInclude]
    [JsonPropertyName("question")]
    public string Question { get; set; } = "";

    /// <summary>
    ///     Answer to the question, should also have a short explanation
    /// </summary>
    /// <remarks>
    ///     Should only be displayed after clicking a button or some other user input
    /// </remarks>
    [BsonElement("answer")]
    [JsonInclude]
    [JsonPropertyName("answer")]
    public string Answer { get; set; } = "";

    /// <summary>
    ///     Stores how frequently the user got the answer right.
    ///     Incremented when answered correctly, and Decremented when not.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Questions with a lower correctness score should be displayed first.
    ///     </para>
    ///     <para>
    ///         This way, Questions that are frequently answered wrong will be memorised faster.
    ///     </para>
    ///     <para>
    ///         Might be ignored, depending upon the user preferences.
    ///     </para>
    /// </remarks>
    [BsonElement("correctnessScore")]
    [JsonInclude]
    [JsonPropertyName("correctnessScore")]
    public int CorrectnessScore { get; set; }
}