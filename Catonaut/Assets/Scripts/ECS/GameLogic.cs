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
        
        public void Simulate(Input input)
        {
            _world.ClientEntity.Input = input;

            if (input != null)
            {
                
            }
            
            _world.Tick++;
            for (int i = 0, count = _systems.Length; i < count; i++)
            {
                _systems[i].Simulate();
            }
        }

        public World CreateNewWorld(int tickRate)
        {
            var world = new World();

            var map = _scene.CreateMap(); 
            
            for (int i = 0; i < PlayerCount; i++)
            {
                var playerEntity = world.CreateEntity();
                
                var player = playerEntity.AddPlayer();
                
                var transform = playerEntity.AddTransform();
                transform.Position = map.SpawnZones[i].position;
                transform.Rotation = map.SpawnZones[i].eulerAngles;

                playerEntity.AddInput(); 

                if (i == 0)
                {
                    world.ClientEntity = playerEntity; 
                }
                
                var body = _scene.CreatePlayer();
                body.SetEntity(playerEntity);
                player.PlayerObject = body; 
            }

            return world; 
        }
    }
}
