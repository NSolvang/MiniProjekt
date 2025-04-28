using Core;
using MongoDB.Driver;

namespace ServerAPI.Repositories
{
    public class UserRepositoryMongoDb : IUserRepository
    {
        private readonly IMongoCollection<User> _userCollection;

        public UserRepositoryMongoDb()
        {
            // Local MongoDB connection - samme som ProductRepository
            var mongoUri = "$mongodb+srv://niko6041:1234@cluster.codevrj.mongodb.net/?retryWrites=true&w=majority&appName=Cluster";
            
            try
            {
                var client = new MongoClient(mongoUri);
                var dbName = "markedDatabase"; // Samme database som Products
                var collectionName = "user"; // Andet collection navn

                _userCollection = client.GetDatabase(dbName)
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
            // Tjek om brugernavnet allerede findes
            if (_userCollection.Find(u => u.Username == user.Username).Any())
                throw new Exception("Username already exists");

            // Generer nyt ID hvis nødvendigt
            if (user.ID == 0)
                user.ID = GenerateNewId();

            _userCollection.InsertOne(user);
        }

        public User? GetUser(string username, string password)
        {
            return _userCollection.Find(u => u.Username == username && u.Password == password)
                .FirstOrDefault();
        }

        public User? GetUserById(int id)
        {
            return _userCollection.Find(u => u.ID == id)
                .FirstOrDefault();
        }

        private int GenerateNewId()
        {
            // Hvis collection er tom, start med 1
            if (_userCollection.CountDocuments(FilterDefinition<User>.Empty) == 0)
                return 1;

            // Find det højeste ID og increment
            return _userCollection
                .Find(FilterDefinition<User>.Empty)
                .SortByDescending(u => u.ID)
                .Limit(1)
                .FirstOrDefault()?.ID + 1 ?? 1;
        }
    }
}                                                      