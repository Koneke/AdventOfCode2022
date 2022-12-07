Console.WriteLine(
    new[] {"input.txt"}
        .Select(file =>
            File.ReadAllLines(file)
                .Aggregate((
                        sizes: (IEnumerable<(string name, int size)>) new List<(string name, int size)> {("/", 0)},
                        location: (IEnumerable<string>) new List<string> {"/"}),
                    (current, next) =>
                        next.StartsWith("$")
                            ? next[2..] switch
                            {
                                "ls" => current,
                                "cd /" => (current.sizes, location: new List<string> {"/"}),
                                "cd .." => (current.sizes, location: current.location.SkipLast(1)),
                                _ => (sizes: current.sizes.Append((
                                        name: $"{string.Join("/", current.location)}/{next[5..]}",
                                        size: 0)),
                                    location: current.location.Append(next[5..]))
                            }
                            : next.Split(" ")[0].StartsWith("dir")
                                ? current
                                : (
                                    sizes: current.sizes
                                        .Select(dir => string.Join("/", current.location).StartsWith(dir.name)
                                            ? (dir.name, size: dir.size + int.Parse(next.Split(' ')[0]))
                                            : dir),
                                    current.location)))
        // part 1:
        // .Select(result => result
        //     .sizes
        //     .Select(dir => dir.size)
        //     .Where(size => size <= 100_000)
        //     .Sum())
        // .First()
        // part 2:
        .Select(result => result.sizes
            .OrderBy(dir => dir.size)
            .SkipWhile(dir => 70_000_000 - result.sizes.First(d => d.name == "/").size + dir.size < 30_000_000)
            .First())
        .First()
        .size
);