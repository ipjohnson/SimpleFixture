using System;
using SimpleFixture.Conventions;
using SimpleFixture.Tests.Classes;
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

        public delegate ImportSomeClass ImportSomeClassFunc(string stringValue, int intValue); 

        [Fact]
        public void Fixture_Locate_Delegate()
        {
            var fixture = new Fixture();

            var func = fixture.Generate<ImportSomeClassFunc>();

            var instance = func("Hello", 15);

            Assert.NotNull(instance);
            Assert.Equal("Hello", instance.SomeClass.StringValue);
            Assert.Equal(15, instance.SomeClass.IntValue);
        }

        public delegate ImportSomeClass ImportSomeClass2Func(string stringValue);

        [Fact]
        public void Fixture_Locate_Delegate_One_Arg()
        {
            var fixture = new Fixture();

            var func = fixture.Generate<ImportSomeClass2Func>(constraints: new { intValue = 20});

            var instance = func("Hello");

            Assert.NotNull(instance);
            Assert.Equal("Hello", instance.SomeClass.StringValue);
            Assert.Equal(20, instance.SomeClass.IntValue);
        }

        #endregion
    }
}
