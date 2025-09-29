using PokerGame.Models;

namespace PokerGame;

public class HumanPlayer : Player
{
    public override void Naming()
    {
        Console.Write("Please input your name: ");
        var userInput = Console.ReadLine();

        while (!ValidNameInput(userInput))
        {
            Console.Write("Name cannot be empty, please input valid name: ");
            userInput = Console.ReadLine();
        }

        Name = userInput!.Trim();
        Console.WriteLine($"Welcome {Name}!");
    }

    public override Card? Decide()
    {
        if (!HasRemainCard())
        {
            Console.WriteLine($"\n{Name} has no card to play.");
            return null;
        }

        var selectedCard = SelectCard();
        Console.WriteLine($"{Name} issue card:{selectedCard.Rank} of {selectedCard.Suit}");
        return selectedCard;
    }

    private static bool ValidNameInput(string? userInput)
    {
        return string.IsNullOrWhiteSpace(userInput);
    }
}