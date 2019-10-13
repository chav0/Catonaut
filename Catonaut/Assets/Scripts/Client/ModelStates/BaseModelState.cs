using Client.Model;
using ECS;
using UnityEngine;

namespace Client.ModelStates
{
    public abstract class BaseModelState
    {
        public ClientModel Context; 
        public virtual World World { get; protected set; }
        public virtual int TickRate { get; protected set; }
        public virtual ModelStatus Status { get; protected set; }
        
        public virtual void AddGameInput(Input input)
        {
            Debug.Log("Not implemented");
        }
        
        public virtual void OnEnter()
        {
            
        }

        public virtual void OnExit()
        {
            
        }

        public abstract void Update(double currentTime);
        
        public void SetState(BaseModelState newState)
        {
            OnExit();
            newState.Context = Context;
            Context.CurrentState = newState;
            newState.OnEnter();
        }
    }
}
