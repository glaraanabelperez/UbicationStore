using Abrazos.Persistence.Database;
using AplicationCore.Interfaces;
using AplicationCore.Models;
using AplicationCore.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;

namespace AplicationCore.Servicios
{
    public class StoreService : IStoreService
    {
        public readonly ApplicationDbContext dbContext;
        private readonly GeoJsonWriter geojson;
        public StoreService(ApplicationDbContext dbContext_, GeoJsonWriter geojson_) { 
            dbContext = dbContext_;
            geojson= geojson_;
        }
        public Task<StoreDto> GetAsync(long Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<StoreDto>> GetNearbyStorsAsync(double latitude, double longitude, double distanceInMeters)
        {
            var point = new Point(longitude, latitude) { SRID = 4326 };
            var nearbyStores = await dbContext.Store
                    .Where(l => l.Coordinates.Distance(point) <= distanceInMeters)
                    .OrderBy(l => l.Coordinates.Distance(point))
                    .Select(l => new
                    {
                        l.StoreId,
                        l.Name,
                        Distance = l.Coordinates.Distance(point) // In meters
                    })
            .ToListAsync();

            List<StoreDto> list = new List<StoreDto>();
            
            foreach(var store in nearbyStores)
            {
                var st = new StoreDto()
                {
                    StoreId = store.StoreId,
                    Name = store.Name,
                    //Coordinates = geojson.Write(store.Coordinates)
                    Coordinates = store.Distance.ToString()
                };
                list.Add(st);
            };
            return list;

        }

        Task<ResultApp> IStoreService.AddStore(StoreCommand store)
        {
            throw new NotImplementedException();
        }
    }
}
