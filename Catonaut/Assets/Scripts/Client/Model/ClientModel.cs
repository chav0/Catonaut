using Client.ModelStates;
using Client.Scene;
using ECS;
using UnityEngine;

namespace Client.Model
{
    public class ClientModel
    {
        public readonly GameSettings Settings;
        public readonly UnityScene Scene;
        private readonly Resources Resources; 
        
        public BaseModelState CurrentState { get; set; }
        public ModelStatus Status => CurrentState.Status; 
        public World World => CurrentState.World;
        
        public ClientModel(GameSettings gameSettings, UnityScene scene, Resources resources)
        {
            Settings = gameSettings;
            Scene = scene;
            Resources = resources; 
            CurrentState = new InitModelState();
            CurrentState.Context = this; 
            CurrentState.OnEnter();
        }

        public void Update(double currentTime)
        {
            CurrentState.Update(currentTime);
        }

        public void FindMatch()
        {
            CurrentState.SetState(new BattleModelState());
        }

        public void AddGameInput(Input input)
        {
            CurrentState.AddGameInput(input);
        }
    }

    public enum ModelStatus
    {
        Init,
        Battle,
    }
}
