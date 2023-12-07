using System.Reactive.Subjects;

namespace AdventOfCode.Solvers.Year2023.Day7;

public class Day7Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        var listOfHands = new List<Hand>();
        var listOfHandsPart2 = new List<HandWithJoker>();
        foreach(var line in puzzle)
        {
            var hand = new Hand(line);
            listOfHands.Add(hand);

            var handWithJoker = new HandWithJoker(line);
            handWithJoker.Logger.Subscribe(logger);
            listOfHandsPart2.Add(handWithJoker);
        }

        listOfHands.Sort();
        listOfHandsPart2.Sort();

        double count = 1;
        double part1 = 0;
        foreach(var hand in listOfHands)
        {
            part1 += hand.Bid * count;
            count++;
        }

        GiveAnswer1(part1);

        count = 1;
        double part2 = 0;
        foreach(var hand in listOfHandsPart2)
        {
            part2 += hand.Bid * count;
            count++;
        }

        GiveAnswer2(part2);
    }
}

public class Hand : IComparable<Hand>
{
    public ReplaySubject<string> Logger { get; set; }  = new ReplaySubject<string>();
    public TypeOfHand HandType { get; set; }
    public string HandInput { get; set; }
    public double Bid { get; set; }
    public Hand(string input)
    {
        var splits = input.Split(' ');
        HandInput = splits[0];
        HandType = determineHandType(HandInput);
        Bid = double.Parse(splits[1]);
    }

    private TypeOfHand determineHandType(string input)
    {
        var groups = input.GroupBy(c => c);

        var returnType = TypeOfHand.HighCard;

        if (groups.Any(g => g.Count() == 5))
        {
            returnType = TypeOfHand.FiveOfAKind;
        }
        else if (groups.Any(g => g.Count() == 4))
        {
            returnType = TypeOfHand.FourOfAKind;
        }
        else if (groups.Any(g => g.Count() == 3) && groups.Any(g => g.Count() == 2))
        {
            returnType = TypeOfHand.FullHouse;
        }
        else if (groups.Any(g => g.Count() == 3))
        {
            returnType = TypeOfHand.ThreeOfAKind;
        }
        else if (groups.Count(g => g.Count() == 2) == 2)
        {
            returnType = TypeOfHand.TwoPair;
        }
        else if (groups.Any(g => g.Count() == 2))
        {
            returnType = TypeOfHand.OnePair;
        }
        return returnType;
    }

    public int CompareTo(Hand? other)
    {
        if (other == null)
        {
            return 1;
        }

        if (HandType != other.HandType)
        {
            return (int)HandType - (int)other.HandType;
        }

        foreach(var (card, otherCard) in HandInput.Zip(other.HandInput))
        {
            var cardValue = compareCard(card);
            var otherCardValue = compareCard(otherCard);

            if (cardValue != otherCardValue)
            {
                return cardValue - otherCardValue;
            }
        }

        if(other.HandInput == HandInput && other.Bid == Bid)
        {
            return 0;
        }

        throw new Exception("Shouldn't get here");
    }

    private int compareCard(char card) => card switch
    {
        'T' => 10,
        'J' => 11,
        'Q' => 12,
        'K' => 13,
        'A' => 14,
        _ => int.Parse(card.ToString())
    };
}

public class HandWithJoker : IComparable<HandWithJoker>
{
    public ReplaySubject<string> Logger { get; set; }  = new ReplaySubject<string>();
    public TypeOfHand HandType { get; set; }
    public string HandInput { get; set; }
    public double Bid { get; set; }
    public HandWithJoker(string input)
    {
        var splits = input.Split(' ');
        HandInput = splits[0];
        HandType = determineHandType(HandInput);
        Bid = double.Parse(splits[1]);
    }

    private TypeOfHand determineHandType(string input)
    {
        var groups = input.GroupBy(c => c);

        if (input.Contains('J'))
        {
            var biggestGroup = groups.Where(c => c.Key != 'J').OrderByDescending(g => g.Count()).FirstOrDefault();
            if (biggestGroup == null)
            {
                input = "AAAAA";
            }
            else
            {
                input = input.Replace('J', biggestGroup.Key);
            }
            groups = input.GroupBy(c => c);
        }

        var returnType = TypeOfHand.HighCard;

        if (groups.Any(g => g.Count() == 5))
        {
            returnType = TypeOfHand.FiveOfAKind;
        }
        else if (groups.Any(g => g.Count() == 4))
        {
            returnType = TypeOfHand.FourOfAKind;
        }
        else if (groups.Any(g => g.Count() == 3) && groups.Any(g => g.Count() == 2))
        {
            returnType = TypeOfHand.FullHouse;
        }
        else if (groups.Any(g => g.Count() == 3))
        {
            returnType = TypeOfHand.ThreeOfAKind;
        }
        else if (groups.Count(g => g.Count() == 2) == 2)
        {
            returnType = TypeOfHand.TwoPair;
        }
        else if (groups.Any(g => g.Count() == 2))
        {
            returnType = TypeOfHand.OnePair;
        }

        var oldHand = new Hand($"{HandInput} 1");
        Logger.OnNext($"{HandInput} -> {input} is {oldHand.HandType} -> {returnType}");
        return returnType;
    }

    public int CompareTo(HandWithJoker? other)
    {
        if (other == null)
        {
            return 1;
        }

        if (HandType != other.HandType)
        {
            return (int)HandType - (int)other.HandType;
        }

        foreach(var (card, otherCard) in HandInput.Zip(other.HandInput))
        {
            var cardValue = compareCard(card);
            var otherCardValue = compareCard(otherCard);

            if (cardValue != otherCardValue)
            {
                return cardValue - otherCardValue;
            }
        }

        if(other.HandInput == HandInput && other.Bid == Bid)
        {
            return 0;
        }

        throw new Exception("Shouldn't get here");
    }

    private int compareCard(char card) => card switch
    {
        'T' => 10,
        'J' => 1,
        'Q' => 12,
        'K' => 13,
        'A' => 14,
        _ => int.Parse(card.ToString())
    };
}

public enum TypeOfHand
{
    FiveOfAKind = 6,
    FourOfAKind = 5,
    FullHouse = 4,
    ThreeOfAKind = 3,
    TwoPair = 2,
    OnePair = 1,
    HighCard = 0,
}
