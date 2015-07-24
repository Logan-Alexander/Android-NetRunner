using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Corporation
{
    /// <summary>
    /// A card that can be added to a server.
    /// </summary>
    public interface IServerCard
    {
        Server Server { get; }
    }
}
