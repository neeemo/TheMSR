#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using Squared.Tiled;
using System.IO;

#endregion

namespace GameName3
{
 
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Map map;
        private Vector2 viewportPosition;

        public Game1()
            : base()
        {
        
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 640;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {

            this.IsMouseVisible = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            map = Map.Load(Path.Combine(Content.RootDirectory, "map.tmx"), Content);
            map.ObjectGroups["events"].Objects["player"].Texture = Content.Load<Texture2D>("katt");

            /*TODO:
             * Entities such as enemies and npcs can be loaded as objects into the mapfile, texture has to be loaded for them here
             * and the possibility to also create them as game classes here so that logic can be made.
             * New player and npc classes has to be made, to fit the new logic.
            */
           
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            
        }

        protected override void Update(GameTime gameTime)
        {


            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            KeyboardState keyState = Keyboard.GetState();
            float scrollx = 0, scrolly = 0;

            MouseState newState = Mouse.GetState();

            //TODO: put this logic inside player class
            if (keyState.IsKeyDown(Keys.Left))
                scrollx = -1;
            if (keyState.IsKeyDown(Keys.Right))
                scrollx = 1;
            if (keyState.IsKeyDown(Keys.Up))
                scrolly = 1;
            if (keyState.IsKeyDown(Keys.Down))
                scrolly = -1;


            scrollx += gamePadState.ThumbSticks.Left.X;
            scrolly += gamePadState.ThumbSticks.Left.Y;

            //movementspeed atm.
            float scrollSpeed = 8.0f;

            //Events is the object layer, player is the object.
            map.ObjectGroups["events"].Objects["player"].X += (int)(scrollx * scrollSpeed);
            map.ObjectGroups["events"].Objects["player"].Y -= (int)(scrolly * scrollSpeed);
            //map.ObjectGroups["events"].Objects["player"].Width = 100;

            //TODO: ViewportPosition should only scroll if you are moving towards the border of the screen,
            //also limit so that you cannot see outside of the map. use tilesize somehow.
            viewportPosition.X += scrollx * scrollSpeed;
            viewportPosition.Y -= scrolly * scrollSpeed;


            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            /*TODO: CollisionDetection can be made by looking so that the player object does not overlap the 'wall' layer.
             *Can also add so that the player moves faster when he overlaps the 'path' layer.
            */

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);

            //Player is part of the map and is drawn as an object on top.
            spriteBatch.Begin();
            map.Draw(spriteBatch, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), viewportPosition);
            spriteBatch.End();

        }
    }
}
