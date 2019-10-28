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
                var input = entity.Input;

                if (body != null && input != null)
                {
                    body.SetAnimations(input.Speed);
                }
            }

            for (int i = 0; i < world.Monsters.Count; i++)
            {
                var entity = world.Monsters.EntityAt(i);
                var monster = entity.Monster; 
                var body = entity.Monster.Body; 
                
                if (body != null)
                {
                    body.SetAnimations(monster.Move ? .5f : 0f);
                }
            }
        }
    }
}
