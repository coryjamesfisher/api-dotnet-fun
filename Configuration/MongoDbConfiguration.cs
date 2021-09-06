namespace dotnet_restapi.Configuration
{
    public class MongoDbConfiguration
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public string ConnectionString {
            get {
                return $"mongodb://{Host}:{Port}";
            }
        }
    }
}