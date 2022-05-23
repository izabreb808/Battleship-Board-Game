using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship_Board_Game.Model
{
    static class ShipsConsts
    {

        public const int SHIPS_AMOUNT = 10;

        public const int CARRIER_LEN = 5;
        public const int BATTLESHIP_LEN = 4;
        public const int DESTROYER_LEN = 3;
        public const int PATROLBOAT_LEN = 2;


    }

    class ShipSegment
    {
        public Point coords;
        public Boolean destoryed;
        public ShipSegment(Point coordinates)
        {
            this.coords = new Point(coordinates.X, coordinates.Y);
            this.destoryed = false;
        }
    }
    class Ship
    {
        private List<ShipSegment> shipSegments;
        private int length;
        private Boolean sunk;

        public Ship(int shipLength, List<ShipSegment> coords)
        {
            this.shipSegments = coords;
            this.length = shipLength;
            this.sunk = false;
        }
        public int getLength() { return this.length; }
        public void sunkShip() { this.sunk = true; }


        public Boolean isSunk()
        {
            // Find returns null if it doesn't find an element that matches the given expression
            // It looks for an element which is not destroyed 
            if (this.shipSegments.Find(s => s.destoryed == false) == null)
                return true;
            return false;
        }

        public List<ShipSegment> getShipInfo() { return this.shipSegments; }
    }
}