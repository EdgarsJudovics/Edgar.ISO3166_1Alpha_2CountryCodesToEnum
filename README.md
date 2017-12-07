# Edgar.ISO3166_1Alpha_2CountryCodesToEnum
Converts ISO 3166-1 alpha-2 country codes from wiki into a C# enum  

_also worst project name_  

## Usage
* Copy country list table from wiki page [here](https://en.wikipedia.org/wiki/ISO_3166-1_alpha-2)
* Save it as txt file
* Run `dotnet country2enum.dll filename.txt`

Alternatively:
* Put codes.txt into same folder as the binary `country2enum.dll`
* Run `dotnet country2enum.dll filename.txt`

The result will be saved to the same folder where `country2enum.dll` is located so make sure it is writeable

* requires .NET Core runtime