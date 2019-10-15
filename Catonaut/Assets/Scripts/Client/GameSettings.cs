using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Client
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Settings/GameSettings")]
    public class GameSettings : ScriptableObject
    {
        [FormerlySerializedAs("CameraPosition")] [Header("Camera Settings")] 
        public Vector3 CameraOffset;
        public float CameraMaxAngle;
        public float CameraMinAngle;
        public float CameraVerticalRotationSpeed;
        public float CameraLerp;
        public float CameraRotationLerp;
        public float CameraRotationSpeed;

        [Header("Movement")] 
        public float MaxSpeed;
        public float RotationSpeed;
    }
}
