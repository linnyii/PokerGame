using PokerGame.Models;
using PokerGame.Enums;

namespace PokerGame;

public class Deck
{
    private List<Card> Cards { get; }

    public Deck()
    {
        Cards = [];
        InitializeDeck();
    }

    private void InitializeDeck()
    {
        string[] ranks = ["A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K"];
        
        foreach (var suit in Enum.GetValues<Suit>())
        {
            foreach (var rank in ranks)
            {
                Cards.Add(new Card { Suit = suit, Rank = rank });
            }
        }
    }

    public void Shuffle()
    {
        var random = new Random();
        for (var i = Cards.Count - 1; i > 0; i--)
        {
            var j = random.Next(i + 1);
            (Cards[i], Cards[j]) = (Cards[j], Cards[i]);
        }
    }

    public void DrawCard(List<Player> players)
    {
        var playerIndex = 0;
        
        foreach (var card in Cards)
        {
            var currentPlayer = players[playerIndex];
            currentPlayer.AddCard(card);
            
            playerIndex = (playerIndex + 1) % players.Count;
        }
        
        Console.WriteLine($"issue {Cards.Count} cards to  {players.Count} 位玩家");
        foreach (var player in players)
        {
            Console.WriteLine($"player {player.Name} get {player.HandCardCount} cards");
        }
    }
}