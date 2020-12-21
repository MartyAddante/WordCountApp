using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordCountApp;

namespace WordCountAppTests
{
    [TestClass]
    public class TestPorterStemmer
    {
        [TestMethod]
        public void Test_StemWordOutPut_Matches_StaticOutput()
        {
            string filepath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            filepath = Directory.GetParent(Directory.GetParent(Directory.GetParent(filepath).FullName).FullName).FullName;
            
            List<string> StaticOutput = new List<string>();
            List<string> TestOutput = new List<string>();
            try
            {
                string staticOutputPath = filepath + @"\StemmerTestFiles\OutputWords.txt";
                using (var stream = new StreamReader(staticOutputPath))
                {
                    
                    string line = stream.ReadLine();
                    while (line != null)
                    {
                        StaticOutput.Add(line);
                        line = stream.ReadLine();
                    }
                }                
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            PorterStemmer ps = new PorterStemmer();
            try
            {
                string staticOutputPath = filepath + @"\StemmerTestFiles\RawWords.txt";
                using (var stream = new StreamReader(staticOutputPath))
                {

                    string line = stream.ReadLine();
                    while (line != null)
                    {                        
                        TestOutput.Add(ps.StemWord(line));
                        line = stream.ReadLine();
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

                        
            Assert.IsTrue(StaticOutput.SequenceEqual(TestOutput));
        }
    }
}
