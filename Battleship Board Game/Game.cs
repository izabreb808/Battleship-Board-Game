using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Battleship_Board_Game.Model
{
    class Game
    {

        Player p1 = new Player();
        Player p2 = new Player();

        public Game()
        {
            p1 = new Player();
            p2 = new Player();

            int playerTurn = 1;

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Player " + playerTurn + " turn");

                if (playerTurn == 1)
                {
                    p1.makeTurn();
                    playerTurn = 2;
                }
                else
                {
                    p2.makeTurn();
                    playerTurn = 1;
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\n\nPlayer 1 guessing board: \n----------------------\n");
                showShotBoard(p1.getBoard());
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\n\nPlayer 2 guessing board: \n------------------\n");
                showShotBoard(p2.getBoard());

                if (p1.playerSunkAllShips()) { Console.Write("\nPLAYER 1 WINS"); break; }
                if (p2.playerSunkAllShips()) { Console.Write("\nPLAYER 2 WINS"); break; }

                Thread.Sleep(1000);

            }
        }

        public void showBoard(Board board)
        {
            for (int x = 0; x < 10; x++)
            {
                Console.Write("\n");
                for (int y = 0; y < 10; y++)

                    Console.Write(fieldContent(new Point(x, y), board));

            }
        }

        public void showShotBoard(Board board)
        {

            Console.WriteLine("\n");
            for (int x = 0; x < 10; x++)
            {
                Console.Write("\n");
                for (int y = 0; y < 10; y++)
                {
                    if (board.getAvailableFields().Find(p => p == new Point(x, y)) == null)
                    {
                        if (board.isSegmentOnThisCoords(new Point(x, y)))
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.Write(" X ");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(" * ");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(" ~ ");
                    }
                }
            }
        }


        private string fieldContent(Point coords, Board board)
        {
            foreach (Ship s in board.getShipsList())
                foreach (ShipSegment seg in s.getShipInfo())  // s - list of segmants
                    if (seg.coords == coords)
                    {
                        if (seg.destoryed) return " X ";
                        if (s.getLength() == 5) return " C ";
                        if (s.getLength() == 4) return " B ";
                        if (s.getLength() == 3) return " S ";
                        if (s.getLength() == 2) return " P ";
                       
                    }
            return " ~ ";
        }
    }
}
