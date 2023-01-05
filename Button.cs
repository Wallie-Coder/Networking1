using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Networking1
{
    internal class Button
    {
        internal Vector2 Location;
        internal Vector2 Size;

        internal bool pressed = false;
        bool clicked = false;

        internal string Name;

        internal Button(Vector2 location, Vector2 size, string Name)
        {
            Location = location;
            Size = size;

            this.Name = Name;
        }

        internal void Update(GameTime time)
        {
            if(Input.IsMouseOn(Location, Size) && Input.LeftMousePressed())
            {
                clicked = true;
            }
            else if(clicked && Input.IsMouseOn(Location, Size) && Input.LeftMouseReleased())
            {
                clicked = false;
                pressed = true;
            }
            else
            {
                pressed = false;
            }
        }

        internal void Draw(SpriteBatch batch)
        {
            batch.Draw(Input.getSprite("White_Pixel"), Location, null, Color.SkyBlue, 0, Vector2.Zero, Size, SpriteEffects.None, 0f);


            batch.DrawString(Input.getFont("File"), Name, Location + (Size - Input.getFont("File").MeasureString(Name)) * 0.5f, Color.Black);
        }
    }
}
