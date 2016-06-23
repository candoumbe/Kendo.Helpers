using Newtonsoft.Json;
using System;
using Kendo.Helpers.Core;
using Newtonsoft.Json.Schema;
using static Newtonsoft.Json.JsonConvert;
using Kendo.Helpers.Data.Converters;

namespace Kendo.Helpers.Data
{
    /// <summary>
    /// Base class for the kendo fields.
    /// </summary>
    [JsonObject]
    [JsonConverter(typeof(KendoFieldConverter))]
    public abstract class KendoFieldBase : IKendoObject
    {
        /// <summary>
        /// Name of the property in the KendoField json representation object that will holds the default value of the field 
        /// </summary>
        public const string DefaultValuePropertyName = "defaultValue";

        /// <summary>
        /// Name of the property in the KendoField json representation object that will holds the type of the field 
        /// </summary>
        public const string TypePropertyName = "type";
        /// <summary>
        /// Name of the property in the KendoField json representation object that will holds the editable state of the field 
        /// </summary>
        public const string EditablePropertyName = "editable";

        /// <summary>
        /// Name of the property in the KendoField json representation object that will holds the nullable value of the field 
        /// </summary>
        public const string NullablePropertyName = "nullable";

        /// <summary>
        /// Name of the json property that will hold the "from" configuration
        /// </summary>
        public const string FromPropertyName = "from";


        /// <summary>
        /// Builds a new <see cref="KendoFieldBase"/> instance
        /// </summary>
        /// <param name="fieldName">name of the field</param>
        /// <param name="fieldType">Type of the field</param>
        protected KendoFieldBase(string fieldName, FieldType fieldType)
        {
            if (string.IsNullOrWhiteSpace(fieldName))
            {
                throw new ArgumentOutOfRangeException(nameof(fieldName), $"{nameof(fieldName)} cannot be null or empty or whitespace");
            }

            Name = fieldName;
            Type = fieldType;
        }


        /// <summary>
        /// Gets the schema that validates the current field
        /// <param name="name">Name of the field to validate</param>
        /// <param name="fieldType">Type of the field to validate</param>
        /// <see cref="FieldType"/>
        /// </summary>
        public static JSchema Schema(string name, FieldType fieldType)
        {
            JSchema schema;
            switch (fieldType)
            {
                case FieldType.Date:
                    schema = new JSchema
                    {
                        Type = JSchemaType.Object,
                        Properties =
                        {
                            [name] = new JSchema {
                                Type = JSchemaType.Object,
                                Properties = {
                                    [TypePropertyName] = new JSchema {Type = JSchemaType.String, Default = "date"},
                                    [EditablePropertyName] = new JSchema {Type = JSchemaType.Boolean, Default = true},
                                    [DefaultValuePropertyName] = new JSchema() { Type = JSchemaType.String, Default = "new Date()" },
                                    [NullablePropertyName] = new JSchema { Type = JSchemaType.Boolean, Default = false },
                                    [FromPropertyName] = new JSchema {Type = JSchemaType.String }
                                },
                                Required = { TypePropertyName },
                                AllowAdditionalProperties = false
                            }
                        },
                        Required = { name },
                        AllowAdditionalProperties = false
                    };
                    break;
                case FieldType.Number:
                    schema = new JSchema
                    {
                        Type = JSchemaType.Object,
                        Properties =
                        {
                            [name] = new JSchema {
                                Type = JSchemaType.Object,
                                Properties = {
                                    [TypePropertyName] = new JSchema {Type = JSchemaType.String, Default = "number"},
                                    [EditablePropertyName] = new JSchema {Type = JSchemaType.Boolean, Default = true},
                                    [DefaultValuePropertyName] = new JSchema() { Type = JSchemaType.Number, Default = 0 },
                                    [NullablePropertyName] = new JSchema { Type = JSchemaType.Boolean, Default = false },
                                    [FromPropertyName] = new JSchema {Type = JSchemaType.String }
                                },
                                Required = { TypePropertyName },
                                AllowAdditionalProperties = false
                            }
                        },
                        Required = { name },
                        AllowAdditionalProperties = false
                    };
                    break;
                case FieldType.Boolean:
                    schema = new JSchema
                    {
                        Type = JSchemaType.Object,
                        Properties =
                        {
                            [name] = new JSchema {
                                Type = JSchemaType.Object,
                                Properties =
                                {
                                    [TypePropertyName] = new JSchema {Type = JSchemaType.String, Default = "boolean"},
                                    [EditablePropertyName] = new JSchema {Type = JSchemaType.Boolean, Default = true},
                                    [DefaultValuePropertyName] = new JSchema() { Type = JSchemaType.Boolean },
                                    [NullablePropertyName] = new JSchema { Type = JSchemaType.Boolean, Default = false },
                                    [FromPropertyName] = new JSchema {Type = JSchemaType.String }
                                },
                                Required = { TypePropertyName },
                                AllowAdditionalProperties = false
                            }
                        },
                        Required = { name },
                        AllowAdditionalProperties = false
                    };
                    break;
                default:
                    schema = new JSchema
                    {
                        Type = JSchemaType.Object,
                        Properties =
                        {
                            [name] = new JSchema {
                                Type = JSchemaType.Object,
                                Properties = {
                                    [TypePropertyName] = new JSchema {Type = JSchemaType.String, Default = "string"},
                                    [EditablePropertyName] = new JSchema {Type = JSchemaType.Boolean, Default = true},
                                    [DefaultValuePropertyName] = new JSchema() { Type = JSchemaType.String, Default = string.Empty },
                                    [NullablePropertyName] = new JSchema { Type = JSchemaType.Boolean, Default = false },
                                    [FromPropertyName] = new JSchema {Type = JSchemaType.String }
                                },
                                Required = { TypePropertyName },
                                AllowAdditionalProperties = false
                            }
                        },
                        Required = { name },
                        AllowAdditionalProperties = false
                    };
                    break;
            }

            return schema;
        }

        /// <summary>
        /// Default value of the field
        /// </summary>
        [JsonProperty(PropertyName = DefaultValuePropertyName)]
        public object DefaultValue { get; set; }

        /// <summary>
        /// Name of the field
        /// </summary>
        [JsonIgnore]
        public string Name { get; }

        /// <summary>
        /// Gets/sets the type of the field
        /// </summary>
        [JsonProperty(PropertyName = TypePropertyName)]
        public FieldType Type { get; }

        /// <summary>
        /// Gets/sets if the field can be edited
        /// </summary>
        [JsonProperty(PropertyName = EditablePropertyName)]
        public bool? Editable { get; set; }

        /// <summary>
        /// Gets/sets if the field can be set to null
        /// </summary>
        [JsonProperty(PropertyName = NullablePropertyName)]
        public bool? Nullable { get; set; }

        /// <summary>
        /// Gets/sets the name of the backing item
        /// </summary>
        [JsonProperty(PropertyName = FromPropertyName)]
        public string From { get; set; }

        public virtual string ToJson()
#if DEBUG
            => SerializeObject(this, Formatting.Indented);
#else
            => SerializeObject(this);
#endif


#if DEBUG
        public override string ToString() => ToJson();
#endif

    }
}