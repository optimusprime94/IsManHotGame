using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonsterBlaster.Components
{
    public class MovementComponent:IEngineComponent
    {
        public Vector2 Velocity { get; set; }
    }
}
