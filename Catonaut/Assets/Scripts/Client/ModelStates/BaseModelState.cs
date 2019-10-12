using Client.Model;

namespace Client.ModelStates
{
    public abstract class BaseModelState
    {
        public ClientModel Context; 
        
        public virtual void OnEnter()
        {
            
        }

        public virtual void OnExit()
        {
            
        }

        public abstract void Update(double currentTime);
        
        protected void SetState(BaseModelState newState)
        {
            OnExit();
            newState.Context = Context;
            Context.CurrentState = newState;
            newState.OnEnter();
        }
    }
}
