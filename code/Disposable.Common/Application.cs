namespace Disposable.Common
{
    public class Application : IApplication
    {
        public Application(string name, string description)
        {
            Name = name;
            Description = description;
        }
        
        public string Name { get; private set; }
        
        public string Description { get; private set; }
    }
}