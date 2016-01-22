using System.Runtime.Serialization;
using static Newtonsoft.Json.JsonConvert;
using Kendo.Helpers.Core;
using Newtonsoft.Json.Schema;

namespace Kendo.Helpers.Data
{
    [DataContract]
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
            Title = "transport",
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
        [DataMember(Name = CreatePropertyName, EmitDefaultValue = false)]
        public KendoTransportOperation Create { get; set; }

        [DataMember(Name = ReadPropertyName, EmitDefaultValue = false)]
        public KendoTransportOperation Read { get; set; }

        [DataMember(Name = UpdatePropertyName, EmitDefaultValue = false)]
        public KendoTransportOperation Update { get; set; }

        [DataMember(Name = DeletePropertyName, EmitDefaultValue = false)]
        public KendoTransportOperation Destroy { get; set; }


        public override string ToString() => ToJson();

        public virtual string ToJson() => SerializeObject(this);
    }
}
