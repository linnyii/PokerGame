using PokerGame.Enums;
using PokerGame.Models;

namespace PokerGame;

public class Game(Player[] players, Deck deck)
{
    private const int TotalGameRounds = 13;
    private Deck Deck { get; } = deck;
    public List<Player> Players { get; } = players.ToList();
    private Dictionary<Player, Card> PlayersShowCard { get; set; } = new();
    private int GameRound { get; set; }

    public void Start()
    {
        PlayerNaming();
        Deck.Shuffle();
        Deck.DrawCard(players);

        while (GameRound < TotalGameRounds)
        {
            GameRound += 1;
            foreach (var player in players)
            {
                player.CheckToReturnCard();
                player.ExchangeCard(this);
                var card = player.Decide();
                if (card != null)
                {
                    PlayersShowCard.Add(player, card);
                }
            }

            GetWinnerOfTheRound();
            PlayersShowCard.Clear();
        }

        GetFinalWinner();
    }

    private void GetFinalWinner()
    {
        if (Players.Count == 0)
        {
            Console.WriteLine("沒有玩家參與遊戲。");
            return;
        }

        var winner = Players[0];
        
        foreach (var player in Players)
        {
            if (player.TotalPoints > winner.TotalPoints)
            {
                winner = player;
            }
        }

        Console.WriteLine("\n=== 遊戲結束 ===");
        Console.WriteLine("所有玩家最終分數：");
        
        var sortedPlayers = Players.OrderByDescending(p => p.TotalPoints).ToList();
        for (var i = 0; i < sortedPlayers.Count; i++)
        {
            var rank = i == 0 ? "🏆 冠軍" : $"第{i + 1}名";
            Console.WriteLine($"{rank}: {sortedPlayers[i].Name} - {sortedPlayers[i].TotalPoints} 分");
        }

        Console.WriteLine($"\n🎉 最終獲勝者：{winner.Name}，總分：{winner.TotalPoints} 分！");
    }


    private void GetWinnerOfTheRound()
    {
        if (PlayersShowCard.Count == 0)
        {
            Console.WriteLine("沒有玩家出牌，無法決定勝負。");
            return;
        }

        var winner = PlayersShowCard.First().Key;
        var winningCard = PlayersShowCard.First().Value;

        foreach (var playerCard in PlayersShowCard)
        {
            if (!IsCardBigger(playerCard.Value, winningCard)) continue;
            winner = playerCard.Key;
            winningCard = playerCard.Value;
        }

        Console.WriteLine($"\n本輪勝利者：{winner.Name}，勝利卡牌：{winningCard.Rank} of {winningCard.Suit}");

        winner.TotalPoints++;

        Console.WriteLine($"{winner.Name} 目前總分：{winner.TotalPoints}");
    }

    private static bool IsCardBigger(Card cardFromCurrentPlayer, Card? cardFromCurrentWinner)
    {
        var rankFromCurrentPlayer = GetRankValue(cardFromCurrentPlayer.Rank);
        var rankFromCurrentWinner = GetRankValue(cardFromCurrentWinner!.Rank);

        if (rankFromCurrentPlayer > rankFromCurrentWinner)
        {
            return true;
        }

        if (rankFromCurrentPlayer < rankFromCurrentWinner)
        {
            return false;
        }

        return GetSuitValue(cardFromCurrentPlayer.Suit) > GetSuitValue(cardFromCurrentWinner.Suit);
    }

    private static int GetRankValue(string rank)
    {
        return rank switch
        {
            "2" => 2,
            "3" => 3,
            "4" => 4,
            "5" => 5,
            "6" => 6,
            "7" => 7,
            "8" => 8,
            "9" => 9,
            "10" => 10,
            "J" => 11,
            "Q" => 12,
            "K" => 13,
            "A" => 14,
            _ => 0
        };
    }

    private static int GetSuitValue(Suit suit)
    {
        return suit switch
        {
            Suit.Club => 1, // 梅花
            Suit.Diamond => 2, // 菱形
            Suit.Heart => 3, // 愛心
            Suit.Spade => 4, // 黑桃
            _ => 0
        };
    }

    private void PlayerNaming()
    {
        foreach (var player in players)
        {
            if (string.IsNullOrEmpty(player.Name))
            {
                player.Naming();
            }
        }
    }
}