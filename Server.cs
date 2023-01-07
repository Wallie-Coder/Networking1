using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Networking1
{
    internal class Server : Peer
    {
        public override void Initialize()
        {
            thisPeer = new NetServer(Game1.config);
            thisPeer.Start();

            ChatFunction.messages.Add(new Message("started Server: " + Game1.portNr, "Server"));
        }

        public override void Update(GameTime time)
        {
            base.Update(time);
        }

        public override void SentMessage()
        {
            if (ChatFunction.messages.Count > messageCount)
            {
                messageCount = ChatFunction.messages.Count;

                NetOutgoingMessage om = thisPeer.CreateMessage(ChatFunction.messages[ChatFunction.messages.Count - 1].text);

                NetServer temp = (NetServer)thisPeer;

                temp.SendToAll(om, NetDeliveryMethod.ReliableOrdered);

            }
        }

    }
}
