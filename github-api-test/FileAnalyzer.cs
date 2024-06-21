namespace github_api_test
{
    internal class FileAnalyzer
    {
        private readonly string _path;
        private readonly string _content;
        private readonly Stats _stats;

        public FileAnalyzer(string path, string content, Stats stats)
        {
            _path = path;
            _content = content;
            _stats = stats;
        }

        public void Analyze()
        {
            // index in array will be: letter-'a'
            var counts = new long[26]
            { 
                0, 0, 0, 0, 0,
                0, 0, 0, 0, 0,
                0, 0, 0, 0, 0,
                0, 0, 0 ,0, 0,
                0 ,0 ,0 ,0, 0,
                0
            };

            foreach(char c in _content)
            {
                if (c >= 'a' && c <= 'z')
                {
                    counts[c - 'a']++;
                    continue;
                }

                if(c>='A' && c<='Z')
                {
                    counts[c - 'A']++;
                    continue;
                }
            }

            for (int i = 0; i < 26; i++)
            {
                _stats.Add(i, new StatCell(_path, counts[i]));
            }
        }
    }
}
