using App.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace App.Services;

public class UserService
{
    private readonly IMongoCollection<User> _userCollection;

    public UserService(
        IOptions<UserDatabaseSettings> userDatabaseSettings)
    {   
        string? mongoDbPassword = Environment.GetEnvironmentVariable("MONGODB_PASSWORD");

        var credential = MongoCredential.CreateCredential("admin", "root", mongoDbPassword);

        var settings = MongoClientSettings.FromConnectionString("mongodb://localhost:27017");

        settings.Credential = credential;

        var mongoClient = new MongoClient(settings);

        var mongoDatabase = mongoClient.GetDatabase(
            userDatabaseSettings.Value.DatabaseName);

        _userCollection = mongoDatabase.GetCollection<User>(
            userDatabaseSettings.Value.UserCollectionName);
    }

    public async Task<List<User>> GetAsync() =>
        await _userCollection.Find(_ => true).ToListAsync();

    public async Task RemoveAsync() =>
     await _userCollection.DeleteManyAsync(Builders<User>.Filter.Empty);


    public async Task<User?> GetAsync(int id) =>
        await _userCollection.Find(x => x.UserId == id).FirstOrDefaultAsync();

    public async Task CreateAsync(User newUser) =>
        await _userCollection.InsertOneAsync(newUser);

    public async Task UpdateAsync(int id, User updatedUser) =>
        await _userCollection.ReplaceOneAsync(x => x.UserId == id, updatedUser);

    public async Task RemoveAsync(int id) =>
        await _userCollection.DeleteOneAsync(x => x.UserId == id);
}