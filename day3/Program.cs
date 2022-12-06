Console.WriteLine(
    File.ReadAllLines("input.txt")
        // part 1:
        // .SelectMany(r =>
        //     r[..^(r.Length / 2)]
        //         .Intersect(r[^(r.Length / 2)..])
        //         .Distinct())
        // part 2:
        .Chunk(3)
        .SelectMany(chunk => chunk
            .Aggregate<IEnumerable<char>>((p, n) => p.Intersect(n)))
        // both:
        .Select(c => c - (c >= 97 ? 96 : 38))
        .Sum());