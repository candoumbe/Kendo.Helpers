using System.Runtime.Serialization;
using System.Text;
using Kendo.Helpers.Core;
using Newtonsoft.Json.Schema;

using System.Diagnostics;

using Newtonsoft.Json.Linq;

namespace Kendo.Helpers.Data
{
    /// <summary>
    /// An instance of this class allows to customize the schema configuration of a <see cref="KendoRemoteDataSource"/>
    /// </summary>
    [DataContract]
    public class KendoSchema : IKendoObject
    {

        public const string DataPropertyName = "data";
        public const string TotalPropertyName = "total";
        public const string TypePropertyName = "type";
        public const string ModelPropertyName = "model";


        public static JSchema Schema => new JSchema
        {
            Type = JSchemaType.Object,
            Properties =
            {
                [DataPropertyName] = new JSchema { Type = JSchemaType.String, Default = "json"},
                [TotalPropertyName] = new JSchema { Type = JSchemaType.String, Default = "total"},
                [TypePropertyName] = new JSchema { Type = JSchemaType.String, Default = "json"},
                [ModelPropertyName] = KendoModel.Schema

            }
        };

        /// <summary>
        /// Gets/Sets the data configuration (see http://docs.telerik.com/kendo-ui/api/javascript/data/datasource#configuration-schema.data).
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public string Data { get; set; }

        /// <summary>
        /// <para>
        /// Gets/Sets the field of the server which holds the total number of data items in the response.
        /// </para>
        /// </summary>
        [DataMember(Name = "total", EmitDefaultValue = false)]
        public string Total { get; set; }

        /// <summary>
        /// <para>
        /// Gets/Sets the type of the server response. Can be either "json" or "xml"
        /// </para>
        /// </summary>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public SchemaType? Type { get; set; }

        [DataMember(Name = "model",  EmitDefaultValue = false)]
        public KendoModel Model { get; set; }

        public string ToJson()
        {
            StringBuilder json = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(Data))
            {
                json = json.Append($@"""data"":""{Data}""");
            }
            if (!string.IsNullOrWhiteSpace(Total))
            {
                json = json
                    .Append($"{(json.Length > 0 ? "," : string.Empty)}")
                    .Append($@"""total"":""{Total}""");
            }
            if (Type.HasValue)
            {
                json = json
                    .Append($"{(json.Length > 0 ? "," : string.Empty)}")
                    .Append($@"""type"":""{(Type == SchemaType.Json ? "json" : "xml")}""");
            }
            if (Model != null)
            {
                json = json
                    .Append($"{(json.Length > 0 ? "," : string.Empty)}")
                    .Append($@"""model"":{Model.ToJson()}");
            }
            json = json.Insert(0, "{").Append("}");

            Debug.Assert(JObject.Parse(json.ToString()).IsValid(Schema), $"json obtained from {nameof(ToJson)} should conform to the schema : {Schema}");
            return json.ToString();
        }
    }
}