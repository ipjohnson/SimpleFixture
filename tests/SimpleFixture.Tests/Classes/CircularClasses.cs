using System.Collections.Generic;

namespace SimpleFixture.Tests.Classes
{
    /// <summary>
    /// THese classes are for issue #16
    /// </summary>
    public partial class Order
    {
        public int Id { get; set; }

        public System.Guid AdministrationId { get; set; }

        public virtual Administration Administration { get; set; }
    }

    public partial class Administration
    {
        public System.Guid Id { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }

    public partial class ParentClass
    {
        public ChildClass ChildClass { get; set; }
    }

    public class ChildClass
    {
        public ParentClass ParentClass { get; set; }
    }

    public interface IParentInterfaceClass
    {
        ChildInterfaceClass Child { get; }
    }

    public class ParentInterfaceClass : IParentInterfaceClass
    {
        public ChildInterfaceClass Child { get; set; }
    }

    public class ChildInterfaceClass
    {
        public ChildInterfaceClass(IParentInterfaceClass parent)
        {
            Parent = parent;
        }

        public IParentInterfaceClass Parent { get; }
    }
}
