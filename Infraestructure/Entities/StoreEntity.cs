
using NetTopologySuite.Geometries;

namespace Infraestructure.Entities
{
    public class StoreEntity
    {
        public long StoreId { get; set; }
        public string Name { get; set; }
        public Point Coordinates { get; set; }

        //public string Direction { get; set; }
        //public decimal Latitude { get; set; }
        //public decimal Longitude { get; set; }

    }
}