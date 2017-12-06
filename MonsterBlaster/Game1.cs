using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonsterBlaster.Components;
using MonsterBlaster.Content;
using MonsterBlaster.Managers;
using MonsterBlaster.Systems;
using System.Collections.Generic;

namespace MonsterBlaster
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private RenderingSystem renderingSystem;
        private PhysicsSystem physicsSystem;
        private CollisionDetectionSystem collisionDetectionSystem;
        private InputSystem inputSystem;
        private SpawnSystem spawnSystem;
        private TextSystem textSystem;
        private ScoreSystem scoreSystem;

        private Texture2D background;
        private Song song;
        private double count = 5;
        private SpriteFont font;

        public static Texture2D PlayerTexture { get; set; }
        public static Texture2D BlockTexture { get; set; }

        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 480;
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;

            renderingSystem = new RenderingSystem();
            physicsSystem = new PhysicsSystem();
            collisionDetectionSystem = new CollisionDetectionSystem();
            inputSystem = new InputSystem();
            spawnSystem = new SpawnSystem();
            textSystem = new TextSystem();
            scoreSystem = new ScoreSystem();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            int area = 200;

            AssetManager.Get().GameSceneViewport = graphics.GraphicsDevice.Viewport;
            AssetManager.Get().SpawnArea = new Rectangle(
                graphics.GraphicsDevice.Viewport.X - area,
                graphics.GraphicsDevice.Viewport.Y - area,
                graphics.GraphicsDevice.Viewport.Width + area,
                graphics.GraphicsDevice.Viewport.Height + area
                );

            // TODO: use this.Content to load your game content here
            background = Content.Load<Texture2D>("background");
            song = Content.Load<Song>("jaws");
            font = Content.Load<SpriteFont>("font");

            PlayerTexture = Content.Load<Texture2D>("player");
            BlockTexture = Content.Load<Texture2D>("monster2");

            var list = new List<SoundEffectInstance>();
            list.Add(Content.Load<SoundEffect>("kakaka").CreateInstance());
            list.Add(Content.Load<SoundEffect>("skidi").CreateInstance());
            list.Add(Content.Load<SoundEffect>("thethinggoes").CreateInstance());

            AssetManager.Get().SoundBank = list;
            CreateEntities();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            if (MediaPlayer.State == MediaState.Stopped)
            {
                MediaPlayer.Play(song);
            }

            if (collisionDetectionSystem.CollisionOccured)
            {
                if (count <= 0)
                {
                    ResetGame();
                    count = 5;
                }
                count -= gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                // TODO: Add your update logic here
                collisionDetectionSystem.Update(gameTime);
                physicsSystem.Update(gameTime);
                spawnSystem.Update(gameTime);
                inputSystem.Update(gameTime);
                scoreSystem.Update(gameTime);

            }


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, AssetManager.Get().GameSceneViewport.Bounds, Color.White);

 


            // TODO: Entity Component Systems
            renderingSystem.Draw(gameTime, spriteBatch);
            textSystem.Draw(gameTime, spriteBatch);

            if (collisionDetectionSystem.CollisionOccured)
            {
                // GraphicsDevice.Clear(Color.Red);
                if (count >= 0)
                {
                    var message = "GAME OVER \nMan Got HOT!";
                    var height = AssetManager.Get().GameSceneViewport.Height;
                    var width = AssetManager.Get().GameSceneViewport.Width;

                    spriteBatch.DrawString(font, message, new Vector2((width - font.MeasureString(message).X) * 0.5f, (height - font.MeasureString(message).Y) * 0.5f), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                }
            }
            else
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);

            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void ResetGame()
        {
            ComponentManager.Get().ClearComponents();

            renderingSystem = new RenderingSystem();
            physicsSystem = new PhysicsSystem();
            collisionDetectionSystem = new CollisionDetectionSystem();
            inputSystem = new InputSystem();
            spawnSystem = new SpawnSystem();
            textSystem = new TextSystem();
            CreateEntities();
        }

        private void CreateEntities()
        {
            var playerId = ComponentManager.Get().NewEntity();

            ComponentManager.Get().AddComponentToEntity(new SpriteComponent() { Texture = PlayerTexture, Scale = 0.5f }, playerId);
            ComponentManager.Get().AddComponentToEntity(new PositionComponent() { Position = new Vector2((graphics.GraphicsDevice.Viewport.Width - PlayerTexture.Width) * 0.5f, (graphics.GraphicsDevice.Viewport.Height - PlayerTexture.Height) * 0.5f) }, playerId);
            ComponentManager.Get().AddComponentToEntity(new InputComponent(), playerId);
            //ComponentManager.Get().AddComponentToEntity(new MovementComponent(), playerId);
            ComponentManager.Get().AddComponentToEntity(new CollisionComponent(), playerId);
            ComponentManager.Get().AddComponentToEntity(new PlayerComponent(), playerId);
            ComponentManager.Get().AddComponentToEntity(new ScoreComponent()
            {
                Font = font
            }, playerId);

        }

    }
}
