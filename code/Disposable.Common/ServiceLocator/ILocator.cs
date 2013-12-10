namespace Disposable.Common.ServiceLocator
{
    /// <summary>
    /// Service locator function instance location services
    /// </summary>
    public interface ILocator
    {
        /// <summary>
        /// Gets the implementation of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Instance<T>() where T : class;
    }
}
