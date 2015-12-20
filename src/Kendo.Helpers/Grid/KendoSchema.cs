using static Newtonsoft.Json.JsonConvert;
using System.Runtime.Serialization;
using System.Text;

namespace Kendo.Helpers.Grid
{
    [DataContract]
    public class KendoSchema
    {
        [DataMember(Name = "model",  EmitDefaultValue = false)]
        public KendoModel Model { get; set; }

        public override string ToString()
        {
            StringBuilder json = new StringBuilder();

            if (Model != null)
            {
                json = json.Append($@"""model"":{Model}");
            }

            return json.Insert(0, "{").Append("}").ToString();
        }
    }
}