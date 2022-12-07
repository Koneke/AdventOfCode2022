/*
Console.WriteLine(
    @"- / (dir)
  - a (dir)
    - e (dir)
      - i (file, size=584)
    - f (file, size=29116)
    - g (file, size=2557)
    - h.lst (file, size=62596)
  - b.txt (file, size=14848514)
  - c.dat (file, size=8504156)
  - d (dir)
    - j (file, size=4060174)
    - d.log (file, size=8033020)
    - d.ext (file, size=5626152)
    - k (file, size=7214296)"
        .Split("\n")
        .Aggregate(
            (sizes: (IEnumerable<(string name, int size)>) new List<(string, int)>(),
                above: (IEnumerable<(string name, int depth)>) new List<(string, int)>()),
            (state, next) => (
                sizes: next.Contains("dir")
                    ? state.sizes.Append((next.TrimStart().Split(" ")[1], 0))
                    : state.sizes
                        .Select(pair => state.above
                            .TakeWhile(above => above.depth < next.Length - next.TrimStart().Length)
                            .Any(parent => parent.name == pair.name)
                            ? (pair.name, pair.size + int.Parse(next.Trim().Split('=')[1][..^1]))
                            : pair),
                above: next.Contains("dir")
                    ? state.above
                        .TakeWhile(above => above.depth < next.Length - next.TrimStart().Length)
                        .Append((next.TrimStart().Split(" ")[1], next.Length - next.TrimStart().Length))
                    : state.above))
        .sizes
        .Where(size => size.size <= 100_000)
        .Sum(size => size.size));
*/