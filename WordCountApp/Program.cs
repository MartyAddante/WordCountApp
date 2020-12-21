using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;

namespace WordCountApp
{
    class Program
    {

        static void Main(string[] args)
        {
            int topX = GetNumOfTopResultsFromConfig();
            string analysisTxt = GetFilePathFromConfig("Project","Text1","AnalysisText");
            string stopWordsTxt = GetFilePathFromConfig("Project","Text1","StopWords");

            TextMutator ts = new TextMutator(stopWordsTxt,analysisTxt,"project");
            CalculationEngine ce = new CalculationEngine(ts.StemmedWords);
            List<KeyValuePair<string, int>> TopTerms = ce.GetTopTerms(topX);
            
            foreach (var s in TopTerms)
            {
                Console.WriteLine($"{s.Key}: {s.Value}");
            }
        }

        private static string GetFilePathFromConfig(string env, string txtVersion, string txtFile)
        {
            var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("parameters.json", optional: true, reloadOnChange: true);                            
            string pathEnding = builder.Build().GetSection("Env").GetSection(env).GetSection(txtVersion).GetSection("FilePath").GetSection(txtFile).Value;
            return pathEnding;
        } 
        
        private static int GetNumOfTopResultsFromConfig()
        {
            var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("parameters.json", optional: true, reloadOnChange: true);
            int num = Int32.Parse(builder.Build().GetSection("NumOfTopResults").Value);
            return num;
        }
    }
}
