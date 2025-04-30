using Core;
using MongoDB.Driver;

namespace ServerAPI.Repositories
{
    public class UserRepositoryMongoDb : IUserRepository
    {
        private readonly IMongoCollection<User> _userCollection;

        public UserRepositoryMongoDb()
        {
            var client = new MongoClient("mongodb+srv://niko6041:1234@cluster.codevrj.mongodb.net/?retryWrites=true&w=majority&appName=Cluster");
            _userCollection = client.GetDatabase("markedDatabase")
                .GetCollection<User>("user");
        }

        public void AddUser(User user)
        {
            // Check if username already exists
            if (_userCollection.Find(u => u.Username == user.Username).Any())
                throw new Exception("Username already exists");

            // Generate new ID if needed
            if (user.Id == 0)
                user.Id = GenerateNewId();


            _userCollection.InsertOne(user);
        }

        public User? GetUser(string username, string password)
        {
            return _userCollection.Find(u => u.Username == username && u.Password == password)
                .FirstOrDefault();
        }

        public User? GetUserById(int id)
        {
            return _userCollection.Find(u => u.Id == id)
                .FirstOrDefault();
        }

        private int GenerateNewId()
        {
            if (_userCollection.CountDocuments(FilterDefinition<User>.Empty) == 0)
                return 1;

            return _userCollection
                .Find(FilterDefinition<User>.Empty)
                .SortByDescending(u => u.Id)
                .Limit(1)
                .FirstOrDefault()?.Id + 1 ?? 1;
        }
    }
}