using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonsterBlaster.Components
{
    public class PositionComponent:IEngineComponent
    {
        public Vector2 Position { get; set; }
        public float Orientation { get; set; }
    }
}   
