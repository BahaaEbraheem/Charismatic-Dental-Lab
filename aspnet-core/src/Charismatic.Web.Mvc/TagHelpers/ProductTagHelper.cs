using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Charismatic.Web.TagHelpers
{
    [HtmlTargetElement("Product")]
    public class ProductTagHelper : TagHelper
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public string Image { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.SetHtmlContent(
                $@"<div class=""col-6"">
                    <div class=""product-box"">
                        <div class=""row w-100 mx-auto"">
                            <div class=""col-12 col-md-6 mx-auto p-0"">
                                <div class=""property-image"">
                                    <img class=""img-responsive"" src=""{Image}"" alt=""Product"">
                                </div>
                            </div>
                            <div class=""col-12 col-md-6 mx-auto align-self-center p-0"">
                                <div class=""product-details p-3 p-md-4 p-lg-5"">
                                    <h5 class=""text-blue mb-4"">{Name}</h5>
                                    <p>
                                          Lorem ipsum dolor sit amet consectetur adipisicing elit. Assumenda inventore,
                                          beatae maiores labore modi in dolor vel totam ab veritatis facilis explicabo,
                                    </p>
                                    <button class=""bg-blue rounded-2 px-3"">More</button>
                                </div><!--.product-details-->
                            </div>
                        </div><!--.row-->
                    </div><!--.product-box-->
                </div><!--.col-6-->"
                );
        }
}
}
