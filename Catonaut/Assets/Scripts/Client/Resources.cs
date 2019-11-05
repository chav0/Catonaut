using Client.Objects;
using UnityEngine;

namespace Client
{
    [CreateAssetMenu(fileName = "Resources", menuName = "Settings/Resources")]
    public class Resources : ScriptableObject
    {
        public PlayerObject PlayerObject;
        public MapObject MapObject;
        public FxObject Fx; 
        public FxObject FxMonster; 
    }
}
