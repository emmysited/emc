/// <summary>
/// Represents the top-level GeoJSON FeatureCollection returned by the USGS earthquake API.
/// JSON structure:
/// {
///   "features": [
///     {
///       "properties": {
///         "mag": 2.5,
///         "place": "10km NE of somewhere"
///       }
///     },
///     ...
///   ]
/// }
/// </summary>
public class FeatureCollection
{
    public List<Feature> Features { get; set; } = new();
}

/// <summary>
/// Represents a single earthquake feature in the GeoJSON response.
/// </summary>
public class Feature
{
    public EarthquakeProperties Properties { get; set; } = new();
}

/// <summary>
/// Represents the properties of a single earthquake event.
/// </summary>
public class EarthquakeProperties
{
    public string Place { get; set; } = "";
    public double? Mag { get; set; }
}
