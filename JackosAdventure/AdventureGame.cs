﻿using JackosAdventure.UI.Components;
using Microsoft.Xna.Framework;
using System;

namespace JackosAdventure
{
    internal class AdventureGame : Game
    {
        private readonly GraphicsDeviceManager grapicDeviceManager;

        public AdventureGame()
        {
            grapicDeviceManager = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1280,
                PreferredBackBufferHeight = 720,
                IsFullScreen = false,
                SynchronizeWithVerticalRetrace = true
            };

            Window.Title = "Jacko's Adventure";
            Content.RootDirectory = "Assets";
            

            Disposed += (s, e) => OnDisposed();
        }

        protected override void Initialize()
        {
            var screenComponent = new ScreenComponent(this);

            Components.Add(screenComponent);
            base.Initialize();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            base.Draw(gameTime);
        }

        public void OnDisposed()
        {
            ((IDisposable)grapicDeviceManager).Dispose();
        }
    }
}
