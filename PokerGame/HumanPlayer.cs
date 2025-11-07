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
            Console.WriteLine($"\n{Name} 沒有卡牌可以出。");
            return null;
        }

        var selectedCard = SelectCardForExchange();
        Console.WriteLine($"{Name} 出牌：{selectedCard.Rank} of {selectedCard.Suit}");
        RemoveCard(selectedCard);
        return selectedCard;
    }

    public override Card SelectCardForExchange()
    {
        var cardList = HandCards.ToList();
        InputHelper.DisplayCards(cardList, Name);
        
        var index = InputHelper.GetValidIndex("請選擇要出的卡牌編號: ", cardList.Count);
        return cardList[index - 1];
    }

    public override Player SelectTargetPlayer(List<Player> availablePlayers)
    {
        InputHelper.DisplayPlayers(availablePlayers, "可以交換卡牌的玩家");
        
        var index = InputHelper.GetValidIndex("請選擇要交換的玩家編號: ", availablePlayers.Count);
        return availablePlayers[index - 1];
    }

    public override bool WantsToExchange()
    {
        return InputHelper.GetYesNoAnswer($"Hi {Name}，您想要與其他玩家交換卡牌嗎？");
    }
}