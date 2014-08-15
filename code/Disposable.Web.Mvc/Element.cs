using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

using Disposable.Common.Extensions;

namespace Disposable.Web.Mvc
{
    public abstract class ElementBase : IEnumerable<ElementAttribute>
    {
        private readonly Dictionary<string, Action<string>> _wellKnownAttributeHandlers;

        private readonly ElementAttributeCollection attributes = new ElementAttributeCollection();

        protected ElementBase(string tag, IEnumerable<KeyValuePair<string, Action<object>>> wellKnownAttributeHandlers = null)
        {
            _tag = tag;
            _wellKnownAttributeHandlers = new Dictionary<string, Action<string>>(StringComparer.InvariantCultureIgnoreCase);
        }

        protected void AddHandler(string attributeName, Action<object> handlerAction)
        {
            _wellKnownAttributeHandlers.Add(attributeName, handlerAction);
        }

        protected void AddHandler(string attributeName, Action<string> handlerAction)
        {
            _wellKnownAttributeHandlers.Add(attributeName, handlerAction);
        }

        public string Attribute(string name)
        {
            return attributes.Get(name);
        }

        public void Attribute(string name, object value, bool append = true)
        {
            Action<string> handler;
            if (_wellKnownAttributeHandlers.TryGetValue(name, out handler))
            {
                handler.Invoke(value.ToString());
                return;
            }

            if (append)
            {
                AppendAttribute(name, value);
            }
            else
            {
                SetAttribute(name, value);
            }
        }

        private string _tag;

        public string Tag()
        {
            return _tag;
        }

        public void Tag(string value)
        {
            _tag = value.ToLower().Trim();
        }

        protected void SetAttribute(string name, object value)
        {
            attributes.Set(name, value);
        }

        protected void AppendAttribute(string name, object value)
        {
            attributes.Append(name, value);
        }

        public IEnumerator<ElementAttribute> GetEnumerator()
        {
            return attributes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public MvcHtmlString ToMvcHtmlString()
        {
            var attributeList = this.Select(x => string.Format(@"{0}=""{1}""", x.Name, x.Value.Concat(" "))).Concat(" ");
            return MvcHtmlString.Create(string.Format(@"<{0} {1}>element</{0}>", _tag, attributeList));
        }
    }

    public class Element : ElementBase
    {
        private static readonly string _id = "id";

        private static readonly string _class = "class";

        public Element() : this("div")
        {
        }

        public Element(string tag) : base(tag)
        {
            AddHandler(_id, Id);
            AddHandler(_class, Class);
        }

        public string Id()
        {
            return Attribute(_id);
        }

        public void Id(string value)
        {
            SetAttribute(_id, value);
        }
        
        public string Class()
        {
            return Attribute(_class);
        }

        public void Class(string value)
        {
            AppendAttribute(_class, value);
        }
    }

    public class ElementAttribute
    {
        public string Name;

        public readonly List<string> Value = new List<string>();

        internal ElementAttribute(string name)
        {
            Name = name;
        }
    }

    public class ElementAttributeCollection : IEnumerable<ElementAttribute>
    {
        private readonly Dictionary<string, ElementAttribute> dictionary = new Dictionary<string, ElementAttribute>(StringComparer.InvariantCultureIgnoreCase);

        public void Append(string name, object value)
        {
            Append(GetElementAttribute(name), value);
        }

        public void Set(string name, object value)
        {
            var elementAttribute = GetElementAttribute(name);
            elementAttribute.Value.Clear();
            Append(elementAttribute, value);
        }

        private void Append(ElementAttribute elementAttribute, object value)
        {
            elementAttribute.Value.Add(value.ToString().Trim());
        }

        public string Get(string name)
        {
            return GetElementAttribute(name).Value.Concat(" ");
        }

        public IEnumerator<ElementAttribute> GetEnumerator()
        {
            return dictionary.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private ElementAttribute GetElementAttribute(string name)
        {
            name = name.Trim();

            ElementAttribute elementAttribute;

            if (!dictionary.TryGetValue(name, out elementAttribute))
            {
                elementAttribute = new ElementAttribute(name);
                dictionary.Add(name, elementAttribute);
            }

            return elementAttribute;
        }
    }
}
