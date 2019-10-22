namespace ECS.Systems
{
    public class ProjectileSystem : SystemBase
    {
        public override void Execute()
        {
            for (int i = 0; i < World.Projectiles.Count; i++)
            {
                var entity = World.Projectiles.EntityAt(i);
                var projectile = entity.Projectile;

                if (projectile != null)
                {
                    projectile.Position += projectile.Direction * projectile.SpeedPerTick;
                    
                    if (projectile.RemainingLifetime == World.Tick)
                    {
                        projectile.IsDead = true; 
                    }
                }
            }
        }
    }
}
