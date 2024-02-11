using BlazorAppServer.Data.Services;
using System.Text;
using System.Xml;

namespace BlazorAppServer.Data
{
    public class SitemapService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly FileRepository _fileRepository;

        public SitemapService(IWebHostEnvironment webHostEnvironment, FileRepository fileRepository)
        {
            _webHostEnvironment = webHostEnvironment;
            _fileRepository = fileRepository;
            _fileRepository.OnFileUpdated += async () => await GenerateSitemapXml();

        }

        public async Task GenerateSitemapXml()
        {
            var sitemapPath = Path.Combine(_webHostEnvironment.WebRootPath, "sitemap.xml");
            var files = await _fileRepository.GetLoadsAsync();
            var stringBuilder = new StringBuilder();
            using (var xmlWriter = XmlWriter.Create(stringBuilder, new XmlWriterSettings { Indent = true }))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9");

                foreach (var item in files)
                {
                    xmlWriter.WriteStartElement("url");
                    xmlWriter.WriteElementString("loc", $"https://soft-portal.org{item.GetFilePath()}");
                    xmlWriter.WriteEndElement();
                }

                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
            }

            await File.WriteAllTextAsync(sitemapPath, stringBuilder.ToString());
        }
    }
}
