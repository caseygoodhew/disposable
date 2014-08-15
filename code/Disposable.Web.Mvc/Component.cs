namespace Disposable.Web.Mvc
{
    public class Component : IComponent
    {
        public Component() : this(new Element())
        {
            
        }

        protected Component(Element element)
        {
            Element = element;
        }
        
        protected internal Element Element;
    }
}