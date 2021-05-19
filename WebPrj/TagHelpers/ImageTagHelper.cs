using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;

namespace WebPrj.TagHelpers
{
    [HtmlTargetElement(tag: "img", Attributes = "img-action, img-controller")]
    public class ImageTagHelper : TagHelper
    {
        LinkGenerator _linkGenerator;

        public string ImgAction { get; set; }  // имя Action(метода контроллера)
        public string ImgController { get; set; }  // имя контроллера

        public ImageTagHelper(LinkGenerator linkGenerator)
        {
            _linkGenerator = linkGenerator;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var uri = _linkGenerator.GetPathByAction(ImgAction, ImgController);
            output.Attributes.Add("src", uri);
        }

    }
}