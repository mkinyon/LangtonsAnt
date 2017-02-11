using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LangtonsAnt
{
    public class Board
    {
        private int gridWidth;
        private int gridHeight;
        private Color[,] board;
        private Random rand = new Random();
        private ContentManager content;
        private Texture2D texture;
        Vector2 origin;

        float timeSinceLastTick;
        private int stepCount;

        private Ant ant;

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="content"></param>
        /// <param name="gridWidth"></param>
        /// <param name="gridHeight"></param>
        public Board( ContentManager content, int gridWidth, int gridHeight )
        {
            this.content = content;
            texture = content.Load<Texture2D>( "Colors" );
            origin = new Vector2( texture.Width / 2, texture.Height / 2 );

            this.gridWidth = gridWidth;
            this.gridHeight = gridHeight;

            board = new Color[gridWidth + 1, gridHeight + 1];

            for ( int x = 0; x < gridWidth; x++ )
            {
                for ( int y = 0; y < gridHeight; y++ )
                {
                    board[x, y] = Color.White;
                }
            }

            ant = new Ant( gridWidth / 2, gridHeight / 2 );
        }

        /// <summary>
        /// Processes the simulation
        /// </summary>
        public void Simulate()
        {
            var currentPosition = ant.GetCurrentPosition();
            var currentDirection = ant.GetCurrentDirection();
            
            if ( currentPosition.X > 0 && currentPosition.X <= gridWidth &&
                    currentPosition.Y > 0 && currentPosition.Y <= gridHeight )
            {
                //change current square color
                if ( board[( int ) currentPosition.X, ( int ) currentPosition.Y] == Color.White )
                {
                    board[( int ) currentPosition.X, ( int ) currentPosition.Y] = Color.Red;
                    ant.MoveLeft();
                }
                else
                {
                    board[( int ) currentPosition.X, ( int ) currentPosition.Y] = Color.White;
                    ant.TurnRight();
                }
            }

            stepCount++;
        }

        /// <summary>
        /// Gets the number of iterations from the simulation
        /// </summary>
        /// <returns></returns>
        public int GetSteps()
        {
            return stepCount;
        }

        /// <summary>
        /// Updates the board during the game tick
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update( GameTime gameTime )
        {
            timeSinceLastTick += ( float ) gameTime.ElapsedGameTime.TotalSeconds;

            if ( true )
            {
                Simulate();
                timeSinceLastTick = 0f;
            }
        }

        /// <summary>
        /// Draws the cells to the screen
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw( SpriteBatch spriteBatch )
        {
            for ( int x = 0; x < gridWidth; x++ )
            {
                for ( int y = 0; y < gridHeight; y++ )
                {
                    if ( true )
                    {
                        spriteBatch.Draw( texture, new Vector2( x + 2, y + 2 ), new Rectangle( 3, 3, 1, 1 ), board[x,y], 0f, origin, 1f, SpriteEffects.None, 0f );
                    }
                }
            }
        }
    }
}