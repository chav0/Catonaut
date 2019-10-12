using System;

namespace ECS
{
    public abstract class SystemBase : IDisposable
    {
        protected World World;
        protected int TickRate;

        public abstract void Simulate();
        public virtual void Predict() {}

        public void Init(World world, int tickRate)
        {
            World = world;
            TickRate = tickRate;
            OnInit();
        }

        public virtual void Dispose() {}
        
        protected virtual void OnInit()
        {
        }
    }
}