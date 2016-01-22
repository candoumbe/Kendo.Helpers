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
                [IdPropertyName] = new JSchema
                {
                    Type = JSchemaType.String,
                    Description = "Name of the property that uniquely identifies an item"
                },
                [FieldsPropertyName] = new JSchema
                {
                    Type = JSchemaType.Object,
                    Description = "A set of key/value pairs the configure the model fields. The key specifies the name of the field. Quote the key if it contains spaces or other symbols which are not valid for a JavaScript identifier."
                }
            },
            MinimumProperties = 1
        };


        

        [DataMember(Name = IdPropertyName, EmitDefaultValue = false, Order = 1)]
        public string Id { get; set; }

        [DataMember(Name = FieldsPropertyName, EmitDefaultValue = false, Order = 2)]
        public IEnumerable<KendoFieldBase> Fields { get; set; }


        public string ToJson()
        {
            JObject obj = new JObject();

            obj.Add(IdPropertyName, Id);
            if (Fields?.Any() ?? false)
            {
                IEnumerable<JProperty> fieldsProperties = Fields
                    ?.Select(item => new JProperty(item.Name, JObject.Parse(item.ToJson())))
                    .ToArray() 
                    ?? Enumerable.Empty<JProperty>();

                JObject properties = new JObject();
                foreach (JProperty prop in fieldsProperties)
                {
                    properties.Add(prop);
                }

                obj.Add(FieldsPropertyName, properties);
            }
            
            return obj.ToString();
        }


        public override string ToString() => ToJson();

    }
}