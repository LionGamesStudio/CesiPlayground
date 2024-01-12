using System;

namespace Assets.Scripts.Core.Events.Interfaces
{
    public interface IEventBinding<T>
    {
        public Action<T> OnEvent { get; set; }
        public Action OnEventNoArgs { get; set; }
    }
}
