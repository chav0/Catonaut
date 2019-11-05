using System;
using System.Collections.Generic;
using Client.Objects;
using Client.Scene;
using ECS;
using ECS.Components;
using UnityEngine;

namespace Client.Updaters
{
    public class FxUpdater : IUpdateImplementer<FxObject>
    {
        private readonly ComponentPool<FxObject> _pool;
        private readonly ComponentPool<FxObject> _poolMonsters;
        private readonly CollectionUpdater<FxObject> _collectionUpdater;
        private World _world; 

        public FxUpdater(Resources resources)
        {
            _pool = new ComponentPool<FxObject>(resources.Fx, 10, true);
            _poolMonsters = new ComponentPool<FxObject>(resources.FxMonster, 10, true);
            _collectionUpdater = new CollectionUpdater<FxObject>();
        }
        
        public void Update(World world)
        {
            _world = world;

            for (int i = 0; i < world.Players.Count; i++)
            {
                var weapon = world.Players.EntityAt(i).Weapon; 
                world.Players[i].PlayerObject.SetLaser(weapon.WeaponState == WeaponState.Ready);
            }
            
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
            FxObject fx = null; 
            var entity = _world[entityId];
            if (entity.Projectile.Owner.Player != null)
            {
                fx = _pool.Get();
                fx.PlayerOwner = true; 
            }
            else
            {
                fx = _poolMonsters.Get();
                fx.PlayerOwner = false; 
            }
            
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
            viewElement.transform.rotation = Quaternion.LookRotation(projectile.Direction);
        }

        public void Dispose(uint entityId, FxObject viewElement)
        {
            if (viewElement.PlayerOwner)
                _pool.Return(viewElement);
            else
                _poolMonsters.Return(viewElement);
        }

        public bool HasEntityWithId(uint entityId)
        {
            return _world.HasEntity(entityId);
        }
    }
}
