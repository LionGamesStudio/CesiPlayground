using Assets.Scripts.Core.Events.Interfaces;
using System;

namespace Assets.Scripts.Core.Events
{
    public class EventBinding<T> : IEventBinding<T> where T : IEvent
    {
        private Action<T> _onEvent = _ => { };
        private Action _onEventNoArgs = () => { };

        Action<T> IEventBinding<T>.OnEvent 
        { 
            get => _onEvent; 
            set => _onEvent = value; 
        }

        Action IEventBinding<T>.OnEventNoArgs 
        { 
            get => _onEventNoArgs; 
            set => _onEventNoArgs = value; 
        }

        public EventBinding(Action<T> onEvent) => _onEvent = onEvent;
        public EventBinding(Action onEventNoArgs) => _onEventNoArgs = onEventNoArgs;

        public void AddListener(Action<T> onEvent) => _onEvent += onEvent;
        public void AddListener(Action onEventNoArgs) => _onEventNoArgs += onEventNoArgs;

        public void RemoveListener(Action<T> onEvent) => _onEvent -= onEvent;
        public void RemoveListener(Action onEventNoArgs) => _onEventNoArgs -= onEventNoArgs;
    }
}
