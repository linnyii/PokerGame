using PokerGame.Enums;
using PokerGame.Models;

namespace PokerGame.Services;

public class CardComparator
{
    private static readonly Dictionary<string, int> RankValues = new()
    {
        {"2", 2}, {"3", 3}, {"4", 4}, {"5", 5}, {"6", 6}, {"7", 7},
        {"8", 8}, {"9", 9}, {"10", 10}, {"J", 11}, {"Q", 12}, {"K", 13}, {"A", 14}
    };

    private static readonly Dictionary<Suit, int> SuitValues = new()
    {
        {Suit.Club, 1}, {Suit.Diamond, 2}, {Suit.Heart, 3}, {Suit.Spade, 4}
    };

    public bool IsCardBigger(Card card1, Card card2)
    {
        var rank1 = RankValues[card1.Rank];
        var rank2 = RankValues[card2.Rank];

        if (rank1 != rank2)
        {
            return rank1 > rank2;
        }

        return SuitValues[card1.Suit] > SuitValues[card2.Suit];
    }

    public Player GetRoundWinner(Dictionary<Player, Card> playersShowCard)
    {
        if (!playersShowCard.Any())
            throw new InvalidOperationException("沒有玩家出牌");

        var winner = playersShowCard.First().Key;
        var winningCard = playersShowCard.First().Value;

        foreach (var playerCard in playersShowCard.Skip(1))
        {
            if (IsCardBigger(playerCard.Value, winningCard))
            {
                winner = playerCard.Key;
                winningCard = playerCard.Value;
            }
        }

        return winner;
    }
}

