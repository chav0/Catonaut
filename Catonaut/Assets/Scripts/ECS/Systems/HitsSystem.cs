using Client;
using ECS.Physics;
using UnityEngine;

namespace ECS.Systems
{
    public class HitsSystem : SystemBase
    {
        private readonly GameSettings _gameSettings; 
    
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
                
                var overlaps = PhysicsUtils.OverlapSphere(projectile.Position, _gameSettings.ProjectileRadius, Layers.MovementMask);
                
                foreach (var entity in overlaps)
                {
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
            }
        }
    }
}
