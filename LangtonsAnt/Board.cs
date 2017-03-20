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
        private GraphicsDevice graphics;
        private ContentManager content;
        private Texture2D texture;
        private Texture2D backBuffer;
        private Vector2 origin;

        private float timeSinceLastTick;
        private int stepCount;

        private Ant ant;
        private List<UIControl> controls;

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="content"></param>
        /// <param name="gridWidth"></param>
        /// <param name="gridHeight"></param>
        public Board( GraphicsDevice graphics, ContentManager content, int gridWidth, int gridHeight, List<UIControl> controls )
        {
            this.controls = controls;
            this.graphics = graphics;
            backBuffer = new Texture2D( graphics, gridWidth, gridHeight );
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
            var counter = 0;
            var currentPosition = ant.GetCurrentPosition();
            var currentDirection = ant.GetCurrentDirection();
            
            if ( currentPosition.X > 0 && currentPosition.X <= gridWidth &&
                    currentPosition.Y > 0 && currentPosition.Y <= gridHeight )
            {
                // iterate through each control
                foreach ( var control in controls )
                {
                    counter++; 

                    // if we find a matching color..
                    if ( board[( int ) currentPosition.X, ( int ) currentPosition.Y] == control.CurrentColor )
                    {
                        if ( counter < controls.Count )
                        {
                            // ..change that square to the next color on our control list
                            board[( int ) currentPosition.X, ( int ) currentPosition.Y] = controls.Select( c => c.CurrentColor ).ElementAt( counter );
                        }
                        else
                        {
                            // ..else change the color back to the first control on the list
                            board[( int ) currentPosition.X, ( int ) currentPosition.Y] = controls.Select( c => c.CurrentColor ).First();
                        }
                        
                        // change the ant's direction based on the direction of the control
                        if ( control.CurrentDirection == UIControl.TurnDirection.Left )
                        {
                            ant.MoveLeft();
                        }
                        else
                        {
                            ant.MoveRight();
                        }

                        // update control step counter
                        control.StepCounter++;

                        // since we have a successfully found and processed a match.. break!
                        break;
                    }
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
            // create a new temporary texture..
            Color[] data = new Color[gridWidth * gridHeight];

            // ..and for each pixel, assign the color from the board
            for ( int x = 0; x < gridWidth; x++ )
            {
                for ( int y = 0; y < gridHeight; y++ )
                {
                    data[x + y * gridWidth] = board[x, y];
                }
            }

            // set color data to texture
            backBuffer.SetData( data );

            // draw texture
            spriteBatch.Draw( backBuffer, new Rectangle( 0, 0, gridWidth, gridHeight ), Color.White );
        }
    }
}