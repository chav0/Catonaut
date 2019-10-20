using System.Collections.Generic;

namespace ECS.Components
{
    public class Inventory : Component
    {
        public List<Entity> Keys = new List<Entity>(); 
    }
}
