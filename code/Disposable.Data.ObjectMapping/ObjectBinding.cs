using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Disposable.Data.ObjectMapping.Attributes;

namespace Disposable.Data.ObjectMapping
{
    internal class ObjectBinding<T> : IObjectBinding<T> where T : class
    {
        private readonly IEnumerable<MemberMapper<T>> members;
        
        internal ObjectBinding()
        {
            members = typeof(T).GetMembers(BindingFlags.NonPublic)
                               .Where(x => !x.GetCustomAttributes(typeof(NoMapAttribute), true).Any())
                               .Select(x => new MemberMapper<T>(x));
        }
        
        public IEnumerator<IMemberMapper<T>> GetEnumerator()
        {
            return members.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
