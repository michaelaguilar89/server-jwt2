using Microsoft.Extensions.Options;
using MongoDB.Driver;
using server_Jwt2.Models;

namespace server_Jwt2.Repository_s
{
    public class categoryService
    {
        private readonly IMongoCollection<category> _categoryCollection;

        public categoryService(
            IOptions<clientDatabaseSettings> _clientDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                _clientDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                _clientDatabaseSettings.Value.DatabaseName);

            _categoryCollection = mongoDatabase.GetCollection<category>(
                _clientDatabaseSettings.Value.CategorysCollectionName);
        }

        public async Task<List<category>> GetAsync()
        {
            try
            {
                var collection=await _categoryCollection.Find(_ => true).ToListAsync();
                return collection;
            }
            catch (Exception)
            {

                return null;
            }
        }
           

        public async Task<category?> GetByIdAsync(string id)
        {
            try
            {
               var myCategory= await _categoryCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
                return myCategory;

            }
            catch (Exception)
            {

                return null;
            }
        }


        public async Task<string> CreateAsync(category newBook) 
        {
            try
            {
                await _categoryCollection.InsertOneAsync(newBook);
                return "Ok";
            }
            catch (Exception e)
            {

                return e.Message;
            }
        }
            

        public async Task<string> UpdateAsync( category updatedCategory)
        {
            try
            {

                await _categoryCollection.ReplaceOneAsync(x => x.Id == updatedCategory.Id, updatedCategory);
                return "Ok";
            }
            catch (Exception e)
            {

                return e.Message;
            }
        }

        public async Task<string> RemoveAsync(string id)
        {
            try
            {
                await _categoryCollection.DeleteOneAsync(x => x.Id == id);
                return "Ok";
            }
            catch (Exception e)
            {

                return e.Message;
            }
        }
           
    }
}

