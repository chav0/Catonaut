using Client.View;
using UnityEngine;

namespace Client
{
    public class Root : MonoBehaviour
    {
        public Camera Camera;
        public Screens Screens; 
        public GameSettings Settings;
        public Resources Resources;

        private Presenter _presenter;
        private bool _init; 
        
        public void Awake()
        {
            Application.targetFrameRate = 60;
            _presenter = new Presenter(Settings, Resources, Screens, Camera);
        }
		
        public void LateUpdate()
        {
            _presenter?.Update();
        }

        public void OnApplicationQuit()
        {
			
        }
    }
}
