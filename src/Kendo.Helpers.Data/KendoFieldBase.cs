using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Runtime.Serialization;

namespace Kendo.Helpers.Data
{
    [DataContract]
    public abstract class KendoFieldBase : IKendoObject
    {
        
        public KendoFieldBase(string name, FieldType fieldType)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(nameof(name));
            }
            Name = name;
            Type = fieldType;
        }

        [DataMember(Name = "defaultValue", EmitDefaultValue = false)]
        public string DefaultValue { get; set; }


        public string Name { get;  }

        [DataMember(Name = "type", EmitDefaultValue = false, Order = 1)]
        [JsonConverter(typeof(StringEnumConverter))]
        public FieldType Type { get;  }

        [DataMember(Name = "editable", EmitDefaultValue =false, Order = 2)]
        public bool? Editable { get; set; }

        public virtual string ToJson()
        {
            JObject json = new JObject();
            switch (Type)
            {
                case FieldType.Date:
                    json.Add("type", "date");
                    break;
                case FieldType.Number:
                    json.Add("type", "number");
                    break;
                default:
                    break;
            }

            if (DefaultValue != null)
            {
                json.Add("defaultValue", DefaultValue);
            }

            if (Editable.HasValue)
            {
                json.Add("editable", Editable.Value);
            }


            return $"{Name}:{json.ToString(Formatting.None)}";
            
        }
    }
}