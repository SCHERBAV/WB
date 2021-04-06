namespace WebPrj.Models
{
    public class MenuItem
    {
        public bool IsRazorPageOrControllerMethod { get; set; } //является ли вызываемая ссылка Razor страницей или методом контроллера
        public string Text { get; set; } //текст надписи
        public string NameMethod { get; set; }  //имя метода контроллера
        public string NamePage { get; set; }    //имя страницы
        public string Action { get; set; }  //действие
        public string NameController { get; set; }  //имя контроллера
        public string NameArea { get; set; }    //имя области
        public string NameCSSActive { get; set; }  //имя класса CSS для активного пункта меню
    }
}