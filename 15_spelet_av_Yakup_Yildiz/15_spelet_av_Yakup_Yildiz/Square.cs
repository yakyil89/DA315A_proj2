using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace _15_spelet_av_Yakup_Yildiz
{
    class Square
    {
        // Lägg till variabler som ska användas som en egenskap för en tile
        Texture2D texture;

        public Vector2 position;
        public Vector2 target;

        Color color;

        Rectangle rect;

        int index;



        // Ange vad en "tile" ska ha för egenskaper
        public Square(Texture2D texture, Vector2 position, Rectangle rect, Color color, int index)
        {
            this.texture = texture;
            this.position = position;
            this.rect = rect;
            this.color = color;
            this.index = index;
            this.target = position;
        }

        public void Update()
        {
            if (position.X < target.X)
            {
                position.X = position.X + 1;
            }
            if (position.X > target.X)
            {
                position.X = position.X - 1;
            }
            if (position.Y < target.Y)
            {
                position.Y = position.Y + 1;
            }
            if (position.Y > target.Y)
            {
                position.Y = position.Y - 1;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (index != 0)
            {
                spriteBatch.Draw(texture, position, rect, color);
            }
        }
    }
}
