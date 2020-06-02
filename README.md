# String Matcher

This Function App is accountable for:

1. receiving and parse a text and subtext
2. performing a string matcher algorithm to match all ocurrencies of the subtext in the text
3. responding with a JSON object containing all the found matches.

## Implementation notes

1. The string matching algorithm used as part of this exercise is the Knuth-Morris-Pratt
2. Dependency Injection pattern has been used across various services classses

## Key technologies

* C# (.Net Core 2.2 for [Function Apps](https://docs.microsoft.com/en-us/azure/azure-functions/functions-overview)
* XUnit
* Nsubstitute
* Autofac

## Getting started

* Install [Git](https://git-scm.com/download/win)
* install Visual Studio with support to the following [workloads](https://www.visualstudio.com/vs/support/selecting-workloads-visual-studio-2017/):
	* Universal Windows Platform development
	* Azure development

* Clone the repo `git@github.com:osiro/string-matcher.git`
* Open the Visual Studio solution
* Build and run app

This should make the API available from: `http://localhost:7071/api/StringMatch?text={text}&subtext={subtext}`
