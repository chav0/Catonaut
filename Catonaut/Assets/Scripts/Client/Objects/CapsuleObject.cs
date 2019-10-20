using System.Collections.Generic;
using ECS.Components;
using UnityEngine;

namespace Client.Objects
{
    public class CapsuleObject : EntityRefObject
    {
        public List<float> Probability;
        public int RequiredKeysCount; 
        
        public List<KeyColor> RequiredKeys { get; } = new List<KeyColor>();
    }
}
