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

        bool connectstatus = false;


        internal void Update(GameTime Time, bool connectStatus)
        {
            Server.Update(Time);

            Connect.Update(Time);

            this.connectstatus = connectStatus;
        }

        internal void Draw(SpriteBatch Batch)
        {
            Server.Draw(Batch);
            Connect.Draw(Batch);

            if(Server.activated == true)
            {
                Batch.DrawString(Input.getFont("File"), "Server", new Vector2(550, 100), Color.Black);
            }
            else
            {
                Batch.DrawString(Input.getFont("File"), "Client", new Vector2(550, 100), Color.Black);
            }

            if(connectstatus == true)
            {
                Batch.DrawString(Input.getFont("File"), "Connected", new Vector2(550, 300), Color.Black);
            }
            else
            {
                Batch.DrawString(Input.getFont("File"), "not Connected", new Vector2(550, 300), Color.Black);
            }
        }
    }
}
