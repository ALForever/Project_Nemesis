using System;
using System.Collections.Generic;

namespace Assets.Scripts.CSharpClasses.Storage
{
    public class GlobalStorage
    {
        private readonly Dictionary<string, StorageElement> m_globalStorage;

        public GlobalStorage()
        {
            m_globalStorage = new Dictionary<string, StorageElement>();
        }

        public bool TryGetValue(string key, out StorageElement storageElement)
        {
            return m_globalStorage.TryGetValue(key, out storageElement);
        }

        public bool TryAddValue(string key, object obj, Type type)
        {
            if (!m_globalStorage.TryGetValue(key, out _))
            {
                StorageElement storageElement = new(obj, type);
                m_globalStorage.Add(key, storageElement);
                return true;
            }
            return false;
        }

        public bool TrySetValue(string key, object obj, Type type)
        {
            if (m_globalStorage.TryGetValue(key, out StorageElement storageElement))
            {
                storageElement.Value = obj;
                storageElement.ElementType = type;
                return true;
            }
            return false;
        }
    }
}
