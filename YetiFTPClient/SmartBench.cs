namespace YetiFTPClient
{
    /*
     * Object for each smartbench found - use this ftpconnection object to upload files
     */
    public class SmartBench
    {
        private string ip;
        private string name;
        private FTPConnection connection;

        public SmartBench(string ip, string name, FTPConnection connection)
        {
            this.ip = ip;
            this.name = name;
            this.connection = connection;
        }

        public string GetName()
        {
            return this.name;
        }

        public string GetIP()
        {
            return this.ip;
        }

        public FTPConnection GetConnection()
        {
            return this.connection;
        }
    }
}
