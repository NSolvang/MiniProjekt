using Core;
using MongoDB.Driver;

namespace ServerAPI.Repositories
{
    public class UserRepositoryMongoDb : IUserRepository
    {
        private readonly IMongoCollection<User> _UserCollection;

        public UserRepositoryMongoDb()
        {
            // Local MongoDB connection
            var mongoUri = "mongodb://localhost:27017/";
            
            try
            {
                var client = new MongoClient(mongoUri);
                var dbName = "markedDatabase";  // Fixed typo in "markedDasebase"
                var collectionName = "UserCollection";

                _UserCollection = client.GetDatabase(dbName)
                    .GetCollection<User>(collectionName);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Database connection error: {e.Message}");
                throw;
            }
        }

        public void AddUser(User user)
        {
            // Generate a new ID if not already set
            if (user.UserID == 0)
            {
                user.UserID = GenerateNewId();
            }
            
            _UserCollection.InsertOne(user);
        }

        private int GenerateNewId()
        {
            // If collection is empty, start with 1
            if (_UserCollection.CountDocuments(FilterDefinition<User>.Empty) == 0)
            {
                return 1;
            }

            // Find the maximum ID and increment
            return _UserCollection
                .Find(FilterDefinition<User>.Empty)
                .SortByDescending(p => p.UserID)
                .Limit(1)
                .FirstOrDefault()?.UserID + 1 ?? 1;
        }

        public User? GetById(int id)
        {
            return _UserCollection.Find(p => p.UserID == id).FirstOrDefault();
        }
    }
}