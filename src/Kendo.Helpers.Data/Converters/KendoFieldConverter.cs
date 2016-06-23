using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using Kendo.Helpers.Tools;

namespace Kendo.Helpers.Data.Converters
{
    /// <summary>
    /// <see cref="JsonConverter"/> implementation that support converting from/to <see cref="KendoFieldBase"/>
    /// </summary>
    public class KendoFieldConverter : JsonConverter
    {

        public override bool CanRead => true;

        /// <inheritdoc />
        public override bool CanWrite => true;

        /// <inheritdoc />
        public override bool CanConvert(Type type) => type == typeof(KendoFieldBase)
            || type.GetTypeInfo().IsSubclassOf(typeof(KendoFieldBase));

        /// <inheritdoc />
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            KendoFieldBase kf = null;
            JToken token = JToken.ReadFrom(reader, new JsonLoadSettings { CommentHandling = CommentHandling.Ignore });

            if (token.Type == JTokenType.Object)
            {
                JObject jObj = (JObject)token;
                string fieldName = jObj.Properties().ElementAt(0).Name;
                if (jObj.IsValid(KendoFieldBase.Schema(fieldName, FieldType.Boolean)))
                {
                    kf = new KendoBooleanField(fieldName);

                }
                else if (jObj.IsValid(KendoFieldBase.Schema(fieldName, FieldType.Date)))
                {
                    kf = new KendoBooleanField(fieldName);

                }
                else if (jObj.IsValid(KendoFieldBase.Schema(fieldName,FieldType.String)))
                {
                    kf = new KendoStringField(fieldName);
                }
                else if (jObj.IsValid((KendoFieldBase.Schema(fieldName, FieldType.Number))))
                {
                    kf = new KendoNumericField(fieldName);
                }

                kf.From = jObj[kf.Name][KendoFieldBase.FromPropertyName].Value<string>();
                kf.Nullable = jObj[kf.Name][KendoFieldBase.FromPropertyName].Value<bool?>();
                kf.Editable = jObj[kf.Name][KendoFieldBase.EditablePropertyName].Value<bool?>();
                kf.DefaultValue = jObj[kf.Name][KendoFieldBase.FromPropertyName].Value<string>();
            }

            return kf.As(objectType);

        }


        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            KendoFieldBase field = (KendoFieldBase)value;
            IEnumerable<PropertyInfo> properties = typeof(KendoFieldBase).GetProperties();


            JSchema schema = KendoFieldBase.Schema(field.Name, field.Type);

            writer.WriteStartObject();
            writer.WritePropertyName(field.Name, true);

            writer.WriteStartObject();

            writer.WritePropertyName(KendoFieldBase.TypePropertyName);
            writer.WriteValue(field.Type.ToString().ToLower());

            if (field.DefaultValue != null)
            {
                writer.WritePropertyName(KendoFieldBase.DefaultValuePropertyName);
                writer.WriteValue(field.DefaultValue?.ToString() ?? string.Empty);
            }

            if (field.Editable.HasValue)
            {
                writer.WritePropertyName(KendoFieldBase.EditablePropertyName);
                writer.WriteValue(field.Editable.Value);
            }

            if (field.Nullable.HasValue)
            {
                writer.WritePropertyName(KendoFieldBase.NullablePropertyName);
                writer.WriteValue(field.Nullable.Value);
            }

            if (field.From != null)
            {
                writer.WritePropertyName(KendoFieldBase.FromPropertyName);
                writer.WriteValue(field.From);
            }

            writer.WriteEndObject();

            writer.WriteEndObject();
        }
    }
}
