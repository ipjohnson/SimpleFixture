namespace SimpleFixture
{
    /// <summary>
    /// Fluent helper class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ExportAs<T>
    {
        private readonly Fixture _fixture;
        private readonly bool _isSingleton;
        private object _singleton;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="fixture"></param>
        /// <param name="isSingleton"></param>
        public ExportAs(Fixture fixture, bool isSingleton)
        {
            _fixture = fixture;
            _isSingleton = isSingleton;
        }

        /// <summary>
        /// As a specific implementation
        /// </summary>
        /// <typeparam name="TExport"></typeparam>
        /// <returns></returns>
        public ReturnConfiguration<TExport> As<TExport>()
        {
            return _fixture.Return(r =>
                                   {
                                       if (_singleton != null)
                                       {
                                           return (TExport)_singleton;
                                       }

                                       var newRequest = new DataRequest(r.ParentRequest,
                                           _fixture,
                                           typeof(T),
                                           r.DependencyType,
                                           r.RequestName,
                                           r.Populate,
                                           r.Constraints,
                                           r.ExtraInfo);

                                       var returnValue = (TExport)r.Fixture.Generate(newRequest);

                                       if (_isSingleton)
                                       {
                                           _singleton = returnValue;
                                       }

                                       return returnValue;
                                   });
        }
    }
}
