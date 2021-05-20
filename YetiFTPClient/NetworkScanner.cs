using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace YetiFTPClient
{
    public class NetworkScanner
    {

        private string defaultGateway;
        public NetworkScanner(string defaultGateway)
        {
            this.defaultGateway = defaultGateway;
        }

        //Scan all IPs in parallel for a successful return
        //Remove last macip for release version
        public List<String> GetConnections()
        {
            var openIps = new List<String>();

            Parallel.ForEach(GetIPRange(defaultGateway, 17, 20), ip =>
            {
                try
                {
                    Ping ping = new Ping();
                    PingReply reply = ping.Send(ip, 30);
                    if (reply.Status == IPStatus.Success)
                        if(GetMacByIp(ip).StartsWith("b8-27-eb") || GetMacByIp(ip).StartsWith("dc-a6-32") || GetMacByIp(ip).StartsWith("e4-5f-01") || GetMacByIp(ip).StartsWith("f0-18"))
                        {
                            openIps.Add(ip);
                            System.Diagnostics.Debug.WriteLine(ip);
                        }
                } catch
                {
                    System.Diagnostics.Debug.WriteLine("Couldn't ping/get MAC ip");
                }
            });

            return openIps;
        }

        public List<SmartBench> GetSmartbenches()
        {
            var smartbenches = new List<SmartBench>();

            Parallel.ForEach(GetConnections(), ip =>
            {
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    socket.Connect(ip, 65432);
                    byte[] messageReceived = new byte[1024];

                    int byteRecv = socket.Receive(messageReceived);
                    string message = Encoding.UTF8.GetString(messageReceived, 0, byteRecv);

                    System.Diagnostics.Debug.WriteLine("Message received: " + message);
                    smartbenches.Add(new SmartBench(ip, message));
                    socket.Close();
                }
                catch
                {
                    System.Diagnostics.Debug.WriteLine("Not a smartbench: " + ip);
                    socket.Close();
                }
            });
            return smartbenches;
        }

        //Get range of IPs on default gateway
        private List<String> GetIPRange(string defaultGateway, int min, int max)
        {
            var ips = new List<string>();
            for(var i = min; i < max; i++)
            {
                ips.Add($"{defaultGateway}.{i}");
            }
            return ips;
        }

        //Get mac address from IP
        public string GetMacByIp(string ip)
        {
            var pairs = this.GetMacIpPairs();

            foreach (var pair in pairs)
            {
                if (pair.IpAddress == ip)
                    return pair.MacAddress;
            }

            throw new Exception($"Can't retrieve mac address from ip: {ip}");
        }

        public IEnumerable<MacIpPair> GetMacIpPairs()
        {
            System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
            pProcess.StartInfo.FileName = "arp";
            pProcess.StartInfo.Arguments = "-a ";
            pProcess.StartInfo.UseShellExecute = false;
            pProcess.StartInfo.RedirectStandardOutput = true;
            pProcess.StartInfo.CreateNoWindow = true;
            pProcess.Start();

            string cmdOutput = pProcess.StandardOutput.ReadToEnd();
            string pattern = @"(?<ip>([0-9]{1,3}\.?){4})\s*(?<mac>([a-f0-9]{2}-?){6})";

            foreach (Match m in Regex.Matches(cmdOutput, pattern, RegexOptions.IgnoreCase))
            {
                yield return new MacIpPair()
                {
                    MacAddress = m.Groups["mac"].Value,
                    IpAddress = m.Groups["ip"].Value
                };
            }
        }

        public struct MacIpPair
        {
            public string MacAddress;
            public string IpAddress;
        }

    }
}
