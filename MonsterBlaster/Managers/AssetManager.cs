using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace MonsterBlaster.Managers
{
    public class AssetManager
    {

        private static AssetManager assetManager;
        public Viewport GameSceneViewport { get; set; }
        public Rectangle SpawnArea { get; set; }

        public List<SoundEffectInstance> SoundBank  { get; set; }
        private AssetManager()
        {

        }

        public static AssetManager Get()
        {
            if (assetManager == null)
            {
                assetManager = new AssetManager();
            }
            return assetManager;
        }
    }
}
