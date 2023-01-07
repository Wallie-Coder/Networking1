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
                        string s = im.ReadString();
                        string[] splits = s.Split("\\r\\");
                        
                        foreach(string sub in splits)
                            ChatFunction.messages.Add(new Message(sub, im.SenderEndPoint.ToString(), Color.DarkRed));
                        messageCount++;
                        break;

                    case NetIncomingMessageType.StatusChanged:
                        // handle connection status messages
                        switch (im.SenderConnection.Status)
                        {
                            case NetConnectionStatus.Connected:
                                ChatFunction.messages.Add(new Message("Connected", im.SenderEndPoint.ToString()));
                                messageCount++;
                                break;
                            case NetConnectionStatus.Disconnected:
                                ChatFunction.messages.Add(new Message("DisConnected", im.SenderEndPoint.ToString()));
                                messageCount++;
                                break;
                        }
                        break;

                    case NetIncomingMessageType.DebugMessage:
                        // handle debug messages
                        // (only received when compiled in DEBUG mode)
                        ChatFunction.messages.Add(new Message(im.ReadString(), "Server"));
                        messageCount++;
                        break;

                    default:
                        ChatFunction.messages.Add(new Message("unhandled message with type: " + im.MessageType, "Server"));
                        messageCount++;
                        break;
                }
            }
        }
    }
}
