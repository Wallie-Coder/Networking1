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
        internal enum Peer {Server, Client, None}

        internal Peer PeerType = Peer.None;

        internal Button Server = new Button(new Vector2(510, 50), new Vector2(150, 100), "start server");

        internal Button Client = new Button(new Vector2(510, 300), new Vector2(150, 100), "start client");


        internal void Update(GameTime Time)
        {
            if (PeerType == Peer.None)
            {
                Server.Update(Time);
                Client.Update(Time);
            }

            //Server.Update(Time);
            //Client.Update(Time);
        }

        internal void Draw(SpriteBatch Batch)
        {

            if (PeerType == Peer.None)
            {
                Server.Draw(Batch);
                Client.Draw(Batch);
            }
            if (PeerType == Peer.Client)
                Client.Draw(Batch);
            if (PeerType == Peer.Server)
                Server.Draw(Batch);


        }
    }
}
