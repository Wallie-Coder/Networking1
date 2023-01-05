using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Networking1
{
    internal class ServerOrClient
    {
        internal Switch Server = new Switch(new Vector2(510, 50), new Vector2(250, 145), Input.getSprite("White_Pixel"));

        internal Switch Connect = new Switch(new Vector2(510, 300), new Vector2(250, 145), Input.getSprite("White_Pixel"));



        internal void Update(GameTime Time)
        {
            Server.Update(Time);

            Connect.Update(Time);
        }

        internal void Draw(SpriteBatch Batch)
        {
            Server.Draw(Batch);
            Connect.Draw(Batch);
        }
    }
}
