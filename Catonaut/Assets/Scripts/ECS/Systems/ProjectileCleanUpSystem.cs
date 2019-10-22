namespace ECS.Systems
{
    public class ProjectileCleanUpSystem : SystemBase
    {
        public override void Execute()
        {
            for (int i = World.Projectiles.Count - 1; i >= 0 ; i--)
            {
                var entity = World.Projectiles.EntityAt(i);
                var projectile = entity.Projectile;

                if (projectile == null) 
                    continue;
                
                if (projectile.IsDead)
                {
                    World.DestroyEntity(entity);
                }
            }
        }
    }
}
