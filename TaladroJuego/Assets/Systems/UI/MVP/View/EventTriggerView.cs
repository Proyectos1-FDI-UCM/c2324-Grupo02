using MVPFramework.View;
using UnityEngine.EventSystems;

namespace UISystem.MVP.View
{
    internal readonly struct EventTriggerView : IView<EventTriggerView.PressConfiuration>,
        IView<EventTriggerView.EnterConfiguration>,
        IView<EventTriggerView.ExitConfiguration>
    {
        public delegate void PressConfiuration(BaseEventData baseEventData);
        public delegate void EnterConfiguration(BaseEventData baseEventData);
        public delegate void ExitConfiguration(BaseEventData baseEventData);

        private readonly EventTrigger _eventTrigger;
        private readonly bool _clear;

        public EventTriggerView(EventTrigger eventTrigger, bool clear)
        {
            _eventTrigger = eventTrigger;
            _clear = clear;
        }

        public bool TryUpdateWith(PressConfiuration model)
        {
            EventTrigger.Entry entry = new EventTrigger.Entry 
            { 
                eventID = EventTriggerType.PointerClick,
                callback = new EventTrigger.TriggerEvent()
            };

            entry.callback.AddListener((data) => model(data));
            if (_clear)
                _eventTrigger.triggers.Clear();
            _eventTrigger.triggers.Add(entry);
            return true;
        }

        public bool TryUpdateWith(EnterConfiguration model)
        {
            EventTrigger.Entry entry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerEnter,
                callback = new EventTrigger.TriggerEvent()
            };

            entry.callback.AddListener((data) => model(data));
            if (_clear)
                _eventTrigger.triggers.Clear();
            _eventTrigger.triggers.Add(entry);
            return true;
        }

        public bool TryUpdateWith(ExitConfiguration model)
        {
            EventTrigger.Entry entry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerExit,
                callback = new EventTrigger.TriggerEvent()
            };

            entry.callback.AddListener((data) => model(data));
            if (_clear)
                _eventTrigger.triggers.Clear();
            _eventTrigger.triggers.Add(entry);
            return true;
        }
    }
}