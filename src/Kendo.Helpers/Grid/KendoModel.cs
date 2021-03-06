﻿using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using static Newtonsoft.Json.JsonConvert;

namespace Kendo.Helpers.Grid
{
    [DataContract]
    public class KendoModel
    {
        [DataMember(Name = "id", EmitDefaultValue = false, Order = 1)]
        public string Id { get; set; }


        [DataMember(Name = "fields", EmitDefaultValue = false, Order = 2)]
        public IEnumerable<KendoFieldBase> Fields { get; set; }


        public override string ToString()
        {
            JObject jObject = new JObject();

            jObject.Add("id", Id);
            if (Fields?.Any() ?? false)
            {
                JObject fields = new JObject();
                IEnumerable<JProperty> fieldsProperties = Fields
                    .Select(item =>
                    {
                        JObject fieldConfiguration = new JObject();
                        switch (item.Type)
                        {
                            case FieldType.Date:
                                fieldConfiguration.Add("type", "date");
                                break;
                            case FieldType.Number:
                                fieldConfiguration.Add("type", "number");
                                break;
                            default:
                                break;
                        }

                        if (item.Editable ?? false)
                        {
                            fieldConfiguration.Add("editable", item.Editable.Value);
                        }

                        return new JProperty(item.Name, fieldConfiguration);
                    })
                    .ToArray();

                foreach (JProperty property in fieldsProperties)
                {
                    fields.Add(property);
                }

                jObject.Add("fields", fields);
            }
            
            return jObject.ToString(Newtonsoft.Json.Formatting.None);
        }

    }
}