Console.WriteLine(
    new[] {File.ReadAllLines("input.txt")}
        .Select(input => (
            grid: input.Select(s => s.Select(c => (int) char.GetNumericValue(c)).ToArray()).ToArray(),
            width: input.Select(s => s.Select(c => (int) char.GetNumericValue(c)).ToArray()).ToArray()
                .First().Length,
            height: input.Select(s => s.Select(c => (int) char.GetNumericValue(c)).ToArray()).ToArray()
                .Length))
        .Select(tuple => new[]
                {
                    Enumerable.Range(0, tuple.height).Select(y => Enumerable.Range(0, tuple.width).Select(x => (x, y))),
                    Enumerable.Range(0, tuple.width).Select(x => Enumerable.Range(0, tuple.height).Select(y => (x, y)))
                }
                .SelectMany(range => new[] {range, range.Select(r => r.Reverse())})
                .SelectMany(way => way
                    .Aggregate<
                        IEnumerable<(int y, int x)>,
                        IEnumerable<(int x, int y, bool visible, int scenic)>
                    >(
                        new List<(int, int, bool, int)>(),
                        (current, line) => current.Concat(
                            line.Aggregate<
                                (int x, int y),
                                (IEnumerable<int> heights, IEnumerable<(int x, int y, bool visible, int scenic)> seen)
                            >(
                                (new List<int>(), new List<(int, int, bool, int)>()),
                                (meta, coord) => (
                                    meta.heights.Append(tuple.grid[coord.y][coord.x]),
                                    seen: meta.seen.Append((
                                        coord.x,
                                        coord.y,
                                        visible: meta.heights
                                                     .OrderDescending()
                                                     .FirstOrDefault(-1)
                                                 < tuple.grid[coord.y][coord.x],
                                        (meta.heights.Any()
                                            ? meta.heights
                                                .Reverse()
                                                .Take(1 + meta.heights.Reverse()
                                                    .TakeWhile(tree => tree < tuple.grid[coord.y][coord.x])
                                                    .Count())
                                                .Aggregate<int, IEnumerable<int>>(
                                                    new List<int>(),
                                                    (visible, next) =>
                                                        !visible.Any(tree => tree >= tuple.grid[coord.y][coord.x])
                                                            ? visible.Append(next)
                                                            : visible)
                                            : Array.Empty<int>())
                                        .Count())))).seen)))
                // part 1:
                // .Where(tree => tree.visible)
                // .GroupBy(tree => (tree.x, tree.y))
                // .Count()
                // part 2:
                .ToArray()
                .GroupBy(tree => (tree.x, tree.y))
                .Select(group => (group.Key.x, group.Key.y, scenic: group.Aggregate(1, (c, n) => c * n.scenic)))
                .OrderByDescending(tree => tree.scenic)
                .First()
                .scenic
        ).First());