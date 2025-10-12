using PokerGame.Models;

namespace PokerGame;

public abstract class Player
{
    public HashSet<Card> HandCards { get; set; } = [];
    public string Name { get; protected set; } = string.Empty;
    public int TotalPoints { get; set; }
    private Card? ExchangedCard { get; set; }
    private Player? ExchangedPlayer { get; set; }
    public bool HasUsedExchange { get; set; }
    private int ExchangeRound { get; set; }

    public virtual void Naming()
    {
    }

    public abstract Card? Decide();

    public virtual void AcceptCardExchange(Player targetPlayer, Card acceptedCard)
    {
        SetExchangeInfo(targetPlayer, acceptedCard);
        var selectedCard = SelectCard();
        HandCards.Remove(selectedCard);
        targetPlayer.SetExchangeInfo(this, selectedCard);
    }

    public void SetExchangeInfo(Player targetPlayer, Card acceptedCard)
    {
        ExchangedCard = acceptedCard;
        ExchangedPlayer = targetPlayer;
        ExchangeRound += 1;
        HasUsedExchange = true;
        Console.WriteLine($"{Name} received {acceptedCard.Rank} of {acceptedCard.Suit} from {targetPlayer.Name}");

    }

    private List<Card> ListAllHandCards()
    {
        var cardList = HandCards.ToList();
        for (var i = 0; i < cardList.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {cardList[i].Rank} of {cardList[i].Suit}");
        }

        return cardList;
    }

    private static Card ChooseTheCard(List<Card> cardList)
    {
        int cardIndex;
        while (true)
        {
            Console.Write("please input the index of card that you would like to exchange: ");
            if (int.TryParse(Console.ReadLine(), out cardIndex) &&
                cardIndex >= 1 && cardIndex <= cardList.Count)
            {
                break;
            }

            Console.WriteLine($"invalid card index, please input number between 1 and {cardList.Count}");
        }

        return cardList[cardIndex - 1];
    }

    protected Card SelectCard()
    {
        Console.WriteLine($"\n{Name}'s HandCard for now：");
        
        var cardList = ListAllHandCards();
        
        return ChooseTheCard(cardList);
    }

    private Player SelectTargetPlayer(Game game)
    {
        var playersCanExchange = game.Players.Where(x => !x.HasUsedExchange && x != this).ToList();

        Console.WriteLine("\n the players can exchange card：");

        for (var i = 0; i < playersCanExchange.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {playersCanExchange[i].Name}");
        }

        int playerIndex;
        while (true)
        {
            Console.Write("choose index of player ");
            if (int.TryParse(Console.ReadLine(), out playerIndex) &&
                playerIndex >= 1 && playerIndex <= playersCanExchange.Count)
            {
                break;
            }

            Console.WriteLine(
                $"invalid player index, please input number between 1 and {playersCanExchange.Count}");
        }

        return playersCanExchange[playerIndex - 1];
    }
    public virtual void ExchangeCard(Game game)
    {
        if (HasUsedExchange || (!HasRemainCard())) return;

        Console.WriteLine($"Hi, {Name}, Do u wanna exchange card with other player? Y/N");
        var answer = Console.ReadLine()?.ToLower() ?? "";

        while (answer != "y" && answer != "n")
        {
            Console.WriteLine("Please input Y or N:");
            answer = Console.ReadLine()?.ToLower() ?? "";
        }

        if (answer == "y")
        {
            ProcessCardExchange(game);
        }
    }
    
    protected static bool HasOtherPlayersCanDoExchange(Game game)
    {
        return game.Players.Where(x => !x.HasUsedExchange).ToList().Count > 1;
    }

    protected virtual void ProcessCardExchange(Game game)
    {
        if (!HasOtherPlayersCanDoExchange(game)) return;
        
        var selectedCard = SelectCard();
        var targetPlayer = SelectTargetPlayer(game);

        HandCards.Remove(selectedCard);
        
        targetPlayer.AcceptCardExchange(this, selectedCard);
    }

    protected bool HasRemainCard()
    {
        return HandCards.Count > 0;
    }

    private void ReturnExchangedCard()
    {
        Console.WriteLine($"{Name} return {ExchangedCard!.Rank} of {ExchangedCard.Suit} back to {ExchangedPlayer!.Name}");

        ExchangedPlayer.HandCards.Add(ExchangedCard);
        ExchangedCard = null;
        ExchangedPlayer = null;
    }

    public void CheckToReturnCard()
    {
        if (!HasUsedExchange) return;
        if (ExchangeRound == 3 && ExchangedCard is not null)
        {
            ReturnExchangedCard();
            return;
        }
        
        if (ExchangedCard is not null)
        {
            ExchangeRound += 1;
        }
        
    }
}