using System;

namespace Assets.Scripts.CSharpClasses.Storage
{
    public class StorageElement
    {
        public object Value { get; set; } = new object();
        public Type ElementType { get; set; }

        public StorageElement(object value, Type type)
        {
            Value = value;
            ElementType = type;
        }
    }
}
