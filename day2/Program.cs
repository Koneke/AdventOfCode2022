Console.WriteLine(
    String
        .Join(" ", File.ReadAllLines("input.txt"))
        .Split(" ")
        .Select(x => x switch {
            "A" or "X" => 0,
            "B" or "Y" => 1,
            "C" or "Z" => 2,
            _ => throw new ArgumentOutOfRangeException()
        })
        .Chunk(2)
        .Select(xs => ((3 + xs[1] - xs[0]) % 3) switch {
            0 => 4 + xs[1],
            1 => 7 + xs[1],
            2 => 1 + xs[1],
            _ => throw new ArgumentOutOfRangeException()
        })
        .Sum());