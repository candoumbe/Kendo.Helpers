using static Newtonsoft.Json.JsonConvert;
using Kendo.Helpers.Core;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json;

namespace Kendo.Helpers.Data
{
    public class KendoTransport : IKendoObject
    {
        /// <summary>
        /// Name of the json property that holds the "create" configuration
        /// </summary>
        public const string CreatePropertyName = "create";
        /// <summary>
        /// Name of the json property that holds the "read" configuration
        /// </summary>
        public const string ReadPropertyName = "read";
        /// <summary>
        /// Name of the property that holds the "update" configuration
        /// </summary>
        public const string UpdatePropertyName = "update";
        /// <summary>
        /// Name of the property that holds the "destroy" configuration
        /// </summary>
        public const string DeletePropertyName = "destroy";

        public static JSchema Schema => new JSchema
        {
            Type = JSchemaType.Object,
            Properties =
            {
                [CreatePropertyName] = KendoTransportOperation.Schema,
                [ReadPropertyName] = KendoTransportOperation.Schema,
                [UpdatePropertyName] = KendoTransportOperation.Schema,
                [DeletePropertyName] = KendoTransportOperation.Schema
            },
            MinimumProperties = 1,
            AllowAdditionalProperties = false
        };


        /// <summary>
        /// Gets/Sets the "create" configuration.
        /// <para>This configuration will be used when creating new items</para>
        /// </summary>
        [JsonProperty(PropertyName = CreatePropertyName, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public KendoTransportOperation Create { get; set; }

        [JsonProperty(PropertyName = ReadPropertyName, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public KendoTransportOperation Read { get; set; }

        [JsonProperty(PropertyName = UpdatePropertyName, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public KendoTransportOperation Update { get; set; }

        [JsonProperty(PropertyName = DeletePropertyName, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public KendoTransportOperation Destroy { get; set; }

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
