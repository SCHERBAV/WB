// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//lb7. 4.3.2
$(document).ready(function () {
    // подписать кнопки пейджера на событие click
    $(".page-link").click(function (e)
    {
        e.preventDefault();
        
        var uri = this.attributes["href"].value;    // получить адрес

        $("#list").load(uri); // отправить асинхронный запрос и поместить ответ в контейнер с id="list"
        
        $(".active").removeClass("active disabled");    // снять выделение с кнопки
        
        $(this).parent().addClass("active");    // выделить текущую кнопку
        
        history.pushState(null, null, uri); // изменить адрес в адресной строке браузера
    });
});