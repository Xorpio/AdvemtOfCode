using Spectre.Console;

namespace PuzzleConsole.Year2021.Day19;

public class Day19 : ISolver
{
    List<string> Found = new List<string>();
    List<string> NotFound = new List<string>();
    private List<Scanner> scanners;

    public string[] Solve(string[] puzzle)
    {
        scanners = ParsePuzzle(puzzle);

        Found.Add(scanners[0].Name);
        NotFound = scanners.Select(x => x.Name).ToList();
        NotFound.Remove(Found[0]);

        AnsiConsole.Status()
            .Start("Working..,", ctx =>
            {
                while (NotFound.Count > 0)
                {
                    if (findNext(ctx) == false && NotFound.Count > 0)
                    {
                        throw new Exception("Solution not found");
                    }
                }
            });

        //var s1 = scanners[0];
        //var s2 = scanners[1];

        //s1.ToRoot();
        //s2.ToRoot();

        //var x1 = s1.Beacons.OrderBy(b => b.X + b.Y + b.Z)
        //    .Take(2)
        //    .ToList();

        //var corner = new Coordinates
        //{
        //    X = x1[0].X - x1[1].X,
        //    Y = x1[0].Y - x1[1].Y,
        //    Z = x1[0].Z - x1[1].Z
        //};

        ////for (int side = 0; side < 6; side++)
        ////{
        //    for (int rot = 0; rot < 4; rot++)
        //    {
        //    //check
        //        var x2 = s2.Beacons.OrderBy(b => b.X + b.Y + b.Z);
        //    var result = x2.Zip<Coordinates, Coordinates, (Coordinates a, Coordinates b)>(x2.Skip(1), (a, b) => new(a, b));
        //    var x = result.ToDictionary(r => new Coordinates()
        //    {
        //        X = r.a.X - r.b.X,
        //        Y = r.a.Y - r.b.Y,
        //        Z = r.a.Z - r.b.Z
        //    }, r => r);

        //        if (x.ContainsKey(corner))
        //        {
        //            Console.WriteLine("");
        //        }


        //        s2.RotateZ();
        //    }
        ////}

        //Console.WriteLine(x1[0]);
        //Console.WriteLine(x1[1]);

        //throw new NotImplementedException();

        //for (int a = 0; a < 12; a++)
        //{
        //    for (int b = 0; b < 12; b++)
        //    {
        //        if (a == b)
        //        {
        //            continue;
        //        }

        //            var ansa = new Coordinates()
        //            {
        //                X = scanners[0].Beacons[a].X - scanners[0].Beacons[b].X,
        //                Y = scanners[0].Beacons[a].Y - scanners[0].Beacons[b].Y,
        //                Z = scanners[0].Beacons[a].Z - scanners[0].Beacons[b].Z
        //            };
        //            Console.WriteLine($"{scanners[0].Name}: {scanners[0].Beacons[a]} -> {scanners[0].Beacons[b]} = {ansa}");

        //            var ansb = new Coordinates()
        //            {
        //                X = scanners[1].Beacons[a].X - scanners[1].Beacons[b].X,
        //                Y = scanners[1].Beacons[a].Y - scanners[1].Beacons[b].Y,
        //                Z = scanners[1].Beacons[a].Z - scanners[1].Beacons[b].Z
        //            };
        //            Console.WriteLine($"{scanners[1].Name}: {scanners[1].Beacons[a]} -> {scanners[1].Beacons[b]} = {ansb}");
        //            Console.WriteLine($"IsEqual: {ansa.IsEqualTo(ansb)}");
        //    }
        //}
        var answer = 0;

        var beacons = new List<string>();
        foreach (var scanner in scanners)
        {
            foreach(var b in scanner.Beacons)
            {
                beacons.Add(b.Value.ToString());
            }

            foreach (var otherScanner in scanners)
            {
                var distance = Math.Abs(scanner.ScannerLocation.X - otherScanner.ScannerLocation.X) +
                    Math.Abs(scanner.ScannerLocation.Y - otherScanner.ScannerLocation.Y) +
                    Math.Abs(scanner.ScannerLocation.Z - otherScanner.ScannerLocation.Z);

                if (distance > answer)
                    answer = distance;
            }
        }


        return new string[] { beacons.Distinct().Count().ToString(), answer.ToString() };
    }
    
    public bool findNext(StatusContext ctx)
    {
        for (int i = 0; i < NotFound.Count; i++)
        {
            var looseScanner = scanners.First(s => s.Name == NotFound[i]);
            for (int side = 0; side < 6; side++)
            {
                ctx.Status = $"Trying side {side} of {looseScanner.Name}";
                for (int Orientation = 0; Orientation < 4; Orientation++)
                {
                    ctx.Status = $"Trying side {side} of {looseScanner.Name}, Orientation {Orientation}";
                    foreach (var f in Found)
                    {
                        var fixedScanner = scanners.First(s => s.Name == f);

                        ctx.Status = $"Trying side {side} of {looseScanner.Name}, Orientation {Orientation}, to scanner {fixedScanner.Name}";

                        foreach (var corner in fixedScanner.Corners)
                        {
                            ctx.Status = $"Trying side {side} of {looseScanner.Name}, Orientation {Orientation}, to scanner {fixedScanner.Name} and corner {corner.Key}";
                            foreach (var beacon in looseScanner.Beacons)
                            {
                                ctx.Status = $"Trying side {side} of {looseScanner.Name}, Orientation {Orientation}, to scanner {fixedScanner.Name} and corner {corner.Key} and beacon {beacon.Key}";
                                var adjust = new Coordinates()
                                {
                                    X = corner.Value.X - beacon.Value.X,
                                    Y = corner.Value.Y - beacon.Value.Y,
                                    Z = corner.Value.Z - beacon.Value.Z
                                };

                                //set beacon to corner
                                looseScanner.ToCoords(adjust);

                                if (
                                       looseScanner.Beacons
                                           .Select(b => b.Value)
                                           .Intersect(
                                                fixedScanner.Beacons.Select(b => b.Value)
                                            )
                                           .Count() >= 12)
                                {
                                    Found.Add(looseScanner.Name);
                                    NotFound.Remove(looseScanner.Name);
                                    Console.WriteLine($"FOUND!!! {looseScanner.Name}");
                                    return true;
                                }
                            }
                        }
                    }

                    var tozero = new Coordinates()
                    {
                        X = looseScanner.ScannerLocation.X * -1,
                        Y = looseScanner.ScannerLocation.Y * -1,
                        Z = looseScanner.ScannerLocation.Z * -1
                    };

                    looseScanner.ToCoords(tozero);
                    looseScanner.RotateZ();
                }

                if (side < 3)
                {
                    looseScanner.RotateX();
                }
                else if (side == 3)
                {
                    looseScanner.RotateZ();
                    looseScanner.RotateX();
                }
                else
                {
                    looseScanner.RotateX();
                    looseScanner.RotateX();
                }
            }
        }
        return false;
    }

    public List<Scanner> ParsePuzzle(string[] puzzle)
    { 
        var scanners = new List<Scanner>();
        Scanner scanner = null;
        foreach (var line in puzzle)
        {
            if (string.IsNullOrEmpty(line))
            {
                scanners.Add(scanner);
            }
            else if (line[0..3] == "---")
            {
                scanner = new Scanner() { Name = line };
            }
            else
            {
                var coordinates = new Coordinates(line);
                scanner.Beacons.Add(line, coordinates);
            }
        }

        scanners.Add(scanner);

        scanners.ForEach(scanner => scanner.FindCorners());

        return scanners;
    }

}

public class Scanner
{
    public string Name { get; set; }

    public Dictionary<string, Coordinates> Beacons { get; set; } = new Dictionary<string, Coordinates>();

    public Dictionary<string, Coordinates> Corners { get; set; } = new Dictionary<string, Coordinates>();

    public Coordinates ScannerLocation = new Coordinates { X = 0, Y = 0, Z = 0, };

    //public List<string> lines 
    //{ 
    //    get 
    //    {
    //        var lines = new List<string>();
    //        foreach (var a in Beacons)
    //        {
    //            var length = int.MaxValue;
    //            string line = "";
    //            foreach (var b in Beacons)
    //            {
    //                var l = Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y) + Math.Abs(a.Z - b.Z);
    //                if (l > 0 && l < length)
    //                {
    //                    length = l;
    //                    line = $"From {a} -> {b}, ({a.X - b.X},{a.Y - b.Y}, {a.Z - b.Z})";
    //                    lines.Add(line);
    //                }
    //            }
    //        }

    //        return lines;
    //    } 
    //}

    internal void RotateZ()
    {
        if (ScannerLocation.X != 0 || ScannerLocation.Y != 0 || ScannerLocation.Z != 0)
        {
            throw new Exception($"Cannot Turn on coords: {ScannerLocation}");
        }

        foreach (var b in Beacons)
        {
            Beacons[b.Key].TurnZ();
        }

        foreach (var b in Corners)
        {
            Corners[b.Key].TurnZ();
        }
    }
    internal void RotateX()
    {
        if (ScannerLocation.X != 0 || ScannerLocation.Y != 0 || ScannerLocation.Z != 0)
        {
            throw new Exception($"Cannot Turn on coords: {ScannerLocation}");
        }

        foreach (var b in Beacons)
        {
            Beacons[b.Key].TurnX();
        }

        foreach (var b in Corners)
        {
            Corners[b.Key].TurnX();
        }
    }

    //internal void ToRoot()
    //{
    //    var minX = Beacons.MinBy(b => b.X).X * -1;
    //    var minY = Beacons.MinBy(b => b.Y).Y * -1;
    //    var minZ = Beacons.MinBy(b => b.Z).Z * -1;

    //    Beacons.ForEach(b =>
    //    {
    //        b.X += minX;
    //        b.Y += minY;
    //        b.Z += minZ;
    //    });

    //    ScannerLocation.X += minX;
    //    ScannerLocation.Y += minY;
    //    ScannerLocation.Z += minZ;
    //}

    internal void ToCoords(Coordinates c)
    {
        foreach (var b in Beacons)
        {
            Beacons[b.Key].X += c.X;
            Beacons[b.Key].Y += c.Y;
            Beacons[b.Key].Z += c.Z;
        }

        foreach (var b in Corners)
        {
            Corners[b.Key].X += c.X;
            Corners[b.Key].Y += c.Y;
            Corners[b.Key].Z += c.Z;
        }

        ScannerLocation.X += c.X;
        ScannerLocation.Y += c.Y;
        ScannerLocation.Z += c.Z;
    }

    internal void FindCorners()
    {
        if (ScannerLocation.X != 0 || ScannerLocation.Y != 0 || ScannerLocation.Z != 0)
        {
            throw new Exception($"Cannot find corners on coords: {ScannerLocation}");
        }

        for (int i = 0; i < 4; i++)
        {
            var min = Beacons.MinBy(b => b.Value.X + b.Value.Y + b.Value.Z);
            Corners.Add(min.Key, new Coordinates
            {
                X = min.Value.X,
                Y = min.Value.Y,
                Z = min.Value.Z
            });

            var max = Beacons.MaxBy(b => b.Value.X + b.Value.Y + b.Value.Z);
            Corners.Add(max.Key, new Coordinates
            {
                X = max.Value.X,
                Y = max.Value.Y,
                Z = max.Value.Z
            });

            //turn Z
            RotateZ();
        }
    }
}

public record Coordinates
{
    public Coordinates() { }
    public Coordinates(string line)
    {
        var coords = line.Split(',');
        X = int.Parse(coords[0]);
        Y = int.Parse(coords[1]);
        Z = int.Parse(coords[2]);
    }

    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }

    //public bool IsEqualTo(Coordinates coordinates)
    //{
    //    if (X == coordinates.X && Y == coordinates.Y && Z == coordinates.Z)
    //        return true;
    //    coordinates = coordinates.TurnZ();
    //    if (X == coordinates.X && Y == coordinates.Y && Z == coordinates.Z)
    //        return true;
    //    coordinates = coordinates.TurnZ();

    //    if (X == coordinates.X && Y == coordinates.Y && Z == coordinates.Z)
    //        return true;
    //    coordinates = coordinates.TurnZ();
    //    if (X == coordinates.X && Y == coordinates.Y && Z == coordinates.Z)
    //        return true;
    //    coordinates = coordinates.TurnZ();
    //    if (X == coordinates.X && Y == coordinates.Y && Z == coordinates.Z)
    //        return true;
    //    coordinates = coordinates.TurnZ();
    //    if (X == coordinates.X && Y == coordinates.Y && Z == coordinates.Z)
    //        return true;
    //    coordinates = coordinates.TurnZ();

    //    coordinates = coordinates.TurnX();

    //    if (X == coordinates.X && Y == coordinates.Y && Z == coordinates.Z)
    //        return true;
    //    coordinates = coordinates.TurnZ();
    //    if (X == coordinates.X && Y == coordinates.Y && Z == coordinates.Z)
    //        return true;
    //    coordinates = coordinates.TurnZ();
    //    if (X == coordinates.X && Y == coordinates.Y && Z == coordinates.Z)
    //        return true;
    //    coordinates = coordinates.TurnZ();
    //    if (X == coordinates.X && Y == coordinates.Y && Z == coordinates.Z)
    //        return true;
    //    coordinates = coordinates.TurnZ();

    //    //oranje
    //    coordinates = coordinates.TurnX();

    //    if (X == coordinates.X && Y == coordinates.Y && Z == coordinates.Z)
    //        return true;
    //    coordinates = coordinates.TurnZ();
    //    if (X == coordinates.X && Y == coordinates.Y && Z == coordinates.Z)
    //        return true;
    //    coordinates = coordinates.TurnZ();
    //    if (X == coordinates.X && Y == coordinates.Y && Z == coordinates.Z)
    //        return true;
    //    coordinates = coordinates.TurnZ();
    //    if (X == coordinates.X && Y == coordinates.Y && Z == coordinates.Z)
    //        return true;
    //    coordinates = coordinates.TurnZ();

    //    coordinates = coordinates.TurnX();

    //    if (X == coordinates.X && Y == coordinates.Y && Z == coordinates.Z)
    //        return true;
    //    coordinates = coordinates.TurnZ();
    //    if (X == coordinates.X && Y == coordinates.Y && Z == coordinates.Z)
    //        return true;
    //    coordinates = coordinates.TurnZ();
    //    if (X == coordinates.X && Y == coordinates.Y && Z == coordinates.Z)
    //        return true;
    //    coordinates = coordinates.TurnZ();
    //    if (X == coordinates.X && Y == coordinates.Y && Z == coordinates.Z)
    //        return true;
    //    coordinates = coordinates.TurnZ();

    //    coordinates = coordinates.TurnZ();
    //    coordinates = coordinates.TurnX();

    //    if (X == coordinates.X && Y == coordinates.Y && Z == coordinates.Z)
    //        return true;
    //    coordinates = coordinates.TurnZ();
    //    if (X == coordinates.X && Y == coordinates.Y && Z == coordinates.Z)
    //        return true;
    //    coordinates = coordinates.TurnZ();
    //    if (X == coordinates.X && Y == coordinates.Y && Z == coordinates.Z)
    //        return true;
    //    coordinates = coordinates.TurnZ();
    //    if (X == coordinates.X && Y == coordinates.Y && Z == coordinates.Z)
    //        return true;
    //    coordinates = coordinates.TurnZ();

    //    coordinates = coordinates.TurnX();
    //    coordinates = coordinates.TurnX();

    //    if (X == coordinates.X && Y == coordinates.Y && Z == coordinates.Z)
    //        return true;
    //    coordinates = coordinates.TurnZ();
    //    if (X == coordinates.X && Y == coordinates.Y && Z == coordinates.Z)
    //        return true;
    //    coordinates = coordinates.TurnZ();
    //    if (X == coordinates.X && Y == coordinates.Y && Z == coordinates.Z)
    //        return true;
    //    coordinates = coordinates.TurnZ();
    //    if (X == coordinates.X && Y == coordinates.Y && Z == coordinates.Z)
    //        return true;

    //    return false;
    //}

    public void TurnZ()
    {
        var z = Z;
        Z = X * -1;
        X = z;
    }

    public void TurnX()
    {
        var y = Y;
        Y = X * -1;
        X = y;
    }

    public override string ToString()
    {
        return $"({X},{Y},{Z})";
    }
}
