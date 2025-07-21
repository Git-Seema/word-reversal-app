using Xunit;
using WordReversalApp;

namespace WordReversalApp.Tests;

public class WordReversalKataTests
{
    // Pure logic tests -------------------------------------------------------
    private readonly IWordReverser _sut = new SpaceSeparatedReverser();

    [Theory]
    [InlineData("this is a test", "test a is this")]
    [InlineData("single", "single")]
    [InlineData("  extra  spaces  ", "spaces extra")]
    public void Reverse_ReturnsExpected(string input, string expected)
        => Assert.Equal(expected, _sut.Reverse(input));

    [Fact]
    public void Reverse_Null_Throws()
        => Assert.Throws<ArgumentNullException>(() => _sut.Reverse(null!));

    // Integration test on Runner 
    [Fact]
    public void Runner_FullFlow_ProducesExpectedOutput()
    {
        var fake = new FakeConsoleIO();
        fake.AddInput("2",             
                      "hello world",   // sentence #1
                      "all your base");

        var runner = new Runner(fake, _sut);
        runner.Execute();

        var expected = new[]
        {
            "Enter the number of test cases: ",
            "Enter sentence #1: ",
            "Enter sentence #2: ",
            "",               // newline before "Results:"
            "Results:",
            "Case 1: world hello",
            "Case 2: base your all"
        };

        Assert.Equal(expected, fake.Out);
    }
}
