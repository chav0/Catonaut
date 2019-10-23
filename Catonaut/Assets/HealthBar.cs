using System.Collections;
using System.Collections.Generic;
using ECS;
using UnityEngine;

namespace Client.Updaters
{
    public class HealthBarUpdater
    {
        public void Update(World world)
        {
            for (int i = 0; i < world.Players.Count; i++)
            {
                var entity = world.Players.EntityAt(i);
                var body = entity.Player.PlayerObject; 
                var health = entity.Health;

                if (body != null && health != null)
                {
                    body.HealthBar.transform.localScale = new Vector3(body.HealthBar.transform.localScale.y,(health.CurrentHealth / (float) health.MaxHealth)
                        , body.HealthBar.transform.localScale.z);
                }
            }
        }
    }
}
