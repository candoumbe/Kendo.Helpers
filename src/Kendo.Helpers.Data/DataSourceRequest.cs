namespace Kendo.Helpers.Data
{
    public class DataSourceRequest
    {
        public int Limit { get; set; }

        public int Offset { get; set; }

        public IKendoFilter Filter { get; set; }
    }
}
