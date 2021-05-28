using System;
using System.Collections;
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

        public List<String> GetConnections()
        {
            var openIPs = new List<String>();

            Parallel.ForEach(GetIPRange(defaultGateway, 0, 255), ip =>
            {
                Ping ping = new Ping();

                PingReply reply = ping.Send(ip, 100);
                if (reply.Status == IPStatus.Success)
                {
                    Logging.TryLog(ip, "Ping was successful: " + ip);
                    try
                    {
                        if (GetMacByIp(ip).StartsWith("b8-27-eb") || GetMacByIp(ip).StartsWith("dc-a6-32") || GetMacByIp(ip).StartsWith("e4-5f-01"))
                        {
                            openIPs.Add(ip);
                            Logging.TryLog(ip, "IP conformed to MAC prefix: " + ip);
                        }
                        else
                        {
                            Logging.TryLog(ip, $"MAC prefix doesn't conform to list of console MAC prefixes: {GetMacByIp(ip)}");
                        }
                    }
                    catch
                    {
                        Logging.TryLog(ip, "Failed to retrieve MAC IP from device at: " + ip);
                    }
                }
            });

            return openIPs;
        }

        //Create socket connections to get smartbenches and their nmaes.
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

                    string shortMessage = message;
                    shortMessage = shortMessage.Replace("\n", "");
                    if (shortMessage.Length > 16)
                    {
                        shortMessage = shortMessage.Substring(0, 16);
                    }

                    Logging.TryLog(ip, "Connected to socket at IP: " + ip);
                    smartbenches.Add(new SmartBench(ip, shortMessage, message));
                    socket.Close();
                }
                catch
                {
                    Logging.TryLog(ip, "Couldn't connect to SmartBench socket to retrieve name");
                    Logging.TryLog(ip, "SmartTransfer requires your SmartBench to be running Easycut 1.7.0 or higher");
                }
            });
            return smartbenches;
        }

        //Get range of IPs on default gateway
        private List<String> GetIPRange(string defaultGateway, int min, int max)
        {
            var ips = new List<string>();
            for (var i = min; i < max; i++)
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

            throw new Exception("");
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
