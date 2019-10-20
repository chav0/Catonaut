using System.Collections.Generic;
using Client.Objects;

namespace ECS.Components
{
    public class Capsule : Component
    {
        public CapsuleObject Body; 
        public List<KeyColor> RequiredKeys = new List<KeyColor>();
    }
}
