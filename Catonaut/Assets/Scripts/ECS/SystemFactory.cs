using Client;
using Client.Scene;
using ECS.Systems;

namespace ECS
{
    public class SystemFactory
    {
        private readonly UnityScene _scene;
        private readonly GameSettings _settings;

        public SystemFactory(UnityScene scene, GameSettings gameSettings)
        {
            _scene = scene;
            _settings = gameSettings; 
        }
        
        public SystemBase[] CreateSystems()
        {
            return new SystemBase[]
            {
                new ApplyInputToMovementSystem(_settings), 
                new FlashlightSystem(_settings), 
            };
        }
    }
}