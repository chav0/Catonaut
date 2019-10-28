using UnityEngine;

namespace Client.Objects
{
    public class MapObject : MonoBehaviour
    {
        public Transform[] SpawnZones;
        public KeyObject[] Keys;
        public CapsuleObject Capsule;
        public DamageZoneObject[] DamageZoneObjects;
        public MonsterObject[] Monsters; 
    }
}
