using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Kendo.Helpers.Core;
using Newtonsoft.Json.Schema;
using static Newtonsoft.Json.JsonConvert;
using Kendo.Helpers.Data.Converters;

namespace Kendo.Helpers.Data
{
    [JsonObject]
    [JsonConverter(typeof(KendoModelConverter))]
    public class KendoModel : IKendoObject
    {
        /// <summary>
        /// Name of the json property that holds the "id" property 
        /// </summary>
        public const string IdPropertyName = "id";
        
        /// <summary>
        /// Name of the json property that holds the "fields" property 
        /// </summary>
        public const string FieldsPropertyName = "fields";

        public static JSchema Schema => new JSchema
        {
            Type = JSchemaType.Object,
            Properties =
            {
                [IdPropertyName] = new JSchema
                {
                    Type = JSchemaType.String,
                    Description = "Name of the property that uniquely identifies an item"
                },
                [FieldsPropertyName] = new JSchema
                {
                    Type = JSchemaType.Object,
                    Description = "A set of key/value pairs the configure the model fields. The key specifies the name of the field. Quote the key if it contains spaces or other symbols which are not valid for a JavaScript identifier.",
                }
            },
            MinimumProperties = 1
        };


        

        [JsonProperty(
            PropertyName = IdPropertyName, 
            DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, 
            Order = 1)]
        public string Id { get; set; }

        [JsonProperty(
            PropertyName = FieldsPropertyName,
            DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public IEnumerable<KendoFieldBase> Fields { get; set; } = Enumerable.Empty<KendoFieldBase>();


        public string ToJson()
#if DEBUG
            => SerializeObject(this, Formatting.Indented);
#else
            => SerializeObject(this);
#endif

        public override string ToString() => ToJson();

    }
}