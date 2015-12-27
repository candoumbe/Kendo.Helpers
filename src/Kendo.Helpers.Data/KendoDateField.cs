using System.Runtime.Serialization;
namespace Kendo.Helpers.Data
{
    [DataContract]
    public class KendoDateField : KendoFieldBase
    {
        public KendoDateField(string name) : base(name, FieldType.Date)
        { }

    }
}
