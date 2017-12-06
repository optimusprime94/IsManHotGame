using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace MonsterBlaster.Components
{
    public class SpriteComponent : IEngineComponent
    {
        public Texture2D Texture { get; set; }
        public float Scale { get; set; } = 1f;
    }
}
