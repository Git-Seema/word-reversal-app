// ============================================================================
//  WordReversalApp
//
//  Purpose
//  • Accept N lines of text.
//  • Reverse the order of the words in each line.
//  • Display “Case x: …” lines after all input has been captured.
//
//  Assessment Notes
//  • Built with .NET 8 console host.
//  • Applies SOLID principles.
//  • Console I/O abstracted for unit testing.
// ============================================================================

#region Service Registration
// Register concrete classes for the required interfaces.
// The host resolves and manages lifetimes.
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host
    .CreateDefaultBuilder(args)
    .ConfigureServices(svc =>
    {
        svc.AddSingleton<IConsoleIO, ConsoleIO>();               // Console adapter
        svc.AddSingleton<IWordReverser, SpaceSeparatedReverser>(); // Core logic
        svc.AddSingleton<Runner>();                              // Application flow
    })
    .Build();
#endregion

// Start the application.
host.Services.GetRequiredService<Runner>().Execute();

// --------------------------------------------------------------------------
//  Runner
//
//  Responsibilities
//  • Prompt user for number of cases.
//  • Collect all input lines.
//  • Produce one result block at the end.
//
//  Dependence
//  • IConsoleIO        – input / output
//  • IWordReverser     – domain algorithm
// --------------------------------------------------------------------------
public sealed class Runner
{
    private readonly IConsoleIO _io;
    private readonly IWordReverser _reverser;

    public Runner(IConsoleIO io, IWordReverser reverser)
        => (_io, _reverser) = (io, reverser);

    public void Execute()
    {
        // Step 1 – Capture number of lines.
        int cases = PromptForCount();

        // Step 2 – Read each sentence.
        var sentences = new List<string>(cases);
        for (int i = 1; i <= cases; i++)
        {
            _io.Write($"Enter sentence #{i}: ");
            sentences.Add(_io.ReadLine());
        }

        // Step 3 – Display results.
        _io.WriteLine("");             // Blank line for clarity
        _io.WriteLine("Results:");
        for (int i = 0; i < sentences.Count; i++)
        {
            string reversed = _reverser.Reverse(sentences[i]);
            _io.WriteLine($"Case {i + 1}: {reversed}");
        }
    }

    // Prompt until a positive integer is supplied.
    private int PromptForCount()
    {
        while (true)
        {
            _io.Write("How many sentences do you want to reverse? ");
            string? value = _io.ReadLine()?.Trim();

            if (!string.IsNullOrEmpty(value) &&
                int.TryParse(value, out int count) &&
                count > 0)
            {
                return count;
            }

            _io.WriteLine("Please enter a number greater than zero.");
        }
    }
}

// --------------------------------------------------------------------------
//  IConsoleIO  – minimal interface for console operations.
// --------------------------------------------------------------------------
public interface IConsoleIO
{
    string ReadLine();
    void WriteLine(string line);
    void Write(string text);
}

// Implementation using System.Console.
public sealed class ConsoleIO : IConsoleIO
{
    public string ReadLine() => Console.ReadLine() ?? string.Empty;
    public void WriteLine(string txt) => Console.WriteLine(txt);
    public void Write(string txt) => Console.Write(txt);
}

// --------------------------------------------------------------------------
//  IWordReverser  – algorithm contract.
// --------------------------------------------------------------------------
public interface IWordReverser
{
    string Reverse(string sentence);
}

// Space‑separated implementation.
public sealed class SpaceSeparatedReverser : IWordReverser
{
    public string Reverse(string sentence)
    {
        if (sentence is null) throw new ArgumentNullException(nameof(sentence));

        // Split, reverse, and join.
        var words = sentence.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        Array.Reverse(words);
        return string.Join(' ', words);
    }
}
