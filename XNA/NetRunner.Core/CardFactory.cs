using NetRunner.Core.Conditions;
using NetRunner.Core.ContinuousEffects;
using NetRunner.Core.Selectors;
using NetRunner.Core.InstantEffects;
using NetRunner.Core.Triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetRunner.Core.Corporation;
using NetRunner.Core.Runner;

namespace NetRunner.Core
{
    public partial class CardFactory
    {
        public Card Create(string name)
        {
            switch (name)
            {
                case "Sure Gamble":
                    {
                        return CreateSureGamble();
                    }

                case "Neural EMP":
                    {
                        return CreateNeuralEMP();
                    }

                case "Precognition":
                    {
                        return CreatePrecognition();
                    }

                case "Chum":
                    {
                        return CreateChum();
                    }

                case "Neural Katana":
                    {
                        return CreateNeuralKatana();
                    }


                case "Wall of Thorns":
                    {
                        return CreateWallOfThorns();
                    }

                case "Akitaro Watanabe":
                    {
                        return CreateAkitaroWatanabe();
                    }

                case "Hedge Fund":
                    {
                        return CreateHedgeFund();
                    }

                case "Enigma":
                    {
                        return CreateEnigma();
                    }

                case "Wall of Static":
                    {
                        return CreateWallOfStatic();
                    }


                default:
                    throw new NotSupportedException();
            }
        }
    }
}
