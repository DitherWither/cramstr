namespace Shared;

public class FlashCardSet
{
    public FlashCardSet(List<FlashCard> flashCards)
    {
        FlashCards = flashCards;
    }

    /// <summary>
    ///     An array storing all the flashcards
    ///     Is an empty array by default
    /// </summary>
    private List<FlashCard> FlashCards { get; }

    /// <summary>
    /// A random number generator
    /// </summary>
    private readonly Random _rnd = new Random();
    

    public List<FlashCard> GetQuiz(int length, bool useCorrectnessScore = true)
    {
        if (useCorrectnessScore == false)
        {
            return GetRandomQuiz(length);
        }
        else
        {
            return GetScoreSortedQuiz(length);
        }
        
    }

    private List<FlashCard> GetRandomQuiz(int length)
    {
        // TODO: Test this
        return (List<FlashCard>) FlashCards.OrderBy(_ => _rnd.Next()).Take(length);
    }

    private List<FlashCard> GetScoreSortedQuiz(int length)
    {
        // TODO: Test this
        return (List<FlashCard>)FlashCards
            .OrderBy(x => x.CorrectnessScore)
            .Take(length * 2) // The reason for taking twice the length is to introduce some randomness to the quiz, otherwise the quiz would end up being too same
            .OrderBy(_ => _rnd.Next())
            .Take(length);
    }
}