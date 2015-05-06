using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.InstantEffects
{
    public class DamageTheRunner : InstantEffect
    {
        private DamageType mDamageType;
        private int mAmount;

        public DamageTheRunner(DamageType damageType, int amount)
        {
            mDamageType = damageType;
            mAmount = amount;
        }

        protected override void ExecuteInstantEffect(GameContext context)
        {
            switch (mDamageType)
            {
                case DamageType.Meat:
                case DamageType.Net:
                    // TODO: Damage the runner.
                    break;

                case DamageType.Brain:
                    // TODO: Give the runner brain damage.
                    break;

                default:
                    throw new NotSupportedException();
            }
        }
    }
}
