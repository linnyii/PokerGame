using PokerGame.Enums;
using PokerGame.Models;
using PokerGame.Services;

namespace PokerGame;

public class Game(Player[] players)
{
    private const int TotalGameRounds = 13;
    private readonly Deck _deck = new();
    private readonly CardComparator _cardComparator = new();
    private readonly ExchangeManager _exchangeManager = new();
    public List<Player> Players { get; } = players.ToList();
    private Dictionary<Player, Card> PlayersShowCard { get; } = new();
    private int GameRound { get; set; }

    public void Start()
    {
        InitializeGame();
        PlayRounds();
        ShowFinalResults();
    }

    private void InitializeGame()
    {
        InitializePlayerNames();
        _deck.Shuffle();
        _deck.DrawCard(Players);
    }

    private void PlayRounds()
    {
        while (GameRound < TotalGameRounds)
        {
            GameRound++;
            Console.WriteLine($" Game Round : {GameRound} ");
            PlaySingleRound();
        }
    }

    private void PlaySingleRound()
    {
        _exchangeManager.NeedReturnCards(Players);
        
        foreach (var player in Players)
        {
            _exchangeManager.ProcessExchange(player, this);
        }

        CollectPlayerCards();
        
        DetermineRoundWinner();
        
        PlayersShowCard.Clear();
    }

    private void CollectPlayerCards()
    {
        foreach (var player in Players)
        {
            var card = player.Decide();
            if (card != null)
            {
                PlayersShowCard.Add(player, card);
            }
        }
    }

    private void DetermineRoundWinner()
    {
        if (!PlayersShowCard.Any())
        {
            Console.WriteLine("沒有玩家出牌，無法決定勝負。");
            return;
        }

        var winner = _cardComparator.GetRoundWinner(PlayersShowCard);
        var winningCard = PlayersShowCard[winner];

        Console.WriteLine($"本輪勝利者：{winner.Name}，勝利卡牌：{winningCard.Rank} of {winningCard.Suit}");
        winner.TotalPoints++;
        Console.WriteLine($"{winner.Name} 目前總分：{winner.TotalPoints}");
    }

    private void InitializePlayerNames()
    {
        foreach (var player in Players.Where(player => string.IsNullOrEmpty(player.Name)))
        {
            player.Naming();
        }
    }

    private void ShowFinalResults()
    {
        var winner = Players.OrderByDescending(p => p.TotalPoints).First();
        
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
}