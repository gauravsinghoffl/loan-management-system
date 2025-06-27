using System.Data.SqlClient;

namespace Data
{
    public static class DBConnUtil
    {
        public static SqlConnection GetDBConnection(string connName)
        {
            string connStr = DBPropertyUtil.GetConnectionString(connName);
            return new SqlConnection(connStr);
        }
    }
}
