using Assets.Scripts.Core.Events.Interfaces;
using System.Collections.Generic;

namespace Assets.Scripts.Core.Events
{
    public static class EventBus<T> where T : IEvent
    {
        static readonly HashSet<IEventBinding<T>> _bindings = new HashSet<IEventBinding<T>>();

        public static void Register(IEventBinding<T> binding) => _bindings.Add(binding);
        public static void Unregister(IEventBinding<T> binding) => _bindings.Remove(binding);

        /// <summary>
        /// Raise an event
        /// </summary>
        /// <param name="evt"></param>
        public static void Raise(T @evt)
        {
            foreach (var binding in _bindings)
            {
                binding.OnEvent.Invoke(@evt);
                binding.OnEventNoArgs.Invoke();
            }
        }

        static void Clear() => _bindings.Clear();
    }
}
