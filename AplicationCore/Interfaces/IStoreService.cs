using AplicationCore.Models;
using AplicationCore.Utils;

namespace AplicationCore.Interfaces
{
    public interface IStoreService
    {
        public Task<ResultApp> AddStore(StoreCommand store);
        public Task<ResultApp> GetAsync(string Id);
        public Task<ResultApp> GetNearbyStorsAsync(double latitude, double longitude, double distanceInMeters);

    }
}
