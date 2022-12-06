using System.Text.RegularExpressions;

Console.WriteLine(
    string.Join("",
        string.Join("\n", File.ReadAllLines("input.txt")).Split("\n\n")
            .Chunk(2)
            .SelectMany(parts =>
                parts[1]
                    .Split("\n")
                    .SelectMany(operation =>
                        new Regex(@"move (\d+) from (\d+) to (\d+)")
                            .Matches(operation)
                            .Select(match =>
                                match.Groups.Values
                                    .Skip(1)
                                    .Select(group => int.Parse(group.Value))
                                    .ToArray())
                            .ToArray())
                    .Aggregate(
                        parts[0]
                            .Split("\n")
                            .TakeWhile(line => line.Contains('['))
                            .Select(line => line.Chunk(4).Select(box => box.ElementAt(1)))
                            .Aggregate(Enumerable.Range(0, parts[0]
                                        .Split("\n")
                                        .Last()
                                        .Chunk(4)
                                        .Count())
                                    .Select(_ => (IEnumerable<char>) new List<char>()),
                                (stacks, line) => stacks
                                    .Select((s, i) =>
                                        line.ElementAt(i) != ' ' ? s.Append(line.ElementAt(i)) : s)
                            ),
                        (stacks, line) =>
                            stacks
                                .Select((s, i) => i == line[1] - 1
                                    ? s.Skip(line[0])
                                    : i == line[2] - 1
                                        ? stacks.ElementAt(line[1] - 1).Take(line[0])
                                            // part 1:
                                            // .Reverse()
                                            .Concat(s)
                                            .ToArray()
                                        : s)
                                .ToArray()))
            .Select(s => s.FirstOrDefault(' '))));