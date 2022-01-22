using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.Net;
using System.Threading;
using System.IO;

namespace LinuxQCDQC.Classes
{
    public class Update
    {
        public void Start()
        {
            WebClient webClient = new WebClient();
            var client = new WebClient();

            try
            {
                Thread.Sleep(5000);
                File.Delete(@".\LinuxQCDQC.exe");
                client.DownloadFile("https://cdn-107.anonfiles.com/tfQ9c7kex1/38bb100d-1640861729/update_push.zip", @"update_push.zip");
                string zipPath = @".\update_push.zip";
                string extractPath = @".\";

            }
            catch (WebException ea)
            {

            }
        }

    }
}
