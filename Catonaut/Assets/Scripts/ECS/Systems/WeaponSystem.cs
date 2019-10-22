using System;
using Client;
using ECS.Components;
using UnityEngine;

namespace ECS.Systems
{
    public class WeaponSystem : SystemBase
    {
        private readonly GameSettings _gameSettings; 
    
        public WeaponSystem(GameSettings settings)
        {
            _gameSettings = settings;
            Debug.Log("Projectile life time: " + (int) (_gameSettings.ProjectileLifeTime * 40));
            Debug.Log("Projectile speed: " + (_gameSettings.ProjectileRange / (int) (_gameSettings.ProjectileLifeTime * 40)));
        }
        
        public override void Execute()
        {
            for (int i = 0; i < World.Weapons.Count; i++)
            {
                var entity = World.Weapons.EntityAt(i);
                var weapon = entity.Weapon;
                var input = entity.Input; 

                if (weapon.WeaponState == WeaponState.Cooldown)
                {
                    Debug.Log(weapon.CooldownTick + " " + World.Tick);
                }
                
                if (weapon.WeaponState == WeaponState.Charge)
                {
                    Debug.Log(weapon.ChargeTick + " " + World.Tick);
                }
                
                if (input != null && input.Aim && weapon.WeaponState == WeaponState.Idle)
                {
                    Debug.Log("Idle to Charge");
                    weapon.ChargeTick = World.Tick + weapon.ChargeTime;
                    weapon.WeaponState = WeaponState.Charge;
                }
                else if (weapon.WeaponState == WeaponState.Charge && weapon.ChargeTick == World.Tick)
                {
                    Debug.Log("Charge to Ready");
                    weapon.WeaponState = WeaponState.Ready;
                } else if (weapon.WeaponState == WeaponState.Cooldown && weapon.CooldownTick == World.Tick)
                {
                    Debug.Log("CoolDown to Idle");
                    weapon.WeaponState = WeaponState.Idle; 
                }

                if (input != null && input.Attack && (weapon.WeaponState == WeaponState.Charge || weapon.WeaponState == WeaponState.Ready))
                {
                    if (weapon.WeaponState == WeaponState.Ready)
                    {
                        var projectileEntity = World.CreateEntity();
                        var projectile = projectileEntity.AddProjectile();
                        projectile.Owner = entity;
                        projectile.Position = entity.Transform.Position; 
                        projectile.Direction = entity.Transform.Rotation * Vector3.forward;
                        projectile.RemainingLifetime = (int) (_gameSettings.ProjectileLifeTime * TickRate) + World.Tick;
                        projectile.SpeedPerTick =  (_gameSettings.ProjectileRange / (int) (_gameSettings.ProjectileLifeTime * TickRate));
                    }
                    
                    Debug.Log("Charge or Ready to Cooldown");
                    weapon.WeaponState = WeaponState.Cooldown;
                    weapon.CooldownTick = World.Tick + weapon.CooldownTime; 
                }
            }
        }
    }
}
