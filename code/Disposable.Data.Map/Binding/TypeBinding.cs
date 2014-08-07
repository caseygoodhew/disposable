using Disposable.Common.ServiceLocator;
using Disposable.Data.Map.Attributes;
using Disposable.Data.Map.DataSource;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

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
        
        private readonly IList<IMemberBinding<TObject>> members;

        private readonly List<MethodInfo> beginMappingMethods;
        
        private readonly List<MethodInfo> endMappingMethods;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeBinding{TObject}"/> class.
        /// </summary>
        internal TypeBinding()
        {
            var objType = typeof(TObject);

            var flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

            var fields = objType.GetFields(flags).Where(x => !x.IsPrivate);
            var properties = objType.GetProperties(flags).Where(x => x.CanRead && x.CanWrite);
           
            members = Enumerable.Empty<MemberInfo>()
                                .Concat(fields)
                                .Concat(properties)
                                .Where(x => !x.IsDefined(typeof(NoMapAttribute)))
                                .Select(memberBindingFactory.Value.Get<TObject>)
                                .ToList();
            
            beginMappingMethods = GetTypeMappingMethods<BeforeMapAttribute>(objType).ToList();

            endMappingMethods = GetTypeMappingMethods<AfterMapAttribute>(objType).ToList();
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
        /// Called after automatic mapping begins.
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

        private static IEnumerable<MethodInfo> GetTypeMappingMethods<TAttributeName>(Type objType) where TAttributeName : Attribute
        {
            var result = objType.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public )
                                .Where(x => x.GetCustomAttribute<TAttributeName>(true) != null)
                                .ToList();

            if (result.Any(x => x.ReturnType != typeof(void)))
            {
                throw new FormatException(string.Format("{0} contains a method flagged with the {1} attribute that uses a return type other than 'void'.", objType.Name, typeof(TAttributeName).Name));
            }

            if (result.Any(x => x.GetParameters().Count() != 1))
            {
                throw new FormatException(string.Format("{0} contains a method flagged with the {1} attribute that does not take exactly one parameter.", objType.Name, typeof(TAttributeName).Name));
            }

            if (result.Any(x => x.GetParameters().Any(p => !p.ParameterType.IsAssignableFrom(typeof(IDataReader)))))
            {
                throw new FormatException(string.Format("{0} contains a method flagged with the {1} attribute that has a parameter than is not assignable from IDataReader.", objType.Name, typeof(TAttributeName).Name));
            }

            return result;
        }
    }
}
