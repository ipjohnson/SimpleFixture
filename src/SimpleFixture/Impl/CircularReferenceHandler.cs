using System;

namespace SimpleFixture.Impl
{
    /// <summary>
    /// interface to handle circular references
    /// </summary>
    public interface ICircularReferenceHandler
    {
        /// <summary>
        /// Handle circular reference
        /// </summary>
        /// <param name="request">data request that has caused the circular reference</param>
        /// <returns>data request value</returns>
        object HandleCircularReference(DataRequest request);
    }

    /// <summary>
    /// Default implementation of ICircularReferenceHandler, throws an exception
    /// </summary>
    public class CircularReferenceHandler : ICircularReferenceHandler
    {
        /// <summary>
        /// Handle circular reference
        /// </summary>
        /// <param name="request">data request that has caused the circular reference</param>
        /// <returns>data request value</returns>
        public object HandleCircularReference(DataRequest request)
        {
            throw new Exception("Circular reference detected creating " + request.RequestedType.FullName);
        }
    }
}
