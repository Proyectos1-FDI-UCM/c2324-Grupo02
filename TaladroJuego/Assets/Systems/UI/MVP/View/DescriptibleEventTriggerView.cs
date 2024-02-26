using MVPFramework.View;

namespace UISystem.MVP.View
{
    internal readonly struct DescriptibleEventTriggerView : IView<string>,
        IView<EventTriggerView.PressConfiuration>,
        IView<EventTriggerView.EnterConfiguration>,
        IView<EventTriggerView.ExitConfiguration>
    {
        private readonly EventTriggerView _eventTriggerView;
        private readonly IView<string> _textView;

        public DescriptibleEventTriggerView(EventTriggerView eventTriggerView, IView<string> textView)
        {
            _eventTriggerView = eventTriggerView;
            _textView = textView;
        }

        public bool TryUpdateWith(string model) =>
            _textView.TryUpdateWith(model);

        public bool TryUpdateWith(EventTriggerView.PressConfiuration model) =>
            _eventTriggerView.TryUpdateWith(model);

        public bool TryUpdateWith(EventTriggerView.EnterConfiguration model) =>
            _eventTriggerView.TryUpdateWith(model);

        public bool TryUpdateWith(EventTriggerView.ExitConfiguration model) =>
            _eventTriggerView.TryUpdateWith(model);
    }
}