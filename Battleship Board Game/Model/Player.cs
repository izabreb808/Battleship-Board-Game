using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Battleship_Board_Game.Model
{
    class Player
    {
        private Random rand = new Random();

        Board board;
        Point prevSuccessShot;
        Point firstSuccessShot;
        char shotDirection;
        Boolean wasHitSoCheckAround;
        char[] prevShotDirections = new char[4];
        int sunkShipsNumber;

        public Player()
        {
            board = new Board();
            sunkShipsNumber = 0;
            wasHitSoCheckAround = false;
            prevSuccessShot = null;
            firstSuccessShot = null;
            shotDirection = 'n';
            erasePrevShotDirections();
        }

        public Board getBoard() { return this.board; }

        public Boolean playerSunkAllShips()
        {
            if (sunkShipsNumber >= ShipsConsts.SHIPS_AMOUNT) return true;
            return false;
        }

        public void makeTurn()
        {
            Point shot = null;
            int shotResult;

            if (prevSuccessShot == null || shotDirection == 'n')
            {
                shot = randomizeShot();
            }
            else
            {
                if (shotDirection == 'd')
                {
                    shot = new Point(prevSuccessShot.X, prevSuccessShot.Y + 1);
                    if (shot.Y == 9) { 
                        prevSuccessShot = firstSuccessShot; 
                        shotDirection = 'u'; 
                    }
                }
                if (shotDirection == 'u')
                {
                    shot = new Point(prevSuccessShot.X, prevSuccessShot.Y - 1);
                    if (shot.Y == 0) { 
                        prevSuccessShot = firstSuccessShot; 
                        shotDirection = 'd'; 
                    }
                }
                if (shotDirection == 'l')
                {
                    shot = new Point(prevSuccessShot.X - 1, prevSuccessShot.Y);
                    if (shot.X == 0) { 
                        prevSuccessShot = firstSuccessShot; 
                        shotDirection = 'r'; 
                    }
                }
                if (shotDirection == 'r')
                {
                    shot = new Point(prevSuccessShot.X + 1, prevSuccessShot.Y);
                    if (shot.X == 9) { 
                        prevSuccessShot = firstSuccessShot; 
                        shotDirection = 'l'; 
                    }
                }
            }

            shotResult = board.checkHit(shot);

            if (shotResult == 0)  // segmant was not destroyed
            {
                if (addPrevShotDirection() == true && wasHitSoCheckAround == true)
                {
                    shotDirection = randomizeNextShotDirection();
                }
                else
                {
                    shotDirection = 'n';
                    wasHitSoCheckAround = false;
                    erasePrevShotDirections();
                }

            }
            if (shotResult == 1) // segment was destroyed
            {
                wasHitSoCheckAround = true;
                if (shotDirection == 'n')
                {
                    shotDirection = randomizeDirection();
                    firstSuccessShot = new Point(shot.X, shot.Y);
                }
                prevSuccessShot = shot;
            }
            if (shotResult == 2) // ship was sunk
            {
                sunkShipsNumber++;
                wasHitSoCheckAround = false;
                shotDirection = 'n';
                prevSuccessShot = null;
                erasePrevShotDirections();
            }

        }

        private char randomizeDirection()
        {
    
            int random = rand.Next(0, 4);
            if (random == 0) return 'u';
            if (random == 1) return 'd';
            if (random == 2) return 'l';
            if (random == 3) return 'r';
            return 'e'; 
            
        }

        private char randomizeNextShotDirection() // ==========================================
        {
            char c = 'n';
            Boolean tryAgain = true;
            while (tryAgain)
            {
                c = randomizeDirection();
                tryAgain = false;

                for (int i = 0; i < 4; i++)
                {
                    if (this.prevShotDirections[i] == c)
                    {
                        tryAgain = true;
                        break;
                    }
                }
            }
            return c;
        }

        private Point randomizeShot()
        {
            return board.getAvailableFields().ElementAt(rand.Next(0, board.getAvailableFields().Count()));
        }

        private Boolean addPrevShotDirection()
        {
            for (int i = 0; i < 4; i++)
            {
                if (this.prevShotDirections[i] == 'n')
                {
                    this.prevShotDirections[i] = shotDirection;
                    return true;
                }
            }
            prevSuccessShot = firstSuccessShot;
            shotDirection = getOppositeDirection(shotDirection);
            return false;
        }

        private void erasePrevShotDirections()
        {
            for (int i = 0; i < 4; i++) this.prevShotDirections[i] = 'n';
        }

        private char getOppositeDirection(char currentDirection)
        {
            if (currentDirection == 'u') return 'd';
            if (currentDirection == 'd') return 'u';
            if (currentDirection == 'l') return 'r';
            if (currentDirection == 'r') return 'l';
            return 'n';
        }
    }
}
