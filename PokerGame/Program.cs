using PokerGame;

Console.WriteLine("=== PokerGame ===\n");

var humanPlayer1 = new HumanPlayer();
var humanPlayer2 = new HumanPlayer();
var aiPlayer1 = new AiPlayer("AI Player 1");
var aiPlayer2 = new AiPlayer("AI Player 2");

Player[] players = [humanPlayer1, humanPlayer2, aiPlayer1, aiPlayer2];

var game = new Game(players);
Console.WriteLine("Start Game");
Console.WriteLine("=====================================\n");

game.Start();

Console.WriteLine("\n=====================================");
Console.WriteLine("Press any key to exit");
Console.ReadKey();
