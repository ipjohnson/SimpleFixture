using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SimpleFixture.Impl;

namespace SimpleFixture.Conventions
{
    public class EnumConvention : IConvention
    {
		private readonly IRandomDataGeneratorService _dataGenerator;


		public EnumConvention(IRandomDataGeneratorService dataGenerator, IConstraintHelper constraintHelper)
        {
            _dataGenerator = dataGenerator;
        }

	    public ConventionPriority Priority
	    {
		    get { return ConventionPriority.Low; }
	    }

        public event EventHandler<PriorityChangedEventArgs> PriorityChanged;

        public object GenerateData(DataRequest request)
        {
			if(request.RequestedType.GetTypeInfo().IsEnum)
			{
				if(!request.Populate)
				{
					return Enum.GetValues(request.RequestedType)
					           .Cast<object>()
					           .First();
				}

				return _dataGenerator.NextEnum(request.RequestedType);
			}

	        return Convention.NoValue;
        }
    }
}
