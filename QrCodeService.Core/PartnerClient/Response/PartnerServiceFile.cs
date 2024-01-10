
using Newtonsoft.Json;

namespace QrCodeService.Core.PartnerClient.Response
{
    public class PartnerServiceFile
    {
        [JsonProperty("id")]
        public string? Id { get; set; }
        /// <summary>
        /// Orginal file name
        /// </summary>
        [JsonProperty("name")]
        public string? Name { get; set; }
        /// <summary>
        /// Size of file
        /// </summary>
        [JsonProperty("size")]
        public int? Size { get; set; }
        /// <summary>
        /// Content-Type from file.
        /// </summary>
        [JsonProperty("mime")]
        public string? Mime { get; set; }
        /// <summary>
        /// Тype of storage (standard or cold).
        /// </summary>
        [JsonProperty("storage")]
        public string? Storage { get; set; }
        /// <summary>
        /// Date of file creation
        /// </summary>
        [JsonProperty("created")]
        public string? Created { get; set; }
        /// <summary>
        /// Date of file update
        /// </summary>
        [JsonProperty("updated")]
        public string? Updated { get; set; }
        /// <summary>
        ///Number of seconds the file is stored, by default - six months.
        /// </summary>
        [JsonProperty("ttl")]
        public long? Ttl { get; set; }

    }
}