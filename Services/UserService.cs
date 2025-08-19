using MongoDB.Driver;
using Microsoft.Extensions.Options;
using AT2CityLinkAPI.Models;


namespace AT2CityLinkAPI.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(IOptions<MongoDbSettings> mongoSettings, IMongoClient client)
        {
            var database = client.GetDatabase("userinfo");
            _users = database.GetCollection<User>("users");
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _users.Find(u => u.Username == username).FirstOrDefaultAsync();
        }

        public async Task CreateUserAsync(User user)
        {
            await _users.InsertOneAsync(user);
        }

    }
}
