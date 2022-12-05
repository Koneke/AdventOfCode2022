Console.WriteLine(
    String
        .Join(" ", File.ReadAllLines("input.txt"))
        .Split(" ")
        .Select(x => x switch
        {
            "A" or "X" => 0,
            "B" or "Y" => 1,
            "C" or "Z" => 2,
            _ => throw new ArgumentOutOfRangeException()
        })
        .Chunk(2)
        .Select(xs => (o: xs[0], y: xs[1]))
        // .Select(xs => (xs.o, y: (5 + xs.o - 2 * xs.y) % 3)) // part 2
        .Select(xs => ((3 + xs.y - xs.o) % 3) switch {
            0 => 4 + xs.y,
            1 => 7 + xs.y,
            2 => 1 + xs.y,
            _ => throw new ArgumentOutOfRangeException()
        })
        .Sum());