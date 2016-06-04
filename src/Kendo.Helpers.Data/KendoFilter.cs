using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System.Collections.Generic;
using System.Runtime.Serialization;


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
                            [FieldJsonPropertyName] = new JSchema { Type = JSchemaType.String,  },
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
        [DataMember(Name = FieldJsonPropertyName, IsRequired = true)]
        public string Field { get; set; }

        /// <summary>
        /// Operator to apply to the filter
        /// </summary>
        [DataMember(Name = OperatorJsonPropertyName, IsRequired = true)]
        public KendoFilterOperator Operator { get; set; }

        /// <summary>
        /// Value of the filter
        /// </summary>
        [DataMember(Name = ValueJsonPropertyName, IsRequired = false)]
        public object Value { get; set; }


        public override string ToString() => ToJson();

        /// <summary>
        /// Defines matches between <see cref="KendoFilterOperator"/> and strings
        /// </summary>
        public static IDictionary<KendoFilterOperator, string> AllowedOperators => new Dictionary<KendoFilterOperator, string>
        {
            [KendoFilterOperator.Contains] = "contains",
            [KendoFilterOperator.EndsWith] = "endswith",
            [KendoFilterOperator.EqualTo] = "eq",
            [KendoFilterOperator.GreaterThan] = "gt",
            [KendoFilterOperator.GreaterThanOrEqual] = "gte",
            [KendoFilterOperator.IsEmpty] = "isempty",
            [KendoFilterOperator.IsNotEmpty] = "isnotempty",
            [KendoFilterOperator.IsNotNull] = "isnotnull",
            [KendoFilterOperator.IsNull] = "isnull",
            [KendoFilterOperator.LessThan] = "lt",
            //[KendoFilterOperator.LessThanOrEqualTo] = "ltz",
            [KendoFilterOperator.NotEqualTo] = "neq",
            [KendoFilterOperator.StartsWith] = "startswith"

        };

        public string ToJson()
        {
            JObject jObj = new JObject
            {
                [FieldJsonPropertyName] = Field,
                [OperatorJsonPropertyName] = AllowedOperators[Operator],

            };

            if (Value != null)
            {
                jObj.Add(ValueJsonPropertyName, JToken.FromObject(Value));
            }
            return jObj.ToString();
        }
    }
}
