using System;
using UnityEngine;

namespace Client
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Settings/GameSettings")]
    public class GameSettings : ScriptableObject
    {
        [Header("Camera Settings")] 
        public Vector3 CameraPosition;
        public Vector3 CameraRotation;

        [Header("Movement")] 
        public float MaxSpeed;
        public float MinSpeed;
        public float RotationSpeed;
    }
}
