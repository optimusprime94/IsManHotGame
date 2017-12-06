using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonsterBlaster.Components;
using MonsterBlaster.Content;
using MonsterBlaster.Managers;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Audio;

namespace MonsterBlaster.Systems
{
    public class InputSystem
    {
        private List<uint> EntitySuicideList = new List<uint>();
        private Random random = new Random();
        public void Update(GameTime gameTime)
        {
            var spawnComponents = ComponentManager.Get().GetComponents<SpawnComponent>();
            var touchCollection = TouchPanel.GetState();

            foreach (var spawnComponent in spawnComponents)
            {
                var boxCollision = ComponentManager.Get().EntityComponent<CollisionComponent>(spawnComponent.Key);
                var boxSprite = ComponentManager.Get().EntityComponent<SpriteComponent>(spawnComponent.Key);
                var boxPosition = ComponentManager.Get().EntityComponent<PositionComponent>(spawnComponent.Key);

                foreach (var touch in touchCollection)
                {
                    if (boxCollision.BoundingRectangle.Contains(touch.Position))
                    {
                        Console.WriteLine("Blasted Monster!");
                        EntitySuicideList.Add(spawnComponent.Key);
                    }
                }


            }
            // Cannot modify the components while looping through them
            RemoveEntities();

        }

        private void RemoveEntities()
        {

            foreach (var entity in EntitySuicideList)
            {
                ComponentManager.Get().DeleteEntity(entity);
                AssetManager.Get().SoundBank.ElementAt(random.Next(0, 2)).Play();
            }
            EntitySuicideList.Clear();
        }
    }
}
