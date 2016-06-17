using Kendo.Helpers.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using static Newtonsoft.Json.JsonConvert;
using static Newtonsoft.Json.DefaultValueHandling;


[JsonObject]
public class PageableMessagesConfiguration : IKendoObject
{

    /// <summary>
    /// Name of the json property that holds the "display" configuration
    /// </summary>
    public const string DisplayPropertyName = "display";

    /// <summary>
    /// Name of the json property that holds the "display" configuration
    /// </summary>
    public const string EmptyPropertyName = "empty";


    /// <summary>
    /// Name of the json property that holds the "itemsPerPage" configuration
    /// </summary>
    public const string ItemsPerPagePropertyName = "itemsPerPage";


    /// <summary>
    /// Name of the json property that holds the "refresh" configuration
    /// </summary>
    public const string RefreshPropertyName = "refresh";


    /// <summary>
    /// Name of the json property that holds the "of" configuration
    /// </summary>
    public const string OfPropertyName = "of";


    /// <summary>
    /// Name of the json property that holds the "first" configuration
    /// </summary>
    public const string FirstPropertyName = "first";


    /// <summary>
    /// Name of the json property that holds the "last" configuration
    /// </summary>
    public const string LastPropertyName = "last";


    /// <summary>
    /// Name of the json property that holds the "next" configuration
    /// </summary>
    public const string NextPropertyName = "next";


    /// <summary>
    /// Name of the json property that holds the "display" configuration
    /// </summary>
    public const string PreviousPropertyName = "previous";

    /// <summary>
    /// Name of the json property that holds the "page" configuration
    /// </summary>
    public const string PagePropertyName = "page";

    /// <summary>
    /// Name of the json property that holds the "morePages" configuration
    /// </summary>
    public const string MorePagesPropertyName = "morePages";


    public static JSchema Schema => new JSchema
    {
        Type = JSchemaType.Object,
        MinimumProperties = 1,
        AllowAdditionalProperties = false,
        Properties =
        {
            [DisplayPropertyName] = new JSchema { Type = JSchemaType.String, Default = "{0} - {1} of {2} items"},
            [EmptyPropertyName] = new JSchema { Type = JSchemaType.String, Default = "No items to display"},
            [PagePropertyName] = new JSchema { Type = JSchemaType.String, Default = "Page" },
            [OfPropertyName] = new JSchema { Type = JSchemaType.String, Default = "of {0}" },
            [ItemsPerPagePropertyName] = new JSchema {Type = JSchemaType.String, Default = "items per page" },
            [FirstPropertyName] = new JSchema { Type = JSchemaType.String, Default = "Go to the first page" },
            [LastPropertyName] = new JSchema { Type = JSchemaType.String, Default = "Go to the last page" },
            [NextPropertyName] = new JSchema { Type = JSchemaType.String, Default = "Go to the next page" },
            [PreviousPropertyName] = new JSchema { Type = JSchemaType.String, Default = "Go to the previous page" },
            [RefreshPropertyName] = new JSchema { Type = JSchemaType.String, Default = "Refresh" },
            [MorePagesPropertyName] = new JSchema { Type = JSchemaType.String, Default = "More pages" }

        }

    };

    [JsonProperty(PropertyName = DisplayPropertyName, DefaultValueHandling = IgnoreAndPopulate)]
    public string Display { get; set; }

    [JsonProperty(PropertyName = EmptyPropertyName, DefaultValueHandling = IgnoreAndPopulate)]
    public string Empty { get; set; }

    [JsonProperty(PropertyName = RefreshPropertyName, DefaultValueHandling = IgnoreAndPopulate)]
    public string Refresh { get; set; }

    [JsonProperty(PropertyName = PagePropertyName, DefaultValueHandling = IgnoreAndPopulate)]
    public string Page { get; set; }

    [JsonProperty(PropertyName = OfPropertyName, DefaultValueHandling = IgnoreAndPopulate)]
    public string Of { get; set; }

    [JsonProperty(PropertyName = ItemsPerPagePropertyName, DefaultValueHandling = IgnoreAndPopulate)]
    public string ItemsPerPage { get; set; }

    [JsonProperty(PropertyName = FirstPropertyName, DefaultValueHandling = IgnoreAndPopulate)]
    public string First { get; set; }

    [JsonProperty(PropertyName = LastPropertyName, DefaultValueHandling = IgnoreAndPopulate)]
    public string Last { get; set; }

    [JsonProperty(PropertyName = NextPropertyName, DefaultValueHandling = IgnoreAndPopulate)]
    public string Next { get; set; }

    [JsonProperty(PropertyName = PreviousPropertyName, DefaultValueHandling = IgnoreAndPopulate)]
    public string Previous { get; set; }

    [JsonProperty(PropertyName = MorePagesPropertyName, DefaultValueHandling = IgnoreAndPopulate)]
    public string MorePages { get; set; }


#if DEBUG
    public override string ToString() => ToJson();
#endif

    public virtual string ToJson()
#if DEBUG
            => SerializeObject(this, Formatting.Indented);
#else
            => SerializeObject(this);
#endif


}