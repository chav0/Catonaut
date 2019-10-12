using Client.Scene;

namespace ECS
{
    public class SystemFactory
    {
        private readonly UnityScene _scene;

        public SystemFactory(UnityScene scene)
        {
            _scene = scene;
        }
        
        public SystemBase[] CreateSystems()
        {
            return new SystemBase[]
            {

            };
        }
    }
}