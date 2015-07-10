using Microsoft.Xna.Framework;
using NetRunner.Core.Actions;
using NetRunner.Core.GameManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.UI.Xna.Components
{
    public interface IRunnerGameService : IGameComponent
    {
        RunnerGame RunnerGame { get; }
        void TakeRunnerAction(ActionBase action);
    }
}
