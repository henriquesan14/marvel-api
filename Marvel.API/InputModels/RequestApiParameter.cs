using Refit;

namespace Marvel.API.InputModels
{
    public class RequestApiParameter
    {
        [AliasAs("ts")]
        public long Ts { get; set; }
        [AliasAs("apikey")]
        public string ApiKey { get; set; }
        [AliasAs("hash")]
        public string Hash { get; set; }
        [AliasAs("limit")]
        public int Limit { get; set; } = 20;
        [AliasAs("offset")]
        public int Offset { get; set; }
        [AliasAs("name")]
        public string? Name { get; set; }
    }
}
