using System.Text.RegularExpressions;

namespace AdventOfCode.Solvers.Year2020.Day4;

public class Day4Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        var validPassports = 0;
        var validPassports2 = 0;
        Dictionary<string, string> passport = new();
        foreach (var line in puzzle)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                if (IsValidPasswordPart1(passport))
                {
                    validPassports++;
                    if (IsValidPasswordPart2(passport))
                    {
                        validPassports2++;
                    }
                }

                passport = new Dictionary<string, string>();
            }
            else
            {
                var data = line.Split(' ');
                foreach (var item in data)
                {
                    var keyVal = item.Split(':');
                    passport.Add(keyVal[0], keyVal[1]);
                }
            }
        }

        if (IsValidPasswordPart1(passport))
        {
            validPassports++;
            if (IsValidPasswordPart2(passport))
            {
                validPassports2++;
            }
        }

        GiveAnswer1(validPassports.ToString());
        GiveAnswer2(validPassports2.ToString());
    }

    private bool IsValidPasswordPart2(Dictionary<string, string> passport)
    {
        foreach(var item in passport)
        {
            switch (item.Key)
            {
                case "byr":
                    if (!int.TryParse(item.Value, out var byr) || byr < 1920 || byr > 2002)
                    {
                        return false;
                    }
                    break;
                case "iyr":
                    if (!int.TryParse(item.Value, out var iyr) || iyr < 2010 || iyr > 2020)
                    {
                        return false;
                    }
                    break;
                case "eyr":
                    if (!int.TryParse(item.Value, out var eyr) || eyr < 2020 || eyr > 2030)
                    {
                        return false;
                    }
                    break;
                case "hgt":
                    if (item.Value.EndsWith("cm"))
                    {
                        if (!int.TryParse(item.Value.Replace("cm", ""), out var hgt) || hgt < 150 || hgt > 193)
                        {
                            return false;
                        }
                    }
                    else if (item.Value.EndsWith("in"))
                    {
                        if (!int.TryParse(item.Value.Replace("in", ""), out var hgt) || hgt < 59 || hgt > 76)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case "hcl":
                    if (!item.Value.StartsWith("#") || item.Value.Length != 7)
                    {
                        return false;
                    }
                    var hcl = item.Value.Replace("#", "");
                    if (!Regex.IsMatch(hcl, @"^[a-f0-9]+$"))
                    {
                        return false;
                    }
                    break;
                case "ecl":
                    if (!new List<string>() { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" }.Contains(item.Value))
                    {
                        return false;
                    }
                    break;
                case "pid":
                    if (!int.TryParse(item.Value, out var pid) || item.Value.Length != 9)
                    {
                        return false;
                    }
                    break;
                default:
                    break;
            }
        }

        return true;
    }

    private bool IsValidPasswordPart1(Dictionary<string,string> passport)
    {
        var requiredFields = new List<string>()
        {
            "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"
        };

        foreach (var field in requiredFields)
        {
            if (!passport.ContainsKey(field))
            {
                return false;
            }
        }

        return true;
    }
}
