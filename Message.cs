using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Networking1
{
    internal class Message
    {
        internal string text = "";
        internal string senderName;
        internal Color color = Color.Black;

        internal Message(string text, string senderName)
        {
            this.text = text;
            this.senderName = senderName;
        }
    }
}
