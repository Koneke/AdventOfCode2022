Console.WriteLine(
    File.ReadAllLines("input.txt")
        .Select(line => line
            .Split(",")
            .Select(elf => elf
                .Split("-")
                .Select(int.Parse)))
        // part 1:
        // .Count(pair =>
        //     pair.First().First() <= pair.Last().First() &&
        //     pair.First().Last() >= pair.Last().Last() ||
        //     pair.Last().First() <= pair.First().First() &&
        //     pair.Last().Last() >= pair.First().Last()));
        // part 2:
        .Count(pair => !(
            pair.First().Last()  < pair.Last().First() ^
            pair.First().First() > pair.Last().Last() ||
            pair.Last().Last()   < pair.First().First() ^
            pair.Last().First()  > pair.First().Last())));
