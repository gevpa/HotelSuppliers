using System.Data.SqlClient;


namespace Suppliers.Services.DBHelper
{
    public static class DBUtil
    {
        //private DBUtil() { }

        public static SqlConnection? GetConnection()
        {
            var ConfigurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            var configuration = ConfigurationBuilder.Build();
            string connString = configuration.GetConnectionString("defaultConnection");

            try
            {
                SqlConnection conn = new(connString);
                return conn;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                return null;
            }
        }
    }
}
