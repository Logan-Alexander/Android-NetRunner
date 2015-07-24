using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Corporation
{
    /// <summary>
    /// A card that can be rezzed.
    /// </summary>
    public interface IRezableCard : IServerCard
    {
        int RezCost { get; }
    }
}
