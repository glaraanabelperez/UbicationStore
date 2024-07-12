using AplicationCore.Models;
using AplicationCore.Utils;

namespace AplicationCore.Interfaces
{
    public interface IStoreService
    {
        public Task<ResultApp> AddStore(StoreCommand store);
        public Task<StoreDto> GetAsync(long Id);
        public Task<List<StoreDto>> GetNearbyStorsAsync(double latitude, double longitude, double distanceInMeters);

    }
}
