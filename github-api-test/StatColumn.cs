namespace github_api_test
{
    internal class StatColumn
    {
        private readonly PriorityQueue<StatCell, long> _pq = new PriorityQueue<StatCell, long>(512);

        public void Add(StatCell cell) { _pq.Enqueue(cell, -cell.Count); }

        public void Output(StreamWriter sw)
        {
            while (_pq.Count > 0)
            {
                var cell = _pq.Dequeue();
                cell.Output(sw);
            }
        }
    }
}
