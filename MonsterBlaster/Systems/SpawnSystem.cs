using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonsterBlaster.Components;
using MonsterBlaster.Content;
using MonsterBlaster.Managers;

namespace MonsterBlaster.Systems
{
    public class SpawnSystem
    {
        private Random random = new Random();
        private const float probability = 0.01f;
        //private Texture2D blockTexture;
        public void LoadContent(ContentManager contentManager)
        {
        }

        public void Update(GameTime gameTime)
        {
            var value = random.NextDouble();
           if (value <= probability)
            {
                Console.Out.WriteLine("Monster Created");
                CreateMonster();
            }
        }

        public void CreateMonster()
        {
            var Monster = ComponentManager.Get().NewEntity();

            ComponentManager.Get().AddComponentToEntity(new SpriteComponent() { Texture = Game1.BlockTexture, Scale = 0.4f }, Monster);
            ComponentManager.Get().AddComponentToEntity(new PositionComponent() { Position = SpawnPosition() }, Monster);
            ComponentManager.Get().AddComponentToEntity(new MovementComponent(), Monster);
            ComponentManager.Get().AddComponentToEntity(new CollisionComponent(), Monster);
            ComponentManager.Get().AddComponentToEntity(new SpawnComponent(), Monster);
        }
         
        Vector2 SpawnPosition()
        {
            while (true)
            {
              var x = random.Next(AssetManager.Get().SpawnArea.X, AssetManager.Get().SpawnArea.Width);
              var y = random.Next(AssetManager.Get().SpawnArea.Y, AssetManager.Get().SpawnArea.Height);
              var pos = new Vector2(x, y);
                if (!AssetManager.Get().GameSceneViewport.TitleSafeArea.Contains(pos))
                    return pos;
            }
        }
    }
}
