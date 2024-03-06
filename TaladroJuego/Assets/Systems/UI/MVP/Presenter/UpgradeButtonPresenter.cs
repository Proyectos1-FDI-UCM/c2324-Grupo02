using MVPFramework.Model;
using MVPFramework.Presenter;
using MVPFramework.View;
using UISystem.MVP.Model;
using UISystem.MVP.View;

namespace UISystem.MVP.Presenter
{
    internal class UpgradeButtonPresenter : IPresenter<string>
    {
        private readonly EventTriggerView _eventTriggerView;
        private readonly IView<string> _textView;
        private readonly IModel<DescriptibleUpgrade> _model;

        public UpgradeButtonPresenter(EventTriggerView eventTriggerView, IView<string> textView, IModel<DescriptibleUpgrade> model)
        {
            _eventTriggerView = eventTriggerView;
            _textView = textView;
            _model = model;

            ConnectView();
            ConnectModel();
        }
    }
}