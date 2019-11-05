using System;
using System.Collections.Generic;
using Client.Objects;
using ECS.Components;
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
            MapGenerate(res); 
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
                Object.Destroy(go);
            }
        }

        private void MapGenerate(MapObject map)
        {
            var keysColor = new List<KeyColor>()
            {
                KeyColor.Red,
                KeyColor.Green,
                KeyColor.Blue,
            };

            var keyIndex = 0; 
            
            for (int i = 0; i < keysColor.Count; i++)
            {
                var keyColor = keysColor[i];
                var probability = map.Capsule.Probability[i];
                var rand = UnityEngine.Random.Range(0f, 1f);

                if (rand > probability ||
                    keysColor.Count - i <= map.Capsule.RequiredKeysCount - map.Capsule.RequiredKeys.Count)
                {
                    map.Capsule.RequiredKeys.Add(keyColor);
                    map.Capsule.KeyObjects[keyIndex].KeyColor = keyColor;
                    map.Capsule.KeyObjects[keyIndex].IsCompleted = false;
                    keyIndex++; 
                    Debug.Log(keyColor);
                }

                if (map.Capsule.RequiredKeys.Count == map.Capsule.RequiredKeysCount)
                    break;
            }

            for (int i = keysColor.Count - 1; i >= 1; i--)
            {
                int j = UnityEngine.Random.Range(0, i + 1);
                var temp = keysColor[j];
                keysColor[j] = keysColor[i];
                keysColor[i] = temp;
            }

            for (int i = 0; i < map.Keys.Length; i++)
            {
                var key = map.Keys[i];
                key.KeyColor = keysColor[i];
            }
        }
    }
}