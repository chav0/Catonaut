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
                
                for (int j = 0; j < World.Players.Count; j++)
                {
                    var entity = World.Players.EntityAt(j); 
                    var player = entity.Player;
                    var inventory = entity.Inventory;
                    var transform = entity.Transform; 
                        
                    if (player == null || inventory == null || (transform.Position - keyBody.transform.position).magnitude > _gameSettings.KeyRadius) 
                        continue;
                        
                    key.HasOwner = true; 
                    key.Body.gameObject.SetActive(false);
                    inventory.Keys.Add(World[key.EntityId]);
                }
            }
        }
    }
}
