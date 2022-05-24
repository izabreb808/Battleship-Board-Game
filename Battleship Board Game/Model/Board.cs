using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleship_Board_Game.Model
{
    class Board
    {
        private Random rand = new Random();

        private int[] lenOfShipToCreate = new int[ShipsConsts.SHIPS_AMOUNT] { ShipsConsts.CARRIER_LEN, ShipsConsts.BATTLESHIP_LEN, ShipsConsts.BATTLESHIP_LEN,
                                                                                  ShipsConsts.DESTROYER_LEN, ShipsConsts.DESTROYER_LEN, ShipsConsts.DESTROYER_LEN,
                                                                                  ShipsConsts.PATROLBOAT_LEN, ShipsConsts.PATROLBOAT_LEN, ShipsConsts.PATROLBOAT_LEN,
                                                                                  ShipsConsts.PATROLBOAT_LEN,};

        private List<Ship> ships; //list of ships on board
        private List<Point> shootAvailableFields; //list of fields that can yet contain a ship [ ~YOUR GUESSING BOARD ]
        private List<Point> forbiddenFields; //list of fields that can't be selected to place the ship

        public Board()
        {
            this.ships = new List<Ship>();
            this.forbiddenFields = new List<Point>();
            this.shootAvailableFields = new List<Point>();
            this.fillShootAvailableList();
            setShipsRandomly();
        }

        public List<Ship> getShipsList() { return this.ships; }
        public List<Point> getAvailableFields() { return this.shootAvailableFields; }

        public void setShipsRandomly() 
        {

            for (int i = 0; i < ShipsConsts.SHIPS_AMOUNT; i++)
            {
                Ship newShip = createRandomShip(this.lenOfShipToCreate[i]);
                this.ships.Add(newShip);
            }
        }

        private Ship createRandomShip(int shipLength) 
        {
            List<ShipSegment> shipSegments = new List<ShipSegment>();
            bool shipPositionNotOK = true;
            int shipOrientation;
            Point segmentCoords;

            while (shipPositionNotOK)
            {
                shipSegments.Clear(); 
                shipOrientation = rand.Next(0, 2); 

               
                if (shipOrientation == 1) 
                {
                    segmentCoords = new Point(rand.Next(0, 10), rand.Next(0, 10 - shipLength + 1));
                }
                else
                {
                    segmentCoords = new Point(rand.Next(0, 10 - shipLength + 1), rand.Next(0, 10)); 
                }

                for (int i = 0; i < shipLength; i++)
                {
                    if (isFieldAvailable(segmentCoords) == false) break;
                    shipSegments.Add(new ShipSegment(segmentCoords));
                    _ = shipOrientation == 1 ? segmentCoords.Y++ : segmentCoords.X++; // _ = don't assign the result of an expression to a variable, just increment x or y
                    if (i == shipLength - 1) shipPositionNotOK = false; 
                }
            }
            addForbiddenFields(shipSegments);
            Ship newShip = new Ship(shipLength, shipSegments);
            return newShip;
        }



            private void addForbiddenFields(List<ShipSegment> shipSegments) // add a segment and all segments around it to the list of forbidden fields
            {
                int x, y;
                foreach (ShipSegment seg in shipSegments)
                {
                    x = seg.coords.X;
                    y = seg.coords.Y;

                    this.forbiddenFields.Add(new Point(x + 1, y + 1));              //   [-1, -1]  [0,-1 ]  [1, -1]
                    this.forbiddenFields.Add(new Point(x, y + 1));                  //   [-1, 0]   [0, 0]   [1, 0]
                    this.forbiddenFields.Add(new Point(x + 1, y));                  //   [-1, 1]   [0, 1]   [1, 1]
                    this.forbiddenFields.Add(new Point(x, y - 1));                           
                    this.forbiddenFields.Add(new Point(x - 1, y));                         
                    this.forbiddenFields.Add(new Point(x - 1, y - 1));
                    this.forbiddenFields.Add(new Point(x + 1, y - 1));
                    this.forbiddenFields.Add(new Point(x - 1, y + 1));
                }
                this.deleteDuplicats();

            }
            private Boolean isFieldAvailable(Point p)
            {
                if (forbiddenFields == null) return true;
                foreach (Point field in forbiddenFields) { if (field == p) return false; }
                return true;
            }

        private void deleteDuplicats() // delete fields added more than once
        {
            this.forbiddenFields = forbiddenFields.GroupBy(p => new { p.X, p.Y }).Select(p => p.First()).ToList();
        }

        private void fillShootAvailableList() // set all the fields as available at the beginning 
        {
            for (int x = 0; x < 10; x++)
                for (int y = 0; y < 10; y++)
                    this.shootAvailableFields.Add(new Point(x, y));
        }
     
        public int checkHit(Point shoot) 
        {
            foreach (Ship s in this.ships)
            {
                foreach (ShipSegment seg in s.getShipInfo())
                {
                    if (seg.coords == shoot)
                    {
                        seg.destoryed = true;
                        removeShootFieldAndFieldsDiagonally(shoot);
                        if (s.isSunk())
                        {
                            removeFieldsAroundShip(s.getShipInfo());
                            s.sunkShip(); 
                            return 2;
                        }
                        return 1;
                    }
                }
            }
            removeSpecificField(shoot);
            return 0;
        }

        private void removeShootFieldAndFieldsDiagonally(Point shoot)
        {
            foreach (Point field in this.shootAvailableFields)
            {
                if (shoot == field)
                {
                    this.shootAvailableFields.Remove(field);
                    removeSpecificField(new Point(shoot.X + 1, shoot.Y + 1));
                    removeSpecificField(new Point(shoot.X - 1, shoot.Y - 1));
                    removeSpecificField(new Point(shoot.X - 1, shoot.Y + 1));
                    removeSpecificField(new Point(shoot.X + 1, shoot.Y - 1));
                    return;
                }

            }

        }
        private void removeFieldsAroundShip(List<ShipSegment> shipSegments)
        {
            int x, y;
            foreach (ShipSegment seg in shipSegments)
            {
                x = seg.coords.X;
                y = seg.coords.Y;
                removeSpecificField(new Point(x + 1, y + 1));
                removeSpecificField(new Point(x, y + 1));
                removeSpecificField(new Point(x + 1, y));
                removeSpecificField(new Point(x, y - 1));
                removeSpecificField(new Point(x - 1, y));
                removeSpecificField(new Point(x - 1, y - 1));
                removeSpecificField(new Point(x + 1, y - 1));
                removeSpecificField(new Point(x - 1, y + 1));
            }

        }

        private void removeSpecificField(Point p)
        {
            this.shootAvailableFields.Remove(this.shootAvailableFields.Find(P => p == P));
        }

        public Boolean isSegmentOnThisCoords(Point coords)
        {
            foreach (Ship s in ships) 
            { 
                foreach (ShipSegment seg in s.getShipInfo()) 
                { 
                    if (seg.coords == coords) return true; 
                } 
            }
            return false;
        }

    }
    }

