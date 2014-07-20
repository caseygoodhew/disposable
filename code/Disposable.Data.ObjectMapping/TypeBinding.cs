using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

using Disposable.Data.ObjectMapping.Attributes;

namespace Disposable.Data.ObjectMapping
{
    /// <summary>
    /// Provides type mapping services and <see cref="IMemberMapper{TObject}"/> enumeration.
    /// </summary>
    /// <typeparam name="TObject">The type to map.</typeparam>
    internal class TypeBinding<TObject> : ITypeBinding<TObject> where TObject : class
    {
        private readonly IEnumerable<IMemberMapper<TObject>> members;

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
                             .Select(x => new MemberMapper<TObject>(x));

            beginMappingMethods = GetTypeMappingMethods<BeginMappingAttribute>(objType).ToList();

            endMappingMethods = GetTypeMappingMethods<EndMappingAttribute>(objType).ToList();
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<IMemberMapper<TObject>> GetEnumerator()
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
        /// Called before any data is automatically mapped against the object.
        /// </summary>
        /// <param name="obj">The object that is being mapped to.</param>
        /// <param name="mapperDataReader">The <see cref="MapperDataReader"/> that contains the data to map.</param>
        public void BeginMapping(TObject obj, MapperDataReader mapperDataReader)
        {
            InvokeTypeMappingMethods(beginMappingMethods, obj, mapperDataReader);
        }

        /// <summary>
        /// Called after all data is automatically mapped against the object.
        /// </summary>
        /// <param name="obj">The object that is being mapped to.</param>
        /// <param name="mapperDataReader">The <see cref="MapperDataReader"/> that contains the data to map.</param>
        public void EndMapping(TObject obj, MapperDataReader mapperDataReader)
        {
            InvokeTypeMappingMethods(endMappingMethods, obj, mapperDataReader);
        }

        private static void InvokeTypeMappingMethods(List<MethodInfo> methods, TObject obj, MapperDataReader mapperDataReader)
        {
            if (methods == null || !methods.Any())
            {
                return;
            }

            var parameters = new object[] { mapperDataReader };

            methods.ForEach(x => x.Invoke(obj, parameters));
        }

        private static IEnumerable<MethodInfo> GetTypeMappingMethods<T>(Type objType) where T : Attribute
        {
            return objType.GetMethods(BindingFlags.NonPublic)
                          .Where(x => x.GetCustomAttribute<T>(true) != null)
                          .Where(x => x.ReturnType == typeof(void))
                          .Where(x => x.GetParameters().Count() == 1)
                          .Where(x => x.GetParameters().All(p => p.ParameterType.IsAssignableFrom(typeof(IDataReader))));
        }
    }
}
