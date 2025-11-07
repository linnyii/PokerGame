namespace PokerGame.Models;

public class ExchangeInfo
{
    public bool HasUsedExchange { get; set; }
    public Card? ExchangedCard { get; private set; }
    public Player? ExchangedPlayer { get; private set; }
    public int ExchangeRound { get; private set; }

    public void SetExchangeInfo(Player targetPlayer, Card acceptedCard)
    {
        ExchangedCard = acceptedCard;
        ExchangedPlayer = targetPlayer;
        ExchangeRound += 1;
        HasUsedExchange = true;
    }

    public void IncrementExchangeRound()
    {
        if (ExchangedCard != null)
        {
            ExchangeRound += 1;
        }
    }

    public void ReturnExchangedCard(string playerName)
    {
        if (ExchangedCard == null || ExchangedPlayer == null)
            return;

        Console.WriteLine($"{playerName} return {ExchangedCard.Rank} of {ExchangedCard.Suit} back to {ExchangedPlayer.Name}");
        ExchangedPlayer.AddCard(ExchangedCard);
        
        // 重置交换信息
        ExchangedCard = null;
        ExchangedPlayer = null;
    }

    public bool IsActive => ExchangedCard != null && ExchangedPlayer != null;
    public bool ShouldReturn => ExchangeRound == 3 && IsActive;
}
