using Client;
using UnityEngine;

namespace ECS.Systems
{
    public class MonsterAttackSystem : SystemBase
    {
        private readonly GameSettings _gameSettings; 
    
        public MonsterAttackSystem(GameSettings settings)
        {
            _gameSettings = settings;
        }
        
        public override void Execute()
        {
            for (int i = 0; i < World.Monsters.Count; i++)
            {
                var monsterEntity = World.Monsters.EntityAt(i);
                var monster = monsterEntity.Monster; 

                uint targetId = 0;
                var minDistance = float.MaxValue; 

                for (int j = 0; j < World.Players.Count; j++)
                {
                    var playerTransform = World.Players.EntityAt(j).Transform;
                    var distance = (playerTransform.Position - monster.Body.transform.position).magnitude;
                    
                    if (distance < _gameSettings.FollowMonsterRadius && distance < minDistance)
                    {
                        minDistance = distance;
                        targetId = World.Players.EntityAt(j).Id; 
                    }
                }

                monster.TargetId = targetId;

                if (targetId != 0)
                {
                    var targetEntity = World[targetId]; 
                    var distance = (targetEntity.Transform.Position - monster.Body.transform.position).magnitude;

                    if (distance < _gameSettings.AttackMonsterRadius && monster.NextAttackTick < World.Tick)
                    {
                        monster.NextAttackTick = World.Tick + _gameSettings.AttackIntervalTicks;
                        var projectileEntity = World.CreateEntity();
                        var projectile = projectileEntity.AddProjectile();
                        projectile.Position = monster.Body.transform.position + monster.Body.transform.rotation * new Vector3(0f, 0.25f, 0.25f); 
                        projectile.Direction =  monster.Body.transform.rotation * Vector3.forward;
                        projectile.RemainingLifetime = (int) (_gameSettings.MonsterProjectileLifeTimeSeconds * TickRate) + World.Tick;
                        projectile.SpeedPerTick =  (_gameSettings.MonsterProjectileRange / (int) (_gameSettings.MonsterProjectileLifeTimeSeconds * TickRate));
                        projectile.Damage = _gameSettings.MonsterDamage; 
                        projectile.Owner = monsterEntity; 
                    } 
                    
                    monster.Attack = distance < _gameSettings.AttackMonsterRadius; 
                }
                else
                {
                    monster.Attack = false; 
                }
            }
        }
    }
}
