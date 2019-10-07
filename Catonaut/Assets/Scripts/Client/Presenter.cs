using Client.Model;
using Client.View;

namespace Client
{
    public class Presenter
    {
        private ClientView _view;
        private ClientModel _model;

        public Presenter()
        {
            _view = new ClientView();
            _model = new ClientModel();
        }

        public void Update()
        {
            _view.PreModelUpdate();
            _model.Update();
            _view.PostModelUpdate();
        }
    }
}
