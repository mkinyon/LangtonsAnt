using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace LangtonsAnt
{
    public class Ant
    {
        private Vector2 position;

        public enum Direction
        {
            N = 0,
            E = 1,
            S = 2,
            W = 3
        }

        private Direction currentDirection;

        public Ant( int xPos, int yPos )
        {
            currentDirection = Direction.N;

            Move( xPos, yPos );
        }

        /// <summary>
        /// Moves the ant to a new position.
        /// </summary>
        /// <param name="xPos">The X position</param>
        /// <param name="yPos">The Y position</param>
        public void Move( int xPos, int yPos )
        {
            position = new Vector2( xPos, yPos );
        }

        /// <summary>
        /// Gets the current position of the ant.
        /// </summary>
        /// <returns></returns>
        public Vector2 GetCurrentPosition()
        {
            return position;
        }

        /// <summary>
        /// Gets the current direction of the ant.
        /// </summary>
        /// <returns></returns>
        public Direction GetCurrentDirection()
        {
            return currentDirection;
        }

        /// <summary>
        /// Turns the ant to the left and moves forward one space.
        /// </summary>
        public void MoveLeft()
        {
            if ( currentDirection == Direction.W )
            {
                currentDirection = Direction.S;
                Move( ( int ) position.X, ( int ) position.Y + 1 );
            }
            else if ( currentDirection == Direction.S )
            {
                currentDirection = Direction.E;
                Move( ( int )position.X + 1, ( int ) position.Y );
            }
            else if ( currentDirection == Direction.E )
            {
                currentDirection = Direction.N;
                Move( ( int ) position.X, ( int ) position.Y - 1 );
            }
            else
            {
                currentDirection = Direction.W;
                Move( ( int ) position.X - 1, ( int ) position.Y );
            }
        }

        /// <summary>
        /// Turns the ant to the right and moves forward one space.
        /// </summary>
        public void TurnRight()
        {
            if ( currentDirection == Direction.W )
            {
                currentDirection = Direction.N;
                Move( ( int ) position.X, ( int ) position.Y - 1 );
            }
            else if ( currentDirection == Direction.N)
            {
                currentDirection = Direction.E;
                Move( ( int ) position.X + 1, ( int ) position.Y );
            }
            else if ( currentDirection == Direction.E )
            {
                currentDirection = Direction.S;
                Move( ( int ) position.X, ( int ) position.Y + 1 );
            }
            else
            {
                currentDirection = Direction.W;
                Move( ( int ) position.X - 1, ( int ) position.Y );
            }
        }
    }
}
