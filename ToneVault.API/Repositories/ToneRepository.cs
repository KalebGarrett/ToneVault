using MongoDB.Driver;
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


    public Task<IEnumerable<Tone>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Tone> GetById(string id)
    {
        throw new NotImplementedException();
    }

    public Task<Tone> GetByOwnerId(string id)
    {
        throw new NotImplementedException();
    }

    public Task<Tone> Create(Tone data)
    {
        throw new NotImplementedException();
    }

    public Task<Tone> Update(string id, Tone data)
    {
        throw new NotImplementedException();
    }

    public Task<Tone> Delete(string id, bool hardDelete = false)
    {
        throw new NotImplementedException();
    }
}