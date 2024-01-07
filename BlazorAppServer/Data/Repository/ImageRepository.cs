using BlazorAppServer;
using BlazorAppServer.Data;
using BlazorAppServer.Data.Models;
using BlazorAppServer.Data.Services;
using Microsoft.EntityFrameworkCore;

namespace BlazorAppServer.Data.Services
{
    public class ImageRepository
    {
        private readonly ApplicationDbContext _service;
        public ImageRepository(ApplicationDbContext service)
        {
            _service = service;
        }

        public async Task<List<ImageModel>> GetLoadsAsync()
        {
            var item = _service.Images.FirstOrDefault();

            return await _service.Images.ToListAsync();
        }

        public async Task<ImageModel> GetImageFirstAsync(int id)
        {
            ImageModel load = await _service.Images.SingleOrDefaultAsync(x => x.FileModelId == id);
            return load;
        }

        public async Task<List<ImageModel>> GetImagesAsync(int fileId)
        {
            List<ImageModel> load = await _service.Images.Where(x => x.FileModelId == fileId).ToListAsync();
            return load;
        }

        public async Task<bool> InsertLoadAsync(ImageModel load)
        {
            await _service.Images.AddAsync(load);
            await _service.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveLoadAsync(ImageModel load)
        {
            _service.Images.Remove(load);
            await _service.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateLoadAsync(ImageModel load)
        {
            _service.Images.Update(load);
            await _service.SaveChangesAsync();

            return true;
        }
    }
}
