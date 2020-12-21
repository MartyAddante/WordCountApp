using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordCountApp;

namespace WordCountAppTests
{
    [TestClass]
    public class TestTextMutatorAndCalcEngine
    {
        [TestMethod]        
        public void Test_StaticTopTerms_Matches_TestTopTerms()
        {
            List<KeyValuePair<string, int>> StaticTopTerms = new List<KeyValuePair<string, int>>();
            StaticTopTerms.Add(new KeyValuePair<string, int>("wish", 12));
            StaticTopTerms.Add(new KeyValuePair<string, int>("buffalo", 8));
            StaticTopTerms.Add(new KeyValuePair<string, int>("on", 7));
            StaticTopTerms.Add(new KeyValuePair<string, int>("lake", 6));
            StaticTopTerms.Add(new KeyValuePair<string, int>("luck", 5));
            StaticTopTerms.Add(new KeyValuePair<string, int>("luke", 5));


            TextMutator tm = new TextMutator("\\calcEngineTestFiles\\TestStopWords.txt", "\\calcEngineTestFiles\\TestAnalysisText.txt", "Test");
            CalculationEngine ce = new CalculationEngine(tm.StemmedWords);
            List<KeyValuePair<string, int>> TestTopTerms = ce.GetTopTerms(5);

            Assert.IsTrue(StaticTopTerms.SequenceEqual(TestTopTerms));            
        }     
    }
}
