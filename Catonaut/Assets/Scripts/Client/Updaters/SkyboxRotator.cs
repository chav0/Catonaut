using DG.Tweening;
using UnityEngine;

namespace Client.Updaters
{
    public class SkyboxRotator : MonoBehaviour
    {
        private static readonly int Rotation = Shader.PropertyToID("_Rotation");

        private void Update()
        {
            RenderSettings.skybox.SetFloat(Rotation, Time.time * 5f);
        }
    }
}
