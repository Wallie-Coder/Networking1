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
    internal class Client : Peer
    {
        public override void Initialize()
        {
            thisPeer = new NetClient(Game1.config);

            thisPeer.Start();

            thisPeer.Connect(host: Game1.IP, port: Game1.portNr, thisPeer.CreateMessage("Connected new Peer"));
        }

        public override void Update(GameTime time)
        {


            base.Update(time);
        } 

        public override void SentMessage()
        {
            if (ChatFunction.messages.Count < messageCount)
            {

                NetOutgoingMessage om = thisPeer.CreateMessage(ChatFunction.messages[ChatFunction.messages.Count - 1].text);

                NetClient temp = (NetClient)thisPeer;

                temp.SendMessage(om, NetDeliveryMethod.ReliableOrdered);

            }
        }
    }
}
