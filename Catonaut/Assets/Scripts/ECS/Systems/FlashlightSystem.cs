using Client;
using UnityEngine;

namespace ECS.Systems
{
    public class FlashlightSystem : SystemBase
    {
        private readonly GameSettings _gameSettings; 
    
        public FlashlightSystem(GameSettings settings)
        {
            _gameSettings = settings;
        }
        
        public override void Simulate()
        {
            for (int i = 0; i < World.Flashlights.Count; i++)
            {
                var entity = World.Flashlights.EntityAt(i);
                var input = entity.Input;
                var body = entity.Player.PlayerObject; 
                var flashlight = entity.Flashlight;

                if (input == null) 
                    continue;
                
                var speedRatio = 1f - input.Speed;
                var newRange = _gameSettings.FlashlightRange.Evaluate(speedRatio) * +_gameSettings.maxRange; 
                var newIntensity = _gameSettings.FlashlighIntensity.Evaluate(speedRatio) * +_gameSettings.maxIntensity; 
                var newYOffset = _gameSettings.FlashlightYOffset.Evaluate(speedRatio) * +_gameSettings.maxYOffset;

                flashlight.Range = Mathf.Lerp(newRange, flashlight.Range, _gameSettings.FlashlightLerp);
                flashlight.Intensity = Mathf.Lerp(newIntensity, flashlight.Intensity, _gameSettings.FlashlightLerp);
                flashlight.YOffset = Mathf.Lerp(newYOffset, flashlight.YOffset, _gameSettings.FlashlightLerp);

                body.SetFlashLight(flashlight.Intensity, flashlight.YOffset, flashlight.Range);
            }
        }
    }
}
