using github_api_test;
using Microsoft.Extensions.Configuration;

internal class Program
{
    private static async Task Main(string[] args)
    {
        Console.WriteLine("GITHUB-API-TEST BEGIN");

        try
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            if (config == null)
                throw new Exception("config! couldn't config from appsettings.json!");

            var configSettings = config.GetRequiredSection("Settings");
            if (configSettings == null)
                throw new Exception("config! no Settings node detected!");

            string token = configSettings.GetRequiredSection("Token").Value;
            if(token == null)
                throw new Exception("config! null token!");

            string repoOwner = configSettings.GetRequiredSection("RepoOwner").Value;
            if (repoOwner == null)
                throw new Exception("config! null repoOwner!");

            string repoName = configSettings.GetRequiredSection("RepoName").Value;
            if (repoName == null)
                throw new Exception("config! null repoName!");

            string outputDir = configSettings.GetRequiredSection("OutputDir").Value;
            if (outputDir == null)
                throw new Exception("config! null outputDir!");
            if (!Directory.Exists(outputDir))
                throw new Exception("config! outputDir doesn't exist!");

            var processor = new Processor(
                token,
                repoOwner,
                repoName,
                outputDir);


            await processor.Process();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception caught in main: {e}");
        }

        Console.WriteLine("GITHUB-API-TEST END");
    }
}