using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Runner
{
    public class EventCard : RunnerCard
    {
        private List<Effect> mEffects;

        public EventCard(int id, string title, int influence, RunnerFaction faction, int cost)
            : base(id, title, influence, faction, cost)
        {
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
            
            context.RemoveFromGrip(this);
            context.AddToHeap(this);

            foreach (Effect effect in mEffects)
            {
                effect.Execute(context);
            }
        }
    }
}
