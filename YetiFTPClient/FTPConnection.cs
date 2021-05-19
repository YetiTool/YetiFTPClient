using System.IO;
using WinSCP;

namespace YetiFTPClient
{
    public class FTPConnection
    {
        private SessionOptions options;
        private string ip;
        private string username;
        private string password;
        public FTPConnection(string ip, string username, string password)
        {
            options = new SessionOptions
            {
                GiveUpSecurityAndAcceptAnySshHostKey = true,
                Protocol = Protocol.Sftp,
                HostName = ip,
                UserName = username,
                Password = password
            };

            this.ip = ip;
            this.username = username;
            this.password = password;
        }

        /*
         * Try get easycut source code, this is run if there's no smartbench_name.txt
         */
        public bool TryConnect()
        {
            try
            {
                using (Session session = new Session())
                {
                    session.Open(options);
                    if (session.Opened)
                        if (session.FileExists("/home/pi/easycut-smartbench/src/main.py"))
                            return true;
                        else
                            return false;
                    else
                        return false;
                }
            }catch
            {
                System.Diagnostics.Debug.WriteLine("Error occured testing connection");
                return false;
            }
        }

        /*
         * Read smartbench name from smartbench_name.txt
         */
        public string GetSmartbenchName()
        {
            try
            {
                using (Session session = new Session())
                {
                    string path = "/home/pi/smartbench_name.txt";
                    session.Open(options);
                    if(session.Opened)
                    {
                        if (session.FileExists(path))
                        {
                            string tempPath = Path.GetTempFileName();

                            var sourcePath = RemotePath.EscapeFileMask(path);
                            session.GetFiles(sourcePath, tempPath).Check();

                            string[] lines = File.ReadAllLines(tempPath);
                            File.Delete(tempPath);
                            return lines[0];
                        }
                    }
                }
            }catch
            {
                return null;
            }

            return null;
        }

        //Upload the drag & dropped file to the jobcache folder (ready to be used)
        public bool Upload(string file)
        {
            try
            {
                using(Session session = new Session())
                {
                    session.Open(options);

                    TransferOptions transferOptions = new TransferOptions();
                    transferOptions.TransferMode = TransferMode.Binary;

                    TransferOperationResult result = session.PutFiles(file, "/home/pi/easycut-smartbench/src/jobCache/", false, transferOptions);
                    result.Check();

                    if (result.IsSuccess)
                        return true;
                    else
                        return false;
                }
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("Error occured uploading file");
                return false;
            }
        }
    }
}
