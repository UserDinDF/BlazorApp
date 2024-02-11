using BlazorAppServer.Data.Services;


namespace BlazorAppServer.Data
{
    public class FileService
    {
        public readonly FileRepository fileRepository;
        public readonly CommentRepository commentRepository;
        public readonly ImageRepository imageRepository;
        public readonly SitemapService sitemapService;

        public FileService(FileRepository fileRepository, CommentRepository commentRepository, ImageRepository imageRepository,SitemapService _sitemapService)
        {
            this.fileRepository = fileRepository;
            this.commentRepository = commentRepository;
            this.imageRepository = imageRepository;
            this.sitemapService = _sitemapService;
        }
    }
}
