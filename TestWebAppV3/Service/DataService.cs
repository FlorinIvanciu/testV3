using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWebAppV3.JsonPlaceHolderData;
using TestWebAppV3.Models;

namespace TestWebAppV3.Service
{
    public class DataService : IDataService
    {
        private readonly JsonPlaceHolderClient jsonPlaceHolderClient;

        public DataService(JsonPlaceHolderClient jsonPlaceHolderClient)
        {
            this.jsonPlaceHolderClient = jsonPlaceHolderClient ?? throw new ArgumentNullException(nameof(jsonPlaceHolderClient));
        }

        public async Task<IEnumerable<AlbumAndPhoto>> GetDataAsync()
        {
            return await GetAlbumsAndPhotosAsync();
        }

        public async Task<IEnumerable<AlbumAndPhoto>> GetDataByUserIdAsync(int id)
        {
            return (await GetAlbumsAndPhotosAsync()).Where(a => a.UserId == id);
        }

        private async Task<IEnumerable<AlbumAndPhoto>> GetAlbumsAndPhotosAsync()
        {
            var albumsTask = jsonPlaceHolderClient.GetAlbumsAsync();
            var photosTask = jsonPlaceHolderClient.GetPhotosAsync();

            await Task.WhenAll(albumsTask, photosTask);
            var albums = albumsTask.Result;
            var photos = photosTask.Result;

            return albums.SelectMany(a => photos.Where(p => p.AlbumId == a.Id).Select(d => new AlbumAndPhoto()
            {
                AlbumName = a.Title,
                PhotoTitle = d.Title,
                Thumbnail = d.ThumbnailUrl,
                Url = d.Url,
                UserId = a.UserId
            }));
        }
    }
}
