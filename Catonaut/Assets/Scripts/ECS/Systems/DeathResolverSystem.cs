using Client;
using UnityEngine;

namespace ECS.Systems
{
    public class DeathResolverSystem : SystemBase
    {
        public override void Execute()
        {
            for (int i = 0; i < World.Health.Count; i++)
            {
                var entity = World.Health.EntityAt(i);
                var health = entity.Health;
                var player = entity.Player;
                var monster = entity.Monster; 
                var damageZone = entity.DamageZone;
                var inventory = entity.Inventory; 

                if (health.CurrentHealth == 0 && player != null)
                {
                    //Debug.Log("Player Death");
                    var transform = entity.Transform;
                    var spawnPoint = entity == World.ClientEntity ? World.SpawnPoints[0] : World.SpawnPoints[1];

                    for (int j = 0; j < inventory.Keys.Count; j++)
                    {
                        var key = inventory.Keys[j];
                        key.Key.Body.transform.position =
                            transform.Position + new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
                        key.Key.Body.gameObject.SetActive(true);
                        key.Key.HasOwner = false; 
                    }

                    inventory.Keys.Clear();
                    
                    transform.Position = spawnPoint.Position;
                    transform.Rotation = spawnPoint.Rotation;
                    health.CurrentHealth = health.MaxHealth; 
                }

                if (health.CurrentHealth == 0 && monster != null)
                {
                    //Debug.Log("Monster Death");
                    Object.Destroy(monster.Body.gameObject);
                    World.DestroyEntity(entity);
                }

                if (health.CurrentHealth == 0 && damageZone != null)
                {
                    //Debug.Log("DamageZone Destroed");
                    Object.Destroy(damageZone.Body.gameObject);
                    World.DestroyEntity(entity);
                }
            }
        }
    }
}
