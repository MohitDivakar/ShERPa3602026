using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShERPa360net.Class
{
    public class MobexListing
    {
        public List<SourceItem> sourceItems { get; set; }
    }

    public class SourceItem
    {
        public string sku { get; set; }
        public string source_code { get; set; }
        public decimal quantity { get; set; }
        public int status { get; set; }
    }

    public class CustomAttribute
    {
        public string attribute_code { get; set; }
        public decimal value { get; set; }
    }

    public class Product
    {
        public decimal price { get; set; }
        public List<CustomAttribute> custom_attributes { get; set; }
    }

    public class MobexPrice
    {
        public Product product { get; set; }
    }

    public class MobexPriceResponse
    {
        public int id { get; set; }
        public string sku { get; set; }
        public string name { get; set; }
        public int attribute_set_id { get; set; }
        public decimal price { get; set; }
        public int status { get; set; }
        public int visibility { get; set; }
        public string type_id { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public int weight { get; set; }
        public Extension extension_attributes { get; set; }
        public List<object> product_links { get; set; }
        public List<object> options { get; set; }
        public List<MediaGalleryEntry> media_gallery_entries { get; set; }
        public List<object> tier_prices { get; set; }
        public List<Custom> custom_attributes { get; set; }
    }

    public class MediaGalleryEntry
    {
        public int id { get; set; }
        public string media_type { get; set; }
        public object label { get; set; }
        public int position { get; set; }
        public bool disabled { get; set; }
        public List<string> types { get; set; }
        public string file { get; set; }
    }

    public class Extension
    {
        public List<int> website_ids { get; set; }
        public List<CategoryLink> category_links { get; set; }
    }

    public class Custom
    {
        public string attribute_code { get; set; }
        public object value { get; set; }
    }

    public class CategoryLink
    {
        public int position { get; set; }
        public string category_id { get; set; }
    }

    public class MobexBadRequestResponse
    {
        public string message { get; set; }
    }

}