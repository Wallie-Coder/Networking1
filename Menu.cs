using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Networking1
{
    internal class Menu
    {

        internal static Button Server = new Button(new Vector2(510, 50), new Vector2(150, 100), "start server");

        internal static Button Client = new Button(new Vector2(510, 300), new Vector2(150, 100), "start client");


        internal void Update(GameTime Time)
        {
            Server.Update(Time);
            Client.Update(Time);
        }

        internal void Draw(SpriteBatch Batch)
        {
            if (Game1.activePeer == null)
            {
                Server.Draw(Batch);
                Client.Draw(Batch);
            }
            else if(Game1.activePeer.GetType() == typeof(Client))
            {
                Client.Draw(Batch);
            }
            else if (Game1.activePeer.GetType() == typeof(Server))
            {
                Server.Draw(Batch);
            }
        }
    }
}
