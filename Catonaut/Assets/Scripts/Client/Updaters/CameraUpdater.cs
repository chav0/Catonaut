using ECS;
using UnityEngine;

namespace Client.Updaters
{
    public class CameraUpdater 
    {
        private readonly Camera _camera;
        private readonly GameSettings _gameSettings;
        private Vector3 _cameraPos;

        public CameraUpdater(Camera camera, GameSettings gameSettings)
        {
            _camera = camera;
            _gameSettings = gameSettings; 
        }

        public void Update(World world, float yOfView)
        {
            var body = world.ClientEntity.Player.PlayerObject; 
            
            if (body == null)
                return;
            
            var playerPos = body.transform.position;
            _cameraPos = playerPos + Quaternion.Euler(0, yOfView, 0) * _gameSettings.CameraPosition;

            _camera.transform.position = Vector3.Lerp(_camera.transform.position, _cameraPos,
                Time.deltaTime * 25f);
            
            _camera.transform.LookAt(_camera.transform.position 
                                     - Quaternion.Euler(0, yOfView, 0) * _gameSettings.CameraPosition 
                                     + Vector3.up * 1.1f);
            Debug.DrawLine(_camera.transform.position, playerPos + Vector3.up * 1.1f, Color.cyan);
        }
    }
}
