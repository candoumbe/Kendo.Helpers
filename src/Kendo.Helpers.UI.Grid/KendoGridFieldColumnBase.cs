using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using System.Collections.Generic;
using static Newtonsoft.Json.JsonConvert;
using static Newtonsoft.Json.DefaultValueHandling;

namespace Kendo.Helpers.UI.Grid
{
    [JsonObject]
    public abstract class KendoGridFieldColumnBase : KendoGridColumnBase
    {

        /// <summary>
        /// Name of the json property that holds the "field" configuration
        /// </summary>
        public const string FieldPropertyName = "field";
        /// <summary>
        /// Name of the json property that holds the "title" configuration
        /// </summary>
        public const string TitlePropertyName = "title";
        /// <summary>
        /// Name of the json property that holds the "attributes" configuration
        /// </summary>
        public const string AttributesPropertyName = "attributes";
        /// <summary>
        /// Name of the json property that holds the "encoded" property value
        /// </summary>
        public const string EncodedPropertyName = "encoded";
        /// <summary>
        /// Name oof the json property that holds the "format" configuration
        /// </summary>
        public const string FormatPropertyName = "format";

        /// <summary>
        /// Name of the json property that holds the "groupable" configuration
        /// </summary>
        public const string GroupablePropertyName = "groupable";

        
        /// <summary>
        /// Name of the json property that holds the "hidden" configuration
        /// </summary>
        public const string HiddenPropertyName = "hidden";

        /// <summary>
        /// Name of the json property that holds the "locked" configuration
        /// </summary>
        public const string LockedPropertyName = "locked";

        /// <summary>
        /// Name of the json property that holds the "lockable" configuration
        /// </summary>
        public const string LockablePropertyName = "lockable";

        /// <summary>
        /// Name of the json property that holds the "minScreenWidth" configuration
        /// </summary>
        public const string MinScreenWidthPropertyName = "minScreenWidth";

        /// <summary>
        /// Name of the json property that holds the "template" configuration
        /// </summary>
        public const string TemplatePropertyName = "template";

        /// <summary>
        /// Name of the json property that holds the "columns" configuration
        /// </summary>
        public const string ColumnsPropertyName = "columns";

        /// <summary>
        /// Name of the json property that holds the "values" configuration
        /// </summary>
        public const string ValuesPropertyName = "values";

        public static JSchema Schema => new JSchema
        {
            Type = JSchemaType.Object,
            Properties  =
            {
                [FieldPropertyName] = new JSchema { Type = JSchemaType.String },
                [TitlePropertyName] = new JSchema { Type = JSchemaType.String },
                [AttributesPropertyName] = new JSchema { Type = JSchemaType.Object },
                [EncodedPropertyName] = new JSchema { Type = JSchemaType.Boolean, Default = true },
                [FormatPropertyName] = new JSchema { Type = JSchemaType.String },
                [GroupablePropertyName] = new JSchema { Type = JSchemaType.Boolean, Default = false},
                [HiddenPropertyName] = new JSchema { Type = JSchemaType.Boolean, Default = false },
                [LockedPropertyName] = new JSchema { Type = JSchemaType.Boolean, Default = false },
                [MinScreenWidthPropertyName] = new JSchema { Type = JSchemaType.Number },
                [LockablePropertyName] = new JSchema { Type = JSchemaType.Boolean, Default = true },
                [TemplatePropertyName] = new JSchema { Type = JSchemaType.String },
                [ColumnsPropertyName] = new JSchema { Type = JSchemaType.Array },
                [ValuesPropertyName] = new JSchema { Type = JSchemaType.Array }
            },
            AllowAdditionalProperties = false,
            MinimumProperties = 1
        };

        [JsonProperty(PropertyName = ValuesPropertyName, DefaultValueHandling = IgnoreAndPopulate)]
        public IEnumerable<ColumnValues> Values { get; set; }


        [JsonProperty(PropertyName = EncodedPropertyName, DefaultValueHandling = IgnoreAndPopulate)]
        public bool? Encoded { get; set; }

        /// <summary>
        /// Gets/sets the format of the column
        /// </summary>
        [JsonProperty(PropertyName = FormatPropertyName, DefaultValueHandling = IgnoreAndPopulate)]
        public string Format { get; set; }

        /// <summary>
        /// Gets/sets the field the column will represents
        /// </summary>
        [JsonProperty(PropertyName = FieldPropertyName, DefaultValueHandling = IgnoreAndPopulate)]
        public string Field { get; set; }
        /// <summary>
        /// Gets/Sets the title of the column.
        /// </summary>
        [JsonProperty(PropertyName = TitlePropertyName, DefaultValueHandling = IgnoreAndPopulate)]
        public string Title{ get; set; }

        /// <summary>
        /// Gets/sets if the column is groupable
        /// </summary>
        [JsonProperty(PropertyName = GroupablePropertyName, DefaultValueHandling = IgnoreAndPopulate)]
        public bool? Groupable { get; set; }

        /// <summary>
        /// Gets/sets if the column is hidden
        /// </summary>
        [JsonProperty(PropertyName = HiddenPropertyName, DefaultValueHandling = IgnoreAndPopulate)]
        public bool? Hidden { get; set; }

        /// <summary>
        /// Gets/sets if the column is locked.
        /// </summary>
        [JsonProperty(PropertyName = LockedPropertyName, DefaultValueHandling = IgnoreAndPopulate)]
        public bool? Locked { get; set; }


        /// <summary>
        /// Gets/sets if the column is lockable.
        /// </summary>
        [JsonProperty(PropertyName = LockablePropertyName, DefaultValueHandling = IgnoreAndPopulate)]
        public bool? Lockable { get; set; }

        /// <summary>
        /// Gets/sets the screen width under which the column will not be displayed.
        /// </summary>
        [JsonProperty(PropertyName = MinScreenWidthPropertyName, DefaultValueHandling = IgnoreAndPopulate)]
        public int? MinScreenWidth { get; set; }


        /// <summary>
        /// Gets/sets additional CSS attributes for the current column.
        /// </summary>
        [JsonProperty(PropertyName = AttributesPropertyName, DefaultValueHandling = IgnoreAndPopulate)]
        public IDictionary<string, object> Attributes { get; set; }

        /// <summary>
        /// Gets/sets subcolumns of the current column
        /// </summary>
        [JsonProperty(PropertyName = ColumnsPropertyName, DefaultValueHandling = IgnoreAndPopulate)]
        public IEnumerable<KendoGridFieldColumn> Columns { get; set; }

        /// <summary>
        /// Gets/sets the template used to render the value of the column
        /// </summary>
        [JsonProperty(PropertyName = TemplatePropertyName, DefaultValueHandling = IgnoreAndPopulate)]
        public string Template { get; set; }

#if DEBUG
        public override string ToString() => ToJson();
#endif

        public override string ToJson()
#if DEBUG
            => SerializeObject(this, Formatting.Indented);
#else
            => SerializeObject(this);
#endif

    }
}