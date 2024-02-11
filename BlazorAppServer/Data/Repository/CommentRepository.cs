using Microsoft.EntityFrameworkCore;
using BlazorAppServer.Data;
using BlazorAppServer;
using BlazorAppServer.Data.Services;
using BlazorAppServer.Data.Models;

namespace BlazorAppServer.Data.Services
{
    public class CommentRepository
    {
        private readonly ApplicationDbContext _service;
        public CommentRepository(ApplicationDbContext service)
        {
            _service = service;
        }

        public async Task<List<CommentModel>> GetCommentsAsync()
        {
            var item = _service.Comments.FirstOrDefault();

            return await _service.Comments.ToListAsync();
        }

        public async Task<List<CommentModel>> GetCommentsForFileAsync(int fileId)
        {
            return await _service.Comments
                .Where(c => c.FileModel.Id == fileId)
                .ToListAsync();
        }


        public async Task<CommentModel> GetCommentAsync(int id)
        {
            CommentModel load = await _service.Comments.SingleOrDefaultAsync(x => x.Id == id);
            return load;
        }

        public async Task<bool> InsertCommentAsync(CommentModel load)
        {
            await _service.Comments.AddAsync(load);
            await _service.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveCommentAsync(CommentModel load)
        {
            _service.Comments.Remove(load);
            await _service.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateCommentAsync(CommentModel load)
        {
            _service.Comments.Update(load);
            await _service.SaveChangesAsync();

            return true;
        }
    }
}
