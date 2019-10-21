using System;
using ECS.Components;
using UnityEngine;

namespace ECS.Systems
{
    public class WeaponSystem : SystemBase
    {
        public override void Execute()
        {
            for (int i = 0; i < World.Weapons.Count; i++)
            {
                var entity = World.Weapons.EntityAt(i);
                var weapon = entity.Weapon;
                var input = entity.Input; 
                
                if(input == null)
                    continue;

                if (input.Aim && weapon.WeaponState == WeaponState.Idle)
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

                if (input.Attack)
                {
                    if (weapon.WeaponState == WeaponState.Ready)
                    {
                        var projectileEntity = World.CreateEntity();
                        var projectile = projectileEntity.AddProjectile();
                        projectile.Owner = entity;
                        projectile.Position = entity.Transform.Position; 
                        projectile.Direction = input.Direction.normalized;
                    }
                    
                    weapon.WeaponState = WeaponState.Cooldown;
                }
            }
        }
    }
}
