using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterBlaster.Components
{
    public class SpawnComponent : IEngineComponent
    {

        public double SpawnProbability { get; set; } = 0.11f;
    }
}
