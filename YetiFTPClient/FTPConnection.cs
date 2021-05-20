using System.IO;
using WinSCP;

namespace YetiFTPClient
{
    public class FTPConnection
    {
        private SessionOptions options;
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
