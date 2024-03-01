using MVPFramework.View;
using static UISystem.MVP.View.EventTriggerView;

namespace UISystem.MVP.View
{
    internal readonly struct LabeledEventTriggerView : IView<string>,
        IObservableView<PressConfiuration>,
        IObservableView<EnterConfiguration>,
        IObservableView<ExitConfiguration>
    {
        private readonly EventTriggerView _eventTriggerView;
        private readonly IView<string> _textView;

        public LabeledEventTriggerView(EventTriggerView eventTriggerView, IView<string> textView)
        {
            _eventTriggerView = eventTriggerView;
            _textView = textView;
        }

        public bool TryUpdateWith(string model) => _textView.TryUpdateWith(model);

        void IObservableView<PressConfiuration>.Subscribe<UDelegate>(UDelegate observer)
        {
            ((IObservableView<PressConfiuration>)_eventTriggerView).Subscribe(observer);
        }

        void IObservableView<EnterConfiguration>.Subscribe<UDelegate>(UDelegate observer)
        {
            ((IObservableView<EnterConfiguration>)_eventTriggerView).Subscribe(observer);
        }

        void IObservableView<ExitConfiguration>.Subscribe<UDelegate>(UDelegate observer)
        {
            ((IObservableView<ExitConfiguration>)_eventTriggerView).Subscribe(observer);
        }

        void IObservableView<PressConfiuration>.Unsubscribe<UDelegate>(UDelegate observer)
        {
            ((IObservableView<PressConfiuration>)_eventTriggerView).Unsubscribe(observer);
        }

        void IObservableView<EnterConfiguration>.Unsubscribe<UDelegate>(UDelegate observer)
        {
            ((IObservableView<EnterConfiguration>)_eventTriggerView).Unsubscribe(observer);
        }

        void IObservableView<ExitConfiguration>.Unsubscribe<UDelegate>(UDelegate observer)
        {
            ((IObservableView<ExitConfiguration>)_eventTriggerView).Unsubscribe(observer);
        }
    }
}