﻿using ECS;
using UnityEngine;

namespace Client.Updaters
{
    public class CameraUpdater 
    {
        private readonly Camera _camera;
        private readonly GameSettings _gameSettings;
        private Vector3 _cameraPos;
        private float _horizontalRotation; 
        private float _verticalRotation;
        private Quaternion _startRotation; 

        public CameraUpdater(Camera camera, GameSettings gameSettings)
        {
            _camera = camera;
            _startRotation = _camera.transform.rotation; 
            _gameSettings = gameSettings; 
        }

        public void Update(World world)
        {
            var body = world.ClientEntity.Player.PlayerObject; 
            
            if (body == null)
                return;

            /*_horizontalRotation += rotation.x;
            
            _verticalRotation += rotation.y * _gameSettings.CameraVerticalRotationSpeed;
            _verticalRotation =
                Mathf.Clamp(_verticalRotation, _gameSettings.CameraMinAngle, _gameSettings.CameraMaxAngle);  */

            var playerPos = body.transform.position;
            /*_cameraPos = playerPos + Quaternion.Euler(0, _horizontalRotation, 0) * _gameSettings.CameraOffset;*/
            _cameraPos = playerPos + Quaternion.identity * _gameSettings.CameraOffset;

            _camera.transform.position = Vector3.Lerp(_camera.transform.position, _cameraPos,
                Time.deltaTime * _gameSettings.CameraLerp);

            _camera.transform.LookAt(_camera.transform.position - _gameSettings.CameraOffset); 
            /*- Quaternion.Euler(0, _horizontalRotation, 0) * _gameSettings.CameraOffset 
            + Vector3.down * _verticalRotation);*/
        }
    }
}
