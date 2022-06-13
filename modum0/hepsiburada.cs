using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modum0
{
    public class hepsiburada
    {
        public class Rover
        {
            public Position Position { get; set; }
            public List<char> Movements { get; set; }
        }

        public class Position
        {
            public int X { get; set; }
            public int Y { get; set; }
            public char Face { get; set; }
        }

        public static List<Rover> MarsRover(int xBoundary, int yBoundary, List<Rover> rovers)
        {
            var result = new List<string>();

            foreach (var item in rovers.Select((rover, index) => (rover, index)))
            {
                if (item.rover.Position.X > xBoundary || item.rover.Position.Y > yBoundary)
                {
                    result.Add("Rover " + item.index + ". has invalid positions.");
                    continue;
                }

                foreach (var move in item.rover.Movements)
                {
                    item.rover.Position = ChangePosition(item.rover.Position, Char.ToLower(move), xBoundary, yBoundary);
                }
            }
            return rovers;
        }

        public static Position ChangePosition(Position oldPosiiton, char movement, int xLimit, int yLimit)
        {
            if (movement == 'l' || movement == 'r')
            {
                oldPosiiton.Face = TurnFlow(oldPosiiton.Face, movement);
            } else if (movement == 'm')
            {
                if (oldPosiiton.Face == 'n')
                {
                    oldPosiiton.Y = oldPosiiton.Y == yLimit ? oldPosiiton.Y : oldPosiiton.Y + 1;
                }
                else if (oldPosiiton.Face == 'e')
                {
                    oldPosiiton.X = oldPosiiton.X == xLimit ? oldPosiiton.X : oldPosiiton.X + 1;
                } 
                else if (oldPosiiton.Face == 's')
                {
                    oldPosiiton.Y = oldPosiiton.Y == 0 ? oldPosiiton.Y : oldPosiiton.Y - 1;
                } 
                else if (oldPosiiton.Face == 'w')
                {
                    oldPosiiton.X = oldPosiiton.X == 0 ? oldPosiiton.X : oldPosiiton.X - 1;
                }
            }
            return oldPosiiton;
        }

        public static char TurnFlow(char curDirection, char rotation)
        {
            if (curDirection == 'n' && rotation == 'l')
            {
                return 'w';
            } else if (curDirection == 'n' && rotation == 'r')
            {
                return 'e';
            } else if (curDirection == 'e' && rotation == 'l')
            {
                return 'n';
            } else if (curDirection == 'e' && rotation == 'r')
            {
                return 's';
            }
            else if (curDirection == 's' && rotation == 'l')
            {
                return 'e';
            }
            else if (curDirection == 's' && rotation == 'r')
            {
                return 'w';
            }
            else if (curDirection == 'w' && rotation == 'l')
            {
                return 's';
            }
            else if (curDirection == 'w' && rotation == 'r')
            {
                return 'n';
            } else
            {
                return curDirection;
            }
        }

    }
}
