using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

using static Newtonsoft.Json.JsonConvert;
using System.Runtime.Serialization;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Kendo.Helpers.Data.Converters;

namespace Kendo.Helpers.Data
{
    /// <summary>
    /// An instance of this class holds a kendo filter
    /// </summary>
    public class KendoFilter : IKendoFilter
    {
        /// <summary>
        /// Name of the json property that holds the field name
        /// </summary>
        public const string FieldJsonPropertyName = "field";

        /// <summary>
        /// Name of the json property that holds the operator
        /// </summary>
        public const string OperatorJsonPropertyName = "operator";

        /// <summary>
        /// Name of the json property that holds the value
        /// </summary>
        public const string ValueJsonPropertyName = "value";

        public static JSchema Schema(KendoFilter filter)
        {
            JSchema schema;
            switch (filter?.Operator ?? KendoFilterOperator.EqualTo)
            {
                case KendoFilterOperator.Contains:
                case KendoFilterOperator.IsEmpty:
                case KendoFilterOperator.IsNotEmpty:
                case KendoFilterOperator.StartsWith:
                case KendoFilterOperator.EndsWith:
                    schema = new JSchema
                    {
                        Type = JSchemaType.Object,
                        Properties =
                        {
                            [FieldJsonPropertyName] = new JSchema { Type = JSchemaType.String },
                            [OperatorJsonPropertyName] = new JSchema { Type = JSchemaType.String },
                            [ValueJsonPropertyName] = new JSchema { Type = JSchemaType.String }
                        },
                            Required = { FieldJsonPropertyName, OperatorJsonPropertyName }
                    };
                    break;

                default:
                    schema = new JSchema
                    {
                        Type = JSchemaType.Object,
                        Properties =
                        {
                            [FieldJsonPropertyName] = new JSchema { Type = JSchemaType.String,  },
                            [OperatorJsonPropertyName] = new JSchema { Type = JSchemaType.String },
                            [ValueJsonPropertyName] = new JSchema { Type = JSchemaType.String | JSchemaType.Number | JSchemaType.Integer | JSchemaType.Boolean }
                        },
                        Required = { FieldJsonPropertyName, OperatorJsonPropertyName }
                    };
                    break;

            }

            return schema;

        }

        /// <summary>
        /// Name of the field to filter
        /// </summary>
        [JsonProperty(FieldJsonPropertyName, Required = Required.Always)]
        public string Field { get; set; }

        /// <summary>
        /// Operator to apply to the filter
        /// </summary>
        [JsonProperty(OperatorJsonPropertyName, Required = Required.Always)]
        [JsonConverter(typeof(KendoFilterOperatorConverter))]
        public KendoFilterOperator Operator { get; set; }

        /// <summary>
        /// Value of the filter
        /// </summary>
        [JsonProperty(ValueJsonPropertyName, Required = Required.AllowNull, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public object Value { get; set; }

        
        public string ToJson() => SerializeObject(this);
        
        
    }
}
