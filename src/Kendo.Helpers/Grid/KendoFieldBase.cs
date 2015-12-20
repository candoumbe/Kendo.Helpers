using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Runtime.Serialization;
using static Newtonsoft.Json.JsonConvert;

namespace Kendo.Helpers.Grid
{
    [DataContract]
    public abstract class KendoFieldBase
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

        public override string ToString() => SerializeObject(this);
    }
}