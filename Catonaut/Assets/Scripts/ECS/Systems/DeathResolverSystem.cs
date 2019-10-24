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
                var damageZone = entity.DamageZone;

                if (health.CurrentHealth == 0 && player != null)
                {
                    Debug.Log("Player Death");
                    var transform = entity.Transform;
                    var spawnPoint = entity == World.ClientEntity ? World.SpawnPoints[0] : World.SpawnPoints[1];

                    transform.Position = spawnPoint.Position;
                    transform.Rotation = spawnPoint.Rotation;
                    health.CurrentHealth = health.MaxHealth; 
                }

                if (health.CurrentHealth == 0 && damageZone != null)
                {
                    Debug.Log("DamageZone Destroed");
                    Object.Destroy(damageZone.Body.gameObject);
                    World.DestroyEntity(entity);
                }
            }
        }
    }
}
