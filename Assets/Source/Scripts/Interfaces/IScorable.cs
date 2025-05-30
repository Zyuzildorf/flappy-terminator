using System;

namespace Source.Scripts.Interfaces
{
    public interface IScorable
    {
        public event Action<int> GivingScore;
    }
}