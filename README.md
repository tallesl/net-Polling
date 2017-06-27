# Polling

[![][build-img]][build]
[![][nuget-img]][nuget]

[Polls] whatever you need.

[build]:     https://ci.appveyor.com/project/TallesL/net-Polling
[build-img]: https://ci.appveyor.com/api/projects/status/github/tallesl/net-Polling?svg=true
[nuget]:     https://www.nuget.org/packages/Polling
[nuget-img]: https://badge.fury.io/nu/Polling.svg
[Polls]:     https://en.wikipedia.org/wiki/Polling_(computer_science)

## Usage

```cs
using static PollingLibrary.Polling;

Console.WriteLine("Waiting 10 seconds for the file creation...");

try
{
    var content = Poll(() => File.Exists(Filename) ? File.ReadAllText(Filename) : null);
    Console.WriteLine("Here's the file contents: {0}", content);
}
catch (PollingTimeoutException)
{
    Console.WriteLine("Couldn't find the file!"); 
}
```
