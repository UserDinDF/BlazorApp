using BlazorAppServer.Data.Services;


namespace BlazorAppServer.Data
{
    public class FileService
    {
        public readonly FileRepository fileRepository;
        public readonly CommentRepository commentRepository;
        public readonly ImageRepository imageRepository;

        public FileService(FileRepository fileRepository, CommentRepository commentRepository, ImageRepository imageRepository)
        {
            this.fileRepository = fileRepository;
            this.commentRepository = commentRepository;
            this.imageRepository = imageRepository;
        }
    }
}
