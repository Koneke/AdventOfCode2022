Console.WriteLine(
    File.ReadAllLines("input.txt")
        .SelectMany(r =>
            r[..^(r.Length / 2)]
                .Intersect(r[^(r.Length / 2)..])
                .Distinct())
        .Select(c => c - (c >= 97 ? 96 : 38))
        .Sum());