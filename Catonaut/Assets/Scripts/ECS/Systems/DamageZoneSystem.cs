using Client;
using ECS.Physics;

namespace ECS.Systems
{
    public class DamageZoneSystem : SystemBase
    {
        private readonly GameSettings _gameSettings; 
    
        public DamageZoneSystem(GameSettings settings)
        {
            _gameSettings = settings;
        }
        
        public override void Execute()
        {
            var damageZoneCount = World.DamageZones.Count;
            for (int i = 0; i < damageZoneCount; i++)
            {
                var entity = World.DamageZones.EntityAt(i);
                var damageZone = entity.DamageZone;
                var health = entity.Health;
                var overlaps = PhysicsUtils.OverlapSphere(damageZone.Position, damageZone.Radius, Layers.MovementMask);

                foreach (var overlap in overlaps)
                {
                    var player = overlap.Player;
                    if (player == null) 
                        continue;
                    
                    var playerHealth = overlap.Health;
                    if (playerHealth == null) 
                        continue;
                    
                    if (World.Tick % _gameSettings.DamageTicks == 0)
                    {
                        playerHealth.CurrentHealth -= damageZone.Damage;
                        if (playerHealth.CurrentHealth < 0) 
                            playerHealth.CurrentHealth = 0;
                    }
                }
            }
        }
    }
}
