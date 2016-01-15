using Kendo.Helpers.Core;
using Newtonsoft.Json.Schema;
using System.Runtime.Serialization;
using static Newtonsoft.Json.JsonConvert;

[DataContract]
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

    [DataMember(Name = DisplayPropertyName, EmitDefaultValue = false)]
    public string Display { get; set; }

    [DataMember(Name = EmptyPropertyName, EmitDefaultValue = false)]
    public string Empty { get; set; }

    [DataMember(Name = RefreshPropertyName, EmitDefaultValue = false)]
    public string Refresh { get; set; }

    [DataMember(Name = PagePropertyName, EmitDefaultValue = false)]
    public string Page { get; set; }

    [DataMember(Name = OfPropertyName, EmitDefaultValue = false)]
    public string Of { get; set; }

    [DataMember(Name = ItemsPerPagePropertyName, EmitDefaultValue = false)]
    public string ItemsPerPage { get; set; }

    [DataMember(Name = FirstPropertyName, EmitDefaultValue = false)]
    public string First { get; set; }

    [DataMember(Name = LastPropertyName, EmitDefaultValue = false)]
    public string Last { get; set; }

    [DataMember(Name = NextPropertyName, EmitDefaultValue = false)]
    public string Next { get; set; }

    [DataMember(Name = PreviousPropertyName, EmitDefaultValue = false)]
    public string Previous { get; set; }

    [DataMember(Name = MorePagesPropertyName, EmitDefaultValue = false)]
    public string MorePages{ get; set; }

    public override string ToString() => ToJson();
    

    public string ToJson() => SerializeObject(this);


}