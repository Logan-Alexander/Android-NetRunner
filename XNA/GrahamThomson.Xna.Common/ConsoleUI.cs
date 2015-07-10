using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using System.Collections.ObjectModel;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace GrahamThomson.Xna.Common
{
    /// <summary>
    /// Outputs information from the debugger to the screen.
    /// </summary>
    public class ConsoleUI : DrawableGameComponent
    {
        #region Properties

        public string ContentRootFolder { get; private set; }

        public Keys ToggleConsoleKey { get; set; }

        private List<string> _Messages = new List<string>();
        public ReadOnlyCollection<string> Messages
        {
            get { return _Messages.AsReadOnly(); }
        }

        public bool ShowConsole { get; set; }

        private float _Transparency;
        public float Transparency
        {
            get { return _Transparency; }
            set
            {
                if (value < 0 || value > 1)
                {
                    throw new ArgumentException();
                }
                _Transparency = value;
            }
        }

        private const int MaximumMessages = 10;

        private ContentManager _ContentManager;
        private IKeyboardManager _KeyboardManager;
        private IFrameCounter _FrameCounter;
        private IResourceMonitor _ResourceMonitor;

        private SpriteBatch _SpriteBatch;
        private SpriteFont _Font;
        private Texture2D _Texture;
        private SimpleListener _Listener;

        private float _Visibility;

        #endregion

        public ConsoleUI(Game game,
            string contentRootFolder = "Common",
            Keys toggleConsoleKey = Keys.Tab,
            float transparency = 0.75f,
            bool addDefaultFrameCounter = true,
            bool addDefaultResourceMonitor = true)
            : base(game)
        {
            ContentRootFolder = contentRootFolder;
            ToggleConsoleKey = toggleConsoleKey;
            Transparency = transparency;

            _Listener = new SimpleListener();
            _Listener.MessageAdded += new EventHandler<SimpleListener.MessageEventArgs>(_Listener_MessageAdded);
            Debug.Listeners.Add(_Listener);

            ShowConsole = false;

            if (addDefaultFrameCounter)
            {
                FrameCounter frameCounter = new FrameCounter(Game);
            }

            if (addDefaultResourceMonitor)
            {
                ResourceMonitor resourceMonitor = new ResourceMonitor(Game);
            }

            Game.Components.Add(this);
        }

        public override void Initialize()
        {
            base.Initialize();

            _KeyboardManager = (IKeyboardManager)Game.Services.GetService(typeof(IKeyboardManager));
            _FrameCounter = (IFrameCounter)Game.Services.GetService(typeof(IFrameCounter));
            _ResourceMonitor = (IResourceMonitor)Game.Services.GetService(typeof(IResourceMonitor));
        }

        private void AddMessage(string message)
        {
            _Messages.Add(string.Format("{0:HH:mm:ss:fff} - {1}", DateTime.Now, message));
            while (_Messages.Count > MaximumMessages)
            {
                _Messages.RemoveAt(0);
            }
        }

        #region Load / Unload

        protected override void LoadContent()
        {
            base.LoadContent();

            _SpriteBatch = new SpriteBatch(GraphicsDevice);

            _ContentManager = new ContentManager(Game.Services, ContentRootFolder);
            _Font = _ContentManager.Load<SpriteFont>("Fonts/Console");
            _Texture = _ContentManager.Load<Texture2D>("Textures/Console");
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();

            _SpriteBatch.Dispose();
            _ContentManager.Unload();
        }

        #endregion

        #region Update

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (_KeyboardManager != null)
            {
                HandleKeyboardInput();
            }

            if (ShowConsole && _Visibility < 1f)
            {
                _Visibility += (float)gameTime.ElapsedGameTime.TotalSeconds * 5f;
                if (_Visibility > 1f)
                {
                    _Visibility = 1f;
                }
            }
            else if (!ShowConsole && _Visibility > 0f)
            {
                _Visibility -= (float)gameTime.ElapsedGameTime.TotalSeconds * 5f;
                if (_Visibility < 0f)
                {
                    _Visibility = 0f;
                }
            }
        }

        private void HandleKeyboardInput()
        {
            if (_KeyboardManager.IsKeyPressed(ToggleConsoleKey))
            {
                ShowConsole = !ShowConsole;
            }
        }

        #endregion

        #region Draw

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (_Visibility > 0)
            {
                Color color = Color.White * _Visibility * _Transparency;
                
                _SpriteBatch.Begin();

                int height = (int)((_Font.LineSpacing * MaximumMessages) * _Visibility);
                int width = GraphicsDevice.Viewport.Width;

                _SpriteBatch.Draw(_Texture, new Rectangle(0, 0, width, height), new Rectangle(6, 0, 1, 8), color);
                _SpriteBatch.Draw(_Texture, new Rectangle(0, height, width, 4), new Rectangle(1, 0, 1, 8), color);

                float y = 0;
                if (_FrameCounter != null)
                {
                    string frameCounterText = string.Format("U: {0}, D: {1}", 
                        _FrameCounter.UpdatesPerSecond,
                        _FrameCounter.RendersPerSecond);
                    Vector2 frameCounterTextSize = _Font.MeasureString(frameCounterText);
                    _SpriteBatch.DrawString(_Font, frameCounterText, new Vector2(width - frameCounterTextSize.X, y), color);
                    y += _Font.LineSpacing;
                }

                if (_ResourceMonitor != null)
                {
                    string memoryText = string.Format("Mem: {0:N0} kB",
                        _ResourceMonitor.PrivateBytes / 1024);
                    Vector2 memoryTextSize = _Font.MeasureString(memoryText);
                    _SpriteBatch.DrawString(_Font, memoryText, new Vector2(width - memoryTextSize.X, y), color);
                    y += _Font.LineSpacing;
                }

                y = height - (_Font.LineSpacing * Messages.Count);
                foreach (string message in Messages)
                {
                    _SpriteBatch.DrawString(_Font, message, new Vector2(0, y), color);

                    y += _Font.LineSpacing;
                }

                _SpriteBatch.End();
            }
        }

        #endregion

        private void _Listener_MessageAdded(object sender, ConsoleUI.SimpleListener.MessageEventArgs e)
        {
            AddMessage(e.Message);
        }

        private class SimpleListener : TraceListener
        {
            public event EventHandler<MessageEventArgs> MessageAdded;
            protected void OnMessageAdded(MessageEventArgs e)
            {
                EventHandler<MessageEventArgs> temp = MessageAdded;
                if (temp != null)
                {
                    temp(this, e);
                }
            }

            public override void Write(string message)
            {
                WriteLine(message);
            }

            public override void WriteLine(string message)
            {
                OnMessageAdded(new MessageEventArgs(message));
            }

            public class MessageEventArgs : EventArgs
            {
                public string Message { get; private set; }

                public MessageEventArgs(string message)
                {
                    Message = message;
                }
            }
        }
    }
}
