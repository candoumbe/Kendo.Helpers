using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Runtime.Serialization;
using Kendo.Helpers.Core;
using Newtonsoft.Json.Schema;
using System.Collections.Generic;
using System.Diagnostics;

namespace Kendo.Helpers.Data
{
    [DataContract]
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

        public KendoFieldBase(string name, FieldType fieldType)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentOutOfRangeException(nameof(name), $"{nameof(name)} cannot be null or whitespace");
            }

            Name = name;
            Type = fieldType;
        }


        /// <summary>
        /// Gets the schema that validates the current field
        /// </summary>
        public static JSchema Schema(string fieldName, FieldType fieldType)
        {

            if (string.IsNullOrWhiteSpace(fieldName))
            {
                throw new ArgumentOutOfRangeException(nameof(fieldName), $"{nameof(fieldName)} cannot be null or empty");
            }


            JSchema schema = null;
            switch (fieldType)
            {
                case FieldType.Date:
                    schema = new JSchema()
                    {
                        Type = JSchemaType.Object,
                        Properties =
                        {
                            [fieldName] = new JSchema
                            {
                                Type = JSchemaType.Object,
                                Properties =
                                {
                                    [TypePropertyName] = new JSchema {Type = JSchemaType.String, Default = "date"},
                                    [EditablePropertyName] = new JSchema {Type = JSchemaType.Boolean, Default = true},
                                    [DefaultValuePropertyName] = new JSchema() { Type = JSchemaType.None },
                                    [NullablePropertyName] = new JSchema { Type = JSchemaType.Boolean, Default = false }
                                }
                            }
                        },
                        Required = { TypePropertyName }
                    };
                    break;
                case FieldType.Number:
                    schema = new JSchema()
                    {
                        Type = JSchemaType.Object,
                        Properties =
                        {
                            [fieldName] = new JSchema
                            {
                                Type = JSchemaType.Object,
                                Properties =
                                {
                                    [TypePropertyName] = new JSchema {Type = JSchemaType.String, Default = "number"},
                                    [EditablePropertyName] = new JSchema {Type = JSchemaType.Boolean, Default = true},
                                    [DefaultValuePropertyName] = new JSchema() { Type = JSchemaType.Number, Default = 0 },
                                    [NullablePropertyName] = new JSchema { Type = JSchemaType.Boolean, Default = false }
                                }
                            }
                        },
                        Required = { TypePropertyName }
                    };
                    break;
                case FieldType.Boolean:
                    schema = new JSchema()
                    {
                        Type = JSchemaType.Object,
                        Properties =
                        {
                            [fieldName] = new JSchema
                            {
                                Type = JSchemaType.Object,
                                Properties =
                                {
                                    [TypePropertyName] = new JSchema {Type = JSchemaType.String, Default = "boolean"},
                                    [EditablePropertyName] = new JSchema {Type = JSchemaType.Boolean, Default = true},
                                    [DefaultValuePropertyName] = new JSchema() { Type = JSchemaType.Boolean },
                                    [NullablePropertyName] = new JSchema { Type = JSchemaType.Boolean, Default = false }
                                }
                            }
                        },
                        Required = { TypePropertyName }
                    };
                    break;
                default:
                    schema = new JSchema()
                    {
                        Type = JSchemaType.Object,
                        Properties =
                        {
                            [fieldName] = new JSchema
                            {
                                Type = JSchemaType.Object,
                                Properties =
                                {
                                    [TypePropertyName] = new JSchema {Type = JSchemaType.String, Default = "string"},
                                    [EditablePropertyName] = new JSchema {Type = JSchemaType.Boolean, Default = true},
                                    [DefaultValuePropertyName] = new JSchema() { Type = JSchemaType.String, Default = string.Empty },
                                    [NullablePropertyName] = new JSchema { Type = JSchemaType.Boolean, Default = false }
                                }
                            },
                        },
                        Required = { TypePropertyName }
                    };
                    break;
            }

            return schema;


        }

        [DataMember(Name = DefaultValuePropertyName, EmitDefaultValue = false)]
        public object DefaultValue { get; set; }


        public string Name { get; }

        [DataMember(Name = TypePropertyName, EmitDefaultValue = false)]
        [JsonConverter(typeof(StringEnumConverter))]
        public FieldType Type { get; }

        [DataMember(Name = EditablePropertyName, EmitDefaultValue = false)]
        public bool? Editable { get; set; }

        [DataMember(Name = NullablePropertyName, EmitDefaultValue = false)]
        public bool? Nullable { get; set; }

        public virtual string ToJson()
        {
            JObject json = new JObject();
            switch (Type)
            {
                case FieldType.Date:
                    json.Add(TypePropertyName, nameof(FieldType.Date).ToLower());
                    break;
                case FieldType.Number:
                    json.Add(TypePropertyName, nameof(FieldType.Number).ToLower());
                    break;
                case FieldType.Boolean:
                    json.Add(TypePropertyName, nameof(FieldType.Boolean).ToLower());
                    break;
                default:
                    json.Add(TypePropertyName, nameof(FieldType.String).ToLower());
                    break;
            }

            if (DefaultValue != null)
            {
                json.Add(DefaultValuePropertyName, JToken.FromObject(DefaultValue));
            }

            if (Editable.HasValue)
            {
                json.Add(EditablePropertyName, Editable.Value);
            }

            if (Nullable.HasValue)
            {
                json.Add(NullablePropertyName, Nullable.Value);
            }
            
            Debug.Assert(JObject.Parse(json.ToString()).IsValid(Schema(Name, Type)), 
                $@"The result of ""{nameof(ToJson)}"" should be valid according the schema obtained calling {nameof(KendoFieldBase)}.{nameof(Schema)}(""{Name}"", {nameof(FieldType)}.{Type})");
           
            return json.ToString();

        }


    }
}