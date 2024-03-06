using System;

namespace Assets.Scripts.CSharpClasses.Reactive
{
    public class ReactiveProperty<T>
    {
        private T m_value;

        public T Value
        {
            get => m_value;
            set
            {
                m_value = value;
                OnValueChanged?.Invoke(m_value);
            }
        }

        public event Action<T> OnValueChanged;
    }
}
