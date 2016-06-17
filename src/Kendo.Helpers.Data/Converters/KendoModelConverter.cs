using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Kendo.Helpers.Data.Converters
{
    /// <summary>
    /// <see cref="JsonConverter"/> implementation that support converting from/to <see cref="KendoModel"/>
    /// </summary>
    public class KendoModelConverter : JsonConverter
    {

        public override bool CanRead => true;

        /// <inheritdoc />
        public override bool CanWrite => true;

        /// <inheritdoc />
        public override bool CanConvert(Type type) => type == typeof(KendoModel);

        /// <inheritdoc />
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            KendoModel km = null;
            JToken token = JToken.ReadFrom(reader, new JsonLoadSettings { CommentHandling = CommentHandling.Ignore });

            if (token.Type == JTokenType.Object)
            {
                JObject jObj = (JObject)token;
                km.Id = jObj[KendoModel.IdPropertyName].Value<string>();

                IList<KendoFieldBase> fields = new List<KendoFieldBase>();
                IEnumerable<JToken> tokens = jObj[KendoModel.FieldsPropertyName].Children<JProperty>();
                foreach (var child in tokens)
                {
                    if (child.Type == JTokenType.Object)
                    {
                        fields.Add(child.ToObject<KendoFieldBase>());
                    }
                }

                km.Fields = fields;
            }
            return km;

        }


        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            KendoModel km = (KendoModel)value;

            writer.WriteStartObject();

            writer.WritePropertyName(KendoModel.IdPropertyName);
            writer.WriteValue(km.Id);

            writer.WritePropertyName(KendoModel.FieldsPropertyName);
            writer.WriteStartObject();

            foreach (var item in km.Fields)
            {
                writer.WritePropertyName(item.Name);

                JObject obj = new JObject();
                obj.Add(KendoFieldBase.TypePropertyName, item.Type.ToString().ToLower());
                if (item.DefaultValue != null)
                {
                    obj.Add(KendoFieldBase.DefaultValuePropertyName, JToken.FromObject(item.DefaultValue));
                }
                if (item.Editable.HasValue)
                {
                    obj.Add(KendoFieldBase.EditablePropertyName, item.Editable); 
                }
                if (item.Editable.HasValue)
                {
                    obj.Add(KendoFieldBase.FromPropertyName, item.From);
                }
                if (item.Nullable.HasValue)
                {
                    obj.Add(KendoFieldBase.NullablePropertyName, item.Nullable);
                }
                serializer.Serialize(writer, obj);
            }
            writer.WriteEndObject();

            writer.WriteEndObject();


        }
    }
}
