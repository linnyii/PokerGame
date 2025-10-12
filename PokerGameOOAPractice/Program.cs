using PokerGame;

Console.WriteLine("=== 歡迎來到撲克牌遊戲 ===\n");

var humanPlayer1 = new HumanPlayer();
var humanPlayer2 = new HumanPlayer();
var aiPlayer1 = new AiPlayer("AI玩家1");
var aiPlayer2 = new AiPlayer("AI玩家2");

Player[] players = [humanPlayer1, humanPlayer2, aiPlayer1, aiPlayer2];

var game = new Game(players);
Console.WriteLine("遊戲開始！");
Console.WriteLine("=====================================\n");

game.Start();

Console.WriteLine("\n=====================================");
Console.WriteLine("感謝遊玩！按任意鍵退出...");
Console.ReadKey();
