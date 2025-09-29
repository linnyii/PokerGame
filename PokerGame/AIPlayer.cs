using PokerGame.Models;

namespace PokerGame;

public class AiPlayer : Player
{
    public AiPlayer( string name)
    {
        Name = name;
    }

    public override void ExchangeCard(Game game)
    {
        if (HasUsedExchange || (!HasRemainCard())) return;

        var wantToExchange = new Random().Next(2) == 1;
        
        Console.WriteLine($"Hi, {Name}, Do u wanna exchange card with other player? Y/N");
        var answer = wantToExchange ? "y" : "n";
        Console.WriteLine($"AI選擇: {answer.ToUpper()}");

        if (answer == "y")
        {
            ProcessCardExchange(game);
        }
    }

    public override Card? Decide()
    {
        if (!HasRemainCard())
        {
            Console.WriteLine($"\n{Name} has no card to play.");
            return null;
        }

        var selectedCard = SelectRandomCard();
        Console.WriteLine($"{Name} issue card:{selectedCard.Rank} of {selectedCard.Suit}");
        return selectedCard;
    }

    protected override void ProcessCardExchange(Game game)
    {
        if (!HasOtherPlayersCanDoExchange(game))
        {
            return;
        } 
        
        var selectedCard = SelectRandomCard();
        var targetPlayer = SelectRandomTargetPlayer(game);

        HandCards.Remove(selectedCard);
        HasUsedExchange = true;
        targetPlayer.AcceptCardExchange(this,selectedCard);
    }

    private Player SelectRandomTargetPlayer(Game game)
    {
        var playersCanExchange = game.Players.Where(x => !x.HasUsedExchange && x != this).ToList();
        Console.WriteLine("\n the players can exchange card：");
        for (var i = 0; i < playersCanExchange.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {playersCanExchange[i].Name}");
        }

        var random = new Random();
        var playerIndex = random.Next(1, playersCanExchange.Count + 1);
        Console.WriteLine($"AI選擇玩家: {playerIndex}");

        return playersCanExchange[playerIndex - 1];
    }

    private Card SelectRandomCard()
    {
        Console.WriteLine($"\n{Name}'s HandCard for now：");
        var cardList = HandCards.ToList();
        for (var i = 0; i < cardList.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {cardList[i].Rank} of {cardList[i].Suit}");
        }

        var cardIndex = new Random().Next(1, cardList.Count + 1);
        Console.WriteLine($"AI選擇卡牌: {cardIndex}");

        return cardList[cardIndex - 1];
    }
    
}