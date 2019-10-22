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
                new ProjectileCleanUpSystem(), 

                new FlashlightSystem(_settings), 
                new KeysSystem(_settings),
                new InsertingKeysInCapsuleSystem(_settings),

                new WeaponSystem(_settings),
                new ProjectileSystem(),
                
                new ApplyInputToMovementSystem(_settings), 
                
                new DeathResolverSystem(),
            };
        }
    }
}