using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Kendo.Helpers.Core;
using Newtonsoft.Json.Schema;

namespace Kendo.Helpers.Data
{
    [DataContract]
    public class KendoModel : IKendoObject
    {
        /// <summary>
        /// Name of the json property where the id is store
        /// </summary>
        public const string IdPropertyName = "id";

        public const string FieldsPropertyName = "fields";

        public static JSchema Schema => new JSchema
        {
            Type = JSchemaType.Object,
            Properties =
            {
                [IdPropertyName] = new JSchema { Type = JSchemaType.String, Description = "Name of the property that uniquely identifies an item" },
                [FieldsPropertyName] = new JSchema {Type = JSchemaType.Object | JSchemaType.Array, Description = "A set of key/value pairs the configure the model fields. The key specifies the name of the field. Quote the key if it contains spaces or other symbols which are not valid for a JavaScript identifier."}
            }
        };


        [DataMember(Name = IdPropertyName, EmitDefaultValue = false, Order = 1)]
        public string Id { get; set; }

        [DataMember(Name = FieldsPropertyName, EmitDefaultValue = false, Order = 2)]
        public IEnumerable<KendoFieldBase> Fields { get; set; }


        public string ToJson()
        {
            JObject jObject = new JObject();

            jObject.Add(IdPropertyName, Id);
            if (Fields?.Any() ?? false)
            {
                JArray fields = new JArray();
                IEnumerable<JObject> fieldsProperties = Fields
                    .Select(item => JObject.Parse(item.ToJson()))
                    .ToArray();

                foreach (var property in fieldsProperties)
                {
                    fields.Add(property);
                }

                jObject.Add(FieldsPropertyName, JArray.FromObject(fields));
            }
            
            return jObject.ToString();
        }


        public override string ToString() => ToJson();

    }
}