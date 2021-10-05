using Xunit;

namespace DryLib.xUnitTests;

public class ObjectExtensionsTest
{
    [Fact]
    public void DumpTest()
    {
        string s = "test";
        s.Dump();
    }
}