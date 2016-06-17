using Newtonsoft.Json.Schema;
using static Newtonsoft.Json.JsonConvert;
using Kendo.Helpers.Core;
using Newtonsoft.Json;

namespace Kendo.Helpers.UI.Grid
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    [JsonObject]
    public class ColumnValues : IKendoObject
    {
        /// <summary>
        /// Name of the json property that holds the "text" property
        /// </summary>
        public const string TextPropertyName = "text";

        /// <summary>
        /// Name of the json property that holds the "value" property
        /// </summary>
        public const string ValuePropertyName = "value";



        public ColumnValues(object value, string text)
        {
            Text = text;
            Value = value;
        }

        /// <summary>
        /// The schema that describes the json representation of this element
        /// </summary>
        public static JSchema Schema => new JSchema
        {
            Properties =
            {
                [TextPropertyName] = new JSchema { Type = JSchemaType.String },
                [ValuePropertyName] = new JSchema { }
            },
            AllowAdditionalProperties = false,
            Required = { TextPropertyName, ValuePropertyName }
            
        };


        [JsonProperty(PropertyName = ValuePropertyName)]
        public object Value { get; set; }

        [JsonProperty(PropertyName = TextPropertyName)]
        public string Text { get; set; }

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
