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
                    body.HealthBar.transform.localScale = new Vector3(body.HealthBar.transform.localScale.x,
                        (health.CurrentHealth / (float) health.MaxHealth) / 10f, body.HealthBar.transform.localScale.z);
                }

                if (body.LastHealth == 0)
                {
                    body.LastHealth = health.CurrentHealth; 
                }
                
                if (body.LastHealth != health.CurrentHealth)
                {
                    body.SetDamageImpact();
                    body.LastHealth = health.CurrentHealth; 
                }
            }
        }
    }
}
