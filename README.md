# Geohash.GeoRaptor: .NET Geohash Compression Tool
_Note: This is a port of [ashwin711/georaptor](https://github.com/ashwin711/georaptor) to C#/.NET._

Geohash.GeoRaptor is a geohash compression library for efficiently reducing the size of large geohash collections. If you are wondering what geohashes are all about, I can recommend checking out [this website by Movable Type](https://www.movable-type.co.uk/scripts/geohash.html) which explains geohashing in simple terms while at the same time providing a highly visual technical demo.

## Download
| Package | Link |
| ------- | ---- | 
| Geohash.GeoRaptor | [![image](https://img.shields.io/nuget/v/Geohash.GeoRaptor.svg)](https://www.nuget.org/packages/Geohash.GeoRaptor/) |

# API Quickstart
```csharp
    var geohashes = new HashSet<string>
    {
      "w21zf9", "w21zfc", "w21zg1",
      "w21zg3", "w21zg9", "w21zgc2",
      "w21zu1", "w21zu3", "w21zu9",
      "w21zuc", "w21zv1", "w21zv3",
      "w21zv9", "w21zvc", "w21zy1",
      "w21zy3", "w21zy9", "w21zyc",
      "w21xy8", "w21xyb", "w21xz0"
    };

    var result = GeoRaptor.Compress(_geoHashes, 3, 4);
```
The above snippet of code will compress the input geohashes to a minimum precision of 3 and a maximum precision of 4. The result from this computation is `["w23b","w238","w21x","w21z","w21w"]` and is assigned to the `result` variable. 


