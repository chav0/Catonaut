namespace ECS.Components
{
    public class Weapon : Component
    {
        public WeaponState WeaponState;
        public int ChargeTick;
        public int CooldownTick;

        public int ChargeTime; 
        public int CooldownTime; 
    }

    public enum WeaponState
    {
        Idle,
        Charge,
        Ready,
        Fire,
        Cooldown,
    }
}
