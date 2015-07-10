using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GrahamThomson.Xna.Common.Particles
{
    public class ParticleTexture
    {
        public string TextureName { get; private set; }
        public Texture2D Texture { get; private set; }
        public Rectangle? Source { get; private set; }
        public Vector2 TextureHalfSize { get; private set; }

        public ParticleTexture(string textureName, Texture2D texture)
            : this(textureName, texture, null)
        {
        }
        
        public ParticleTexture(string textureName, Texture2D texture, Rectangle? source)
        {
            TextureName = textureName;
            Texture = texture;
            Source = source;
            TextureHalfSize = new Vector2(Texture.Width / 2, Texture.Height / 2);
        }
    }
}
