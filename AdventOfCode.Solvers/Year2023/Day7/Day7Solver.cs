using System.Reactive.Subjects;

namespace AdventOfCode.Solvers.Year2023.Day7;

public class Day7Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        var listOfHands = new List<Hand>();
        foreach(var line in puzzle)
        {
            var hand = new Hand(line.ToUpper());
            hand.Logger.Subscribe(logger);
            listOfHands.Add(hand);
        }

        listOfHands.Sort();

        double count = 1;
        double part1 = 0;
        foreach(var hand in listOfHands)
        {
            part1 += hand.Bid * count;
            count++;
        }

        GiveAnswer1(part1);
        GiveAnswer2("");
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
        HandType = determineHandType(splits[0]);
        HandInput = splits[0];
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
        Logger.OnNext($"{input} is {returnType}");
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
