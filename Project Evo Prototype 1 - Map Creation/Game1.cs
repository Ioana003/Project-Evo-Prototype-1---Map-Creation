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

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
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
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here


            if (mouseManager.CheckIfClicked(textBoxRectangle) == true)
            {
                allowText = true;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            _spriteBatch.Draw(textBoxTexture, textBoxRectangle, Color.Gray);

            if (allowText == true)
            {
                textWriter.WriteText(_spriteBatch, font_def, inputString);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
