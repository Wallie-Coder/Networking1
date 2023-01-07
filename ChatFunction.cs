using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Lidgren.Network;
using Microsoft.Xna.Framework.Input;

namespace Networking1
{
    internal static class ChatFunction
    {

        public static List<Message> messages = new List<Message>();

        static int timer;
        static string s;

        public static void Update(GameTime time)
        {
            foreach (Keys k in Input.KeysJustPressed())
                if (k == Keys.Enter)
                {
                    s.Trim();

                    if (s.StartsWith("/ROOMCODE "))
                    {
                        s = s.Remove(0, 10);
                        s.Trim();
                        Game1.portNr = Int32.Parse(s);
                        messages.Add(new Message("RoomCode: " + Game1.portNr, "Server"));
                    }
                    else if(s.StartsWith("/IP "))
                    {
                        s = s.Remove(0, 4);
                        s.Trim();
                        Game1.IP = s;
                        messages.Add(new Message("Server IP: " + s, "Server"));
                    }
                    else
                    { 
                        messages.Add(new Message(s, "Me", Color.DarkBlue));
                    }
                    s = "";
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
                else if (k == Keys.OemQuestion)
                {
                    s += "/";
                }
                else if (k == Keys.OemPeriod)
                {
                    s += ".";
                }
                else if(k.ToString().Length > 1)
                {
                    s += k.ToString().Remove(0, 1);
                }
                else
                    s += k.ToString();
        }

        public static void Draw(SpriteBatch batch)
        {
            batch.Draw(Input.getSprite("White_Pixel"), new Vector2(0, 475), null, Color.DarkGray, 0, Vector2.Zero, new Vector2(400, 25), SpriteEffects.None, 0f);

            for (int i = 0; i < messages.Count; i++)
            {
                batch.DrawString(Input.getFont("File"), messages[i].senderName + ": " + messages[i].text, new Vector2(10, 500 - (messages.Count - i + 1) * 20), messages[i].color);
            }

            if(s != null)
                batch.DrawString(Input.getFont("File"), s, new Vector2(10, 480), Color.Black);
        }
    }
}
