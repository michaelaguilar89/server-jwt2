namespace server_Jwt2.Repository_s
{
    public class clientDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set;} = null!;

        public string ClientsCollectionName { get; set; } = null!;
        public string UsersCollectionName { get; set; } = null!;
        public string InventoryCollectionName { get; set; } = null!;
        public string SalesCollectionName { get; set; } = null!;
        public string CategorysCollectionName { get; set; } = null!;
    }
}
