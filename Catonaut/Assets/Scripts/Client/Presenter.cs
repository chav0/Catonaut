using Client.Model;
using Client.Scene;
using Client.View;
using UnityEngine;

namespace Client
{
    public class Presenter
    {
        private ClientView _view;
        private ClientModel _model;

        public Presenter(GameSettings gameSettings, Resources resources, Screens screens, Camera camera)
        {
            var unityScene = new UnityScene(resources); 
            
            _model = new ClientModel(gameSettings, unityScene, resources);
            _view = new ClientView(resources, _model, unityScene, screens, camera);
        }

        public void Update()
        {
            _view.PreModelUpdate();
            _model.Update(Time.realtimeSinceStartup);
            _view.PostModelUpdate();
        }
    }
}
