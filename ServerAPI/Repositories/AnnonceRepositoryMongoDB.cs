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
    
    private int MaxId() {
      
        var annoncer = GetAll();
        if (annoncer.Length == 0)
            return 0;
        return annoncer.Select(b => b.AnnonceID).Max();

    }
    
    public void Add(Annonce annonce)
    {
        var max = 0;
        if (annonceCollection.Count(Builders<Annonce>.Filter.Empty) > 0)
        {
            max = MaxId();
        }
        annonce.AnnonceID = max + 1;
    
        annonceCollection.InsertOne(annonce);
    }

    public void DeleteById(int id)
    {
        var deleteResult = annonceCollection
            .DeleteOne(Builders<Annonce>.Filter.Where(r => r.AnnonceID == id));
    }
    
    public void Update(Annonce item)
    {
        var updateDef = Builders<Annonce>.Update
            .Set(x => x.Title, item.Title)
            .Set(x => x.Category, item.Category)
            .Set(x => x.Price, item.Price)
            .Set(x => x.Description, item.Description);
        annonceCollection.UpdateOne(x => x.AnnonceID == item.AnnonceID, updateDef);
    }
}