using NetRunner.Core.GameFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetRunner.Core.Actions
{
    /// <summary>
    /// Defines an action that can be taken by the player.
    /// </summary>
    public abstract class ActionBase
    {
        public Guid ID { get; protected set; }
        public bool DeferExecution { get; protected set; }

        public ActionBase()
        {
            ID = Guid.NewGuid();
            DeferExecution = false;
        }

        public bool IsValidForCorporation(GameContext context, Flow flow)
        {
            return IsContextValidForCorporation(context) && IsFlowValid(flow);
        }

        public bool IsValidForRunner(GameContext context, Flow flow)
        {
            return IsContextValidForRunner(context) && IsFlowValid(flow);
        }

        public bool IsValidForServer(GameContext context, Flow flow)
        {
            return IsContextValidForServer(context) && IsFlowValid(flow);
        }

        protected virtual bool IsContextValidForCorporation(GameContext context)
        {
            return true;
        }

        protected virtual bool IsContextValidForRunner(GameContext context)
        {
            return true;
        }

        protected virtual bool IsContextValidForServer(GameContext context)
        {
            return true;
        }

        protected virtual bool IsFlowValid(Flow flow)
        {
            return true;
        }

        public virtual void ApplyToCorporation(GameContext context, Flow flow)
        {
            ApplyToAll(context, flow);
        }

        public virtual void ApplyToRunner(GameContext context, Flow flow)
        {
            ApplyToAll(context, flow);
        }

        public virtual void ApplyToServer(GameContext context, Flow flow)
        {
            ApplyToAll(context, flow);
        }

        protected virtual void ApplyToAll(GameContext context, Flow flow)
        {

        }

        public virtual void AddInformationForCorporation(GameContext context, Flow flow)
        {
        }

        public virtual void AddInformationForRunner(GameContext context, Flow flow)
        {
        }

        public bool HasSameID(ActionBase otherAction)
        {
            return ID == otherAction.ID;
        }

        public virtual ActionBase Clone()
        {
            ActionBase clone = CreateInstanceForClone();
            clone.ID = this.ID;
            clone.DeferExecution = this.DeferExecution;
            return clone;
        }

        protected abstract ActionBase CreateInstanceForClone();
    }
}
