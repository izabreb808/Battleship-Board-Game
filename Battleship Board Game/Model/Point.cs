using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship_Board_Game.Model
{
    class Point
    {
        public int X;
        public int Y;
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        public static bool operator == (Point p1, Point p2)
        {
            if (p1 is null && p2 is null) return true;
            return p1.Equals(p2); 
        }
        public static bool operator != (Point p1, Point p2) { return !(p1 == p2); } // the inverse of equality

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var p = (Point)obj;
            return (this.X == p.X && this.Y == p.Y); 
        }

    }
}


