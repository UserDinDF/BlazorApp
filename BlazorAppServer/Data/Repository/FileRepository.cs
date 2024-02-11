using BlazorAppServer;
using BlazorAppServer.Data;
using BlazorAppServer.Data.Models;
using BlazorAppServer.Data.Services;
using Microsoft.EntityFrameworkCore;

namespace BlazorAppServer.Data.Services
{
    public class FileRepository : IFileUpdatePublisher
    {
        public event Action OnFileUpdated;

        private readonly ApplicationDbContext _service;
  
        public FileRepository(ApplicationDbContext service)
        {
            _service = service;
        }

        public async Task<List<FileModel>> GetLoadsAsync()
        {
            var item = _service.Loads.FirstOrDefault();

            return await _service.Loads.ToListAsync();
        }


        public async Task<List<FileModel>> GetLoadFromCategorie(string categoriename)
        {
            var items = await _service.Loads.Where(x => x.Category == categoriename).ToListAsync();
            return items;
        }

        public async Task<FileModel> GetLoadAsync(int id)
        {
            FileModel load = await _service.Loads.SingleOrDefaultAsync(x => x.Id == id);
            return load;
        }

        public async Task<bool> InsertLoadAsync(FileModel load)
        {
            await _service.Loads.AddAsync(load);
            await _service.SaveChangesAsync();
            NotifyFileUpdated(); 
            return true;
        }

        public async Task<bool> RemoveLoadAsync(FileModel load)
        {
            _service.Loads.Remove(load);
            await _service.SaveChangesAsync();
            NotifyFileUpdated();
            return true;
        }

        public async Task<bool> UpdateLoadAsync(FileModel load)
        {
            _service.Loads.Update(load);
            await _service.SaveChangesAsync();

            return true;
        }

        public void NotifyFileUpdated() => OnFileUpdated?.Invoke();

    }

    public interface IFileUpdatePublisher
    {
        event Action OnFileUpdated;
        void NotifyFileUpdated();
    }
}