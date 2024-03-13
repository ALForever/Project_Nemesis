using Assets.Scripts.CSharpClasses.Extensions;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.CSharpClasses.Reactive
{
    public class ReactiveProperty<T>
    {
        private event Action<T> OnValueChanged;
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

        /// <summary>
        /// Устанавливает значение по умолачнию
        /// </summary>
        /// <param name="notify">Уведомлять об изменении</param>
        public void SetDefault(bool notify = true)
        {
            m_value = default;
            if (notify)
            {
                OnValueChanged?.Invoke(m_value);
            }
        }

        /// <summary>
        /// Добавляет подписку на событие изменения значения свойства
        /// </summary>
        /// <param name="action">Делегат для добавления</param>
        public void Subscribe(Action<T> action)
        {
            OnValueChanged += action;
        }

        /// <summary>
        /// Отменяет подписку на событие изменения значения свойства
        /// </summary>
        /// <param name="action">Делегат для удаления</param>
        public void Unsubscribe(Action<T> action)
        {
            OnValueChanged -= action;
        }

        /// <summary>
        /// Добавляет подписку на событие изменения значения свойства для каждого элемента коллекции
        /// </summary>
        /// <param name="actions">Коллекция элементов для добавления</param>
        public void SubcribeAll(IEnumerable<Action<T>> actions)
        {
            foreach (Action<T> action in actions)
            {
                OnValueChanged += action;
            }
        }

        /// <summary>
        /// Отменяет подписку на событие изменения значения свойства для всех делегатов
        /// </summary>
        public void UnsubcribeAll()
        {
            if (OnValueChanged.IsNullObject())
            {
                return;
            }

            foreach (Delegate item in OnValueChanged.GetInvocationList())
            {
                OnValueChanged -= item as Action<T>;
            }
        }
    }
}
