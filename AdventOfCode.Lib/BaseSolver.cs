﻿using System.Reactive.Subjects;

namespace AdventOfCode.Lib;

public abstract class BaseSolver
{
    protected ReplaySubject<string> answer1 = new ReplaySubject<string>(1);
    public IObservable<string> Answer1 => answer1;

    protected ReplaySubject<string> answer2 = new ReplaySubject<string>(1);
    public IObservable<string> Answer2 => answer2;
    protected ReplaySubject<string> logger = new ReplaySubject<string>(1);
    public IObservable<string> Logger => logger;

    protected void GiveAnswer1(string answer)
    {
        answer1.OnNext(answer);
        answer1.OnCompleted();
    }

    protected void GiveAnswer2(string answer)
    {
        answer2.OnNext(answer);
        answer2.OnCompleted();
    }

    public abstract void Solve(string[] puzzle);

    public void Reset()
    {
        answer1 = new ReplaySubject<string>(1);
        answer2 = new ReplaySubject<string>(1);
    }
}
