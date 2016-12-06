using System.Reflection;

namespace SimpleFixture.Attributes
{
    public interface IParameterInfoAware
    {
        ParameterInfo Parameter { get; set; }
    }
}
