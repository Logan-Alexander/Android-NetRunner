using NetRunner.Core.ContinuousEffects;
using NetRunner.Core.Corporation;
using NetRunner.Core.Runner;
using NetRunner.Core.Triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core
{
    public class GameContext
    {
        public event EventHandler<IceEventArgs> IceEncounterStarted;
        public void OnIceEncounterStarted(IceEventArgs e)
        {
            EventHandler<IceEventArgs> temp = IceEncounterStarted;
            if (temp != null)
            {
                temp(this, e);
            }
        }
        
        public event EventHandler<IceEventArgs> IceEncounterEnded;
        public void OnIceEncounterEnded(IceEventArgs e)
        {
            EventHandler<IceEventArgs> temp = IceEncounterEnded;
            if (temp != null)
            {
                temp(this, e);
            }
        }

        public int RunnerCredits { get; set; }
        public Run CurrentRun { get; set; }

        public List<ContinuousEffect> ActiveContinuousEffects { get; private set; }

        public GameContext()
        {
            ActiveContinuousEffects = new List<ContinuousEffect>();
        }

        public virtual void AddToGrip(RunnerCard card)
        {
        }

        public virtual void RemoveFromGrip(RunnerCard card)
        {
        }

        public virtual void AddToStack(RunnerCard card)
        {
        }

        public virtual void RemoveFromStack(RunnerCard card)
        {
        }

        public virtual void AddToHeap(RunnerCard card)
        {
        }

        public virtual void RemoveFromHeap(RunnerCard card)
        {
        }

        public bool CardExists(object cardIdentifier)
        {
            // TODO
            return true;
        }

        public void IdentifityCard(object cardIdentifier, string cardName)
        {
            //TODO: Attach the relevant behaviour to the identified card.
            //Card card = CardIdentifier.FindCard(context);
            //card.Behaviour = CardBehaviourFactory.Create(CardName);
        }
    }
}
