using Client;
using Client.Objects;
using ECS.Physics;
using UnityEngine;

namespace ECS.Systems
{
    public class HitsSystem : SystemBase
    {
        private readonly GameSettings _gameSettings; 
        private readonly RaycastHit[] _results = new RaycastHit[50];
    
        public HitsSystem(GameSettings settings)
        {
            _gameSettings = settings;
        }
        
        public override void Execute()
        {
            var projectileCount = World.Projectiles.Count; 
            for (int i = 0; i < projectileCount; i++)
            {
                var projectile = World.Projectiles[i]; 
                
                if(projectile.IsDead)
                    continue;
                
                var count = UnityEngine.Physics.RaycastNonAlloc(projectile.Position, projectile.Direction, _results,
                    projectile.SpeedPerTick, Layers.GeometryMask | Layers.MovementMask);
            
                if (count == 0)
                    return;
                
                foreach (var result in _results)
                {
                    var collider = _results[i].collider;
                    var entityRef = collider.gameObject.GetComponent<EntityRefObject>();
                    
                    if(entityRef != null)
                    {
                        var entity = entityRef.Entity; 
                        var player = entity.Player;

                        if (player != null && entity != projectile.Owner)
                        {
                            var health = entity.Health;
                            health.CurrentHealth -= _gameSettings.ProjectileDamage;

                            if (health.CurrentHealth < 0)
                                health.CurrentHealth = 0;

                            projectile.IsDead = true;
                        }
                    }
                    else
                    {
                        projectile.IsDead = true;
                    }
                }
            }
        }
    }
}
