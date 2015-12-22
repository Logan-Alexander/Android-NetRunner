using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.UI.Xna.Layout
{
    public interface ILayoutService
    {
        CorporationLayout CorporationLayout { get; }
        RunnerLayout RunnerLayout { get; }
    }
}
