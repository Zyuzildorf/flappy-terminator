using System;

public interface IScorable
{
    public event Action<int> OnScoreValueAdd;
}