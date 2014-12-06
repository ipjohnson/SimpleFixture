using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFixture
{
    public class DataRequest
    {
        public DataRequest(DataRequest parentRequest, Type requestedType) : this(parentRequest,
                                                                                 parentRequest.Fixture,
                                                                                 requestedType,
                                                                                 parentRequest.RequestName,
                                                                                 parentRequest.Populate,
                                                                                 parentRequest.Constraints,
                                                                                 parentRequest.ExtraInfo)
        {
            
        }

        public DataRequest(DataRequest parentRequest, Fixture fixture, Type requestedType, string requestName, bool populate, object constraints, object extraInfo)
        {
            ParentRequest = parentRequest;
            Fixture = fixture;
            RequestedType = requestedType;
            RequestName = requestName ?? "";
            Populate = populate;
            Constraints = constraints;
            ExtraInfo = extraInfo;

            if (ParentRequest != null)
            {
                RequestDepth = parentRequest.RequestDepth + 1;
            }
            else
            {
                RequestDepth = 1;
            }
        }

        public DataRequest ParentRequest { get; private set; }

        public Fixture Fixture { get; private set; }

        public string RequestName { get; private set; }

        public Type RequestedType { get; private set; }

        public bool Populate { get; private set; }

        public object Constraints { get; private set; }

        public object ExtraInfo { get; private set; }

        public int RequestDepth { get; private set; }
    }
}
