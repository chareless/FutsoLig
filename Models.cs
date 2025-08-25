using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FutsoLig
{
    internal class Models
    {
        public class PassoResponse
        {
            [JsonPropertyName("totalItemCount")]
            public int TotalItemCount { get; set; }

            [JsonPropertyName("valueList")]
            public List<EventItem> ValueList { get; set; } = new();
        }

        public class EventItem
        {
            [JsonPropertyName("name")]
            public string Name { get; set; } = "";

            [JsonPropertyName("date")]
            public DateTime Date { get; set; }

            [JsonPropertyName("venueName")]
            public string VenueName { get; set; } = "";

            [JsonPropertyName("id")]
            public int id { get; set; } = 0;

            [JsonPropertyName("homeTeamName")]
            public string? HomeTeamName { get; set; }

            [JsonPropertyName("seoUrl")]
            public string SeoUrl { get; set; } = "";
        }

        public class EventsRequest
        {
            public string SubGenreId { get; set; } = "8617";
            public int LanguageId { get; set; } = 118;
            public int from { get; set; } = 0;
            public int size { get; set; } = 53;
        }

        public class CategoryResponse
        {
            [JsonPropertyName("valueList")]
            public List<CategoryItem> ValueList { get; set; } = new();
        }

        public class CategoryItem
        {
            [JsonPropertyName("id")]
            public int Id { get; set; }

            [JsonPropertyName("name")]
            public string Name { get; set; } = "";

            [JsonPropertyName("formattedPrice")]
            public string FormattedPrice { get; set; } = "";

            [JsonPropertyName("message")]
            public string? Message { get; set; }
        }

        public class BlockItem
        {
            [JsonPropertyName("totalCount")]
            public int TotalCount { get; set; }

            [JsonPropertyName("categoriesCount")]
            public int CategoriesCount { get; set; }

            [JsonPropertyName("id")]
            public int Id { get; set; }

            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("guidId")]
            public string GuidId { get; set; }

            [JsonPropertyName("blockLayoutType")]
            public int BlockLayoutType { get; set; }

            public override string ToString()
            {
                return $"{Name} (Boş: {TotalCount})";
            }
        }

        public class BlockResponse
        {
            [JsonPropertyName("totalItemCount")]
            public int TotalItemCount { get; set; }

            [JsonPropertyName("valueList")]
            public List<BlockItem> ValueList { get; set; }

            [JsonPropertyName("isError")]
            public bool IsError { get; set; }

            [JsonPropertyName("resultCode")]
            public int ResultCode { get; set; }
        }

    }
}
