dotnet restore
dotnet pack -c Release -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg -o Packages