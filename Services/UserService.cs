using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Threading.Tasks;

public class UserService
{
    private readonly IMongoCollection<User> _users;

    public UserService(IOptions<MongoDbSettings> mongoSettings, IMongoClient client)
    {
        var database = client.GetDatabase(mongoSettings.Value.DatabaseName ?? "userinfo");
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

    public async Task<User?> ValidateCredentialsAsync(string username, string password)
    {
        var user = await GetByUsernameAsync(username);
        if (user == null) return null;

        var ok = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        return ok ? user : null;
    }

    public async Task EnsureAdminUserAsync(string username, string email, string password, string role = "Admin")
    {
        var existingAdmin = await _users.Find(u => u.Role == "Admin").FirstOrDefaultAsync();
        if (existingAdmin != null) return;

        var existingByUsername = await GetByUsernameAsync(username);
        if (existingByUsername != null && existingByUsername.Role != "Admin")
        {
            var update = Builders<User>.Update.Set(u => u.Role, "Admin");
            await _users.UpdateOneAsync(u => u.Id == existingByUsername.Id, update);
            return;
        }

        var hash = BCrypt.Net.BCrypt.HashPassword(password);
        var admin = new User
        {
            Username = username,
            Email = email,
            PasswordHash = hash,
            Role = role
        };
        await _users.InsertOneAsync(admin);
    }
}
