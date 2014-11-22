using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using SimpleFixture.Impl;

namespace SimpleFixture.Selenium
{
    public class SeleniumFixture : Fixture
    {
        public SeleniumFixture(IWebDriver webDriver, string baseAddress = null)
            : base(new SeleniumFixtureConfiguration())
        {
            Initialize(webDriver, baseAddress);
        }

        private void Initialize(IWebDriver webDriver, string baseAddress)
        {
            Return(webDriver);
            Return(baseAddress).WhenNamed("BaseAddress");

            Behavior.Add((r, o) =>
                         {
                             if (o.GetType().IsValueType || o is string)
                             {
                                 return o;
                             }

                             PageFactory.InitElements(webDriver, o);

                             return o;
                         });

            Behavior.Add(ImportPropertiesOnLocate);
        }

        private object ImportPropertiesOnLocate(DataRequest r, object o)
        {
            if (o.GetType().IsValueType || o is string || r.Populate)
            {
                return o;
            }

            IModelService modelService = Configuration.Locate<IModelService>();

            TypePopulator typePopulator = new TypePopulator(Configuration.Locate<IConstraintHelper>(), new ImportSeleniumTypePropertySelector(Configuration.Locate<IConstraintHelper>()));

            typePopulator.Populate(o, r, modelService.GetModel(r.RequestedType));

            return 0;
        }
    }
}
