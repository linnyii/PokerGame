using PokerGame.Enums;

namespace PokerGame.Models;

public class Card
{
    public required Suit Suit { get; set; }
    public required string Rank { get; set; }
}