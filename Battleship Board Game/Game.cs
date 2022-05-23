using System;
using System.Collections.Generic;
using System.Text;

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

            Console.Write("Player 1 board: \n\n");
            showBoard(p1.getBoard());
            Console.Write("\n\nPlayer 2 board: \n\n");
            showBoard(p2.getBoard());
        }

        public void showBoard(Board board)
        {
            var ships = board.getShipsList();

            for (int x = 0; x < 10; x++)
            {
                Console.Write("\n");
                for (int y = 0; y < 10; y++)
                    Console.Write(fieldContent(new Point(x, y), ships));
            }
        }

        private string fieldContent(Point p, List<Ship> ships)
        {
            foreach (Ship s in ships)
                foreach (ShipSegment seg in s.getShipInfo())
                    if (seg.coords == p)
                    {
                        if (s.getLength() == 5) return " C ";
                        if (s.getLength() == 4) return " B ";
                        if (s.getLength() == 3) return " S ";
                        if (s.getLength() == 2) return " P ";
                        //return " X ";
                    }

            return " ~ ";
        }
    }
}
