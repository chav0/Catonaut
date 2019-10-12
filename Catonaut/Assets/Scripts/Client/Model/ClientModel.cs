using Client.ModelStates;
using Client.Scene;
using UnityEngine;

namespace Client.Model
{
    public class ClientModel
    {
        public readonly GameSettings Settings;
        public readonly UnityScene Scene;
        private readonly Resources Resources; 
        
        public BaseModelState CurrentState { get; set; }
        
        public ClientModel(GameSettings gameSettings, UnityScene scene, Resources resources)
        {
            Settings = gameSettings;
            Scene = scene;
            Resources = resources; 
            CurrentState = new BattleModelState();
            CurrentState.Context = this; 
            CurrentState.OnEnter();
        }

        public void Update(double currentTime)
        {
            CurrentState.Update(currentTime);
        }
    }
}
