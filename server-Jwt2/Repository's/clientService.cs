using Microsoft.Extensions.Options;
using MongoDB.Driver;
using server_Jwt2.Models;

namespace server_Jwt2.Repository_s
{
    public class clientService
    {
        private readonly IMongoCollection<client> _clientsCollection;

        public clientService(
            IOptions<clientDatabaseSettings> clientDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                clientDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                clientDatabaseSettings.Value.DatabaseName);

            _clientsCollection = mongoDatabase.GetCollection<client>(
                clientDatabaseSettings.Value.ClientsCollectionName);
        }

        public async Task<List<client>> GetAsync()
        {
            try
            {
               var collection= await _clientsCollection.Find(_ => true).ToListAsync();
                return collection;
            }
            catch (Exception)
            {

                return null;
            }
        }
           

        public async Task<client?> GetByIdAsync(string id)
        {
            try
            {

               var client= await _clientsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
                return client;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<String> CreateAsync(client newClient)
        {
            try
            {
                await _clientsCollection.InsertOneAsync(newClient);
                return "true";
            }
            catch (Exception e)
            {
                return e.ToString();

                
            }
        }
            

        public async Task<String> UpdateAsync( client updatedClient)
        {
            try
            {
                var myClient = await _clientsCollection.Find(x => x.Id == updatedClient.Id).FirstOrDefaultAsync();
                if (myClient!=null)
                {
                    await _clientsCollection.ReplaceOneAsync(x => x.Id == updatedClient.Id, updatedClient);
                    return "true";
                }
                return "false";
            }
            catch (Exception e)
            {

                return e.Message;
            }
        }
            

        public async Task<String> RemoveAsync(string id)
        {
            try
            {
                await _clientsCollection.DeleteOneAsync(x => x.Id == id);
                return "true";
            }
            catch (Exception e)
            {

                return e.Message;
            }
        }
            

    }
}
