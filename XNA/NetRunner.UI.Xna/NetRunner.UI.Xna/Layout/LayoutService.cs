﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.UI.Xna.Layout
{
    public class LayoutService : GameComponent, ILayoutService
    {
        public CorporationLayout CorporationLayout { get; private set; }
        public RunnerLayout RunnerLayout { get; private set; }

        public LayoutService(Game game)
            : base(game)
        {
            game.Components.Add(this);
            game.Services.AddService(typeof(ILayoutService), this);
        }

        public override void Initialize()
        {
            base.Initialize();

            Game.Window.ClientSizeChanged += Window_ClientSizeChanged;
        }

        private void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            //Probably shouldn't do both of these, but it's fine for now and a check seems difficult.
            CorporationLayout = new CorporationLayout(Game.GraphicsDevice.Viewport.TitleSafeArea); 
            RunnerLayout = new RunnerLayout(Game.GraphicsDevice.Viewport.TitleSafeArea);
        }
    }
}
