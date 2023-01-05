using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;

namespace Networking1
{
    internal class Main
    {

        public Switch sw = new Switch(new Vector2(0, 0), new Vector2(250, 250), Input.getSprite("White_Pixel"));
        public Switch sw2 = new Switch(new Vector2(0, 250), new Vector2(250, 250), Input.getSprite("White_Pixel"));
        public Switch sw3 = new Switch(new Vector2(250, 0), new Vector2(250, 250), Input.getSprite("White_Pixel"));
        public Switch sw4 = new Switch(new Vector2(250, 250), new Vector2(250, 250), Input.getSprite("White_Pixel"));

        public Main() 
        { 

        }

        public void Update(GameTime time)
        {
            sw.Update(time);
            sw2.Update(time);
            sw3.Update(time);
            sw4.Update(time);

        }

        public void Draw(SpriteBatch batch)
        {
            sw.Draw(batch);
            sw2.Draw(batch);
            sw3.Draw(batch);
            sw4.Draw(batch);

        }
    }
}
