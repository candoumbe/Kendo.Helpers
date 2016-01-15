﻿using Newtonsoft.Json.Schema;
using System.Collections.Generic;
using System.Runtime.Serialization;
using static Newtonsoft.Json.JsonConvert;

namespace Kendo.Helpers.UI.Grid
{
    [DataContract]
    public class KendoGridCommandColumn : KendoGridColumnBase
    {
        
        /// <summary>
        /// Name of the json property where the name of the command is stored
        /// </summary>
        public const string NamePropertyName = "name";

        /// <summary>
        /// Name of the json property where the text of the commmand is stored
        /// </summary>
        public const string TextPropertyName = "text";


        public static JSchema Schema => new JSchema
        {
            Title = "command",
            Type = JSchemaType.Object,

            Properties =
            {
                [NamePropertyName] = new JSchema { Type = JSchemaType.String },
                [TextPropertyName] = new JSchema { Type = JSchemaType.String }
            },
            MinimumProperties = 1

            
        };

        /// <summary>
        /// Gets/Sets the name of the command
        /// </summary>
        [DataMember(Name = NamePropertyName, EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// Gets/Sets the text associated to the command
        /// </summary>
        [DataMember(Name = TextPropertyName, EmitDefaultValue = false)]
        public string Text { get; set; }

        public override string ToJson() => SerializeObject(this);

        public override string ToString() => ToJson();
    }
}
