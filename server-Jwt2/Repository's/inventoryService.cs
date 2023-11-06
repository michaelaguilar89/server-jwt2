using Microsoft.Extensions.Options;
using MongoDB.Driver;
using server_Jwt2.Models;

namespace server_Jwt2.Repository_s
{
   
    public class inventoryService
    {
        private readonly categoryService _category;
        private readonly IMongoCollection<inventory> _inventoryCollection;

        public inventoryService(
            IOptions<clientDatabaseSettings> clientDatabaseSettings,
            categoryService category)
        {
            var mongoClient = new MongoClient(
                clientDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                clientDatabaseSettings.Value.DatabaseName);

            _inventoryCollection = mongoDatabase.GetCollection<inventory>(
                clientDatabaseSettings.Value.InventoryCollectionName);

            _category = category;
        }

       

        public async Task<List<inventory>> GetAsync()
        {
            try
            {
                var inventorys = await _inventoryCollection.Find(_ => true).ToListAsync();
                return inventorys;
            }
            catch (Exception)
            {

                return null;
            }
        }
           

        public async Task<inventory?> GetByIdAsync(string id)
        {
            try
            {
               var inventory=  await _inventoryCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
                if (inventory==null)
                {
                    return null;
                }
                return inventory;
            }
            catch (Exception)
            {

                return null;
            }
        }
           

        public async Task<string> CreateAsync(inventory newProduct)
        {
            try
            { 
                category categoryExist= await _category.GetByIdAsync(newProduct.CategoryId);
                if (categoryExist?.Id==newProduct.CategoryId)
                {

                    await _inventoryCollection.InsertOneAsync(newProduct);
                    return "Ok";
                }
                return "CategoryId not found";
            }
            catch (Exception e)
            {

                return e.Message;
            }
        }   
           

        public async Task<string> UpdateAsync(inventory updatedInventory)
        {
            try
            {
                await _inventoryCollection.ReplaceOneAsync(x => x.Id == updatedInventory.Id, updatedInventory);
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
                var status = await _inventoryCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
                if (status == null)
                {
                    return null;
                }
                await _inventoryCollection.DeleteOneAsync(x => x.Id == id);
                return "Ok";
            }
            catch (Exception)
            {

                return null;
            }
        }


           
    }
}

