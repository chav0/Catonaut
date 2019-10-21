﻿using ECS;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

namespace Client.Objects
{
    public class PlayerObject : EntityRefObject
    {
        public Animator Animator;
        public CharacterController CharacterController; 
        public Rigidbody Rigidbody;
        public Light Flashlight;  
        public GameObject Laser;  
        private static readonly int RightBlend = Animator.StringToHash("RightBlend");
        private static readonly int ForwardBlend = Animator.StringToHash("ForwardBlend");
        private static readonly int BlendSpeed = Animator.StringToHash("BlendSpeed");

        public void SetAnimations(float speed)
        {
            Animator.SetFloat(BlendSpeed, speed);
        }

        public void SetFlashLight(float intensity, float yOffset, float range)
        {
            Flashlight.intensity = intensity; 
            Flashlight.transform.localPosition = new Vector3(0f, yOffset, 0f);
            Flashlight.range = range; 
        }

        public void SetLaser(bool active)
        {
            if (Laser.activeSelf != active)
            {
                Laser.SetActive(active);
            }
        }
    }
}
