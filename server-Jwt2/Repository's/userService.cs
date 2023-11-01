using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using server_Jwt2.Dto;
using server_Jwt2.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace server_Jwt2.Repository_s
{
    public class userService
    {
        private readonly IMongoCollection<user> _userCollection;

        public userService(IOptions<clientDatabaseSettings> clienteDatabaseSettings)
        {
            
            {
                var mongoClient = new MongoClient(
               clienteDatabaseSettings.Value.ConnectionString);

                var mongoDatabase = mongoClient.GetDatabase(
                    clienteDatabaseSettings.Value.DatabaseName);

                _userCollection = mongoDatabase.GetCollection<user>(
                    clienteDatabaseSettings.Value.UsersCollectionName);

                
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
        private string GenerateToken(DateTime myDateNow, user myUser)
        {
            
            var expirationTime = myDateNow.AddHours(2);
            //Configuramos las claims
            var claims = new Claim[]
            {
            
            new Claim(JwtRegisteredClaimNames.Name,myUser.userName),
            new Claim("Role",myUser.role),
            new Claim("ExpirationTime",expirationTime.ToString()),           
            };

            //Añadimos las credenciales
            var signingCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes("G3VF4C6KFV43JH6GKCDFGJH45V36JHGV3H4C6F3GJC63HG45GH6V345GHHJ4623FJL3HCVMO1P23PZ07W8")),
                    SecurityAlgorithms.HmacSha256Signature
            );//luego se debe configurar para obtener estos valores, así como el issuer y audience desde el appsetings.json

            //Configuracion del jwt token
            var jwt = new JwtSecurityToken(
                issuer: "Peticionario",
                audience: "Public",
                claims: claims,
                notBefore: myDateNow,
                expires: expirationTime,
                signingCredentials: signingCredentials
            );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;

        }
        public async Task<Object> createUser(user myUser)
        {


            var userData =
                await _userCollection.Find(x => x.userName == myUser.userName)
                                                        .FirstOrDefaultAsync();
                if (userData!=null)
                {
                    // "userName is taken!"
                    return null;
                }
                await _userCollection.InsertOneAsync(myUser);

                DateTime myDateNow = DateTime.Now;
                var myToken = GenerateToken(myDateNow,myUser);
                userDto userCredentials = new userDto()
                {
                    Id = myUser.Id,
                    userName = myUser.userName,
                    role = myUser.role,
                    token = myToken,
                    expirationTime = myDateNow.AddHours(2),
                    photo = myUser.photo

                };
                return userCredentials;
           
        }//end of create user
        //login
        public async Task<Object> getUserDetails(userLoginDto myUser)
        {
           
            
                var result = await _userCollection
                    .Find(x => x.userName == myUser.username && 
                      x.password == myUser.password).FirstOrDefaultAsync();
                if (result !=null)
                {
                DateTime myDateNow = DateTime.Now;
                var myToken = GenerateToken(myDateNow,result);
                userDto userCredentials = new userDto()
                {
                    Id = result.Id,
                    userName = result.userName,
                    role = result.role,
                    token = myToken,
                    expirationTime = myDateNow.AddHours(2),
                    photo = result.photo

                };
                return userCredentials;
                }//end of if
                //user not found or invalid credentials
                return null;

            
        }//end of getUsersDetails
        public async Task<string> updateUser(user myUser)
        {
            try
            {
                var result = await _userCollection.Find(x => x.Id == myUser.Id).FirstOrDefaultAsync();
                if (result!=null)
                {
                    await _userCollection.ReplaceOneAsync(x=>x.Id == result.Id,result);
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
