using System.Collections.Generic;
using Client.Objects;
using UnityEngine;

namespace ECS.Physics
{
    public static class PhysicsUtils
    {
        private static readonly Collider[] OverlapBuffer = new Collider[20];
        private static readonly List<Entity> Entities = new List<Entity>(20);
        
        public static IReadOnlyList<Entity> OverlapSphere(Vector3 center, float radius, int layerMask)
        {
            Entities.Clear();
            var count = UnityEngine.Physics.OverlapSphereNonAlloc(center, radius, OverlapBuffer, layerMask);
            
            for (int i = 0; i < count; i++)
            {
                var collider = OverlapBuffer[i];
                var entityRef = collider.gameObject.GetComponent<EntityRefObject>();
                if (entityRef == null)
                    continue;
                Entities.Add(entityRef.Entity);
            }
            return Entities;
        }
    }
}