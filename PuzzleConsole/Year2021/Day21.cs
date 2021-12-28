namespace PuzzleConsole.Year2021.Day21;

public class Day21 : ISolver
{
    public int dice;
    public int Turn;
    public Dictionary<int, int> score = new Dictionary<int, int>();
    public int player1;
    public int player2;

    public string[] Solve(string[] puzzle)
    {
        var p1 = int.Parse(puzzle[0].Split(": ")[1]);
        var p2 = int.Parse(puzzle[1].Split(": ")[1]);


        var (ScorePlayer1, ScorePlayer2, rolls) = PlayAGame(p1, p2);

        var loser = (ScorePlayer1 < ScorePlayer2) ? ScorePlayer1 : ScorePlayer2;

        var realGame = DiracDice(p1, p2);

        var heighest = realGame.ScorePlayer1 > realGame.ScorePlayer2 ? realGame.ScorePlayer1 : realGame.ScorePlayer2;

        return new string[] { (loser * rolls).ToString(), heighest.ToString() };
    }

    public (int ScorePlayer1, int ScorePlayer2, int rolls) PlayAGame(int p1, int p2)
    {
        player1 = p1;
        player2 = p2;

        var rolls = 0;
        Turn = 0;
        score.Add(0, 0);
        score.Add(1, 0);

        dice = 0;

        while (score[0] < 1000 && score[1] < 1000)
        {
            var rolled = 0;
            for(int i = 0; i < 3; i++)
            {
                rolls++;
                rolled += Roll();
            }

            MoveAndScore(rolled, Turn);

            Turn++;
        }

        return (score[0], score[1], rolls);
    }

    public (decimal ScorePlayer1, decimal ScorePlayer2) DiracDice(int P1, int P2)
    {
        var states = new Dictionary<string, (int p1Score, int p1Place, int p2Score, int p2Place, int turn, decimal count)>();
        states.Add($"0,{P1},0,{P2},0", (0, P1, 0, P2, 0, 1));

        decimal scorePlayer1 = 0;
        decimal scorePlayer2 = 0;

        while(states.Count > 0)
        {
            var state = states.First();
            states.Remove(state.Key);

            var rolles = new List<List<int>>();
            for(int r1 = 1; r1 <= 3; r1++)
            {
                for (int r2 = 1; r2 <= 3; r2++)
                {
                    for (int r3 = 1; r3 <= 3; r3++)
                    {
                        rolles.Add(new List<int> { r1, r2, r3 });
                    }
                }
            }

            foreach (var roll in rolles)
            {
                var move = roll.Sum();

                var res = _MoveAndScore(move, state.Value.turn, (state.Value.p1Score, state.Value.p1Place), (state.Value.p2Score, state.Value.p2Place));

                var turn = state.Value.turn + 1;

                var key = $"{res.P1.Score},{res.P1.Place},{res.P2.Score},{res.P2.Place},{turn}";
                if (res.P1.Score > 20)
                {
                    scorePlayer1 += state.Value.count;
                }
                else if (res.P2.Score > 20)
                {
                    scorePlayer2 += state.Value.count;
                }
                else if (states.ContainsKey(key))
                {
                    var newState = states[key];
                    newState.count += state.Value.count;
                    states[key] = newState;
                }
                else
                {
                    states.Add(key, new(res.P1.Score, res.P1.Place, res.P2.Score, res.P2.Place, turn, state.Value.count));
                }
            }
        }

        return (scorePlayer1, scorePlayer2);
    }

    public ((int Score, int Place) P1, (int Score, int Place) P2) _MoveAndScore(int rolled, int turn, (int Score, int Place) P1, (int Score, int Place) P2)
    {
        if (turn % 2 == 0)
        {
            P1.Place = P1.Place + (rolled % 10);
            if (P1.Place > 10)
            {
                P1.Place = P1.Place % 10;
            }

            P1.Score += P1.Place;
        }
        else
        {
            P2.Place = P2.Place + (rolled % 10);
            if (P2.Place > 10)
            {
                P2.Place = P2.Place % 10;
            }

            P2.Score += P2.Place;
        }
        
        return (P1, P2);
    }

    public void MoveAndScore(int rolled, int turn)
    {
        if (turn % 2 == 0)
        {
            player1 = player1 + (rolled % 10);
            if ( player1 > 10)
            {
                player1 = player1 % 10;
            }

            score[0] += player1;
        }
        else
        {
            player2 = player2 + (rolled % 10);
            if ( player2 > 10)
            {
                player2 = player2 % 10;
            }

            score[1] += player2;
        }
    }

    public int Roll()
    {
        dice++;

        if (dice > 100)
        {
            dice = 1;
        }

        return dice;
    }
}
