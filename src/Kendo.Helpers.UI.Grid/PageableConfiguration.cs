using Kendo.Helpers.Core;
using static Newtonsoft.Json.JsonConvert;
using static Newtonsoft.Json.DefaultValueHandling;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json;

namespace Kendo.Helpers.UI.Grid
{
    /// <summary>
    /// An instance of this class represents a pageable configuration for the Kendo Grid object
    /// </summary>
    [JsonObject]
    public class PageableConfiguration : IKendoObject
    {
        /// <summary>
        /// Name of the json property that holds the "pageSize" configuration
        /// </summary>
        public const string PageSizePropertyName = "pageSize";

        /// <summary>
        /// Name of the json property that holds the "numeric" configuration
        /// </summary>
        public const string NumericPropertyName = "numeric";

        /// <summary>
        /// Name of the json property that holds the "buttonCount" configuration
        /// </summary>
        public const string ButtonCountPropertyName = "buttonCount";

        /// <summary>
        /// Nameo of the json property that holds the "previousNext" configuration
        /// </summary>
        public const string PreviousNextPropertyName = "previousNext";

        /// <summary>
        /// Name of the json property that holds the "input" configuration
        /// </summary>
        public const string InputPropertyName = "input";

        /// <summary>
        /// Name of the json property that holds the "pageSizes" configuration
        /// </summary>
        public const string PageSizesPropertyName = "pageSizes";

        /// <summary>
        /// Name of the json property that holds the "refresh" configuration
        /// </summary>
        public const string RefreshPropertyName = "refresh";

        /// <summary>
        /// Name of the json property that holds the "refresh" configuration
        /// </summary>
        public const string InfoPropertyName = "info";


        /// <summary>
        /// Name of the json property that holds the "messages" configuration
        /// </summary>
        public const string MessagesPropertyName = "messages";



        public static JSchema Schema => new JSchema
        {
            Type = JSchemaType.Object,
            MinimumProperties = 1,
            AllowAdditionalProperties = false,
            Properties =
            {
                [PageSizePropertyName] = new JSchema { Type = JSchemaType.Number },
                [NumericPropertyName] = new JSchema { Type = JSchemaType.Boolean, Default = true },
                [RefreshPropertyName] = new JSchema { Type = JSchemaType.Boolean, Default = false },
                [ButtonCountPropertyName] = new JSchema {Type = JSchemaType.Number, Default = 10 },
                [PreviousNextPropertyName] = new JSchema { Type = JSchemaType.Boolean, Default = true },
                [InputPropertyName] = new JSchema { Type = JSchemaType.Boolean, Default = false },
                [InfoPropertyName] = new JSchema { Type = JSchemaType.Boolean, Default = true },
                [PageSizesPropertyName] = new JSchema { Type = JSchemaType.Boolean, Default = false},
                [MessagesPropertyName] = PageableMessagesConfiguration.Schema
            }
        };

        /// <summary>
        /// Gets/Sets the number of elements of a page
        /// </summary>
        [JsonProperty(PropertyName =  PageSizePropertyName, DefaultValueHandling = IgnoreAndPopulate)]
        public int? PageSize { get; set; }

        /// <summary>
        /// Defines if an input to go to a specif page should be displayed
        /// </summary>
        [JsonProperty(PropertyName =  NumericPropertyName, DefaultValueHandling = IgnoreAndPopulate)]
        public bool? Numeric { get; set; }

        /// <summary>
        /// Gets/Sets the number of buttons to display in order to jump to a specific page
        /// </summary>
        [JsonProperty(PropertyName =   ButtonCountPropertyName, DefaultValueHandling = IgnoreAndPopulate)]
        public int? ButtonCount { get; set; }

        /// <summary>
        /// Gets/sets if buttons to navigate to previous and next pages should be displayed
        /// </summary>
        [JsonProperty(PropertyName =  PreviousNextPropertyName, DefaultValueHandling = IgnoreAndPopulate)]
        public bool? PreviousNext { get; set; }


        [JsonProperty(PropertyName =  InputPropertyName, DefaultValueHandling = IgnoreAndPopulate)]
        public bool? Input { get; set; }

        [JsonProperty(PropertyName =  PageSizesPropertyName, DefaultValueHandling = IgnoreAndPopulate)]
        public bool? PageSizes { get; set; }

        /// <summary>
        /// Gets/sets if a button to refresh data should be displayed
        /// </summary>
        [JsonProperty(PropertyName =  RefreshPropertyName, DefaultValueHandling = IgnoreAndPopulate)]
        public bool? Refresh { get; set; }



        [JsonProperty(PropertyName =  InfoPropertyName, DefaultValueHandling = IgnoreAndPopulate)]
        public bool? Info { get; set; }

        [JsonProperty(PropertyName =  MessagesPropertyName, DefaultValueHandling = IgnoreAndPopulate)]
        public PageableMessagesConfiguration Messages { get; set; }

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
}
