using PokerGame.Models;

namespace PokerGame.Services;

public static class InputHelper
{
    public static int GetValidIndex(string prompt, int maxValue)
    {
        int index;
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out index) && 
                index >= 1 && index <= maxValue)
            {
                return index;
            }
            Console.WriteLine($"請輸入 1 到 {maxValue} 之間的數字");
        }
    }

    public static string GetValidName()
    {
        string name;
        do
        {
            Console.Write("請輸入您的姓名: ");
            name = Console.ReadLine()?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("姓名不能為空，請重新輸入");
            }
        } while (string.IsNullOrWhiteSpace(name));
        
        return name;
    }

    public static bool GetYesNoAnswer(string question)
    {
        while (true)
        {
            Console.WriteLine($"{question} (Y/N)");
            var answer = Console.ReadLine()?.ToLower().Trim();
            
            switch (answer)
            {
                case "y" or "yes":
                    return true;
                case "n" or "no":
                    return false;
                default:
                    Console.WriteLine("請輸入 Y 或 N");
                    break;
            }
        }
    }

    public static void DisplayCards(List<Card> cards, string playerName)
    {
        Console.WriteLine($"\n{playerName} 的手牌：");
        for (var i = 0; i < cards.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {cards[i].Rank} of {cards[i].Suit}");
        }
    }

    public static void DisplayPlayers(List<Player> players, string title)
    {
        Console.WriteLine($"\n{title}：");
        for (var i = 0; i < players.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {players[i].Name}");
        }
    }
}

