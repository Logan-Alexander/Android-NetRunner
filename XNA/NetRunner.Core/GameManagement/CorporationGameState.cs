using NetRunner.Core.GameFlow;
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
    public class CorporationGameState
    {
        public string SerializedGameContext { get; private set; }
        public SerializedFlow SerializedFlow { get; private set; }

        public CorporationGameState(GameContext gameContext, Flow flow)
        {
            GameContextSerializer gameContextSerializer = new GameContextSerializer();
            SerializedGameContext = gameContextSerializer.Serialize(gameContext, PlayerType.Corporation);

            SerializedFlow = flow.Serialize();
        }
    }
}
