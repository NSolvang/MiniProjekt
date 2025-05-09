using Core;
using MongoDB.Driver;


namespace ServerAPI.Repositories;

public class AnnonceRepositoryMongoDB : IAnnonceRepository
{
    
    private IMongoClient client;
    
    private IMongoCollection<Annonce> annonceCollection;

    public AnnonceRepositoryMongoDB() {
        // atlas database
        
        var mongoUri = $"mongodb+srv://niko6041:1234@cluster.codevrj.mongodb.net/?retryWrites=true&w=majority&appName=Cluster";
       
            
        try
        {
            client = new MongoClient(mongoUri);
        }
        catch (Exception e)
        {
            Console.WriteLine("There was a problem connecting to your " +
                              "Atlas cluster. Check that the URI includes a valid " +
                              "username and password, and that your IP address is " +
                              $"in the Access List. Message: {e.Message}");
            throw; }
        // Provide the name of the database and collection you want to use.
        var dbName = "markedDatabase";
        var collectionName = "annonce";

        annonceCollection = client.GetDatabase(dbName)
            .GetCollection<Annonce>(collectionName);
    }
    
    public Annonce[] GetAll()
    {
        var noFilter = Builders<Annonce>.Filter.Empty;
        return annonceCollection.Find(noFilter).ToList().ToArray();
    }
    
    public void Add(Annonce annonce)
    {
        annonceCollection.InsertOne(annonce);
    }


    public void DeleteById(string id)
    {
        // For MongoDB skal du konvertere string ID til ObjectId
        var objectId = MongoDB.Bson.ObjectId.Parse(id);
        var filter = Builders<Annonce>.Filter.Eq("_id", objectId);
        annonceCollection.DeleteOne(filter);
    }
    
    public async Task Update(Annonce annonce)
    {
        var filter = Builders<Annonce>.Filter.Eq(a => a.Id, annonce.Id);
        await annonceCollection.ReplaceOneAsync(filter, annonce);
    }
    
    public async Task<Annonce[]> GetByBuyerId(int buyerId)
    {
        var result = await annonceCollection.Find(x => x.BuyerId == buyerId).ToListAsync();
        return result.ToArray();
    }
    
    //filter
    public async Task<Annonce[]> GetFiltered(string? category = null, int? minPrice = null, int? maxPrice = null, string? location = null)
    {
        var builder = Builders<Annonce>.Filter;
        var filter = builder.Empty;

        // Tilføj filtrering baseret på de parametre, der er angivet
        if (!string.IsNullOrEmpty(category))
        {
            filter = filter & builder.Eq(a => a.Category, category);
        }

        if (minPrice.HasValue)
        {
            filter = filter & builder.Gte(a => a.Price, minPrice.Value);
        }

        if (maxPrice.HasValue)
        {
            filter = filter & builder.Lte(a => a.Price, maxPrice.Value);
        }

        if (!string.IsNullOrEmpty(location))
        {
            filter = filter & builder.Eq(a => a.Locations, location);
        }

        var result = await annonceCollection.Find(filter).ToListAsync();
        return result.ToArray();
    }
}