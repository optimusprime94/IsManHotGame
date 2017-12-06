using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace MonsterBlaster.Components
{
    public class InputComponent : IEngineComponent
    {
       // public TouchCollection Touch { get; set; }
        public bool IsPressed { get; set; } = false;
    }
}
