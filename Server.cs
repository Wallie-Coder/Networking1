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

        public override void ReceiveMessage(NetPeer peer)
        {
            NetIncomingMessage im;
            while ((im = peer.ReadMessage()) != null)
            {
                switch (im.MessageType)
                {
                    case NetIncomingMessageType.Data:
                        string s = im.ReadString();
                        ChatFunction.messages.Add(new Message(s, im.SenderEndPoint.ToString(), Color.DarkRed));
                        ContinueSendData(im, s);
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

        public void ContinueSendData(NetIncomingMessage im, string s)
        {
            NetServer temp = (NetServer)thisPeer;
            foreach (NetConnection conn in temp.Connections)
            {
                if (im.SenderConnection != null && im.MessageType == NetIncomingMessageType.Data)
                {
                    if (conn.RemoteUniqueIdentifier != im.SenderConnection.RemoteUniqueIdentifier)
                    {
                        NetOutgoingMessage om = thisPeer.CreateMessage(s);
                        temp.SendMessage(om, conn, NetDeliveryMethod.ReliableOrdered);
                    }
                }
            }
        }

    }
}
