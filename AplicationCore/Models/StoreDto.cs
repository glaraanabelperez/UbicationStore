using NetTopologySuite.Geometries;

namespace AplicationCore.Models
{
    public class StoreDto
    {
        public long StoreId { get; set; }
        public string Name { get; set; }
        public string Coordinates { get; set; }
    }
}
