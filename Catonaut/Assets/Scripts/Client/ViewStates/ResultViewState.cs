using ECS.Components;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Client.ViewStates
{
    public class ResultViewState : BaseViewState
    {
        public override void OnEnter()
        {
            var resultScreen = Context.Screens.Result; 
            resultScreen.gameObject.SetActive(true);
            resultScreen.Defeat.SetActive(Context.AppModel.World.Match.Result == MatchResult.Defeat);
            resultScreen.Victory.SetActive(Context.AppModel.World.Match.Result == MatchResult.Victory);
            resultScreen.ToLobby.onClick.AddListener(() => SceneManager.LoadScene(0));
            
            Context.Screens.BattleHud.gameObject.SetActive(false);
            Context.Screens.Lobby.gameObject.SetActive(false);
        }
        
        public override void PreModelUpdate()
        {
            
        }

        public override void PostModelUpdate()
        {
            
        }
    }
}
