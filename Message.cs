using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Design;
using Microsoft.Xna.Framework;

namespace Networking1
{
    internal class Message
    {
        internal string text = "";
        internal string senderName;
        internal Color color;

        internal Message(string text, string senderName, Color? color = null)
        {
            this.color = color ?? Color.Black;

            this.text = text;
            this.senderName = senderName;
        }
    }
}
