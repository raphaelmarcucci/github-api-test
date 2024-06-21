using Octokit;

namespace github_api_test
{
    internal class Processor
    {
        private readonly string _token;
        private readonly string _repoOwner;
        private readonly string _repoName;
        private readonly string _outputDir;
        private readonly Stats _stats = new Stats();

        public Processor(string token, string repoOwner, string repoName, string outputDir)
        {
            _token = token;
            _repoOwner = repoOwner;
            _repoName = repoName;
            _outputDir = outputDir;
        }

        public async Task Process()
        {
            var client = new GitHubClient(new ProductHeaderValue("github-api-test"))
            {
                Credentials = new Credentials(_token)
            };

            var repo = await client.Repository.Get(_repoOwner, _repoName);
            if (repo?.Id == null)
                throw new Exception("Repository.get: failed! null result!");

            await BrowseDir(client, repo, 1, "", 10000);

            using (var sw = new StreamWriter(Path.Join(_outputDir, "output.txt"), true))
            {
                _stats.Output(sw);
            }
        }

        private async Task<int> BrowseDir(GitHubClient client, Repository repo, int level, string path, int fileCount)
        {
            var repoContents = string.IsNullOrEmpty(path) ?
                await client.Repository.Content.GetAllContents(repo.Id) :
                await client.Repository.Content.GetAllContents(repo.Id, path);

            string blanks = "";
            for (int i = 0; i < level; i++)
                blanks += "  ";

            foreach (var rc in repoContents)
            {
                Console.WriteLine($"{blanks}Name={rc.Name} Path={rc.Path} Type={rc.Type} fileCount={fileCount}");

                if (fileCount>0 &&
                    rc.Type==ContentType.File &&
                   (rc.Name.EndsWith(".js") || rc.Name.EndsWith(".ts")))
                {
                    fileCount--;

                    var fileContents = await client.Repository.Content.GetAllContents(repo.Id, rc.Path);
                    string totalFileContent = string.Empty;
                    foreach (var fc in fileContents)
                        totalFileContent += fc.Content;

                    var fileAnalyzer = new FileAnalyzer(rc.Path, totalFileContent, _stats);
                    fileAnalyzer.Analyze();
                }

                if (rc.Type == ContentType.Dir)
                    fileCount = await BrowseDir(client, repo, level + 1, rc.Path, fileCount);
            }

            return fileCount;
        }

    }
}
