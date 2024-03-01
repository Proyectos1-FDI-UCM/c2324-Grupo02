using MVPFramework.View;
using UnityEngine.EventSystems;
using static UISystem.MVP.View.EventTriggerView;

namespace UISystem.MVP.View
{
    internal readonly struct EventTriggerView :
        IObservableView<PressConfiuration>,
        IObservableView<EnterConfiguration>,
        IObservableView<ExitConfiguration>
    {
        public delegate void PressConfiuration(IObservableView<PressConfiuration> sender, BaseEventData baseEventData);
        public delegate void EnterConfiguration(IObservableView<EnterConfiguration> sender, BaseEventData baseEventData);
        public delegate void ExitConfiguration(IObservableView<ExitConfiguration> sender, BaseEventData baseEventData);

        private readonly PressConfiuration _onPress;
        private readonly EnterConfiguration _onEnter;
        private readonly ExitConfiguration _onExit;

        private readonly EventTrigger _eventTrigger;
        private readonly bool _clear;

        public EventTriggerView(EventTrigger eventTrigger, bool clear)
        {
            _eventTrigger = eventTrigger;
            _clear = clear;

            _onPress = (_, _) => { };
            _onEnter = (_, _) => { };
            _onExit = (_, _) => { };
        }

        void IObservableView<PressConfiuration>.Subscribe<UDelegate>(UDelegate observer)
        {
            EventTrigger.Entry entry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerClick,
                callback = new EventTrigger.TriggerEvent()
            };

            entry.callback.AddListener(OnPointerClick);
            if (_clear)
                _eventTrigger.triggers.Clear();
            _eventTrigger.triggers.Add(entry);
        }

        void IObservableView<PressConfiuration>.Unsubscribe<UDelegate>(UDelegate observer)
        {
            foreach (EventTrigger.Entry entry in _eventTrigger.triggers)
                if (entry.eventID == EventTriggerType.PointerClick)
                    entry.callback.RemoveListener(OnPointerClick);
        }

        void IObservableView<EnterConfiguration>.Subscribe<UDelegate>(UDelegate observer)
        {
            EventTrigger.Entry entry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerEnter,
                callback = new EventTrigger.TriggerEvent()
            };

            entry.callback.AddListener(OnPointerEnter);
            if (_clear)
                _eventTrigger.triggers.Clear();
            _eventTrigger.triggers.Add(entry);
        }

        void IObservableView<EnterConfiguration>.Unsubscribe<UDelegate>(UDelegate observer)
        {
            foreach (EventTrigger.Entry entry in _eventTrigger.triggers)
                if (entry.eventID == EventTriggerType.PointerEnter)
                    entry.callback.RemoveListener(OnPointerEnter);
        }

        void IObservableView<ExitConfiguration>.Subscribe<UDelegate>(UDelegate observer)
        {
            EventTrigger.Entry entry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerExit,
                callback = new EventTrigger.TriggerEvent()
            };

            entry.callback.AddListener(OnPointerExit);
            if (_clear)
                _eventTrigger.triggers.Clear();
            _eventTrigger.triggers.Add(entry);
        }

        void IObservableView<ExitConfiguration>.Unsubscribe<UDelegate>(UDelegate observer)
        {
            foreach (EventTrigger.Entry entry in _eventTrigger.triggers)
                if (entry.eventID == EventTriggerType.PointerExit)
                    entry.callback.RemoveListener(OnPointerExit);
        }

        private void OnPointerClick(BaseEventData baseEventData) => _onPress(this, baseEventData);
        private void OnPointerEnter(BaseEventData baseEventData) => _onEnter(this, baseEventData);
        private void OnPointerExit(BaseEventData baseEventData) => _onExit(this, baseEventData);
    }
}