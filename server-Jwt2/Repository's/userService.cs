using Microsoft.Extensions.Options;
using MongoDB.Driver;
using server_Jwt2.Models;
using System.Security.Cryptography.X509Certificates;

namespace server_Jwt2.Repository_s
{
    public class userService
    {
        private readonly IMongoCollection<user> _userCollection;

        public userService(IOptions<userDataBaseSettings> userDataBaseSettings)
        {
            
            {
                var mongoClient = new MongoClient(
                    userDataBaseSettings.Value.ConnectionString);

                var mongoDatabase = mongoClient.GetDatabase(
                    userDataBaseSettings.Value.DatabaseName);

                _userCollection = mongoDatabase.GetCollection<user>(
                    userDataBaseSettings.Value.UsersCollectionName);

                
            }
        }

       public async Task<List<user?>> GetUser()
        {
                var users = await _userCollection.Find(_ => true).ToListAsync();
                if (users!=null)
                {
                    return users;
                }
                return null;
                        
        }
        public async Task<string> createUser(user myUser)
        {
            try
            {
                 

                var userData= await _userCollection.Find(x=>x.userName==myUser.userName).ToListAsync();
                if (userData!=null)
                {
                    return "userName is taken!";
                }
                await _userCollection.InsertOneAsync(myUser);
                return "true";
            }
            catch (Exception e)
            {

                return e.Message;
            }
        }//end of create user
        public async Task<Object> getUserDetails(user myUser)
        {
            Object resp = new object();
            try
            {
                var result = await _userCollection
                    .Find(x => x.userName == myUser.userName && 
                      x.password == myUser.password).FirstOrDefaultAsync();
                if (result !=null)
                {
                   return result;
                }
                return false;

            }
            catch (Exception e)
            {

                return e.Message;
            }
        }//end of getUsersDetails
        public async Task<string> updateUser(user myUser)
        {
            try
            {
                var result = await _userCollection.Find(x => x.Id == myUser.Id).FirstOrDefaultAsync();
                if (result!=null)
                {
                    await _userCollection.ReplaceOneAsync(x=>x.Id == myUser.Id,myUser);
                    return "user has been update";
                }
                return "User invalid or not found";
            }
            catch (Exception e)
            {

                return e.Message;
            }
        }//end of update
        public async Task<string> removeUser(string id)
        {
            try
            {
                var result = await _userCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
                if (result != null)
                {
                    await _userCollection.DeleteOneAsync(x=>x.Id==id);
                        return "user has been removed";
                }
                return "User invalid or not found";
            }
            catch (Exception e)
            {

                return e.Message;
            }
        }//end of remove


    }
}
