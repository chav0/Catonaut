using Client.Objects;
using UnityEngine;

namespace ECS.Components
{
    public class Key : Component
    {
        public KeyColor KeyColor;
        public KeyObject Body;
        public bool HasOwner;
    }

    public enum KeyColor
    {
        Red,
        Green,
        Blue,
    }
}
