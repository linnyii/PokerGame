using PokerGame.Models;
using PokerGame.Services;

namespace PokerGame;

public class AiPlayer : Player
{
    private readonly Random _random = new();

    public AiPlayer(string name)
    {
        Name = name;
    }

    public override void Naming()
    {
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
        HandCards.Remove(selectedCard);
        return selectedCard;
    }

    public override Card SelectCardForExchange()
    {
        var cardList = HandCards.ToList();
        InputHelper.DisplayCards(cardList, Name);

        var cardIndex = _random.Next(1, cardList.Count + 1);
        Console.WriteLine($"AI選擇卡牌: {cardIndex}");

        return cardList[cardIndex - 1];
    }

    public override Player SelectTargetPlayer(List<Player> availablePlayers)
    {
        InputHelper.DisplayPlayers(availablePlayers, "可以交換卡牌的玩家");

        var playerIndex = _random.Next(1, availablePlayers.Count + 1);
        Console.WriteLine($"AI選擇玩家: {playerIndex}");

        return availablePlayers[playerIndex - 1];
    }

    public override bool WantsToExchange()
    {
        var wantToExchange = _random.Next(2) == 1;
        Console.WriteLine($"Hi {Name}，您想要與其他玩家交換卡牌嗎？ (Y/N)");
        var answer = wantToExchange ? "Y" : "N";
        Console.WriteLine($"AI選擇: {answer}");
        return wantToExchange;
    }
}