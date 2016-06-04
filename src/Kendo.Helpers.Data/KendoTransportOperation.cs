﻿using System.Runtime.Serialization;
using static Newtonsoft.Json.JsonConvert;
using Kendo.Helpers.Core;
using Newtonsoft.Json.Schema;

namespace Kendo.Helpers.Data
{
    [DataContract]
    public class KendoTransportOperation : IKendoObject
    {
        /// <summary>
        /// Name of the property that holds the "url" endpoint
        /// </summary>
        public const string UrlPropertyName = "url";
        /// <summary>
        /// Name of the property that holds the type of request
        /// </summary>
        public const string TypePropertyName = "type";
        /// <summary>
        /// Name of the property that holds additional data sent alongside the request
        /// </summary>
        public const string DataPropertyName = "data";
        /// <summary>
        /// Name of the property that holds the cache strategy for requests made
        /// </summary>
        public const string CachePropertyName = "cache";

        /// <summary>
        /// Name of the property that defines the content type of the request
        /// </summary>
        public const string ContentTypePropertyName = "contentType";


        /// <summary>
        /// Gets the schema that can be used to validates the json obtained using the <see cref="ToJson"/> method
        /// </summary>
        public static JSchema Schema => new JSchema
        {
            Type = JSchemaType.Object,
            Properties =
            {
                [UrlPropertyName] = new JSchema { Type = JSchemaType.String },
                [TypePropertyName] = new JSchema { Type = JSchemaType.String },
                [DataPropertyName] = new JSchema { Type = JSchemaType.None },
                [CachePropertyName] = new JSchema { Type = JSchemaType.Boolean },
                [ContentTypePropertyName] = new JSchema { Type = JSchemaType.String, Default = "application/x-www-form-urlencoded" }
            },
            Required = {UrlPropertyName}
        };

        /// <summary>
        /// Gets/Sets the url of the endpoint the datasource will get its date from
        /// </summary>
        [DataMember(Name = UrlPropertyName, EmitDefaultValue = false, IsRequired = true)]
        public string Url { get; set; }

        /// <summary>
        /// Gets/Sets the type of the request
        /// </summary>
        [DataMember(Name = TypePropertyName, EmitDefaultValue = false)]
        public string Type { get; set; }

        /// <summary>
        /// Gets/Sets additional data that should be sent to the endpoint specified by <see cref="Url"/>
        /// </summary>
        [DataMember(Name = DataPropertyName, EmitDefaultValue = false)]
        public object Data { get; set; }

        [DataMember(Name = CachePropertyName, EmitDefaultValue = false)]
        public bool? Cache { get; set; }

        /// <summary>
        /// Gets/Sets the content-type of the endpoint response.
        /// </summary>
        [DataMember(Name = ContentTypePropertyName, EmitDefaultValue = false)]
        public string ContentType { get; set; }
        

        public override string ToString() => ToJson();

        /// <summary>
        /// Computes the Json representation of the current instance
        /// </summary>
        /// <returns>json representation of the current instance</returns>
        public virtual string ToJson() => SerializeObject(this);
    }
}