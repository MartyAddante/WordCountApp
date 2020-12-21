using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace WordCountApp
{
	public class TextMutator
	{
		public List<string> StopWords { get; set; }
		public List<string> StrippedWords { get; set; }
		public List<string> StemmedWords { get; set; }
		private string StopWordsPath { get; set; }// = @"C:\Users\Marty\Desktop\programming_assignment\stopwords.txt";
		private string AnalysisTextPath { get; set; }// = @"C:\Users\Marty\Desktop\programming_assignment\Text1.txt";
		private Regex Rgx = new Regex("[^A-Za-z']");
		private Regex Apos = new Regex("[']");

		

		public TextMutator(string stopWordsPath, string analysisTxtPath, string env)
		{
			if (env.ToLower() == "project")
			{
				string filepath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
				filepath = Directory.GetParent(Directory.GetParent(Directory.GetParent(filepath).FullName).FullName).FullName;
				StopWordsPath = filepath + stopWordsPath;
				AnalysisTextPath = filepath + analysisTxtPath;
			}
			else if (env.ToLower() == "test")
			{
				string filepath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
				filepath = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(filepath).FullName).FullName).FullName).FullName;
				filepath = filepath + @"\WordCountAppTests";
				StopWordsPath = filepath + stopWordsPath;
				AnalysisTextPath = filepath + analysisTxtPath;
			}
			else if (env.ToLower() == "fullpath")
			{
				StopWordsPath = stopWordsPath;
				AnalysisTextPath = analysisTxtPath;
            }
            else
            {
				throw new Exception("Environment does not exist in configuration");
			}

			GetStopWords();
			StripAnalysisText(GetAnalysisText());
			StemStrippedWords();
		}

		private void GetStopWords()
		{
			List<string> stopwords = new List<string>();
			try
			{
				using (var stream = new StreamReader(StopWordsPath))
				{					
					string line = stream.ReadLine();
					while (line != null)
					{
						stopwords.Add(line);
						line = stream.ReadLine();
					}
				}
				StopWords = stopwords;
			}
			catch (IOException e)
			{
				Console.WriteLine("The file could not be read:");
				Console.WriteLine(e.Message);
			}
		}

		private List<string> GetAnalysisText()
		{
			List<string> rawAnalysisWords = new List<string>();
			try
			{
				using (var stream = new StreamReader(AnalysisTextPath))
				{
					string line = stream.ReadLine();
					while (line != null)
					{
						string[] individualStrings = line.Split(" ");
						foreach (string s in individualStrings)
						{
                            if (s.Contains("-"))
                            {
								string[] splitHypenatedWords = s.Split("-");
								foreach(string word in splitHypenatedWords)
                                {
									rawAnalysisWords.Add(word);
                                }
                            } else rawAnalysisWords.Add(s);							
						}
						line = stream.ReadLine();
					}
				}			
			}
			catch (IOException e)
			{
				Console.WriteLine("The file could not be read:");
				Console.WriteLine(e.Message);
			}
			return rawAnalysisWords;
		}

		private void StripAnalysisText(List<string> rawAnalysisWords)
		{
			List<string> strippedList = new List<string>();
			foreach (string s in rawAnalysisWords)
			{
				string p;
				string q;
				if (!string.IsNullOrEmpty(s))
				{
					if (Rgx.IsMatch(s))
					{
						p = Rgx.Replace(s, "").ToLower();
					}
					else p = s.ToLower();

					if (!StopWords.Contains(p.ToLower()) && !string.IsNullOrWhiteSpace(p))
					{
						if (Apos.IsMatch(p))
						{
							q = Apos.Replace(p, "").ToLower();
							strippedList.Add(q);
						}else strippedList.Add(p);
					}
				}
			}			
			StrippedWords = strippedList;			
		}

		private void StemStrippedWords()
		{
			List<string> stemmedList = new List<string>();
			PorterStemmer stemmer = new PorterStemmer();
			foreach (string s in StrippedWords)
			{
				stemmedList.Add(stemmer.StemWord(s));
			}
			StemmedWords = stemmedList;
		}
	}
}
