using Microsoft.EntityFrameworkCore;

namespace BlazorAppServer.Data
{
    public class FileService
    {
        private readonly ApplicationDbContext _service;
        public FileService(ApplicationDbContext service)
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
            var items = await _service.Loads.Where(x=>x.Category == categoriename).ToListAsync();
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

            return true;
        }

        public async Task<bool> RemoveLoadAsync(FileModel load)
        {
            _service.Loads.Remove(load);
            await _service.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateLoadAsync(FileModel load)
        {
            _service.Loads.Update(load);
            await _service.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AddTestModel()
        {
            //FileModel model = new()
            //{
            //    Category = "WiFi",
            //    DownloadUrl = "https://www.softportal.com/get-50082-wifi2hotspot.html",
            //    Comments = 2,
            //    CreationTime = DateTime.Now,
            //    Description = "WiFi2Hotspot - это удобное приложение для создания виртуального WiFi-соединения, которым можно легко поделиться с другими устройствами. Программа разработана с учетом простоты использования и позволяет быстро настроить виртуальное беспроводное соединение.",
            //    DescriptionFull = "WiFi2Hotspot - это удобное приложение для создания виртуального WiFi-соединения, которым можно легко поделиться с другими устройствами. Программа разработана с учетом простоты использования и позволяет быстро настроить виртуальное беспроводное соединение.\r\n\r\nПосле завершения настройки, ваша точка доступа появится в Центре управления сетями и общим доступом в Панели управления. Интуитивный процесс активации соединения и разделения доступа к Интернету обеспечивает удобство использования WiFi2Hotspot.\r\n\r\nОсновные возможности приложения WiFi2Hotspot:\r\n\r\nПростота настройки: Благодаря интуитивно понятному интерфейсу, создание виртуального WiFi становится легким процессом, не требующим специальных знаний.\r\nШифрование WPA2-PSK: Обеспечивайте безопасность вашего соединения с использованием надежного шифрования WPA2-PSK для новой точки доступа.\r\nСовместимость с различными устройствами: Используйте WiFi2Hotspot на ноутбуках, планшетах и других совместимых устройствах с встроенным WiFi адаптером.\r\nГибкость использования: Подключайтесь к Интернету по WiFi смартфонами, планшетами или устройствами без поддержки кабельных роутеров или DSL-модемов.\r\nЛегкость активации и деактивации: Управляйте доступом к созданному соединению, активируя или деактивируя его в удобное для вас время.",
            //    Downloads = 50,
            //    Image = "https://www.softportal.com/scr/50082/wifi2hotspot-big-1.png",
            //    Keywords = "WiFi, Программа, Хотспорт, виртуальный WiFi",
            //    Language = "en",
            //    Name = "WiFi2Hotspot",
            //    Rating = 5,
            //    Seo_Description = "Простое в использовании приложение для создания виртуального WiFi, обеспечивающее безопасный и быстрый доступ к Интернету на различных устройствах. Поддерживая шифрование WPA2-PSK, приложение гарантирует надежную защиту вашего беспроводного соединения",
            //    Seo_Keywords = "Seo WiFi, Программа, Хотспорт, виртуальный WiFi",
            //    Seo_Title = "WiFi2Hotspot - скачать бесплатно WiFi2Hotspot 1.1",
            //    Seo_Url = "wifi2hotspot",
            //    UpdateTime = DateTime.Now,            
            //    Views = 100
            //};
            return new bool();
        }
    }
}