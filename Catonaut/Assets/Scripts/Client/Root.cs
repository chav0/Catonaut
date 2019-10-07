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
        
        public void Awake()
        {
            Application.targetFrameRate = 60;
        }
		
        public void LateUpdate()
        {

        }

        public void OnApplicationQuit()
        {
			
        }
    }
}
