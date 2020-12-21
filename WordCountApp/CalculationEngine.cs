using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace WordCountApp
{
	public class CalculationEngine
	{
		public int WordTotal { get; set; }
		private List<string> ListToAnalyze { get; set; }
		public Dictionary<string, int> WordCount { get; set; }

		public CalculationEngine(List<String> stemmedWordList)
        {
			ListToAnalyze = stemmedWordList;

        }
		private void CreateWordCount()
        {
			Dictionary<string, int> TermsCount = new Dictionary<string, int>();
			foreach(string term in ListToAnalyze)
            {
				if (!TermsCount.ContainsKey(term))
				{
					TermsCount.Add(term, 1);
				}
				else TermsCount[term] = TermsCount[term] + 1;
            }
			WordCount = TermsCount;			
		}
		public List<KeyValuePair<string, int>> GetTopTerms(int numOfTopResults)
        {
			CreateWordCount();
			// create List of Key Value Pairs
			var WordCountKVPList = WordCount.ToList();

			WordCountKVPList.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
			List<KeyValuePair<string, int>> topTerms = new List<KeyValuePair<string, int>>();
			WordCountKVPList.Sort(
				delegate (KeyValuePair<string, int> pair1,
				KeyValuePair<string, int> pair2)
				{
					return pair1.Value.CompareTo(pair2.Value);
				}
			);
			for(int i = WordCountKVPList.Count-1; i > ((WordCountKVPList.Count-1) - numOfTopResults); i--)
            {
				topTerms.Add(WordCountKVPList[i]);
            }

			var matches = WordCountKVPList.Where(KeyValuePair => KeyValuePair.Value == topTerms.Last().Value).Select(pair => pair.Key);
            
			foreach(var kvp in matches)
            {
				if(!topTerms.Contains(new KeyValuePair<string, int>(kvp, topTerms.Last().Value)))
                {
					topTerms.Add(new KeyValuePair<string, int>(kvp, topTerms.Last().Value));
                }
            }
			return topTerms;
		}
	}
}
