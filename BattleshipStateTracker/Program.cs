// See https://aka.ms/new-console-template for more information
using BattleshipStateTracker.Application.Services;

IBoardService board = new BoardService();

board.CreateBattleship();

board.StartBattle();

Console.ReadKey();