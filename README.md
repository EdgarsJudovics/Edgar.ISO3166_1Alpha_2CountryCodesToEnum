# Edgar.ISO3166_1Alpha_2CountryCodesToEnum
Converts ISO 3166-1 alpha-2 country codes from wiki into a C# enum  

_also worst project name_  

## Usage
* Copy table from wiki page
* Save it as txt file
* `dotnet country2enum.dll filename.txt`

Or alternatively save country file as codes.txt and put it in the same folder as binary and simply run `dotnet country2enum.dll`

* requires net core runtime