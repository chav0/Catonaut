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
        }
        
        public override void Execute()
        {
            for (int i = 0; i < World.Weapons.Count; i++)
            {
                var entity = World.Weapons.EntityAt(i);
                var weapon = entity.Weapon;
                var input = entity.Input; 
                
                if (input != null && input.Aim && weapon.WeaponState == WeaponState.Idle)
                {
                    weapon.ChargeTick = World.Tick + weapon.ChargeTime;
                    weapon.WeaponState = WeaponState.Charge;
                }
                else if (weapon.WeaponState == WeaponState.Charge && weapon.ChargeTick == World.Tick)
                {
                    weapon.WeaponState = WeaponState.Ready;
                } else if (weapon.WeaponState == WeaponState.Cooldown && weapon.CooldownTick == World.Tick)
                {
                    weapon.WeaponState = WeaponState.Idle; 
                }

                if (input != null && input.Attack && (weapon.WeaponState == WeaponState.Charge || weapon.WeaponState == WeaponState.Ready))
                {
                    if (weapon.WeaponState == WeaponState.Ready)
                    {
                        var projectileEntity = World.CreateEntity();
                        var projectile = projectileEntity.AddProjectile();
                        projectile.Owner = entity;
                        projectile.Position = entity.Transform.Position + new Vector3(0f, 0.25f, 0.25f); 
                        projectile.Direction = entity.Transform.Rotation * Vector3.forward;
                        projectile.RemainingLifetime = (int) (_gameSettings.ProjectileLifeTime * TickRate) + World.Tick;
                        projectile.SpeedPerTick =  (_gameSettings.ProjectileRange / (int) (_gameSettings.ProjectileLifeTime * TickRate));
                    }
                    
                    weapon.WeaponState = WeaponState.Cooldown;
                    weapon.CooldownTick = World.Tick + weapon.CooldownTime; 
                }
            }
        }
    }
}
