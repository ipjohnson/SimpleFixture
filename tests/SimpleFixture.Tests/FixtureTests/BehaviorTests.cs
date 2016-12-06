using SimpleFixture.Tests.Classes;
using Xunit;

namespace SimpleFixture.Tests.FixtureTests
{
    public class BehaviorTests
    {
        #region Behavior Locate Tests
        [Fact]
        public void Fixture_AddBehavior_AppliesToLocateInt()
        {
            var fixture = new Fixture();

            int behaviorCalled = 0;

            fixture.Behavior.Add((r, o) => { behaviorCalled++; return o; });

            var value = fixture.Locate<int>();

            Assert.Equal(1, behaviorCalled);
        }

        [Fact]
        public void Fixture_AddBehavior_AppliesToLocateComplex()
        {
            var fixture = new Fixture();

            int behaviorCalled = 0;

            fixture.Behavior.Add((r, o) => { behaviorCalled++; return o; });

            var value = fixture.Locate<ImportSomeClass>();

            Assert.Equal(2, behaviorCalled);
        }


        [Fact]
        public void Fixture_AddBehaviorGeneric_AppliesToLocateComplex()
        {
            var fixture = new Fixture();

            int behaviorCalled = 0;

            fixture.Behavior.Add<SomeClass>((r, o) => { behaviorCalled++; return o; });

            var value = fixture.Locate<ImportSomeClass>();

            Assert.Equal(1, behaviorCalled);
        }

        [Fact]
        public void Fixture_BehaviorWhen_AppliesCorrectly()
        {
            var fixture = new Fixture();

            bool apply = false;
            int behaviorCalled = 0;

            fixture.Behavior.Add<SomeClass>((r, o) =>
                                            {
                                                behaviorCalled++;
                                                return o;
                                            })
                                            .When((r,o) => apply);

            var instance = fixture.Generate<SomeClass>();

            Assert.Equal(0, behaviorCalled);

            apply = true;

            instance = fixture.Generate<SomeClass>();

            Assert.Equal(1, behaviorCalled);
        }
        #endregion
    }
}
