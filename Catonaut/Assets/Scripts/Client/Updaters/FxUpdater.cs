using System;
using System.Collections.Generic;
using Client.Objects;
using Client.Scene;
using ECS;
using ECS.Components;

namespace Client.Updaters
{
    public class FxUpdater : IUpdateImplementer<FxObject>
    {
        private readonly ComponentPool<FxObject> _pool;
        private readonly CollectionUpdater<FxObject> _collectionUpdater;
        private World _world; 

        public FxUpdater(Resources resources)
        {
            _pool = new ComponentPool<FxObject>(resources.Fx, 10, true);
            _collectionUpdater = new CollectionUpdater<FxObject>();
        }
        
        public void Update(World world)
        {
            _world = world;
            var selfWeapon = world.ClientEntity.Weapon;
            world.ClientEntity.Player.PlayerObject.SetLaser(selfWeapon.WeaponState == WeaponState.Ready); 
            
            _collectionUpdater.ProcessUpdate(this);
        }

        public void Dispose()
        {
            _pool.Dispose();
        }


        public IEnumerable<uint> GetNextFilteredItem()
        {
            var count = _world.Projectiles.Count;
            for (int i = 0; i < count; i++)
            {
                yield return _world.Projectiles.EntityAt(i).Id; 
            }
        }

        public CreationResult<FxObject> Factory(uint entityId)
        {
            var fx = _pool.Get(); 
            
            var res = new CreationResult<FxObject>
            {
                Result = fx,
                IsCreated = true
            };
            return res;
        }

        public void Update(uint entityId, FxObject viewElement)
        {
            var projectile = _world[entityId].Projectile;
            viewElement.transform.position = projectile.Position; 
        }

        public void Dispose(uint entityId, FxObject viewElement)
        {
            _pool.Return(viewElement);
        }

        public bool HasEntityWithId(uint entityId)
        {
            return _world.HasEntity(entityId);
        }
    }
}
