using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking1
{
    internal class Switch
    {
        Texture2D sprite;
        Vector2 size;
        Vector2 location;
        internal bool activated = false;
        bool hit = false;


        internal Switch(Vector2 Location, Vector2 Size, Texture2D Sprite)
        {
            this.sprite = Sprite;
            this.location = Location;
            this.size = Size;
        }

        public void Update(GameTime time)
        {
            if (Input.IsMouseOn(location, size) && Input.LeftMousePressed())
                hit = true;

            if (Input.IsMouseOn(location, size) && Input.LeftMouseReleased() && hit)
                activated = !activated;

            if (Input.LeftMouseReleased())
                hit = false;
        }

        public void Draw(SpriteBatch batch)
        {
            if(activated)
            {
                batch.Draw(sprite, new Rectangle((int)location.X, (int)location.Y, (int)size.X, (int)size.Y), null, Color.Green, 0, Vector2.Zero, SpriteEffects.None, 0f);
            }
            else
            {
                batch.Draw(sprite, new Rectangle((int)location.X, (int)location.Y, (int)size.X, (int)size.Y), null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 0f);
            }
        }
    }
}
