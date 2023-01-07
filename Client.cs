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
            if (ChatFunction.messages.Count > messageCount)
            {
                messageCount = ChatFunction.messages.Count;

                NetOutgoingMessage om = thisPeer.CreateMessage(ChatFunction.messages[ChatFunction.messages.Count - 1].text);

                NetClient temp = (NetClient)thisPeer;

                if (ChatFunction.messages[ChatFunction.messages.Count - 1].text.Contains("/STATISTICS"))
                {
                    ChatFunction.messages.Add(new Message(temp.Statistics.ToString(), "Server"));
                    for (int i = 0; i < 4; i++)
                    {
                        ChatFunction.messages.Add(new Message("", ""));
                    }
                    return;
                }

                if (ChatFunction.messages[ChatFunction.messages.Count - 1].text.Contains("/PING"))
                {
                    ChatFunction.messages.Add(new Message("ping: " + (temp.Connections[0].AverageRoundtripTime).ToString() + " ms", "Server"));
                    return;
                }

                if (ChatFunction.messages[ChatFunction.messages.Count - 1].text.Length < 2)
                    return;

                temp.SendMessage(om, NetDeliveryMethod.ReliableOrdered);

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
                        ChatFunction.messages.Add(new Message(im.ReadString(), im.SenderEndPoint.ToString(), Color.DarkRed));
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
