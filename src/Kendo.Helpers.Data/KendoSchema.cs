using System.Runtime.Serialization;
using System.Text;

namespace Kendo.Helpers.Data
{
    [DataContract]
    public class KendoSchema : IKendoObject
    {
        [DataMember(Name = nameof(Model),  EmitDefaultValue = false)]
        public KendoModel Model { get; set; }

        public string ToJson()
        {
            StringBuilder json = new StringBuilder();

            if (Model != null)
            {
                json = json.Append($@"""model"":{Model.ToJson()}");
            }

            return json.Insert(0, "{").Append("}").ToString();
        }
    }
}