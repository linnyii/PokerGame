using PokerGame.Models;

namespace PokerGame;

public class AiPlayer : Player
{
    public AiPlayer( string name)
    {
        Name = name;
    }

    public override void ExchangeCard(Game game)
    {
        
    }

    public override Card Decide()
    {
        throw new NotImplementedException();
    }
    
}