using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Corporation
{
    public interface IRezableCard : IServerCard
    {
        int RezCost { get; }
    }
}
