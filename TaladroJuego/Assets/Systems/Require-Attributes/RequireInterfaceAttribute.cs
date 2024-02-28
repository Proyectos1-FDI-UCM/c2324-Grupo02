using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace RequireAttributes
{
    [AttributeUsage(validOn: AttributeTargets.Field, AllowMultiple = true)]
    public class RequireInterfaceAttribute : PropertyAttribute
    {
        public Type RequiredType { get; }
        public Type SearchType { get; }

        public RequireInterfaceAttribute(Type requiredType, Type searchType = null)
        {
            RequiredType = requiredType;
            SearchType = searchType ?? typeof(Object);
        }
    }
}