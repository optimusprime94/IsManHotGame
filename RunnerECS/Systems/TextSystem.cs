﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RunnerECS.Components;
using RunnerECS.Content;

namespace RunnerECS.Systems
{
    public class TextSystem
    {
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var scoreComponents = ComponentManager.Get().GetComponents<ScoreComponent>();

            spriteBatch.Begin();

            foreach (var scoreComponent in scoreComponents)
            {
                var score = scoreComponent.Value as ScoreComponent;

                if (score != null)
                    spriteBatch.DrawString(score.Font, ((int) score.Score).ToString(), new Vector2(20, 20),
                        Color.White);
            }
            spriteBatch.End();
        }
    }
}
