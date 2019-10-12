using Client.Updaters;

namespace Client.ViewStates
{
    public class BattleViewState : BaseViewState
    {
        private CameraUpdater _cameraUpdater;
        private PlayerUpdater _playerUpdater;

        public override void OnEnter()
        {
            _cameraUpdater = new CameraUpdater();
            _playerUpdater = new PlayerUpdater();
        }
        
        public override void PreModelUpdate()
        {
            
        }

        public override void PostModelUpdate()
        {
            
        }
    }
}
