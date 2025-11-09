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

        CollectPlayerShowCards();
        
        DetermineRoundWinner();
        
        PlayersShowCard.Clear();
    }

    private void CollectPlayerShowCards()
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
        if (PlayersShowCard.Count == 0)
        {
            Console.WriteLine("No draw card from any player");
            return;
        }

        var winner = _cardComparator.GetRoundWinner(PlayersShowCard);
        var winningCard = PlayersShowCard[winner];

        Console.WriteLine($"Winner of this round：{winner.Name}, winner card：{winningCard.Rank} of {winningCard.Suit}");
        winner.AddPoints(1);
        Console.WriteLine($"{winner.Name} Total Score：{winner.TotalPoints}");
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
        
        Console.WriteLine("\n=== Game Over ===");
        Console.WriteLine($"\n Final Winner：{winner.Name}, Total Score：{winner.TotalPoints} ！");
    }
}