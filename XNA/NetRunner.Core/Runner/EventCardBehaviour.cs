using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Runner
{
    public class EventCardBehaviour : RunnerCardBehaviour
    {
        public int Cost { get; private set; }

        private List<Effect> mEffects;

        public EventCardBehaviour(Card card, int influence, RunnerFaction faction, int cost)
            : base(card, influence, faction)
        {
            Cost = cost;
            mEffects = new List<Effect>();
        }

        public void AddEffect(Effect effect)
        {
            mEffects.Add(effect);
        }

        public void Play(GameContext context)
        {
            if (context.RunnerCredits < Cost)
            {
                string message = string.Format("The runner does not have the {0} credits required to play \"{1}\".",
                    Cost, this);
                
                throw new IllegalActionException(message);
            }

            context.RunnerCredits -= Cost;
            
            context.Grip.Remove(Card);
            context.Heap.Add(Card);

            foreach (Effect effect in mEffects)
            {
                effect.Execute(context);
            }
        }
    }
}
