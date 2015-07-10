using Microsoft.Xna.Framework;
using NetRunner.Core.Actions;
using NetRunner.Core.GameManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.UI.Xna.Components
{
    public interface ICorporationGameService : IGameComponent
    {
        CorporationGame CorporationGame { get; }

        void TakeCorporationAction(ActionBase action);
    }
}
