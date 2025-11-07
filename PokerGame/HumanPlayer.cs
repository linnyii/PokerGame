using PokerGame.Models;
using PokerGame.Services;

namespace PokerGame;

public class HumanPlayer : Player
{
    public override void Naming()
    {
        Name = InputHelper.GetValidName();
        Console.WriteLine($"Welcome {Name}!");
    }

    public override Card? Decide()
    {
        if (!HasRemainCard())
        {
            Console.WriteLine($"\n{Name} has no card to play");
            return null;
        }

        var selectedCard = SelectCardForExchange();
        Console.WriteLine($"{Name} play the card：{selectedCard.Rank} of {selectedCard.Suit}");
        RemoveCard(selectedCard);
        return selectedCard;
    }

    public override Card SelectCardForExchange()
    {
        var cardList = HandCards.ToList();
        InputHelper.DisplayCards(cardList, Name);
        
        var index = InputHelper.GetValidIndex("Choose the card index ", cardList.Count);
        return cardList[index - 1];
    }

    public override Player SelectTargetPlayer(List<Player> availablePlayers)
    {
        InputHelper.DisplayPlayers(availablePlayers, "Players who can do exchange");
        
        var index = InputHelper.GetValidIndex("Choose the number of player", availablePlayers.Count);
        return availablePlayers[index - 1];
    }

    public override bool WantsToExchange()
    {
        return InputHelper.GetYesNoAnswer($"Hi {Name}, would you like to exchange card with other  players?");
    }
}