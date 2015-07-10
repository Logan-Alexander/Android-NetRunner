﻿using NetRunner.Core.GameFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.GameManagement
{
    /// <summary>
    /// Provides a complete and transportable description of the game from the
    /// Corporation's point of view.
    /// </summary>
    [Serializable]
    public class CorporationGameState
    {
        public object SerlaizedState { get; private set; }

        public CorporationGameState(GameContext gameContext, Flow flow)
        {
            // TODO: Create state from context.
            SerlaizedState = flow.Serialize();
        }
    }
}
