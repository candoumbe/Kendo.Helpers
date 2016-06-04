using Newtonsoft.Json.Schema;
using System.Runtime.Serialization;
using static Newtonsoft.Json.JsonConvert;
using Kendo.Helpers.Core;

namespace Kendo.Helpers.UI.Grid
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    [DataContract]
    public class ColumnValues : IKendoObject
    {
        /// <summary>
        /// Name of the json property that holds the "text" property
        /// </summary>
        public const string TextPropertyName = "text";

        /// <summary>
        /// Name of the json property that holds the "value" property
        /// </summary>
        public const string ValuePropertyName = "value";



        public ColumnValues(object value, string text)
        {
            Text = text;
            Value = value;
        }


        public static JSchema Schema => new JSchema
        {
            Properties =
            {
                [TextPropertyName] = new JSchema { Type = JSchemaType.String },
                [ValuePropertyName] = new JSchema { }
            },
            AllowAdditionalProperties = false,
            Required = { TextPropertyName, ValuePropertyName }
            
        };


        [DataMember(Name = ValuePropertyName)]
        public object Value { get; set; }

        [DataMember(Name = TextPropertyName)]
        public string Text { get; set; }

        public string ToJson() => SerializeObject(this);


        public override string ToString() => ToJson();
    }
}
