using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using SharpDX.X3DAudio;

namespace Networking1
{
    internal static class Input
    {
        static ContentManager content = Game1._content;

        static MouseState oldmouse;
        static MouseState mouse = Mouse.GetState();

        static KeyboardState oldkeyboard;
        static KeyboardState keyboard = Keyboard.GetState();

        internal static void Update(GameTime time)
        {
            oldmouse = mouse;
            mouse = Mouse.GetState();

            oldkeyboard = keyboard;
            keyboard = Keyboard.GetState();
        }

        internal static Vector2 MousePos
        {
            get
            {
                return new Vector2(mouse.X, mouse.Y);
            }
        }

        internal static bool IsMouseOn(Vector2 pos, Vector2 Size)
        {
            if(MousePos.X < pos.X + Size.X && MousePos.X > pos.X && MousePos.Y < pos.Y + Size.X && MousePos.Y > pos.Y)
            {
                return true;
            }
            return false;
        }

        internal static bool LeftMousePressed()
        {
            if(oldmouse.LeftButton == ButtonState.Released && mouse.LeftButton == ButtonState.Pressed) 
            {
                return true;
            }
            return false;
        }

        internal static bool LeftMouseReleased()
        {
            if (mouse.LeftButton == ButtonState.Released)
                return true;
            return false;
        }

        internal static List<Keys> KeysJustPressed()
        {
            List<Keys> keys = new List<Keys>();

            foreach (Keys k in keyboard.GetPressedKeys())
                if(!oldkeyboard.GetPressedKeys().Contains(k))
                    keys.Add(k);

            return keys;
        }

        internal static Texture2D getSprite(string assetName)
        {
            return content.Load<Texture2D>(assetName);
        }

        internal static SpriteFont getFont(string assetName)
        {
            return content.Load<SpriteFont>(assetName);
        }
    }
}
