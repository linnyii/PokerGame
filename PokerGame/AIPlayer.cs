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
            Console.WriteLine($"\n{Name} has no cards to play");
            return null;
        }

        var selectedCard = SelectCardForExchange();
        Console.WriteLine($"{Name} plays the card：{selectedCard.Rank} of {selectedCard.Suit}");
        RemoveCard(selectedCard);
        return selectedCard;
    }

    public override Card SelectCardForExchange()
    {
        var cardList = HandCards.ToList();
        InputHelper.DisplayCards(cardList, Name);

        var cardIndex = _random.Next(1, cardList.Count + 1);
        Console.WriteLine($"AI choose the card: {cardIndex}");

        return cardList[cardIndex - 1];
    }

    public override Player SelectTargetPlayer(List<Player> availablePlayers)
    {
        InputHelper.DisplayPlayers(availablePlayers, "Players who can do exchange");

        var playerIndex = _random.Next(1, availablePlayers.Count + 1);
        Console.WriteLine($"AI choose the exchange player: {playerIndex}");

        return availablePlayers[playerIndex - 1];
    }

    public override bool WantsToExchange()
    {
        var wantToExchange = _random.Next(2) == 1;
        Console.WriteLine($"Hi {Name}, would you like to exchange card with other players？ (Y/N)");
        var answer = wantToExchange ? "Y" : "N";
        Console.WriteLine($"AI answers: {answer}");
        return wantToExchange;
    }
}