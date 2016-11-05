namespace Shuttle.Core.Castle.Tests
{
    public class DoSomething : IDoSomething
    {
        public ISomeDependency SomeDependency {
            get { return null; }
        }
    }
}