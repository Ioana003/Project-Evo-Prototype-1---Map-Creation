using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project_Evo_Prototype_1___Map_Creation
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private SpriteFont font_def;
        private string inputString = "";
        private Rectangle textBoxRectangle;
        private Texture2D textBoxTexture;
        private MouseManager mouseManager = new MouseManager();
        private TextWriter textWriter = new TextWriter();
        private bool allowText = false;
        private const int SIZE_OF_CELL = 10;
        private float[,] numberOfCells;
        private NoiseMaker noiseMaker = new NoiseMaker();
        private float[][] perlinNoise;
        private bool showTyping = true;
        private Rectangle cellRect;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferHeight = 1000;
            _graphics.PreferredBackBufferWidth = 1000;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            textBoxTexture = Content.Load<Texture2D>("textRectangle");

            textBoxRectangle = new Rectangle(Window.ClientBounds.Width / 2 - 150 / 2, Window.ClientBounds.Height / 2 - 25 / 2, 150, 25);

            font_def = Content.Load<SpriteFont>("text_default2");

            textWriter = new TextWriter(textBoxRectangle);

            numberOfCells = new float[Window.ClientBounds.Width / SIZE_OF_CELL, Window.ClientBounds.Height / SIZE_OF_CELL];
            //This makes a grid of all the cells that can be used/made for the area

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            if (showTyping == true)
            {
                if (mouseManager.CheckIfClicked(textBoxRectangle) == true)
                {
                    allowText = true;
                }
                else
                {
                    allowText = false;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                showTyping = false;

                perlinNoise = noiseMaker.GeneratePerlinhNoise(noiseMaker.GenerateWhiteNoise(Window.ClientBounds.Width, Window.ClientBounds.Height, int.Parse(textWriter.GetInputtedString())), 4);

                for (int i = 0; i <= numberOfCells.GetUpperBound(0); i++)
                {
                    for (int j = 0; j <= numberOfCells.GetUpperBound(1); j++)
                    {
                        numberOfCells[i, j] = perlinNoise[i][j];
                    }
                }
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            if (showTyping == true)
            {
                _spriteBatch.Draw(textBoxTexture, textBoxRectangle, Color.Gray);

                if (allowText == true)
                {
                    textWriter.WriteText(_spriteBatch, font_def, inputString);
                }
            }

            else
            {
                for(int i = 0; i <= numberOfCells.GetUpperBound(0); i++)
                {
                    for(int j = 0; j <= numberOfCells.GetUpperBound(1); j++)
                    {
                        if (numberOfCells[i, j] < 0.5 && numberOfCells[i, j] > 0.4)
                        {
                            _spriteBatch.Draw(textBoxTexture, new Rectangle(SIZE_OF_CELL * i, SIZE_OF_CELL * j, SIZE_OF_CELL, SIZE_OF_CELL), Color.CornflowerBlue);
                        }
                        if (numberOfCells[i, j] <= 0.4 && numberOfCells[i, j] > 0.3)
                        {
                            _spriteBatch.Draw(textBoxTexture, new Rectangle(SIZE_OF_CELL * i, SIZE_OF_CELL * j, SIZE_OF_CELL, SIZE_OF_CELL), Color.Blue);
                        }
                        if (numberOfCells[i, j] <= 0.3)
                        {
                            _spriteBatch.Draw(textBoxTexture, new Rectangle(SIZE_OF_CELL * i, SIZE_OF_CELL * j, SIZE_OF_CELL, SIZE_OF_CELL), Color.DarkBlue);
                        }
                        if (numberOfCells[i, j] >= 0.5 && numberOfCells[i, j] < 0.8)
                        {
                            _spriteBatch.Draw(textBoxTexture, new Rectangle(SIZE_OF_CELL * i, SIZE_OF_CELL * j, SIZE_OF_CELL, SIZE_OF_CELL), Color.Beige);
                        }
                    }
                }
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
