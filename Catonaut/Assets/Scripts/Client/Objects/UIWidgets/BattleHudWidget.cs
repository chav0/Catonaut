using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Client.Objects.UIWidgets
{
    public class BattleHudWidget : MonoBehaviour
    {
        public StickWidget MovementStick;
        public StickWidget AttackStick;
        public ScoreWidget EnemyScore;
        public ScoreWidget AllyScore;
    }
}
