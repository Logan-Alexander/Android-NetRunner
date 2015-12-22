using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NetRunner.UI.Xna.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.UI.Xna.Components
{
    public class Background : DrawableGameComponent
    {
        private readonly float quarterTurn = (float)(Math.PI / 2);

        private ILayoutService _LayoutService;

        private SpriteBatch _SpriteBatch;
        private SpriteFont _Font;
        private Texture2D _CorporationBackground;
        private Texture2D _CorporationBoxTopLeftCorner;
        private Texture2D _CorporationBoxTopEdge;
        private Texture2D _CorporationBoxLeftEdge;
        private Texture2D _CorporationBoxMiddle;

        private Rectangle _Bounds;

        public PlayerType Player { get; set; }

        public enum PlayerType
        {
            Corporation,
            Runner
        }

        public Background(Game game)
            : base(game)
        {
            Game.Components.Add(this);
        }

        public override void Initialize()
        {
            base.Initialize();

            _LayoutService = (ILayoutService)Game.Services.GetService(typeof(ILayoutService));

            Game.Window.ClientSizeChanged += Window_ClientSizeChanged;
        }

        private void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            if (_CorporationBackground != null)
            {
                _Bounds = DetermineBounds();
            }
        }

        private Rectangle DetermineBounds()
        {
            float textureAspectRatio = (float)_CorporationBackground.Width / (float)_CorporationBackground.Height;
            float viewportAspectRatio = GraphicsDevice.Viewport.AspectRatio;

            int width;
            int height;
            if (textureAspectRatio > viewportAspectRatio)
            {
                height = GraphicsDevice.Viewport.Height;
                width = (int)(height * textureAspectRatio);
            }
            else
            {
                width = GraphicsDevice.Viewport.Width;
                height = (int)(width / textureAspectRatio);
            }

            return new Rectangle(0, 0, width, height);
        }

        protected override void LoadContent()
        {
            _SpriteBatch = new SpriteBatch(GraphicsDevice);

            _Font = Game.Content.Load<SpriteFont>("Fonts/SpriteFont1");

            _CorporationBackground = Game.Content.Load<Texture2D>("Textures/CorpMat");
            _CorporationBoxTopLeftCorner = Game.Content.Load<Texture2D>("Textures/Box-C-TopLeft");
            _CorporationBoxTopEdge = Game.Content.Load<Texture2D>("Textures/Box-E-Top");
            _CorporationBoxLeftEdge = Game.Content.Load<Texture2D>("Textures/Box-E-Left");
            _CorporationBoxMiddle = Game.Content.Load<Texture2D>("Textures/Box-M");

            _Bounds = DetermineBounds();
        }

        public override void Draw(GameTime gameTime)
        {
            Color corpBlue = new Color(45, 130, 255);

            GraphicsDevice.Clear(Color.Black);

            _SpriteBatch.Begin();

            switch (Player)
            {
                case PlayerType.Corporation:
                    _SpriteBatch.Draw(_CorporationBackground, _Bounds, Color.White);
                    DrawBox(_LayoutService.CorporationLayout.IceArea, "ICE", corpBlue);
                    DrawBox(_LayoutService.CorporationLayout.ScoreArea, "SCORE AREA", corpBlue);
                    DrawBox(_LayoutService.CorporationLayout.CreditsArea, "CREDITS", corpBlue);
                    DrawBox(_LayoutService.CorporationLayout.ArchivesArea, "ARCHIVES", corpBlue);
                    DrawBox(_LayoutService.CorporationLayout.ResearchAndDevelopmentArea, "R&D", corpBlue);
                    DrawBox(_LayoutService.CorporationLayout.HeadQuartersArea, "HEADQUARTERS", corpBlue);
                    DrawBox(_LayoutService.CorporationLayout.RemoteServersArea, "REMOTE SERVERS", corpBlue);
                    break;

                case PlayerType.Runner:
                    _SpriteBatch.Draw(_CorporationBackground, _Bounds, Color.White);
                    DrawBox(_LayoutService.RunnerLayout.ConsoleArea, "CONSOLE", Color.Red);
                    DrawBox(_LayoutService.RunnerLayout.CreditsArea, "CREDITS", Color.Red);
                    DrawBox(_LayoutService.RunnerLayout.HardwareArea, "HARDWARE", Color.Red);
                    DrawBox(_LayoutService.RunnerLayout.HeapArea, "HEAP", Color.Red);
                    DrawBox(_LayoutService.RunnerLayout.IdentityArea, "ID", Color.Red);
                    DrawBox(_LayoutService.RunnerLayout.ProgramArea, "PROGRAMS", Color.Red);
                    DrawBox(_LayoutService.RunnerLayout.ResourcesArea, "RESOURCES", Color.Red);
                    DrawBox(_LayoutService.RunnerLayout.ScoreArea, "SCORE AREA", Color.Red);
                    DrawBox(_LayoutService.RunnerLayout.StackArea, "STACK", Color.Red);
                    break;

                default:
                    throw new NotSupportedException();
            }

            _SpriteBatch.End();
        }
        
        private void DrawBox(Rectangle rectangle, string text, Color color)
        {
            float cornerScale = _LayoutService.CorporationLayout.CardSize.X / 512;

            int cornerWidth = (int)(_CorporationBoxTopLeftCorner.Width * cornerScale);
            int cornerHeight = (int)(_CorporationBoxTopLeftCorner.Height * cornerScale);

            Rectangle topLeft = new Rectangle(rectangle.Left, rectangle.Top, cornerWidth, cornerHeight);
            Rectangle topRight = new Rectangle(rectangle.Right - cornerWidth, rectangle.Top, cornerWidth, cornerHeight);
            Rectangle bottomLeft = new Rectangle(rectangle.Left, rectangle.Bottom - cornerHeight, cornerWidth, cornerHeight);
            Rectangle bottomRight = new Rectangle(rectangle.Right - cornerWidth, rectangle.Bottom - cornerHeight, cornerWidth, cornerHeight);

            _SpriteBatch.Draw(_CorporationBoxTopLeftCorner, topLeft, null, color, 0, Vector2.Zero, SpriteEffects.None, 0);
            _SpriteBatch.Draw(_CorporationBoxTopLeftCorner, topRight, null, color, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
            _SpriteBatch.Draw(_CorporationBoxTopLeftCorner, bottomLeft, null, color, 0, Vector2.Zero, SpriteEffects.FlipVertically, 0);
            _SpriteBatch.Draw(_CorporationBoxTopLeftCorner, bottomRight, null, color, 0, Vector2.Zero, SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically, 0);

            Rectangle top = new Rectangle(rectangle.Left + cornerWidth, rectangle.Top, rectangle.Width - (2 * cornerWidth), cornerHeight);
            Rectangle bottom = new Rectangle(rectangle.Left + cornerWidth, rectangle.Bottom - cornerHeight, rectangle.Width - (2 * cornerWidth), cornerHeight);
            Rectangle left = new Rectangle(rectangle.Left, rectangle.Top + cornerHeight, cornerWidth, rectangle.Height - (2 * cornerHeight));
            Rectangle right = new Rectangle(rectangle.Right - cornerWidth, rectangle.Top + cornerHeight, cornerWidth, rectangle.Height - (2 * cornerHeight));

            _SpriteBatch.Draw(_CorporationBoxTopEdge, top, null, color, 0, Vector2.Zero, SpriteEffects.None, 0);
            _SpriteBatch.Draw(_CorporationBoxTopEdge, bottom, null, color, 0, Vector2.Zero, SpriteEffects.FlipVertically, 0);
            _SpriteBatch.Draw(_CorporationBoxLeftEdge, left, null, color, 0, Vector2.Zero, SpriteEffects.None, 0);
            _SpriteBatch.Draw(_CorporationBoxLeftEdge, right, null, color, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);

            Rectangle middle = new Rectangle(
                rectangle.Left + cornerWidth,
                rectangle.Top + cornerHeight,
                rectangle.Width - (2 * cornerWidth),
                rectangle.Height - (2 * cornerHeight));

            _SpriteBatch.Draw(_CorporationBoxMiddle, middle, null, color, 0, Vector2.Zero, SpriteEffects.None, 0);

            float textScale = _LayoutService.CorporationLayout.CardSize.Y / (25 * _Font.MeasureString("X").X);

            Vector2 textSize = _Font.MeasureString(text);
            
            Vector2 textPosition = new Vector2(
                rectangle.Left + cornerWidth,
                rectangle.Top + cornerHeight + (textSize.X * textScale));

            _SpriteBatch.DrawString(_Font, text, textPosition, Color.White, 3 * quarterTurn, Vector2.Zero, textScale, SpriteEffects.None, 0);
        }
    }
}
