using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleFixture.Conventions;
using Xunit;

namespace SimpleFixture.Tests.FixtureTests.Complex
{
    public class DelegateFixtureTests
    {
        #region Generate Tests
        [Fact]
        public void Fixture_GenerateFuncInt_ReturnsWorkingFunc()
        {
            var fixture = new Fixture();

            var func = fixture.Generate<Func<int>>();

            Assert.NotNull(func);

            var intValue = func();
        }

        [Fact]
        public void Fixture_GenerateActionInt_ReturnsWorkingFunc()
        {
            var fixture = new Fixture();

            var func = fixture.Generate<Action<int>>();

            Assert.NotNull(func);

            func(5);
        }
        #endregion

        #region Locate Tests
        [Fact]
        public void Fixture_LocateFuncInt_ReturnsWorkingAction()
        {
            var fixture = new Fixture();

            var func = fixture.Locate<Func<int>>();

            Assert.NotNull(func);

            Assert.Equal(IntConvention.LocateValue,func());
        }

        [Fact]
        public void Fixture_LocateActionInt_ReturnsWorkingAction()
        {
            var fixture = new Fixture();

            var func = fixture.Locate<Action<int>>();

            Assert.NotNull(func);

            func(5);
        }
        #endregion
    }
}
