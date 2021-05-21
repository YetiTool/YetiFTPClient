namespace YetiFTPClient
{
    /*
     * Object for each smartbench found
     */
    public class SmartBench
    {
        private string ip;
        private string name;

        public SmartBench(string ip, string name)
        {
            this.ip = ip;
            this.name = name;
        }

        public string GetName()
        {
            return this.name;
        }

        public string GetIP()
        {
            return this.ip;
        }
    }
}
