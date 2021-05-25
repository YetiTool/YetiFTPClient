namespace YetiFTPClient
{
    /*
     * Object for each smartbench found
     */
    public class SmartBench
    {
        private string ip;
        private string name;
        private string fullName;
        public SmartBench(string ip, string name, string fullName)
        {
            this.ip = ip;
            this.name = name;
            this.fullName = fullName;
        }

        public string GetName()
        {
            return this.name;
        }

        public string GetIP()
        {
            return this.ip;
        }

        public string GetFullName()
        {
            return this.fullName;
        }
    }
}
