using System.Collections.Generic;
using Client;
using Client.Scene;
using UnityEngine;

namespace ECS
{
    public class GameLogic
    {
        private const int PlayerCount = 2;
        private readonly SystemFactory _factory;
        private readonly GameSettings _gameSettings;
        private readonly UnityScene _scene;
        private SystemBase[] _systems;
        private World _world;
        
        public GameLogic(SystemFactory factory, GameSettings settings, UnityScene scene)
        {
            _factory = factory;
            _gameSettings = settings; 
            _scene = scene;
        }
        
        public void Init(World world, int tickRate)
        {
            _world = world;
            
            _systems = _factory.CreateSystems();
            for (int i = 0, count = _systems.Length; i < count; i++)
            {
                _systems[i].Init(world, tickRate);
            }
        }
        
        public void Simulate(IReadOnlyList<Input> inputs)
        {
            /*var inputCount = Mathf.Min(inputs.Count, _inputTable.Count);
            for (int i = 0; i < inputCount; i++)
            {
                _inputTable.SetAt(i, inputs[i]);
            }*/
            
            _world.Tick++;
            for (int i = 0, count = _systems.Length; i < count; i++)
            {
                _systems[i].Simulate();
            }
        }

        public World CreateNewWorld(int tickRate)
        {
            var world = new World();

            for (int i = 0; i < PlayerCount; i++)
            {
                var player = world.CreateEntity();
                player.AddPlayer(); 

                if (i == 0)
                {
                    _world.ClientEntity = player; 
                }
                
                var body = _scene.CreatePlayer();
                body.SetEntity(player);
            }

            return world; 
        }
    }
}
