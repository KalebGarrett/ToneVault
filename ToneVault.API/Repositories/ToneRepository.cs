using MongoDB.Driver;
using MongoDB.Driver.Linq;
using ToneVault.API.Repositories.Interfaces;
using ToneVault.Models;

namespace ToneVault.API.Repositories;

public class ToneRepository : IMongoRepository<Tone>
{
    private readonly IMongoClient _mongoClient;

    public ToneRepository(IMongoClient mongoClient)
    {
        _mongoClient = mongoClient;
    }

    public async Task<IEnumerable<Tone>> GetAll()
    {
        var tones = await GetCollection().AsQueryable()
            .Where(u => !u.Deleted).ToListAsync();
        
        return tones;
    }

    public async Task<Tone> GetById(string id)
    {
        var tone = await GetCollection().AsQueryable()
            .FirstOrDefaultAsync(u => u.Id == id && !u.Deleted);
        
        return tone;
    }

    public async Task<Tone> GetByOwnerId(string id)
    {
        var tone = await GetCollection().AsQueryable()
            .FirstOrDefaultAsync(u => u.OwnerId == id && !u.Deleted);

        return tone;
    }

    public async Task<Tone> Create(Tone data)
    {
        data.Id = Guid.NewGuid().ToString();
        data.CreatedAt = DateTime.UtcNow;
        data.UpdatedAt = DateTime.UtcNow;
        await GetCollection().InsertOneAsync(data);
        var toneList = await GetCollection().AsQueryable().ToListAsync();
        return toneList.FirstOrDefault(x => x.Id == data.Id);
    }

    public async Task<Tone> Update(string id, Tone data)
    {
        data.UpdatedAt = DateTime.UtcNow;
        data.Version++;
        await GetCollection().ReplaceOneAsync(x => x.Id == id, data);
        return data;
    }

    public async Task Delete(string id, bool hardDelete = false)
    {
        if (hardDelete)
        {
            await GetCollection().DeleteOneAsync(x => x.Id == id);
        }
        else
        {
            await GetCollection().UpdateOneAsync<Tone>(x => x.Id == id,
                Builders<Tone>.Update.Set(x => x.Deleted, true));
        }
    }

    private IMongoCollection<Tone> GetCollection()
    {
        var database = _mongoClient.GetDatabase("ToneVault");
        var collection = database.GetCollection<Tone>("Tones");
        return collection;
    }
}