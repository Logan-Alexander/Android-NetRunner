using NetRunner.Core.Corporation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.CardIdentifiers
{
    /// <summary>
    /// Allows a way for a player to refer to an asset or agenda by using the ID of
    /// the remote server where it is installed.
    /// </summary>
    [Serializable]
    public class AssetOrAgendaIdentifier : CardIdentifier
    {
        public int RemoteServerIndex { get; private set; }

        public AssetOrAgendaIdentifier(int remoteServerIndex)
        {
            if (remoteServerIndex < 0)
            {
                throw new ArgumentOutOfRangeException("remoteServerIndex");
            }

            RemoteServerIndex = remoteServerIndex;
        }

        public RemoteServer ResolveRemoteServer(GameContext context)
        {
            RemoteServer server;
            
            if (!TryResolveRemoteServer(context, out server))
            {
                throw new CardIdentifierResolutionException();
            }

            return server;
        }

        public override Card Resolve(GameContext context)
        {
            Card card;

            if (!TryResolve(context, out card))
            {
                throw new CardIdentifierResolutionException();
            }

            return card;
        }

        public bool TryResolveRemoteServer(GameContext context, out RemoteServer server)
        {
            if (RemoteServerIndex >= context.RemoteServers.Count)
            {
                server = null;
                return false;
            }

            server = context.RemoteServers[RemoteServerIndex];
            return true;
        }

        public override bool TryResolve(GameContext context, out Card card)
        {
            RemoteServer server;
            
            if (!TryResolveRemoteServer(context, out server))
            {
                card = null;
                return false;
            }

            if (server.AssetOrAgenda == null)
            {
                card = null;
                return false;
            }

            card = server.AssetOrAgenda;
            return true;
        }
    }
}
