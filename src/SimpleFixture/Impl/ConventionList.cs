using System.Collections.Generic;

namespace SimpleFixture.Impl
{
    public interface IConventionList
    {
        void AddConvention(IConvention convention);

        bool TryGetValue(DataRequest dataRequest, out object value);
    }

    public class ConventionList : IConventionList
    {
        private readonly List<IConvention> _conventions = new List<IConvention>(); 

        public void AddConvention(IConvention convention)
        {
            InternalAddConvention(convention);

            convention.PriorityChanged += 
                (sender, args) =>
                {
                    _conventions.Remove(convention);

                    InternalAddConvention(convention);
                };
        }

        public bool TryGetValue(DataRequest dataRequest, out object value)
        {
            bool returnValue = false;

            value = null;

            foreach (IConvention convention in _conventions)
            {
                object newValue = convention.GenerateData(dataRequest);

                if (newValue == Convention.NoValue)
                {
                    continue;
                }

                value = newValue;
                returnValue = true;
                break;
            }

            return returnValue;
        }

        private void InternalAddConvention(IConvention convention)
        {
            for (int i = 0; i < _conventions.Count; i++)
            {
                if (convention.Priority > _conventions[i].Priority)
                {
                    continue;
                }

                _conventions.Insert(i, convention);
                return;
            }

            _conventions.Add(convention);
        }
    }
}
