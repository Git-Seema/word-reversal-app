using System.Collections.Generic;
using WordReversalApp;

namespace WordReversalApp.Tests;

/// Test double that feeds canned input to Runner and records output.
internal sealed class FakeConsoleIO : IConsoleIO
{
    private readonly Queue<string> _in = new();
    public readonly List<string> Out = new();

    public void AddInput(params string[] lines)
    {
        foreach (var l in lines) _in.Enqueue(l);
    }

    public string ReadLine() => _in.Count > 0 ? _in.Dequeue() : string.Empty;
    public void Write(string text) => Out.Add(text);
    public void WriteLine(string txt) => Out.Add(txt);
}
