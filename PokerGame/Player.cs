using PokerGame.Models;

namespace PokerGame;

public abstract class Player
{
    private readonly HashSet<Card> _handCards = [];
    public string Name { get; protected set; } = string.Empty;
    public int TotalPoints { get; private set; }

    protected IReadOnlyCollection<Card> HandCards => _handCards;
    
    public void AddCard(Card card)
    {
        _handCards.Add(card);
    }
    
    public void RemoveCard(Card card)
    {
        _handCards.Remove(card);
    }

    public int HandCardCount => _handCards.Count;
    
    public void AddPoints(int points)
    {
        TotalPoints += points;
    }

    public ExchangeInfo ExchangeInfo { get; } = new();

    public abstract void Naming();
    public abstract Card? Decide();
    public abstract Card SelectCardForExchange();
    public abstract Player SelectTargetPlayer(List<Player> availablePlayers);
    public abstract bool WantsToExchange();

    public void SetExchangeInfo(Player targetPlayer, Card acceptedCard)
    {
        ExchangeInfo.SetExchangeInfo(targetPlayer, acceptedCard);
        Console.WriteLine($"{Name} received {acceptedCard.Rank} of {acceptedCard.Suit} from {targetPlayer.Name}");
    }

    public bool HasRemainCard() => HandCardCount > 0;

    public void IncrementExchangeRound()
    {
        ExchangeInfo.IncrementExchangeRound();
    }

    public void ReturnExchangedCard()
    {
        ExchangeInfo.ReturnExchangedCard(Name);
    }
}