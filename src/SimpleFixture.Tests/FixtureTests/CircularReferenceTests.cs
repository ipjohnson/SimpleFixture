using SimpleFixture.Tests.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SimpleFixture.Tests.FixtureTests
{
    public class CircularReferenceTests
    {
        [Fact]
        public void Fixture_CircularReferenceHandling_MaxDepth_Throws()
        {
            var fixture = new Fixture();

            Assert.ThrowsAny<Exception>(() => fixture.Generate<Order>());
        }

        [Fact]
        public void Fixture_CircularRefenceHandling_MaxDepth_WithSkip()
        {
            var fixture = new Fixture();

            var order = fixture.Generate<Order>(constraints: new { _skipProperties = new[] { "Order" } });

            Assert.NotNull(order);
            Assert.NotNull(order.Administration);
            Assert.Null(order.Administration.Order);
        }

        [Fact]
        public void Fixture_CircularReferenceHandling_MaxDepth_AssignNull()
        {
            var fixture = new Fixture();

            var order = fixture.Generate<Order>(constraints: new { Administration = (Administration)null });

            Assert.NotNull(order);
            Assert.Null(order.Administration);
        }

        [Fact]
        public void Fixture_CircularReferenceHandling_MaxDepth_HandWire()
        {
            var fixture = new Fixture();

            fixture.Return<ICollection<Order>>(request =>
            {
                var rootRequest = request;

                while (rootRequest.ParentRequest != null)
                {
                    rootRequest = rootRequest.ParentRequest;
                }

                return new List<Order> { rootRequest.Instance as Order };
            });

            var order = fixture.Generate<Order>();

            Assert.NotNull(order);
            Assert.NotNull(order.Administration);
            Assert.NotNull(order.Administration.Order);
            Assert.Equal(1, order.Administration.Order.Count);
            Assert.Same(order, order.Administration.Order.First());
        }

        [Fact]
        public void Fixture_CircularReferenceHandling_AutoWire_WithComplexList()
        {
            var fixture = new Fixture(DefaultFixtureConfiguration.AutoWire);

            var administration = fixture.Generate<Administration>();

            Assert.NotNull(administration);
            Assert.NotNull(administration.Order);
            Assert.True(administration.Order.Count > 0);

            foreach (var order in administration.Order)
            {
                Assert.Same(administration, order.Administration);
            }
        }

        [Fact]
        public void Fixture_CircularReferenceHandling_AutoWire_WithNestedList()
        {
            var fixture = new Fixture(DefaultFixtureConfiguration.AutoWire);

            var order = fixture.Generate<Order>();

            Assert.NotNull(order);
            Assert.NotNull(order.Administration);
            Assert.NotNull(order.Administration.Order);
            Assert.Equal(1, order.Administration.Order.Count);
            Assert.Same(order, order.Administration.Order.First());
        }

        [Fact]
        public void Fixture_CircularReferenceHandling_AutoWire_ParentChild()
        {
            var fixture = new Fixture(DefaultFixtureConfiguration.AutoWire);

            var parent = fixture.Generate<ParentClass>();

            Assert.NotNull(parent);
            Assert.NotNull(parent.ChildClass);
            Assert.Same(parent, parent.ChildClass.ParentClass);
        }

        [Fact]
        public void Fixture_CircularReferenceHandling_AutoWire_ParentChild_WithInterface()
        {
            var fixture = new Fixture(DefaultFixtureConfiguration.AutoWire);

            var parent = fixture.Generate<ParentInterfaceClass>();

            Assert.NotNull(parent);
            Assert.NotNull(parent.Child);
            Assert.Same(parent, parent.Child.Parent);
        }

        [Fact]
        public void Fixture_CircularReferenceHandling_Omit()
        {
            var fixture = new Fixture(DefaultFixtureConfiguration.OmitCircularReferences);

            var parent = fixture.Generate<ParentClass>();

            Assert.NotNull(parent);
            Assert.NotNull(parent.ChildClass);
            Assert.Null(parent.ChildClass.ParentClass);
        }
    }
}
