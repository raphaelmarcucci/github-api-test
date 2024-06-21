namespace github_api_test
{
    internal class Stats
    {
        // index in array will be: letter-'a'
        private readonly StatColumn[] _stats = new StatColumn[26] {
            new StatColumn(), new StatColumn(),new StatColumn(),new StatColumn(),new StatColumn(),
            new StatColumn(), new StatColumn(),new StatColumn(),new StatColumn(),new StatColumn(),
            new StatColumn(), new StatColumn(),new StatColumn(),new StatColumn(),new StatColumn(),
            new StatColumn(), new StatColumn(),new StatColumn(),new StatColumn(),new StatColumn(),
            new StatColumn(), new StatColumn(),new StatColumn(),new StatColumn(),new StatColumn(),
            new StatColumn()
        };

        public void Add(int i, StatCell cell)
        {
            if (cell == null)
                throw new Exception("Stats.Add: unexpected cell: null!");
            if (i<0 || i>=26)
                throw new Exception("Stats.Add: unexpected index: should be between 0 and 26!");

            _stats[i].Add(cell);
        }

        public void Output(StreamWriter sw)
        {
            sw.WriteLine("==== STATS OUTPUT BEGIN ====");
            for (int i = 0; i < 26; i++) {
                sw.WriteLine($"== { (char)( 'a'+i ) } ==");
                _stats[i].Output(sw);
            }
            sw.WriteLine("==== STATS OUTPUT END ====");
        }

    }
}
