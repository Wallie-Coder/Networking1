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
    internal class Peer
    {
        internal enum PeerType { Server, Client, None }

        internal PeerType peerType = PeerType.None;

        internal NetPeer thisPeer;

        internal int messageCount;

        public virtual void Initialize()
        {
            messageCount = ChatFunction.messages.Count;
        }

        public virtual void Update(GameTime time)
        {
            SentMessage();
            ReceiveMessage(thisPeer);
        }

        public virtual void SentMessage()
        {

        }

        public virtual void ReceiveMessage(NetPeer peer)
        {

        }
    }
}
