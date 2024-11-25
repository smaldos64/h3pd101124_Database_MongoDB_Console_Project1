using System.Text.Json;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDB_Console_Project1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // MongoDB connection string (update as needed)
            string connectionString = "mongodb://localhost:27017";
            //string connectionString = "mongodb://223.187.56.30:27017";
            try
            {
                // Create a MongoDB client
                var client = new MongoClient(connectionString);

                // Get a reference to the database (create it if it doesn't exist)
                var database = client.GetDatabase("TestDatabase_h3pd101124");

                // Get a reference to a collection (create it if it doesn't exist)
                var collection = database.GetCollection<BsonDocument>("TestCollection");

                // Insert a document/row into the collection
                var document = new BsonDocument
                {
                    { "Name", "Stribe Pedersen" },
                    { "Age", 12 },
                    { "City", "Gudumholm" }
                };
                await collection.InsertOneAsync(document);
                Console.WriteLine("Document inserted successfully!");

                var document1 = new BsonDocument
                {
                    { "Name", "Stribe Pedersen" },
                    { "Age", 12 },
                    { "City", "Gudumholm" },
                    { "Owners", "The Thise Family" }
                };
                await collection.InsertOneAsync(document1);
                Console.WriteLine("Document inserted successfully!");

                // Retrieve the inserted document
                var filter = Builders<BsonDocument>.Filter.Eq("Name", "Stribe Pedersen");
                var result = await collection.Find(filter).FirstOrDefaultAsync();

                if (result != null)
                {
                    Console.WriteLine("Retrieved document:");
                    Console.WriteLine(result.ToJson());
                }
                else
                {
                    Console.WriteLine("No document found!");
                }

                var filter1 = Builders<BsonDocument>.Filter.Empty;
                var result1 = await collection.Find(filter1).ToListAsync();
                if (result1 != null)
                {
                    Console.WriteLine("Retrived documents:");
                    foreach (var thisDocument in result1)
                    {
                        Console.WriteLine(thisDocument.ToJson());
                    }
                }
                else
                {
                    Console.WriteLine("No document found!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
