using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.UI.Xna.Components
{
    /// <summary>
    /// A camera has the responsibility of defining the view matrix (where the camera is and
    /// what it's looking at) and the perspective matrix (how to project the 3D world onto the
    /// 2D viewport).
    /// </summary>
    public interface ICamera
    {
        Vector3 Position { get; }
        Vector3 LookAt { get; }
        Vector3 Up { get; }

        Matrix ViewMatrix { get; }
        Matrix PerspectiveMatrix { get; }
    }

    /// <summary>
    /// This basic camera just lets up specify values. It's pretty crap.
    /// 
    /// A better implementation would let us do things like tell the camera to focus on
    /// specific things like: camera.ZoomToHeadQuarters().
    /// 
    /// It would also handle interpolating (animating) the position of the camera so that
    /// we don't jump around and get seasick.
    /// </summary>
    public class Camera : GameComponent, ICamera
    {
        public Vector3 Position { get; private set; }
        public Vector3 LookAt { get; private set; }
        public Vector3 Up { get; private set; }

        public Matrix ViewMatrix { get; private set; }
        public Matrix PerspectiveMatrix { get; private set; }

        public Camera(Game game)
            : base(game)
        {
            Game.Components.Add(this);
            Game.Services.AddService(typeof(ICamera), this);
        }

        public override void Initialize()
        {
            base.Initialize();

            Set(new Vector3(0, 0, 1), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
            ResetPerspective();
        }

        public void Set(Vector3 position, Vector3 lookAt, Vector3 up)
        {
            Position = position;
            LookAt = lookAt;
            Up = up;

            ViewMatrix = Matrix.CreateLookAt(Position, LookAt, Up);
        }

        public void ResetPerspective()
        {
            PerspectiveMatrix = Matrix.CreatePerspectiveFieldOfView(
                (float)(Math.PI * 0.5),
                Game.GraphicsDevice.Viewport.AspectRatio,
                0.01f,
                100f);
        }
    }
}
