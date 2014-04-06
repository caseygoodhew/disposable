namespace Disposable.Common
{
    /// <summary>
    /// <see cref="IApplication"/> implementation
    /// </summary>
    public class Application : IApplication
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Application"/> class
        /// </summary>
        /// <param name="name">The application name</param>
        /// <param name="description">The application description</param>
        public Application(string name, string description)
        {
            Name = name;
            Description = description;
        }
        
        /// <summary>
        /// Gets the application name
        /// </summary>
        public string Name { get; private set; }
        
        /// <summary>
        /// Gets the application description
        /// </summary>
        public string Description { get; private set; }
    }
}