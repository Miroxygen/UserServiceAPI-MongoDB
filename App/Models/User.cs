using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace App.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; private set; } // MongoDB ObjectId

    [BsonElement("UserId")]
    public int UserId { get; set; } // Matches "UserId" in the document Make unique?

    [BsonElement("Name")]
    public string? Name {get; set;} //Matches "Name"

    [BsonElement("Username")]
    public string? Username {get; set;} //Matches username

    [BsonElement("Followers")]
    public int Followers { get; set; } // Matches "Followers"

   [BsonElement("Following")]
    public List<int> Following { get; set; } // List of UserIds that this user is following
    
    public User()
    {
        Following = []; // Initialize the Following list
    }
}
