namespace Kendo.Helpers.Grid
{
    public class KendoTransportOperation
    {
        public string Url { get; set; }

        public string Type { get; set; }

        public object Data { get; set; }

        public bool? Cache { get; set; }


        public string ContentType { get; set; } = "application/x-www-form-urlencoded";
    }
}