using System.Collections.Generic;
using Client.Model;
using Client.Scene;
using ECS;
using UnityEngine;

namespace Client.ModelStates
{
    public class BattleModelState : BaseModelState
    {
        private GameLogic _gameLogic;
        private World _world;
        private float _startTime;
        private UnityScene _scene;
        private Input _input;
        
        public override int TickRate { get; protected set; } = 40;
        public override World World => _world;
        public override ModelStatus Status => ModelStatus.Battle; 

        public override void OnEnter()
        {
            _scene = Context.Scene;
            _gameLogic = new GameLogic(new SystemFactory(_scene, Context.Settings), Context.Settings, _scene);
            _world = _gameLogic.CreateNewWorld(TickRate);
            _gameLogic.Init(_world, TickRate);
            _startTime = Time.realtimeSinceStartup;
        }
        
        public override void OnExit()
        {
            _scene.Dispose();
            _world.Dispose();
        }
        
        public override void Update(double currentTime)
        {
            var currentTick = (int) ((currentTime - _startTime) * TickRate) + 1; 
            
            while (_world.Tick < currentTick)
            {
                _gameLogic.Simulate(_input);
                _input = null; 
            }
        }

        public override void AddGameInput(Input input)
        {
            _input = input;
        }
    }
}
