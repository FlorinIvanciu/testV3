using System.Collections.Generic;
using System.Threading.Tasks;
using TestWebAppV3.Models;

namespace TestWebAppV3.Service
{
    public interface IDataService
    {
        Task<IEnumerable<AlbumAndPhoto>> GetDataAsync();
        Task<IEnumerable<AlbumAndPhoto>> GetDataByUserIdAsync(int id);
    }
}