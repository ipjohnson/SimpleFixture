namespace SimpleFixture.Tests.Classes
{
    public class ImportSomeClass
    {
        private readonly SomeClass _someClass;

        public ImportSomeClass(SomeClass someClass)
        {
            _someClass = someClass;
        }

        public SomeClass SomeClass => _someClass;
    }
}
