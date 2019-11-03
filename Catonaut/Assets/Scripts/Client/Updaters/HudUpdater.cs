using Client.Objects.UIWidgets;
using ECS;
using ECS.Components;
using UnityEngine;

namespace Client.Updaters
{
    public class HudUpdater 
    {
        private BattleHudWidget _hud;

        public HudUpdater(BattleHudWidget widget)
        {
            _hud = widget; 
        }
        
        public void Update(World world)
        {
            for (int i = 0; i < world.Players.Count; i++)
            {
                var entity = world.Players.EntityAt(i);

                if (world.ClientEntity == entity)
                {
                    SetScore(_hud.AllyScore, entity); 
                }
                else
                {
                    SetScore(_hud.EnemyScore, entity); 
                }
            }
        }

        private void SetScore(ScoreWidget widget, Entity playerEntity)
        {
            var inventory = playerEntity.Inventory;

            bool red = false, blue = false, green = false; 
            foreach (var key in inventory.Keys)
            {
                red |= key.Key.KeyColor == KeyColor.Red;
                blue |= key.Key.KeyColor == KeyColor.Blue;
                green |= key.Key.KeyColor == KeyColor.Green;
            }
            
            widget.SetKeys(red, green, blue);
        }
    }
}
