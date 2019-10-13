using ECS;
using UnityEngine;

namespace Client.Updaters
{
    public class PlayerUpdater 
    {
        public void Update(World world)
        {
            for (int i = 0; i < world.Players.Count; i++)
            {
                var entity = world.Players.EntityAt(i);
                var body = entity.Player.PlayerObject; 
                var transform = entity.Transform;

                if (body != null && transform != null)
                {
                    body.SetAnimations(transform.Speed);
                }
            }
        }
    }
}
