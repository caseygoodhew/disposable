using System;
using System.Reflection;

namespace Disposable.Data.ObjectMapping
{
    /*public static class ObjectMapperFactory
    {
        public static object Create<T>()
        {
            var typeT = typeof(T);
            if (typeT.IsClass && typeT.GetConstructor(Type.EmptyTypes) != null)
            {
                var t = this.GetType();
                var m = t.GetMethod("TestTwo", BindingFlags.NonPublic | BindingFlags.Instance);
                var g = m.MakeGenericMethod(typeT);
                g.Invoke(this, null);
            }
        }
    }*/
}
