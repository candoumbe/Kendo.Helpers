using Kendo.Helpers.Core;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System.Runtime.Serialization;
using static Newtonsoft.Json.JsonConvert;

namespace Kendo.Helpers.UI.Grid
{
    [DataContract]
    public class GridFilterableConfiguration : IKendoObject
    {
        /// <summary>
        /// Name of the json property that holds the "mode" configuration
        /// </summary>
        public const string ModePropertyName = "mode";
        /// <summary>
        /// Name of the json property that holds the "extra" configuration
        /// </summary>
        public const string ExtraPropertyName = "extra";


        /// <summary>
        /// Gets/Sets the Filterable mode
        /// </summary>
        [DataMember(Name = ModePropertyName, EmitDefaultValue = false)]
        public GridFilterableMode? Mode { get; set; }

        /// <summary>
        /// Gets/sets if the filter menu allows the user to input a second criterion
        /// </summary>
        [DataMember(Name = ExtraPropertyName, EmitDefaultValue = false)]
        public bool? Extra { get; set; }

        public static JSchema Schema => new JSchema()
        {
            Type = JSchemaType.Object,
            MinimumProperties = 1,
            Properties =
            {
                [ModePropertyName] = new JSchema { Type = JSchemaType.String, Default = "menu" },
                [ExtraPropertyName] = new JSchema { Type = JSchemaType.Boolean }
            }
        };

       

        public string ToJson()
        {
            JObject json = new JObject();

            if (Mode.HasValue)
            {
                switch (Mode.Value)
                {
                    case GridFilterableMode.Menu:
                        json.Add(ModePropertyName, "menu");
                        break;
                    case GridFilterableMode.Row:
                        json.Add(ModePropertyName, "row");
                        break;
                    case GridFilterableMode.RowAndMenu:
                        json.Add(ModePropertyName, "menu, row");
                        break;
                    default:
                        break;
                }
            }
            if (Extra.HasValue)
            {
                json.Add(ExtraPropertyName, Extra.Value);
            }


            return json.ToString();
        }
    }
}
