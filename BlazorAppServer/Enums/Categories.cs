using System.ComponentModel;

namespace BlazorAppServer.Enums
{
    public enum Categories
    {
        [Description("Антивирусы и безопасность")]
        [UrlSlugAttribute("antivirus-and-security")]
        AntivirusAndSecurity,

        [Description("Офисное программное обеспечение")]
        [UrlSlug("office-software")]
        OfficeSoftware,

        [Description("Графические редакторы")]
        [UrlSlug("graphic-editors")]
        GraphicEditors,

        [Description("Мультимедиа и видео")]
        [UrlSlug("multimedia-and-video")]
        MultimediaAndVideo,

        [Description("Инструменты разработчика")]
        [UrlSlug("development-tools")]
        DevelopmentTools,

        [Description("Сетевые утилиты")]
        [UrlSlug("networking-tools")]
        NetworkingTools,

        [Description("Образовательное ПО")]
        [UrlSlug("educational-software")]
        EducationalSoftware,

        [Description("Игры и развлечения")]
        [UrlSlug("games-and-entertainment")]
        GamesAndEntertainment,

        [Description("Системные утилиты")]
        [UrlSlug("system-utilities")]
        SystemUtilities,

        [Description("Интернет и коммуникации")]
        [UrlSlug("internet-and-communication")]
        InternetAndCommunication,

        [Description("Драйверы и обновления")]
        [UrlSlug("drivers-and-updates")]
        DriversAndUpdates,

        [Description("Бизнес и финансы")]
        [UrlSlug("business-and-finance")]
        BusinessAndFinance,

        [Description("Хранение данных и резервное копирование")]
        [UrlSlug("data-storage-and-backup")]
        DataStorageAndBackup,

        [Description("Мобильные приложения")]
        [UrlSlug("mobile-apps")]
        MobileApps,

        [Description("Облачные сервисы и хранилища")]
        [UrlSlug("cloud-services-and-storage")]
        CloudServicesAndStorage,

        [Description("Обучающее ПО и тьюториалы")]
        [UrlSlug("educational-tutorials")]
        EducationalTutorials,

        [Description("ПО для здоровья и фитнеса")]
        [UrlSlug("health-and-fitness-software")]
        HealthAndFitnessSoftware,

        [Description("Родительский контроль")]
        [UrlSlug("parental-control")]
        ParentalControl,
    }

    public class UrlSlugAttribute : Attribute
    {
        public string UrlSlug { get; }

        public UrlSlugAttribute(string urlSlug)
        {
            UrlSlug = urlSlug;
        }
    }
}

