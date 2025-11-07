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
            Console.WriteLine($"please input number between  1 to {maxValue} ");
        }
    }

    public static string GetValidName()
    {
        string name;
        do
        {
            Console.Write("Please input player name");
            name = Console.ReadLine()?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Can not be empty, please input again");
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
                    Console.WriteLine(" please input Y or N");
                    break;
            }
        }
    }

    public static void DisplayCards(List<Card> cards, string playerName)
    {
        Console.WriteLine($"\n{playerName} 's handCards：");
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

