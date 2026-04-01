using Azure.AI.OpenAI;
using Microsoft.AspNetCore.Mvc;
using OpenAI.Chat;
using System.ClientModel;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Schema;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using VaderSharp2;

namespace Spring2026_Project3_mbvasquez.Models;

public class AIGen
{
    private static readonly string ApiKey = WebApplication.CreateBuilder().Configuration["ApiKey"] ?? throw new InvalidOperationException("ApiKey was not found.");
    private static readonly string ApiUrl = WebApplication.CreateBuilder().Configuration["ApiUrl"] ?? throw new InvalidOperationException("ApiUrl was not found.");


    //string secret = builder.Configuration.GetSelection("AI");
    private static readonly Uri ApiEndpoint = new(ApiUrl);
    private static readonly ApiKeyCredential ApiCredential = new(ApiKey);
    private const string AiDeployment = "gpt-4.1-mini";
    public record class Tweet(string Username, string Text);
    public record class Tweets(Tweet[] Items);
    public record class MovieName(string Movie);
    public record class MovieNames(MovieName[] Items);
    public record class ActorName(string Actor);
    public record class ActorNames(ActorName[] Items);
    public record class Review(string Text);
    public record class Reviews(Review[] Items);
    public static async Task<Tweets> TwitterApiSimJson(string Actor)
    {

        ChatClient client = new AzureOpenAIClient(ApiEndpoint, ApiCredential).GetChatClient(AiDeployment);

        var options = new JsonSerializerOptions(JsonSerializerDefaults.General)
        {
            UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow,
            TypeInfoResolver = new DefaultJsonTypeInfoResolver()
        };
        JsonNode schema = options.GetJsonSchemaAsNode(typeof(Tweets), new()
        {
            TreatNullObliviousAsNonNullable = true,
        });

        var chatCompletionOptions = new ChatCompletionOptions
        {
            ResponseFormat = ChatResponseFormat.CreateJsonSchemaFormat("XTwitterApiJson", BinaryData.FromString(schema.ToString()), jsonSchemaIsStrict: true),
        };
        var messages = new ChatMessage[]
        {
            new SystemChatMessage($"You represent the X/Twitter social media platform API that returns JSON data."),
            new UserChatMessage($"Generate 10 tweets from a variety of users about the actor {Actor}.")
        };
        ClientResult<ChatCompletion> result = await client.CompleteChatAsync(messages, chatCompletionOptions);

        string jsonString = result.Value.Content.FirstOrDefault()?.Text ?? @"{""Items"":[]}";
        Tweets tweets = JsonSerializer.Deserialize<Tweets>(jsonString) ?? new([]);

        return tweets;
    }
    public static async Task<MovieNames> MoviesWithActor(string Actor)
    {

        ChatClient client = new AzureOpenAIClient(ApiEndpoint, ApiCredential).GetChatClient(AiDeployment);

        var options = new JsonSerializerOptions(JsonSerializerDefaults.General)
        {
            UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow,
            TypeInfoResolver = new DefaultJsonTypeInfoResolver()
        };
        JsonNode schema = options.GetJsonSchemaAsNode(typeof(MovieNames), new()
        {
            TreatNullObliviousAsNonNullable = true,
        });

        var chatCompletionOptions = new ChatCompletionOptions
        {
            ResponseFormat = ChatResponseFormat.CreateJsonSchemaFormat("ActorApiJson", BinaryData.FromString(schema.ToString()), jsonSchemaIsStrict: true),
        };
        var messages = new ChatMessage[]
        {
            new SystemChatMessage($"You represent a movie platform API that returns JSON data."),
            new UserChatMessage($"Generate a list of movies with the actor {Actor}.")
        };
        ClientResult<ChatCompletion> result = await client.CompleteChatAsync(messages, chatCompletionOptions);

        string jsonString = result.Value.Content.FirstOrDefault()?.Text ?? @"{""Items"":[]}";
        MovieNames movies = JsonSerializer.Deserialize<MovieNames>(jsonString) ?? new([]);

        return movies;
    }

    public static async Task<ActorNames> ActorsInMovie(string Movie, int Year)
    {

        ChatClient client = new AzureOpenAIClient(ApiEndpoint, ApiCredential).GetChatClient(AiDeployment);

        var options = new JsonSerializerOptions(JsonSerializerDefaults.General)
        {
            UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow,
            TypeInfoResolver = new DefaultJsonTypeInfoResolver()
        };
        JsonNode schema = options.GetJsonSchemaAsNode(typeof(ActorNames), new()
        {
            TreatNullObliviousAsNonNullable = true,
        });

        var chatCompletionOptions = new ChatCompletionOptions
        {
            ResponseFormat = ChatResponseFormat.CreateJsonSchemaFormat("MovieApiJson", BinaryData.FromString(schema.ToString()), jsonSchemaIsStrict: true),
        };
        var messages = new ChatMessage[]
        {
            new SystemChatMessage($"You represent a movie platform API that returns JSON data."),
            new UserChatMessage($"Generate a list of actors in the movie {Movie} relased in {Year}.")
        };
        ClientResult<ChatCompletion> result = await client.CompleteChatAsync(messages, chatCompletionOptions);

        string jsonString = result.Value.Content.FirstOrDefault()?.Text ?? @"{""Items"":[]}";
        ActorNames actors = JsonSerializer.Deserialize<ActorNames>(jsonString) ?? new([]);

        return actors;
    }
    public static async Task<Reviews> MovieReviews(string Movie, int Year)
    {

        ChatClient client = new AzureOpenAIClient(ApiEndpoint, ApiCredential).GetChatClient(AiDeployment);

        var options = new JsonSerializerOptions(JsonSerializerDefaults.General)
        {
            UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow,
            TypeInfoResolver = new DefaultJsonTypeInfoResolver()
        };
        JsonNode schema = options.GetJsonSchemaAsNode(typeof(Reviews), new()
        {
            TreatNullObliviousAsNonNullable = true,
        });

        var chatCompletionOptions = new ChatCompletionOptions
        {
            ResponseFormat = ChatResponseFormat.CreateJsonSchemaFormat("ReviewApiJson", BinaryData.FromString(schema.ToString()), jsonSchemaIsStrict: true),
        };

        string[] personas = { "is harsh", "loves romance", "loves comedy", "loves thrillers", "loves fantasy" };
        var messages = new ChatMessage[]
        {
            new SystemChatMessage($"You represent a group of {personas.Length} film critics who have the following personalities: {string.Join(",", personas)}. When you receive a question, respond as each member of the group in JSON, but don't indicate which member you are.)"),
            new UserChatMessage($"How would you rate the movie {Movie} released in {Year} out of 10 in 150 words or less?")
        }; 
        ClientResult<ChatCompletion> result = await client.CompleteChatAsync(messages, chatCompletionOptions);

        string jsonString = result.Value.Content.FirstOrDefault()?.Text ?? @"{""Items"":[]}";
        Reviews reviews = JsonSerializer.Deserialize<Reviews>(jsonString) ?? new([]);

        return reviews;
    }
}
