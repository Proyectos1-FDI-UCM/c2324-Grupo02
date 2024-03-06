using MVPFramework.Model;
using MVPFramework.Presenter;
using MVPFramework.View;
using System;
using System.Collections.Generic;
using System.Linq;
using UISystem.MVP.Model;
using UISystem.MVP.View;
using UnityEngine;
using UnityEngine.EventSystems;
using static UISystem.MVP.Model.Descriptible;
using static UISystem.MVP.Model.DescriptibleUpgradeFlyweight;
using static UISystem.MVP.View.DescriptibleTextView;
using static UISystem.MVP.View.EventTriggerView;

namespace UISystem.MVP.Presenter
{
    internal class UpgradeMenuPresenter : MonoBehaviour, IPresenter<IEnumerable<LabeledEventTriggerView>>,
        IObserverPresenter<IEnumerable<LabeledEventTriggerView>>
    {
        [SerializeField]
        private DescriptibleUpgradeFlyweight[] _upgrades;

        private readonly Dictionary<object, DescriptibleUpgradeFlyweight> _viewUpgrades =
            new Dictionary<object, DescriptibleUpgradeFlyweight>();

        private IPresenter<LabeledEventTriggerView, IModel<DescriptibleUpgrade>> _upgradeButtonPresenter;
        private IPresenter<IView<TitledText>, IModel<TitledDescription>> _descriptionPanelPresenter;

        private void Awake()
        {
            _upgradeButtonPresenter = GetComponentInChildren<IPresenter<LabeledEventTriggerView, IModel<DescriptibleUpgrade>>>();
            _descriptionPanelPresenter = GetComponentInChildren<IPresenter<IView<TitledText>, IModel<TitledDescription>>>();
        }

        public bool TryUpdate(IEnumerable<LabeledEventTriggerView> view)
        {
            LabeledEventTriggerView[] labeledEventTriggerViews = view.ToArray();

            for (int i = 0; i < labeledEventTriggerViews.Length; i++)
            {
                LabeledEventTriggerView labeledEventTriggerView = labeledEventTriggerViews[i];
                if (i < _upgrades.Length)
                {
                    DescriptibleUpgradeFlyweight upgrade = _upgrades[i];
                    _viewUpgrades[labeledEventTriggerView] = upgrade;
                    _upgradeButtonPresenter.TryUpdate(labeledEventTriggerView, upgrade);
                }
                else
                    labeledEventTriggerView.TryUpdateWith(string.Empty);
            }

            return true;
        }

        public void ConnectTo(IEnumerable<LabeledEventTriggerView> view)
        {
            foreach (LabeledEventTriggerView labeledEventTriggerView in view)
            {
                IObservableView<EnterConfiguration> enterObservable = labeledEventTriggerView;
                enterObservable.Unsubscribe<EnterConfiguration>(OnButtonEnter);
                enterObservable.Subscribe<EnterConfiguration>(OnButtonEnter);

                IObservableView<ExitConfiguration> exitObservable = labeledEventTriggerView;
                exitObservable.Unsubscribe<ExitConfiguration>(OnButtonExit);
                exitObservable.Subscribe<ExitConfiguration>(OnButtonExit);
            }
        }

        public void DisconnectFrom(IEnumerable<LabeledEventTriggerView> view)
        {
            foreach (LabeledEventTriggerView labeledEventTriggerView in view)
            {
                IObservableView<EnterConfiguration> enterObservable = labeledEventTriggerView;
                enterObservable.Unsubscribe<EnterConfiguration>(OnButtonEnter);

                IObservableView<ExitConfiguration> exitObservable = labeledEventTriggerView;
                exitObservable.Unsubscribe<ExitConfiguration>(OnButtonExit);
            }
        }

        private void OnButtonEnter(IObservableView<EnterConfiguration> sender, BaseEventData baseEventData)
        {
            //_ = _viewUpgrades.TryGetValue(sender, out DescriptibleUpgradeFlyweight model)
            //    && _descriptionPanelPresenter.TryUpdate(
            //        new ,
            //        model);
        }

        private void OnButtonExit(IObservableView<ExitConfiguration> sender, BaseEventData baseEventData)
        {
            throw new NotImplementedException();
        }


    }
}