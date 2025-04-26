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
    
    public void Update(Annonce item)
    {
        var updateDef = Builders<Annonce>.Update
            .Set(x => x.Title, item.Title)
            .Set(x => x.Category, item.Category)
            .Set(x => x.Price, item.Price)
            .Set(x => x.Description, item.Description);
        annonceCollection.UpdateOne(x => x.Id == item.Id, updateDef);
    }
}