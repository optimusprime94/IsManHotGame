﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonsterBlaster.Components;
using MonsterBlaster.Content;

namespace MonsterBlaster.Systems
{
    public class ScoreSystem
    {
        public void Update(GameTime gameTime)
        {
            var scoreComponents = ComponentManager.Get().GetComponents<ScoreComponent>();

            foreach (var scoreComponent in scoreComponents)
            {
                var score = scoreComponent.Value as ScoreComponent;

                score.Score += gameTime.ElapsedGameTime.TotalSeconds;
            }

        }
    }
}
