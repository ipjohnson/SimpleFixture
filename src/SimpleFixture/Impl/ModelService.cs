using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFixture.Impl
{
    public interface IModelService
    {
        ComplexModel GetModel(Type modelType);
    }

    public class ModelService : IModelService
    {
        private readonly Dictionary<Type, ComplexModel> _models = new Dictionary<Type, ComplexModel>();

        public ComplexModel GetModel(Type modelType)
        {
            ComplexModel model;

            if (!_models.TryGetValue(modelType, out model))
            {
                model = new ComplexModel();

                _models[modelType] = model;
            }

            return model;
        }
    }
}
