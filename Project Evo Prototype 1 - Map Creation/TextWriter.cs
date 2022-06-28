using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project_Evo_Prototype_1___Map_Creation
{
    class TextWriter
    {
        private Rectangle textPos;
        private char characterInputted = ' ';
        private string inputtedString = "";
        private bool stopTyping = false;

        public TextWriter()
        {

        }

        public TextWriter(Rectangle inPosition)
        {
            textPos = inPosition;
        }

        public char GetKeyCharacter(string characterString)
        {
            if(Keyboard.GetState().GetPressedKeys().Length > 0)
            {
                characterString = Keyboard.GetState().GetPressedKeys()[0].ToString();
                foreach(char c in characterString)
                {
                        characterInputted = c;
                }
            }

            return characterInputted;
        }

        public void WriteText(SpriteBatch spriteBatch, SpriteFont spritefont, string characterString)
        {

            if (stopTyping == false)
            {
                if (Keyboard.GetState().GetPressedKeys().Length > 0 && Keyboard.GetState().GetPressedKeys().Length <= 1)
                {
                    if (char.IsDigit(GetKeyCharacter(characterString)))
                    {
                        inputtedString = inputtedString + GetKeyCharacter(characterString);
                        stopTyping = true;
                    }
                }
            }
            else
            {
                if (Keyboard.GetState().GetPressedKeys().Length == 0)
                {
                    stopTyping = false;
                }
            }

            spriteBatch.DrawString(spritefont, inputtedString, new Vector2(textPos.X, textPos.Y), Color.Black);
        }

    }
}
