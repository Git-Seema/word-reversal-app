# Word Reversal App – Quick Guide

A tiny **.NET 8** console programme that reverses the words in each line you give it.

---

## 1. Requirements

* Install the **.NET 8 SDK** – [https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download)

---

## 2. Run the app

```bash
# From the folder that contains WordReversalApp.sln

dotnet run --project WordReversalApp
```

Sample session:

```text
How many sentences? 3
Enter sentence #1: Hello world
Enter sentence #2: summer in Durban
Enter sentence #3: biltong and braai

Results:
Case 1: world hello
Case 2: Durban in summer
Case 3: braai and biltong
```

---

## 3. Run the unit tests

```bash
dotnet test
```

You should see all tests passing with a green tick.

---

## 4. Project layout

```text
WordReversalApp/        ← console code (Program.cs)
WordReversalApp.Tests/  ← xUnit tests
```

---

Enjoy reversing words! 🎉
