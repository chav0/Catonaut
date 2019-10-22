using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Client
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Settings/GameSettings")]
    public class GameSettings : ScriptableObject
    {
        [Header("Camera Settings")] 
        public Vector3 CameraOffset;
        public float CameraMaxAngle;
        public float CameraMinAngle;
        public float CameraVerticalRotationSpeed;
        public float CameraLerp;
        public float CameraRotationLerp;
        public float CameraRotationSpeed;

        [Header("Flashlight")] 
        public float maxYOffset;
        public float maxIntensity;
        public float maxRange;
        public AnimationCurve FlashlightRange;
        public AnimationCurve FlashlighIntensity;
        public AnimationCurve FlashlightYOffset;
        public float FlashlightLerp; 

        [Header("Movement")] 
        public float MaxSpeed;
        public float AimSpeedModificator;
        public float RotationSpeed;

        [Header("Mode Settings")] 
        public float KeyRadius; 
        public float CapsuleRadius;

        [Header("Player Settings")] 
        public int MaxPlayerHealth;
        
        [Header("Weapon")] 
        public float WeaponChargeTime;
        public float WeaponCooldownTime;
        public float ProjectileLifeTime;
        public float ProjectileRange; 
        public float ProjectileRadius; 
        public int ProjectileDamage; 
    }
}
