using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;

namespace WebPrj.TagHelpers
{
    public class PagerTagHelper : TagHelper
    {
        LinkGenerator _linkGenerator;

        public int PageCurrent { get; set; }    // номер текущей страницы
        public int PageTotal { get; set; }   // общее количество страниц
        public string PagerClass { get; set; }  // доп. css-класс для пейджера(переключателя страниц)
        public string Action { get; set; }  // имя Action(метода контроллера)
        public string Controller { get; set; }  // имя контроллера
        public int? GroupId { get; set; }   // id группы

        public PagerTagHelper(LinkGenerator linkGenerator) 
        {
            _linkGenerator = linkGenerator;    
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "nav"; // контейнер разметки пейджера

            //пейджер
            var ulTag = new TagBuilder("ul");
            ulTag.AddCssClass("pagination");
            ulTag.AddCssClass(PagerClass);

            for(int i = 1; i <= PageTotal; i++)
            {
                var url = _linkGenerator.GetPathByAction(Action, Controller, new { pageNo = i, group = GroupId == 0 ? null : GroupId });

                // получение разметки одной кнопки пейджера
                var item = GetPagerItem(url: url, text: i.ToString(), active: i == PageCurrent, disabled: i == PageCurrent);

                // добавление кнопки в разметку пейджера
                ulTag.InnerHtml.AppendHtml(item);
            }

            output.Content.AppendHtml(ulTag);   // добавление пейджера в контейнер

        }

        /// <summary>
        /// Генерирует разметку одной кнопки пейджера
        /// </summary>
        /// <param name="url">адрес</param>
        /// <param name="text">текст кнопки пейджера</param>
        /// <param name="active">признак текущей страницы</param>
        /// <param name="disabled">запретить доступ к кнопке</param>
        /// <returns>объект класса TagBuilder</returns>
        private TagBuilder GetPagerItem(string url, string text, bool active = false, bool disabled = false)
        {
            //создать тэг - <li>
            var liTag = new TagBuilder("li");
            liTag.AddCssClass("page-item");
            liTag.AddCssClass(active ? "active" : "");
            //liTag.AddCssClass(disabled ? "disabled" : "");

            //создать тэг - <a>
            var aTag = new TagBuilder("a");
            aTag.AddCssClass("page-link");
            aTag.Attributes.Add("href", url);
            aTag.InnerHtml.Append(text);

            //добавить тэг <a> внутрь тэга <li>
            liTag.InnerHtml.AppendHtml(aTag);

            return liTag;
        }
    }
}