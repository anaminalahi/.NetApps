using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WithLLM
{
    public class MCPMessage
    {
        [JsonPropertyName("jsonrpc")]
        public string JsonRpc { get; set; } = "2.0";

        [JsonPropertyName("id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object? Id { get; set; }  // peut être string ou nombre

        [JsonPropertyName("method")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Method { get; set; }

        [JsonPropertyName("params")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, object>? Params { get; set; }

        [JsonPropertyName("result")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object? Result { get; set; }

        [JsonPropertyName("error")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public JSONRPCError? Error { get; set; }
    }

    public class JSONRPCError
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("data")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object? Data { get; set; }
    }

    // Exemples d’éléments du schéma MCP (basés sur la spécification) :contentReference[oaicite:0]{index=0}
    public class ImageContent
    {
        [JsonPropertyName("_meta")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, object>? Meta { get; set; }

        [JsonPropertyName("annotations")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Annotations? Annotations { get; set; }

        [JsonPropertyName("data")]
        public string Data { get; set; } = string.Empty;

        [JsonPropertyName("mimeType")]
        public string MimeType { get; set; } = string.Empty;

        [JsonPropertyName("type")]
        public string Type { get; set; } = "image";
    }

    public class Annotations
    {
        // selon le schéma, annotations peut contenir des clés arbitraires
        [JsonExtensionData]
        public Dictionary<string, JsonElement>? AdditionalData { get; set; }
    }

    public class Implementation
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("title")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Title { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; } = string.Empty;
    }
}
