#Introduction
The code uses an algorithm that was pulled from here: https://tartarus.org/martin/PorterStemmer/csharp3.txt which is what the class
PorterStemmer is fully made from. The code will pass in a stopword.txt file and another .txt file which will get analyzed. The code 
removes all stopwords and punctuation and then normalizes the text to its root stem (i.e. accompanied -> accompani).

#Requirements
This is built using .NET core 3.1

#Getting it to Run
The text files to test it are contained in the project itself in the folder "txtFiles". However, if you want to analyze 
a different file or set of files you will need to make a reference to it(them) in the parameters.json as well as pass 
the required variables in to the GetFilePathFromConfig() method into the constructor for the code which mutates the words 
in the .txt files.

#Output Examples

For *Alice In Wonderland* the output is:
said: 462
alic: 402
littl: 128
on: 115
look: 106
like: 97
know: 92
went: 83
thing: 80
go: 77
time: 77
queen: 76
thought: 76
sai: 71
see: 68
get: 68
king: 64
think: 64
well: 63
turtl: 61

For the Declaration of Independence the output is:
us: 11
peopl: 10
govern: 10
right: 10
law: 9
state: 9
power: 8
time: 6
refus: 5
declar: 5
establish: 5
among: 5
coloni: 4
larg: 4
assent: 4
legislatur: 4
free: 4
independ: 4
new: 4
form: 4
legisl: 4
abolish: 4
