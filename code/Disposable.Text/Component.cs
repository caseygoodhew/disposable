using System;

namespace Disposable.Text
{
    public abstract class Component
    {
        protected readonly string value;
        
        protected readonly Lazy<string> upperLazy;

        protected readonly Lazy<string> lowerLazy;

        protected readonly Lazy<string> properLazy;

        protected Component(string value, Func<string> upperFactory, Func<string> lowerFactory, Func<string> properFactory)
        {
            this.value = value;
            upperLazy = new Lazy<string>(upperFactory);
            lowerLazy = new Lazy<string>(lowerFactory);
            properLazy = new Lazy<string>(properFactory);
        }

        public string Value()
        {
            return value;
        }
        
        public string Upper()
        {
            return upperLazy.Value;
        }

        public string Lower()
        {
            return lowerLazy.Value;
        }

        public string Proper()
        {
            return properLazy.Value;
        }
    }
}
