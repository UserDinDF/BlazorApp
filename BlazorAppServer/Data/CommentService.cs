using Microsoft.EntityFrameworkCore;
using BlazorAppServer.Data;

namespace BlazorAppServer.Data
{
    public class CommentService
    {
        private readonly ApplicationDbContext _service;
        public CommentService(ApplicationDbContext service)
        {
            _service = service;
        }

        public async Task<List<CommentModel>> GetCommentsAsync()
        {
            var item = _service.Comments.FirstOrDefault();

            return await _service.Comments.ToListAsync();
        }


        public async Task<CommentModel> GetLoadAsync(int id)
        {
            CommentModel load = await _service.Comments.SingleOrDefaultAsync(x => x.Id == id);
            return load;
        }

        public async Task<bool> InsertLoadAsync(CommentModel load)
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

        public async Task<bool> UpdateLoadAsync(CommentModel load)
        {
            _service.Comments.Update(load);
            await _service.SaveChangesAsync();

            return true;
        }
    }
}
