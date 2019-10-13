using Client.View;

namespace Client.ViewStates
{
    public abstract class BaseViewState
    {
        public ClientView Context; 
        
        public virtual void OnEnter()
        {
            
        }

        public virtual void OnExit()
        {
            
        }

        public abstract void PreModelUpdate();
        public abstract void PostModelUpdate();
        
        public void SetState(BaseViewState newState)
        {
            OnExit();
            newState.Context = Context;
            Context.CurrentState = newState;
            newState.OnEnter();
        }
    }
}
