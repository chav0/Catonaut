﻿using Client;
using Client.Objects;
using ECS.Physics;
using UnityEngine;

namespace ECS.Systems
{
    public class HitsSystem : SystemBase
    {
        private readonly GameSettings _gameSettings; 
        private readonly RaycastHit[] _results = new RaycastHit[1000];
    
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
                    projectile.SpeedPerTick, Layers.GeometryMask | Layers.MovementMask | Layers.MonstersMask);
               
                if (count == 0)
                    return;

                Debug.Log(count);
                foreach (var result in _results)
                {
                    var collider = result.collider;
                    
                    if(collider == null)
                        continue;
                    
                    var entityRef = collider.gameObject.GetComponent<EntityRefObject>();
                    
                    Debug.Log("entityRef : " + (entityRef != null));
                    
                    if(entityRef != null)
                    {
                        var entity = entityRef.Entity; 
                        var player = entity.Player;
                        var monster = entity.Monster; 
                        
                        Debug.Log("monster : " + (monster != null));

                        if ((player != null || monster != null && projectile.Owner.Player != null) && entity != projectile.Owner)
                        {
                            var health = entity.Health;
                            var coef = 1f;

                            if (monster != null && monster.HaveShield)
                            {
                                coef = monster.DamageCoef; 
                            }
                            
                            health.CurrentHealth -= (int) (projectile.Damage * coef);

                            if (health.CurrentHealth < 0)
                                health.CurrentHealth = 0;

                            Debug.Log("Add damage " + _gameSettings.ProjectileDamage);
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
