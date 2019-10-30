using UnityEngine;

namespace Client.ViewStates
{
    public class LobbyViewState : BaseViewState
    {
        public override void OnEnter()
        {
            var lobby = Context.Screens.Lobby; 
            lobby.Start.onClick.AddListener(() => SetState(new LoadingViewState()));
        }
        
        public override void PreModelUpdate()
        {

        }

        public override void PostModelUpdate()
        {

        }
    }
}
