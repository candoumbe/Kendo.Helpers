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

        /// <summary>
        /// Name of the json property that will hold the "from" configuration
        /// </summary>
        public const string FromPropertyName = "from";

       

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
        /// </summary>
        public static JSchema Schema(FieldType fieldType)
        {

            
            JSchema schema;
            switch (fieldType)
            {
                case FieldType.Date:
                    schema = new JSchema()
                    {
                        Type = JSchemaType.Object,
                        Properties =
                        {
                            [TypePropertyName] = new JSchema {Type = JSchemaType.String, Default = "date"},
                            [EditablePropertyName] = new JSchema {Type = JSchemaType.Boolean, Default = true},
                            [DefaultValuePropertyName] = new JSchema() { Type = JSchemaType.String, Default = 0 },
                            [NullablePropertyName] = new JSchema { Type = JSchemaType.Boolean, Default = false },
                            [FromPropertyName] = new JSchema {Type = JSchemaType.String }

                        },
                        Required = { TypePropertyName },
                        AllowAdditionalProperties = false
                    };
                    break;
                case FieldType.Number:
                    schema = new JSchema
                    {
                        Type = JSchemaType.Object,
                        Properties =
                        {
                            [TypePropertyName] = new JSchema {Type = JSchemaType.String, Default = "number"},
                            [EditablePropertyName] = new JSchema {Type = JSchemaType.Boolean, Default = true},
                            [DefaultValuePropertyName] = new JSchema() { Type = JSchemaType.Number, Default = 0 },
                            [NullablePropertyName] = new JSchema { Type = JSchemaType.Boolean, Default = false },
                            [FromPropertyName] = new JSchema {Type = JSchemaType.String }
                        },
                        Required = { TypePropertyName },
                        AllowAdditionalProperties = false
                    };
                    break;
                case FieldType.Boolean:
                    schema = new JSchema()
                    {
                        Type = JSchemaType.Object,
                        Properties =
                        {
                            [TypePropertyName] = new JSchema {Type = JSchemaType.String, Default = "boolean"},
                            [EditablePropertyName] = new JSchema {Type = JSchemaType.Boolean, Default = true},
                            [DefaultValuePropertyName] = new JSchema() { Type = JSchemaType.Boolean },
                            [NullablePropertyName] = new JSchema { Type = JSchemaType.Boolean, Default = false },
                            [FromPropertyName] = new JSchema {Type = JSchemaType.String }
                        },
                        Required = {TypePropertyName},
                        AllowAdditionalProperties = false

                    };
                    break;
                default:
                    schema = new JSchema()
                    {
                        Type = JSchemaType.Object,
                        Properties =
                        {
                            [TypePropertyName] = new JSchema {Type = JSchemaType.String, Default = "string"},
                            [EditablePropertyName] = new JSchema {Type = JSchemaType.Boolean, Default = true},
                            [DefaultValuePropertyName] = new JSchema() { Type = JSchemaType.String, Default = string.Empty },
                            [NullablePropertyName] = new JSchema { Type = JSchemaType.Boolean, Default = false },
                            [FromPropertyName] = new JSchema {Type = JSchemaType.String }
                        },
                        Required = {TypePropertyName},
                        AllowAdditionalProperties = false
                    };
                    break;
            }

            return schema;
        }

        [DataMember(Name = DefaultValuePropertyName, EmitDefaultValue = false)]
        public object DefaultValue { get; set; }


        public string Name { get; }

        [DataMember(Name = TypePropertyName, EmitDefaultValue = false)]
        public FieldType Type { get; }

        /// <summary>
        /// Gets/sets if the field can be edited
        /// </summary>
        [DataMember(Name = EditablePropertyName, EmitDefaultValue = false)]
        public bool? Editable { get; set; }

        /// <summary>
        /// Gets/sets if the field can be set to null
        /// </summary>
        [DataMember(Name = NullablePropertyName, EmitDefaultValue = false)]
        public bool? Nullable { get; set; }

        /// <summary>
        /// Gets/sets the name of the backing item
        /// </summary>
        [DataMember(Name = FromPropertyName, EmitDefaultValue = false)]
        public string From { get; set; }

        
        public virtual string ToJson()
        {
            JObject properties = new JObject();
            switch (Type)
            {
                case FieldType.Date:
                    properties.Add(TypePropertyName, nameof(FieldType.Date).ToLower());
                    break;
                case FieldType.Number:
                    properties.Add(TypePropertyName, nameof(FieldType.Number).ToLower());
                    break;
                case FieldType.Boolean:
                    properties.Add(TypePropertyName, nameof(FieldType.Boolean).ToLower());
                    break;
                default:
                    properties.Add(TypePropertyName, nameof(FieldType.String).ToLower());
                    break;
            }

            if (DefaultValue != null)
            {
                properties.Add(DefaultValuePropertyName, JToken.FromObject(DefaultValue));
            }

            if (Editable.HasValue)
            {
                properties.Add(EditablePropertyName, Editable.Value);
            }

            if (Nullable.HasValue)
            {
                properties.Add(NullablePropertyName, Nullable.Value);
            }

            if (!string.IsNullOrWhiteSpace(From))
            {
                properties.Add(FromPropertyName, From);
            }
           
            
            return properties.ToString();

        }

        public override string ToString() => ToJson();
    }
}