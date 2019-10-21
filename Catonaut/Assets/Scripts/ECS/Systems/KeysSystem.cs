using Client;
using ECS.Physics;
using UnityEngine;

namespace ECS.Systems
{
    public class KeysSystem : SystemBase
    {
        private readonly GameSettings _gameSettings; 
    
        public KeysSystem(GameSettings settings)
        {
            _gameSettings = settings;
        }
        
        public override void Execute()
        {
            for (int i = 0; i < World.Keys.Count; i++)
            {
                var key = World.Keys[i];
                var keyBody = key.Body;

                if (key.HasOwner) 
                    continue;
                
                var overlaps = PhysicsUtils.OverlapSphere(keyBody.transform.position, _gameSettings.KeyRadius, Layers.MovementMask);

                foreach (var entity in overlaps)
                {
                    var player = entity.Player;
                    var inventory = entity.Inventory; 
                        
                    if (player == null || inventory == null) 
                        continue;
                        
                    key.HasOwner = true; 
                    key.Body.gameObject.SetActive(false);
                    inventory.Keys.Add(World[key.EntityId]);
                }
            }
        }
    }
}
