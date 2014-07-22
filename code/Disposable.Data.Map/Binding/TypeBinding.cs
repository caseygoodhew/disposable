using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

using Disposable.Common.ServiceLocator;
using Disposable.Data.Map.Attributes;
using Disposable.Data.Map.DataSource;

namespace Disposable.Data.Map.Binding
{
    /// <summary>
    /// Generic type binding and mapping decoration with <see cref="IMemberBinding{TObject}"/> enumeration.
    /// </summary>
    /// <typeparam name="TObject">The type to bind to.</typeparam>
    internal class TypeBinding<TObject> : ITypeBinding<TObject> where TObject : class
    {
        private readonly Lazy<IMemberBindingFactory> memberBindingFactory = 
            new Lazy<IMemberBindingFactory>(() => Locator.Current.Instance<IMemberBindingFactory>());
        
        private readonly IEnumerable<IMemberBinding<TObject>> members;

        private readonly List<MethodInfo> beginMappingMethods;
        
        private readonly List<MethodInfo> endMappingMethods;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="TypeBinding{TObject}"/> class.
        /// </summary>
        internal TypeBinding()
        {
            var objType = typeof(TObject);
            
            members = objType.GetMembers(BindingFlags.NonPublic)
                             .Where(x => !x.GetCustomAttributes(typeof(NoMapAttribute), true).Any())
                             .Select(memberBindingFactory.Value.Get<TObject>);

            beginMappingMethods = GetTypeMappingMethods<BeginMapAttribute>(objType).ToList();

            endMappingMethods = GetTypeMappingMethods<EndMapAttribute>(objType).ToList();
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<IMemberBinding<TObject>> GetEnumerator()
        {
            return members.GetEnumerator();
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>The enumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Called before automatic mapping begins.
        /// </summary>
        /// <param name="obj">The object that is being mapped to.</param>
        /// <param name="dataSourceReader">The <see cref="IDataSourceReader"/> that contains the data to map.</param>
        public void BeginMapping(TObject obj, IDataSourceReader dataSourceReader)
        {
            InvokeTypeMappingMethods(beginMappingMethods, obj, dataSourceReader);
        }

        /// <summary>
        /// Called before automatic mapping begins.
        /// </summary>
        /// <param name="obj">The object that is being mapped to.</param>
        /// <param name="dataSourceReader">The <see cref="IDataSourceReader"/> that contains the data to map.</param>
        public void EndMapping(TObject obj, IDataSourceReader dataSourceReader)
        {
            InvokeTypeMappingMethods(endMappingMethods, obj, dataSourceReader);
        }

        private static void InvokeTypeMappingMethods(List<MethodInfo> methods, TObject obj, IDataSourceReader dataSourceReader)
        {
            if (methods == null || !methods.Any())
            {
                return;
            }

            var parameters = new object[] { dataSourceReader };

            methods.ForEach(x => x.Invoke(obj, parameters));
        }

        private static IEnumerable<MethodInfo> GetTypeMappingMethods<T>(IReflect objType) where T : Attribute
        {
            return objType.GetMethods(BindingFlags.NonPublic)
                          .Where(x => x.GetCustomAttribute<T>(true) != null)
                          .Where(x => x.ReturnType == typeof(void))
                          .Where(x => x.GetParameters().Count() == 1)
                          .Where(x => x.GetParameters().All(p => p.ParameterType.IsAssignableFrom(typeof(IDataReader))));
        }
    }
}
