using Abrazos.Persistence.Database;
using AplicationCore.Interfaces;
using AplicationCore.Models;
using AplicationCore.Utils;
using Infraestructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using System.Collections.Generic;

namespace AplicationCore.Servicios
{
    public class StoreService : IStoreService
    {
        public readonly ApplicationDbContext dbContext;
        private readonly ILogger _logger;
        private IResultApp result;

        public StoreService(ApplicationDbContext dbContext_, IResultApp _result, ILogger<StoreService> logger ) { 
            dbContext = dbContext_;
            _logger= logger;
            result = _result;
        }
        public async Task<ResultApp> GetAsync(string Id)
        {
            var store =  dbContext.Store
                    .SingleOrDefault(l => l.StoreId.Equals(Id));

            if(store != null)
            {
                var storeEntity =  new StoreDto()
                {
                    StoreId = store.StoreId,
                    Name = store.Name,
                    Coordinates = store.Coordinates.ToString()
                };
                result.Send(true, null, null, storeEntity);
                return result.GetResult();
            }

            result.Send(false, "No se encontraron resultados", null, null);
            return result.GetResult();

        }

        public async Task<ResultApp> GetNearbyStorsAsync(double latitude, double longitude, double distanceInMeters)
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

            _logger.LogInformation("Test");

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
            result.Send(false, "Resultados no encontrados", null,list);
            return result.GetResult();

        }

        public async Task<ResultApp> AddStore(StoreCommand store)
        {
            ResultApp result = new ResultApp();

            dbContext.Store.Add(mapToStore(store));
            await dbContext.SaveChangesAsync();

            result.Send(true, "Resultados Guardados", null, null);
            return result.GetResult();
        }

        public StoreEntity mapToStore(StoreCommand store)
        {
            var point = new Point(store.Longitude, store.Latitude) { SRID = 4326 };

            StoreEntity newStore = new StoreEntity()
            {
                StoreId = GenerateRandomText.GenerateRandom(),
                Name = store.Name,
                Coordinates = point
            };
            return newStore;
        }
    }
}
