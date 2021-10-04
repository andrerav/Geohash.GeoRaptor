<a name='assembly'></a>
# Geohash.GeoRaptor.CLI

## Contents

- [Program](#T-Geohash-GeoRaptor-CLI-Program 'Geohash.GeoRaptor.CLI.Program')
  - [BoundingBox(geohash)](#M-Geohash-GeoRaptor-CLI-Program-BoundingBox-System-String- 'Geohash.GeoRaptor.CLI.Program.BoundingBox(System.String)')
  - [CompressGeohashes(o)](#M-Geohash-GeoRaptor-CLI-Program-CompressGeohashes-Geohash-GeoRaptor-CLI-CommandLineOptions- 'Geohash.GeoRaptor.CLI.Program.CompressGeohashes(Geohash.GeoRaptor.CLI.CommandLineOptions)')
  - [GetInputReader(o)](#M-Geohash-GeoRaptor-CLI-Program-GetInputReader-Geohash-GeoRaptor-CLI-CommandLineOptions- 'Geohash.GeoRaptor.CLI.Program.GetInputReader(Geohash.GeoRaptor.CLI.CommandLineOptions)')
  - [LoadGeohashes(reader,geohashes)](#M-Geohash-GeoRaptor-CLI-Program-LoadGeohashes-System-IO-TextReader,System-Collections-Generic-HashSet{System-String}- 'Geohash.GeoRaptor.CLI.Program.LoadGeohashes(System.IO.TextReader,System.Collections.Generic.HashSet{System.String})')
  - [OutputCompressedGeohashes(o,compressed)](#M-Geohash-GeoRaptor-CLI-Program-OutputCompressedGeohashes-Geohash-GeoRaptor-CLI-CommandLineOptions,System-Collections-Generic-HashSet{System-String}- 'Geohash.GeoRaptor.CLI.Program.OutputCompressedGeohashes(Geohash.GeoRaptor.CLI.CommandLineOptions,System.Collections.Generic.HashSet{System.String})')
  - [ValidateArguments(o)](#M-Geohash-GeoRaptor-CLI-Program-ValidateArguments-Geohash-GeoRaptor-CLI-CommandLineOptions- 'Geohash.GeoRaptor.CLI.Program.ValidateArguments(Geohash.GeoRaptor.CLI.CommandLineOptions)')
  - [ValidateReader(reader)](#M-Geohash-GeoRaptor-CLI-Program-ValidateReader-System-IO-TextReader- 'Geohash.GeoRaptor.CLI.Program.ValidateReader(System.IO.TextReader)')
  - [WriteError(message)](#M-Geohash-GeoRaptor-CLI-Program-WriteError-System-String- 'Geohash.GeoRaptor.CLI.Program.WriteError(System.String)')

<a name='T-Geohash-GeoRaptor-CLI-Program'></a>
## Program `type`

##### Namespace

Geohash.GeoRaptor.CLI

<a name='M-Geohash-GeoRaptor-CLI-Program-BoundingBox-System-String-'></a>
### BoundingBox(geohash) `method`

##### Summary

Converts a valid geohash to its corresponding bounding box

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| geohash | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-Geohash-GeoRaptor-CLI-Program-CompressGeohashes-Geohash-GeoRaptor-CLI-CommandLineOptions-'></a>
### CompressGeohashes(o) `method`

##### Summary

Compress the geohashes using the specified precision range.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| o | [Geohash.GeoRaptor.CLI.CommandLineOptions](#T-Geohash-GeoRaptor-CLI-CommandLineOptions 'Geohash.GeoRaptor.CLI.CommandLineOptions') |  |

<a name='M-Geohash-GeoRaptor-CLI-Program-GetInputReader-Geohash-GeoRaptor-CLI-CommandLineOptions-'></a>
### GetInputReader(o) `method`

##### Summary

Returns an input reader based on whether the user has specified an input file or not

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| o | [Geohash.GeoRaptor.CLI.CommandLineOptions](#T-Geohash-GeoRaptor-CLI-CommandLineOptions 'Geohash.GeoRaptor.CLI.CommandLineOptions') |  |

<a name='M-Geohash-GeoRaptor-CLI-Program-LoadGeohashes-System-IO-TextReader,System-Collections-Generic-HashSet{System-String}-'></a>
### LoadGeohashes(reader,geohashes) `method`

##### Summary

Loads geohashes from the reader and stores them in the HashSet.
It is assumed that each line contains a single geohash.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| reader | [System.IO.TextReader](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IO.TextReader 'System.IO.TextReader') |  |
| geohashes | [System.Collections.Generic.HashSet{System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.HashSet 'System.Collections.Generic.HashSet{System.String}') |  |

<a name='M-Geohash-GeoRaptor-CLI-Program-OutputCompressedGeohashes-Geohash-GeoRaptor-CLI-CommandLineOptions,System-Collections-Generic-HashSet{System-String}-'></a>
### OutputCompressedGeohashes(o,compressed) `method`

##### Summary

Outputs compressed geohashes to standard output

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| o | [Geohash.GeoRaptor.CLI.CommandLineOptions](#T-Geohash-GeoRaptor-CLI-CommandLineOptions 'Geohash.GeoRaptor.CLI.CommandLineOptions') |  |
| compressed | [System.Collections.Generic.HashSet{System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.HashSet 'System.Collections.Generic.HashSet{System.String}') |  |

<a name='M-Geohash-GeoRaptor-CLI-Program-ValidateArguments-Geohash-GeoRaptor-CLI-CommandLineOptions-'></a>
### ValidateArguments(o) `method`

##### Summary

Validates the arguments specified by the user

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| o | [Geohash.GeoRaptor.CLI.CommandLineOptions](#T-Geohash-GeoRaptor-CLI-CommandLineOptions 'Geohash.GeoRaptor.CLI.CommandLineOptions') |  |

<a name='M-Geohash-GeoRaptor-CLI-Program-ValidateReader-System-IO-TextReader-'></a>
### ValidateReader(reader) `method`

##### Summary

Validate that there is input on the reader

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| reader | [System.IO.TextReader](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IO.TextReader 'System.IO.TextReader') |  |

<a name='M-Geohash-GeoRaptor-CLI-Program-WriteError-System-String-'></a>
### WriteError(message) `method`

##### Summary

Writes an error message to stderr

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
