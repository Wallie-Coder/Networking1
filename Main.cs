using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Networking1
{
    internal class Main
    {

        public List<String> messages = new List<string>();

        int timer;
        string s;

        public Main() 
        { 

        }

        public void Update(GameTime time)
        {
            foreach (Keys k in Input.KeysJustPressed())
                if (k == Keys.Enter)
                {
                    messages.Add(s);
                    s = " ";
                }
                else
                    if (k == Keys.Space)
                    s += " ";
                else if (k == Keys.Back)
                {
                    if (s.Count() >= 1)
                    {
                        s = s.Remove(s.Count() - 1);
                    }
                }
                else
                    s += k.ToString();
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(Input.getSprite("White_Pixel"), new Vector2(0, 475), null, Color.DarkGray, 0, Vector2.Zero, new Vector2(400, 25), SpriteEffects.None, 0f);

            for (int i = 0; i < messages.Count; i++)
            {
                batch.DrawString(Input.getFont("File"), messages[i], new Vector2(10, 500 - (messages.Count - i + 1) * 20), Color.Black);
            }

            if(s != null)
                batch.DrawString(Input.getFont("File"), s, new Vector2(10, 480), Color.Black);
        }
    }
}
