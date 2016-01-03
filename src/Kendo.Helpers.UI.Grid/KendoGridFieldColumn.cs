using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using static Newtonsoft.Json.JsonConvert;

namespace Kendo.Helpers.UI.Grid
{
    [DataContract]
    public class KendoGridFieldColumn : KendoGridColumnBase
    {

        public const string FieldPropertyName = "field";
        public const string TitlePropertyName = "title";
        public const string AttributesPropertyName = "attributes";

        public override JSchema Schema => new JSchema
        {
            Type = JSchemaType.Object,
            Properties  =
            {
                [FieldPropertyName] = new JSchema { Type = JSchemaType.String },
                [TitlePropertyName] = new JSchema { Type = JSchemaType.String },
                [AttributesPropertyName] = new JSchema { Type = JSchemaType.Object }
            },
            Required = {FieldPropertyName}
        };

        /// <summary>
        /// Gets/sets the field the column will represents
        /// </summary>
        [DataMember(Name = FieldPropertyName, EmitDefaultValue = false)]
        public string Field { get; set; }
        /// <summary>
        /// Gets/Sets the title of the column
        /// </summary>
        [DataMember(Name = TitlePropertyName, EmitDefaultValue = false)]
        public string Title{ get; set; }

        /// <summary>
        /// Additional CSS attributes for the columns
        /// </summary>
        [DataMember(Name = AttributesPropertyName, EmitDefaultValue = false)]
        public IDictionary<string, object> Attributes { get; set; }

        

        public IEnumerable<KendoGridFieldColumn> Columns { get; set; }

        public override string ToJson()
            => SerializeObject(this);
    }
}