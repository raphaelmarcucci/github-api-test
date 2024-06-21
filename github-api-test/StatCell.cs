namespace github_api_test
{
    internal class StatCell
    {
        public string Path { get; private set; } = string.Empty;
        public long Count { get; private set; } = 0;

        public StatCell(string path, long count)
        {
            Path = path;
            Count = count;
        }

        public void Output(StreamWriter sw)
        {
            sw.WriteLine($"{Count}\t{Path}");
        }
    }
}
