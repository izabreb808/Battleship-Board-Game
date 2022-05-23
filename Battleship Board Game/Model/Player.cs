using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship_Board_Game.Model
{
    class Player
    {
        private Random rand = new Random();

        Board board;
        Point prevSuccessShoot;
        char shootDirection;
        Boolean wasHitSoCheckAround;
        char[] prevShotDirections = new char[4];
        int sunkShipsNumber;

        public Player()
        {
            board = new Board();
            sunkShipsNumber = 0;
            wasHitSoCheckAround = false;
            prevSuccessShoot = null;
            shootDirection = 'n';
            //erasePrevShotDirections();
        }

        public Board getBoard() { return this.board; }

        public Boolean playerSunkAllShips()
        {
            if (sunkShipsNumber >= ShipsConsts.SHIPS_AMOUNT) return true;
            return false;
        }

        public void PlayerTurn()
        {
            Point shoot = null;
            if (prevSuccessShoot == null || shootDirection == 'n')
            {
                shoot = new Point(rand.Next(0, 10), rand.Next(0, 10));
                shootDirection = randomDirection();
            } 
            else
            {
                if (shootDirection == 'u') shoot = new Point(prevSuccessShoot.X, prevSuccessShoot.Y - 1);
                if (shootDirection == 'd') shoot = new Point(prevSuccessShoot.X, prevSuccessShoot.Y + 1);
                if (shootDirection == 'l') shoot = new Point(prevSuccessShoot.X - 1, prevSuccessShoot.Y);
                if (shootDirection == 'r') shoot = new Point(prevSuccessShoot.X + 1, prevSuccessShoot.Y);
                if (shootDirection == 'n') shoot = new Point(prevSuccessShoot.X, prevSuccessShoot.Y);
            }

            prevSuccessShoot = shoot;
                
        }

        private char randomDirection()
        {
    
            int random = rand.Next(0, 4);
            if (random == 0) return 'u';
            if (random == 1) return 'd';
            if (random == 2) return 'l';
            if (random == 3) return 'r';
            return 'n'; 
            
        }
    }
}
