using Microsoft.Extensions.Options;
using MongoDB.Driver;
using server_Jwt2.Dto;
using server_Jwt2.Models;

namespace server_Jwt2.Repository_s
{
    public class SalesService
    {
        private readonly IMongoCollection<sales> _salesCollection;
        private readonly inventoryService _inventory;

        public SalesService(IOptions<clientDatabaseSettings> clientDatabaseSettings, inventoryService inventory)
        {
            var mongoClient = new MongoClient(
                clientDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                clientDatabaseSettings.Value.DatabaseName);

            _salesCollection = mongoDatabase.GetCollection<sales>(
                clientDatabaseSettings.Value.SalesCollectionName);

            _inventory = inventory;
        }



        public async Task<string> removeSale(string id)
        {
            try
            {
                var mySale = await _salesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
                if (mySale!=null)
                {
                    await _salesCollection.DeleteOneAsync(id);
                    return "Ok";
                }
                return null;
            }
            catch (Exception)
            {

               return null;
            }
        }
        public async Task<string> updateSale(sales editSale)
        {
            try
            {
                var mySale = await _salesCollection.Find(x => x.Id == editSale.Id).FirstOrDefaultAsync();
                if (mySale != null)
                {
                    await _salesCollection.ReplaceOneAsync(x=>x.Id==editSale.Id,editSale);
                    return "Ok";
                }
                return null;
            }
            catch (Exception)
            {

                return null;
            }
        }
        public async Task<Object> createOneAsync(sales newSale)
        {
            try
            {
                List<productList> list = new();
                double total=0;
                for (int i = 0; i < newSale.ProductsSold.Count(); i++)
                {
                   
                    list.Add(new productList
                    {
                        id = newSale.ProductsSold[i].SoldProductId,
                        quantity = newSale.ProductsSold[i].SoldProductQuantity
                    });
                  

                    total += newSale.ProductsSold[i].SoldProductTotal;
                }
                newSale.total = total;
                var message= await _inventory.decreaseQuantity(list);
                if (message=="Sales Ok")
                {
                    await _salesCollection.InsertOneAsync(newSale);
                    return "Ok";
                }
                if (message=="Sales Wrong")
                {
                    return "Sales Wrong";
                }
                if (message==null)//error
                {
                    return "null";
                }
                //algun producto no existe o no tiene saldo sufuciente para conprar
                return message;
            }
            catch (Exception e)
            {

                return e.Message;
            }
        }

        public async Task<sales> getSaleById(string id)
        {
            try
            {
                var mySale = await _salesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
                return mySale;
            }
            catch (Exception)
            {

                return null;
            }
        }
        public async Task<List<sales>> getAllSales()
        {
            try
            {
                var mySales = await _salesCollection.Find(_ => true).ToListAsync();
                return mySales;
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
