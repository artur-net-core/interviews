namespace LoadBalancer.Host.Counters
{
    public interface ICounter
    {
        int Next();
        void Reset(int comparand);
    }
}