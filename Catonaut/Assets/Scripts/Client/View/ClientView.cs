using Client.Model;
using Client.Scene;
using Client.ViewStates;
using UnityEngine;

namespace Client.View
{
    public class ClientView 
    {
        public readonly Resources Resources;
        public readonly ClientModel AppModel;
        public readonly UnityScene Scene;
        public readonly Camera Camera;
        public readonly Screens Screens;
        
        public BaseViewState CurrentState { get; set; }
        
        public ClientView(Resources resources, ClientModel appModel, UnityScene scene, Screens screens, Camera camera)
        {
            Resources = resources;
            AppModel = appModel;
            Scene = scene;
            Camera = camera;
            Screens = screens; 
            
            CurrentState = new LobbyViewState();
            CurrentState.Context = this; 
            CurrentState.OnEnter();
        }

        public void PreModelUpdate()
        {
            CurrentState.PreModelUpdate();
        }

        public void PostModelUpdate()
        {
            CurrentState.PostModelUpdate();
        }
    }
}
