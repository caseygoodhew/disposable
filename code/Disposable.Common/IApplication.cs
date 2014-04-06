namespace Disposable.Common
{
    /// <summary>
    /// Interface of the application details
    /// </summary>
    public interface IApplication
    {
        /// <summary>
        /// Gets the application name
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// Gets the application description
        /// </summary>
        string Description { get; }
    }
}
