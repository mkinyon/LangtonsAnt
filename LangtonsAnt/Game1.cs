using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LangtonsAnt
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        //must be power of 2
        const int renderScale = 8;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        Board board;
        Matrix matrix;
        private KeyboardState oldState;
        List<UIControl> controls;
        private int numOfSimulationsPerTick = 5;

        public Game1()
        {
            graphics = new GraphicsDeviceManager( this );
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// </summary>
        protected override void Initialize()
        {
            this.IsMouseVisible = true;

            matrix = Matrix.CreateScale( renderScale, renderScale, 1.0f );
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();

            // disable frame cap
            IsFixedTimeStep = false;

            controls = new List<UIControl>();

            // First control must always be white
            controls.Add( new UIControl( 32, 32, Content, graphics.GraphicsDevice, Color.White ) );
            controls.Add( new UIControl( 32, 80, Content, graphics.GraphicsDevice, Color.Red ) );

            board = new Board( Content, GraphicsDevice.DisplayMode.Width / renderScale, GraphicsDevice.DisplayMode.Height / renderScale, controls );

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch( GraphicsDevice );

            font = Content.Load<SpriteFont>( "system" );
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update( GameTime gameTime )
        {
            KeyboardState newState = Keyboard.GetState();

            if ( GamePad.GetState( PlayerIndex.One ).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown( Keys.Escape ) )
                Exit();

            // input to increase simulations per tick
            if ( oldState.IsKeyUp( Keys.Add ) && newState.IsKeyDown( Keys.Add ) )
            {
                numOfSimulationsPerTick += 5;
            }

            // input to decrease simulations per tick
            if ( oldState.IsKeyUp( Keys.Subtract ) && newState.IsKeyDown( Keys.Subtract ) )
            {
                if ( numOfSimulationsPerTick >= 5 )
                {
                    numOfSimulationsPerTick -= 5;
                }
            }

            // input to add logic step
            if ( oldState.IsKeyUp( Keys.A ) && newState.IsKeyDown( Keys.A ) )
            {
                var column = controls.Count / ( GraphicsDevice.DisplayMode.Height / 48 );
                var row = controls.Count % (GraphicsDevice.DisplayMode.Height / 48);

                controls.Add( new UIControl( (column  * 128) + 32, ( ( row + 1 ) * 48 ) - 16, Content, graphics.GraphicsDevice ) );
            }

            oldState = newState;

            for ( int i = 0; i < numOfSimulationsPerTick; i++ )
            {
                board.Update( gameTime );
            }
            
            base.Update( gameTime );
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw( GameTime gameTime )
        {
            GraphicsDevice.Clear( Color.DarkSlateGray );

            spriteBatch.Begin( transformMatrix: matrix );
            board.Draw( spriteBatch );
            spriteBatch.End();

            spriteBatch.Begin();
            spriteBatch.DrawString( font, "Iterations: " + board.GetSteps().ToString(), new Vector2( GraphicsDevice.DisplayMode.Width / 2, 32 ), Color.Black );
            spriteBatch.DrawString( font, "Iterations Per Tick: " + numOfSimulationsPerTick.ToString(), new Vector2( GraphicsDevice.DisplayMode.Width / 2, 57 ), Color.Black );

            foreach ( var controls in controls )
            {
                controls.Draw( spriteBatch );
            }

            spriteBatch.End();

            base.Draw( gameTime );
        }
    }
}
