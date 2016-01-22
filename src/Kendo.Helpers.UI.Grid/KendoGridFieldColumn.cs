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

        /// <summary>
        /// Name of the json property that holds the "field" configuration
        /// </summary>
        public const string FieldPropertyName = "field";
        /// <summary>
        /// Name of the json property that holds the "title" configuration
        /// </summary>
        public const string TitlePropertyName = "title";
        /// <summary>
        /// Name of the json property that holds the "attributes" configuration
        /// </summary>
        public const string AttributesPropertyName = "attributes";
        /// <summary>
        /// Name of the json property that holds the "encoded" property value
        /// </summary>
        public const string EncodedPropertyName = "encoded";
        /// <summary>
        /// Name oof the json property that holds the "format" configuration
        /// </summary>
        public const string FormatPropertyName = "format";

        /// <summary>
        /// Name of the json property that holds the "groupable" configuration
        /// </summary>
        public const string GroupablePropertyName = "groupable";

        
        /// <summary>
        /// Name of the json property that holds the "hidden" configuration
        /// </summary>
        public const string HiddenPropertyName = "hidden";

        /// <summary>
        /// Name of the json property that holds the "locked" configuration
        /// </summary>
        public const string LockedPropertyName = "locked";

        /// <summary>
        /// Name of the json property that holds the "lockable" configuration
        /// </summary>
        public const string LockablePropertyName = "lockable";

        /// <summary>
        /// Name of the json property that holds the "minScreenWidth" configuration
        /// </summary>
        public const string MinScreenWidthPropertyName = "minScreenWidth";



        public static JSchema Schema => new JSchema
        {
            Type = JSchemaType.Object,
            Properties  =
            {
                [FieldPropertyName] = new JSchema { Type = JSchemaType.String },
                [TitlePropertyName] = new JSchema { Type = JSchemaType.String },
                [AttributesPropertyName] = new JSchema { Type = JSchemaType.Object },
                [EncodedPropertyName] = new JSchema { Type = JSchemaType.Boolean, Default = true },
                [FormatPropertyName] = new JSchema { Type = JSchemaType.String },
                [GroupablePropertyName] = new JSchema { Type = JSchemaType.Boolean, Default = false},
                [HiddenPropertyName] = new JSchema { Type = JSchemaType.Boolean, Default = false },
                [LockedPropertyName] = new JSchema { Type = JSchemaType.Boolean, Default = false },
                [MinScreenWidthPropertyName] = new JSchema { Type = JSchemaType.Number },
                [LockablePropertyName] = new JSchema { Type = JSchemaType.Boolean, Default = true }
            },
            Required = {FieldPropertyName},
            AllowAdditionalProperties = false
        };

        [DataMember(Name = EncodedPropertyName, EmitDefaultValue = false)]
        public bool? Encoded { get; set; }

        /// <summary>
        /// Gets/sets the format of the column
        /// </summary>
        [DataMember(Name = FormatPropertyName, EmitDefaultValue = false)]
        public string Format { get; set; }

        /// <summary>
        /// Gets/sets the field the column will represents
        /// </summary>
        [DataMember(Name = FieldPropertyName, EmitDefaultValue = false)]
        public string Field { get; set; }
        /// <summary>
        /// Gets/Sets the title of the column.
        /// </summary>
        [DataMember(Name = TitlePropertyName, EmitDefaultValue = false)]
        public string Title{ get; set; }

        /// <summary>
        /// Gets/sets if the column is groupable
        /// </summary>
        [DataMember(Name = GroupablePropertyName, EmitDefaultValue = false)]
        public bool? Groupable { get; set; }

        /// <summary>
        /// Gets/sets if the column is hidden
        /// </summary>
        [DataMember(Name = HiddenPropertyName, EmitDefaultValue = false)]
        public bool? Hidden { get; set; }

        /// <summary>
        /// Gets/sets if the column is locked
        /// </summary>
        [DataMember(Name = LockedPropertyName, EmitDefaultValue = false)]
        public bool? Locked { get; set; }


        /// <summary>
        /// Gets/sets if the column is lockable
        /// </summary>
        [DataMember(Name = LockablePropertyName, EmitDefaultValue = false)]
        public bool? Lockable { get; set; }

        /// <summary>
        /// Gets/sets if the column is lockable
        /// </summary>
        [DataMember(Name = MinScreenWidthPropertyName, EmitDefaultValue = false)]
        public int? MinScreenWidth { get; set; }


        /// <summary>
        /// Gets/sets additional CSS attributes for the current column
        /// </summary>
        [DataMember(Name = AttributesPropertyName, EmitDefaultValue = false)]
        public IDictionary<string, object> Attributes { get; set; }

        public IEnumerable<KendoGridFieldColumn> Columns { get; set; }

        public override string ToJson()
            => SerializeObject(this);
    }
}