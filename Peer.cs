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

        public void ReceiveMessage(NetPeer peer)
        {
            NetIncomingMessage im;
            while ((im = peer.ReadMessage()) != null)
            {
                switch (im.MessageType)
                {
                    case NetIncomingMessageType.Data:
                        ChatFunction.messages.Add(im.ReadString());
                        break;

                    case NetIncomingMessageType.StatusChanged:
                        // handle connection status messages
                        switch (im.SenderConnection.Status)
                        {
                            case NetConnectionStatus.Connected:
                                ChatFunction.messages.Add("Connected");
                                break;
                            case NetConnectionStatus.Disconnected:
                                ChatFunction.messages.Add("DisConnected");
                                break;
                        }
                        break;

                    case NetIncomingMessageType.DebugMessage:
                        // handle debug messages
                        // (only received when compiled in DEBUG mode)
                        ChatFunction.messages.Add(im.ReadString());
                        break;

                    default:
                        ChatFunction.messages.Add("unhandled message with type: " + im.MessageType);
                        break;
                }
            }
        }
    }
}
