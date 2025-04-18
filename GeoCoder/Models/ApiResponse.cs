using System.Collections.Generic;
using System.Drawing;
using ThirdParty.BouncyCastle.Utilities.IO.Pem;

namespace GeoCoder.Models
{
    public class ApiResponse
    {
        public Response Response { get; set; }
        public List<Result> Results { get; set; }
    }

    public class Result
    {
        public Components Components { get; set; }
        public Geometry Geometry { get; set; }
        public string Formatted { get; set; }
    }

    public class Components
    {
        public string City { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string Road { get; set; }
    }

    public class Geometry
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
    }

    public class GeoObject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Point Point { get; set; }
    }

    public class Point
    {
        public string Pos { get; set; }
    }

    public class Response
    {
        public GeoObjectCollection GeoObjectCollection { get; set; }
    }

    public class GeoObjectCollection
    {
        public FeatureMember[] FeatureMember { get; set; }
    }

    public class FeatureMember
    {
        public GeoObject GeoObject { get; set; }
    }
}
