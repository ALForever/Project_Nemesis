using Assets.Scripts.CSharpClasses.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.CSharpClasses.Inventory
{
    public class InventoryBase<T> where T : BaseScriptableObject
    {
        private readonly Dictionary<string, T> m_items = new();
        private string m_currentItemKey;

        public int Count => m_items.Count;
        public T CurrentItem => m_currentItemKey != null ? m_items[m_currentItemKey] : null;

        public event Action<T> OnItemAdd;
        public event Action<T> OnItemRemove;
        public event Action<T> OnItemChanged;

        public InventoryBase()
        {
            m_currentItemKey = null;
        }

        public InventoryBase(IEnumerable<T> items)
        {
            AddRange(items);
        }

        protected void SetDefaultKey()
        {
            m_currentItemKey = m_items.Keys.First();
        }

        public void AddRange(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                TryAddItem(item);
            }
            m_currentItemKey = m_items.Keys.First();
        }

        public virtual bool TryAddItem(T item)
        {
            if (item.Name.IsNullOrWhiteSpace())
            {
                return false;
            }

            if (!m_items.TryAdd(item.Name, item))
            {
                return false;
            }
            OnItemAdd?.Invoke(item);
            return true;
        }

        public void GoToNextItem()
        {
            if (m_items.Count == 0 || !m_items.ContainsKey(m_currentItemKey))
            {
                return;
            }

            List<string> keys = m_items.Keys.ToList();
            int index = keys.IndexOf(m_currentItemKey) + 1;
            m_currentItemKey = index >= m_items.Count ? keys[0] : keys[index];

            T item = m_items[m_currentItemKey];

            OnItemChanged?.Invoke(item);
        }

        public void RemoveCurrentItem()
        {
            RemoveItem(m_currentItemKey);
        }

        public void RemoveItem(T item)
        {
            RemoveItem(item.Name);
        }

        public void RemoveItem(string key)
        {
            m_items.Remove(key, out T removedItem);
            OnItemRemove?.Invoke(removedItem);
        }
    }
}
