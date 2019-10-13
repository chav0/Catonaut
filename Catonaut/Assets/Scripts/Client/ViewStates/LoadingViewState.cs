using Client.Model;
using UnityEngine;

namespace Client.ViewStates
{
    public class LoadingViewState : BaseViewState
    {
        public override void PreModelUpdate()
        {
            if (Context.AppModel.Status == ModelStatus.Init)
            {
                Context.AppModel.FindMatch();
            }
        }

        public override void PostModelUpdate()
        {
            if (Context.AppModel.Status == ModelStatus.Battle)
            {
                if (Context.AppModel.World != null)
                {
                    SetState(new BattleViewState());
                }
            }
        }
    }
}
