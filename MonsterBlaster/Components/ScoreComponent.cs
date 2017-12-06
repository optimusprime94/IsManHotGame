using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace MonsterBlaster.Components
{
    public class ScoreComponent : IEngineComponent
    {
        public SpriteFont Font { get; set; }
        public double Score { get; set; }
    }
}
