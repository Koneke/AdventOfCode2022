Console.WriteLine(
    File.ReadAllLines("input.txt")
        .Select(line => line
            .Split(",")
            .Select(elf => elf
                .Split("-")
                .Select(int.Parse)))
        // part 1:
        // .Count(pair =>
        //     Math.Abs(
        //         Math.Sign(pair.First().First() - pair.Last().First())
        //         + Math.Sign(pair.First().Last() - pair.Last().Last()))
        //     != 2));
        // part 2:
        .Count(pair =>
            Math.Sign(pair.First().Last() - pair.Last().First())
            != Math.Sign(pair.First().First() - pair.Last().Last())));