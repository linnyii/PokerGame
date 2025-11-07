namespace PokerGame.Models;

public class ExchangeInfo
{
    public bool HasUsedExchange { get; set; }
    private Card? ExchangedCard { get; set; }
    private Player? ExchangedPlayer { get; set; }
    private int ExchangeRound { get; set; }

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
        
        ExchangedCard = null;
        ExchangedPlayer = null;
    }

    private bool IsActive => ExchangedCard != null && ExchangedPlayer != null;
    public bool ShouldReturn => ExchangeRound == 3 && IsActive;
}
