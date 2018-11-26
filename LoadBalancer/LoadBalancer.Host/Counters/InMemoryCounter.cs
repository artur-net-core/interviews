using System.Threading;

namespace LoadBalancer.Host.Counters
{
    public class InMemoryCounter : ICounter
    {
        private static int _counter;

        public int Next() => Interlocked.Increment(ref _counter);

        public void Reset(int comparand) =>
            Interlocked.CompareExchange(ref _counter, 0, comparand);
    }
}
