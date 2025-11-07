using PokerGame.Models;

namespace PokerGame;

public abstract class Player
{
    public HashSet<Card> HandCards { get; set; } = new();
    public string Name { get; protected set; } = string.Empty;
    public int TotalPoints { get; set; }
    
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

    // 只負責設定交換資訊
    public void SetExchangeInfo(Player targetPlayer, Card acceptedCard)
    {
        ExchangedCard = acceptedCard;
        ExchangedPlayer = targetPlayer;
        ExchangeRound += 1;
        HasUsedExchange = true;
        Console.WriteLine($"{Name} received {acceptedCard.Rank} of {acceptedCard.Suit} from {targetPlayer.Name}");
    }

    public bool HasRemainCard() => HandCards.Count > 0;

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
        ExchangedPlayer.HandCards.Add(ExchangedCard);
        ExchangedCard = null;
        ExchangedPlayer = null;
    }
}