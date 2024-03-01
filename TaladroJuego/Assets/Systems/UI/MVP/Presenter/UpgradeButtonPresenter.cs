using MVPFramework.Model;
using MVPFramework.Presenter;
using MVPFramework.View;
using ResourceCollectionSystem;
using System.Collections.Generic;
using UISystem.MVP.View;
using UnityEngine;
using UnityEngine.EventSystems;
using static UISystem.MVP.Model.DescriptibleUpgradeFlyweight;
using static UISystem.MVP.View.EventTriggerView;

namespace UISystem.MVP.Presenter
{
    internal class UpgradeButtonPresenter : MonoBehaviour, IPresenter<LabeledEventTriggerView, IModel<DescriptibleUpgrade>>,
        IObserverPresenter<IObservableView<PressConfiuration>>
    {
        [SerializeField]
        private ResourcesContainer _resourcesContainer;
        private readonly Dictionary<IObservableView<PressConfiuration>, IModel<DescriptibleUpgrade>> _viewModels =
            new Dictionary<IObservableView<PressConfiuration>, IModel<DescriptibleUpgrade>>();

        public void ConnectTo(IObservableView<PressConfiuration> view)
        {
            view.Unsubscribe<PressConfiuration>(OnButtonPress);
            view.Subscribe<PressConfiuration>(OnButtonPress);
        }

        public void DisconnectFrom(IObservableView<PressConfiuration> view)
        {
            _viewModels.Remove(view);
            view.Unsubscribe<PressConfiuration>(OnButtonPress);
        }

        private void OnButtonPress(IObservableView<PressConfiuration> view, BaseEventData baseEventData)
        {
            _ = _viewModels.TryGetValue(view, out IModel<DescriptibleUpgrade> model)
                && _resourcesContainer.TryPurchase(model.Capture().upgrade);
        }

        public bool TryUpdate(LabeledEventTriggerView view, IModel<DescriptibleUpgrade> model)
        {
            _viewModels[view] = model;
            return view.TryUpdateWith(model.Capture().title);
        }
    }
}