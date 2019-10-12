using System;
using System.Collections.Generic;
using Client.Objects;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Client.Scene
{
    public class UnityScene : IDisposable
    {
        private readonly Resources _resources;
        private readonly List<GameObject> _createdObjects = new List<GameObject>();
        
        public UnityScene(Resources resources)
        {
            _resources = resources;
        }
        
        public MapObject CreateMap()
        {
            var res = Object.Instantiate(_resources.MapObject);
            _createdObjects.Add(res.gameObject);
            return res;
        }

        public PlayerObject CreatePlayer()
        {
            var res = Object.Instantiate(_resources.PlayerObject);
            _createdObjects.Add(res.gameObject);
            return res;
        }

        public void Dispose()
        {
            foreach (var go in _createdObjects)
            {
                GameObject.Destroy(go);
            }
        }
    }
}