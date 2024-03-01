using MVPFramework.View;
using UnityEngine;

namespace UISystem.MVP.View
{
    internal readonly struct ActivableView : IView<bool>
    {
        private readonly GameObject _gameObject;

        public ActivableView(GameObject gameObject)
        {
            _gameObject = gameObject;
        }

        public bool TryUpdateWith(bool status)
        {
            _gameObject.SetActive(status);
            return true;
        }
    }

    //internal readonly struct ActivableView<TStatus> : IView<TStatus>, IView<bool>
    //{
    //    private readonly IView<bool> _activableView;
    //    private readonly IView<TStatus> _view;

    //    public ActivableView(IView<bool> activableView, IView<TStatus> view)
    //    {
    //        _activableView = activableView;
    //        _view = view;
    //    }

    //    public bool TryUpdateWith(TStatus status) => _view.TryUpdateWith(status);

    //    public bool TryUpdateWith(bool status) => _activableView.TryUpdateWith(status);
    //}
}