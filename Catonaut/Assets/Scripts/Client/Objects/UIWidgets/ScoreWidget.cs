using UnityEngine;

namespace Client.Objects.UIWidgets
{
    public class ScoreWidget : MonoBehaviour
    {
        [SerializeField] private GameObject _red;
        [SerializeField] private GameObject _green;
        [SerializeField] private GameObject _blue;

        public void SetKeys(bool red, bool green, bool blue)
        {
            _red.SetActive(red);
            _green.SetActive(green);
            _blue.SetActive(blue);
        }
    }
}
