using PokerGame.Models;

namespace PokerGame;

public abstract class Player
{
    private HashSet<Card> handCards = new();
    public string Name { get; protected set; } = string.Empty;
    public int TotalPoints { get; set; }
    
    public IReadOnlyCollection<Card> HandCards => handCards;
    
    public void AddCard(Card card)
    {
        handCards.Add(card);
    }
    
    public void RemoveCard(Card card)
    {
        handCards.Remove(card);
    }

    public int HandCardCount => handCards.Count;
    
    // 交換相關狀態
    public bool HasUsedExchange { get; set; }
    public Card? ExchangedCard { get; private set; }
    public Player? ExchangedPlayer { get; private set; }
    public int ExchangeRound { get; private set; }

    public abstract void Naming();
    public abstract Card? Decide();
    public abstract Card SelectCardForExchange();
    public abstract Player SelectTargetPlayer(List<Player> availablePlayers);
    public abstract bool WantsToExchange();

    public void SetExchangeInfo(Player targetPlayer, Card acceptedCard)
    {
        ExchangedCard = acceptedCard;
        ExchangedPlayer = targetPlayer;
        ExchangeRound += 1;
        HasUsedExchange = true;
        Console.WriteLine($"{Name} received {acceptedCard.Rank} of {acceptedCard.Suit} from {targetPlayer.Name}");
    }

    public bool HasRemainCard() => HandCardCount > 0;

    public void IncrementExchangeRound()
    {
        if (ExchangedCard != null)
        {
            ExchangeRound += 1;
        }
    }

    public void ReturnExchangedCard()
    {
        Console.WriteLine($"{Name} return {ExchangedCard!.Rank} of {ExchangedCard.Suit} back to {ExchangedPlayer!.Name}");
        ExchangedPlayer.AddCard(ExchangedCard);
        ExchangedCard = null;
        ExchangedPlayer = null;
    }
}