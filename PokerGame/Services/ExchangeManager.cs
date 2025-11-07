using PokerGame.Models;

namespace PokerGame.Services;

public class ExchangeManager
{
    public void ProcessExchange(Player currentPlayer, Game game)
    {
        if (!CanExchange(currentPlayer, game))
            return;

        if (!currentPlayer.WantsToExchange())
            return;

        var availablePlayers = GetAvailablePlayersForExchange(currentPlayer, game);
        if (availablePlayers.Count == 0)
            return;

        var selectedCard = currentPlayer.SelectCardForExchange();
        var targetPlayer = currentPlayer.SelectTargetPlayer(availablePlayers);

        ExecuteExchange(currentPlayer, targetPlayer, selectedCard);
    }

    public void NeedReturnCards(List<Player> players)
    {
        foreach (var player in players)
        {
            if (!player.ExchangeInfo.HasUsedExchange) continue;

            if (player.ExchangeInfo.ShouldReturn)
            {
                player.ReturnExchangedCard();
            }
            else
            {
                player.IncrementExchangeRound();
            }
        }
    }

    private bool CanExchange(Player player, Game game)
    {
        return !player.ExchangeInfo.HasUsedExchange && 
               player.HasRemainCard() && 
               GetAvailablePlayersForExchange(player, game).Count != 0;
    }

    private List<Player> GetAvailablePlayersForExchange(Player currentPlayer, Game game)
    {
        return game.Players
            .Where(p => !p.ExchangeInfo.HasUsedExchange && p != currentPlayer)
            .ToList();
    }

    private void ExecuteExchange(Player initiator, Player target, Card selectedCard)
    {
        initiator.RemoveCard(selectedCard);
        initiator.ExchangeInfo.HasUsedExchange = true;
        
        target.SetExchangeInfo(initiator, selectedCard);
        var returnCard = target.SelectCardForExchange();
        target.RemoveCard(returnCard);
        
        initiator.SetExchangeInfo(target, returnCard);
    }
}

