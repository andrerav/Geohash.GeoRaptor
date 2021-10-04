<a name='assembly'></a>
# Geohash.GeoRaptor

## Contents

- [GeoRaptor](#T-Geohash-GeoRaptor-GeoRaptor 'Geohash.GeoRaptor.GeoRaptor')
  - [Compress(geohashes,minimumPrecision,maximumPrecision)](#M-Geohash-GeoRaptor-GeoRaptor-Compress-System-Collections-Generic-HashSet{System-String},System-Int32,System-Int32- 'Geohash.GeoRaptor.GeoRaptor.Compress(System.Collections.Generic.HashSet{System.String},System.Int32,System.Int32)')

<a name='T-Geohash-GeoRaptor-GeoRaptor'></a>
## GeoRaptor `type`

##### Namespace

Geohash.GeoRaptor

##### Summary

Geohash.GeoRaptor is a geohash compression library for efficiently reducing the size of large geohash collections.

<a name='M-Geohash-GeoRaptor-GeoRaptor-Compress-System-Collections-Generic-HashSet{System-String},System-Int32,System-Int32-'></a>
### Compress(geohashes,minimumPrecision,maximumPrecision) `method`

##### Summary

Compresses the input set of geohashes within the given minimum and maximum precision levels.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| geohashes | [System.Collections.Generic.HashSet{System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.HashSet 'System.Collections.Generic.HashSet{System.String}') | A set of geohashes. Varying precision levels are ok |
| minimumPrecision | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The minimum precision level to maintain for the compressed output |
| maximumPrecision | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The maximum precision level to maintain for the compressed output |
