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
    public class UIControl
    {
        Random random;

        private GraphicsDevice graphics;
        private Vector2 position;
        private Rectangle sourceRect;
        private Texture2D uiTexture;
        private Texture2D colorTexture;
        private SpriteFont font;

        public int StepCounter;
        public Color CurrentColor;
        public TurnDirection CurrentDirection;

        public enum TurnDirection
        {
            Left = 0,
            Right = 1
        }

        public UIControl ( int xPos, int yPos, ContentManager content, GraphicsDevice graphics )
        {
            random = new Random();
            font = content.Load<SpriteFont>( "system" );

            this.graphics = graphics;
            position.X = xPos;
            position.Y = yPos;

            // load texture
            uiTexture = content.Load<Texture2D>( "UI" );

            // pick a random color and direction
            ChangeColorToRandom();
            ChangeDirectionToRandom();
        }

        public UIControl( int xPos, int yPos, ContentManager content, GraphicsDevice graphics, Color color )
        {
            random = new Random();
            font = content.Load<SpriteFont>( "system" );

            this.graphics = graphics;
            position.X = xPos;
            position.Y = yPos;

            // load texture
            uiTexture = content.Load<Texture2D>( "UI" );

            // pick a random color and direction
            CurrentColor = color;
            colorTexture = CreateColorTexture( CurrentColor );
            ChangeDirectionToRandom();
        }

        public void Update ( GameTime gameTime )
        {

        }

        public Texture2D CreateColorTexture ( Color color )
        {
            Texture2D texture = new Texture2D( graphics, 32, 32 );

            Color[] data = new Color[32 * 32];
            for ( int i = 0; i < data.Length; ++i )
            {
                data[i] = color;
            }
                
            texture.SetData( data );
            return texture;
        }

        public void ChangeDirectionToRandom()
        {
            if ( random.Next( 0, 3 ) == 1 )
            {
                CurrentDirection = TurnDirection.Left;
                sourceRect = new Rectangle( 0, 0, 32, 32 );
            }
            else
            {
                CurrentDirection = TurnDirection.Right;
                sourceRect = new Rectangle( 0, 32, 32, 32 );
            }
        }

        public void ChangeColorToRandom()
        {
            var randomColor = new Color(
                ( byte ) random.Next( 0, 255 ),
                ( byte ) random.Next( 0, 255 ),
                ( byte ) random.Next( 0, 255 ) );

            CurrentColor = randomColor;
            colorTexture = CreateColorTexture( randomColor );
        }

        public void Draw ( SpriteBatch spriteBatch )
        {
            // draw color
            spriteBatch.Draw( colorTexture, new Rectangle( ( int ) position.X, ( int ) position.Y, 32, 32 ), Color.White );

            // draw direction
            spriteBatch.Draw( uiTexture, new Rectangle( ( int ) position.X + 48, ( int ) position.Y, 32, 32 ), sourceRect, Color.White );

            // draw step counter
            spriteBatch.DrawString( font, StepCounter.ToString(), new Vector2( ( int ) position.X + 96, ( int ) position.Y ), Color.Black );

        }

    }
}
