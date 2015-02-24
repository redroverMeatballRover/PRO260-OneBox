using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleDriveTests
{
    class Program
    {
        static readonly string CLIENT_ID = "320329124466-6krqbu5gkdr0d8tfv91plfn31no65l00.apps.googleusercontent.com";
        static readonly string CLIENT_SECRET = "qZ2oSBLR-NS3OK529S4UrTq4";
        static readonly string APP_NAME = "OneBox";
        static readonly string[] SCOPES = new string[] { DriveService.Scope.Drive };

        /* Old stuff */
        // Id: 942210298793-b91s06ssc17fi18jpf8ju3aobem19b62.apps.googleusercontent.com
        // Secret: j9txc2aFOiNOt2-03khAGby7
        // App Name: Drive Console Tests

        static void Main(string[] args)
        {
            QuickStart();
        }

        private static void QuickStart()
        {
            UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync
            (
                new ClientSecrets
                {
                    ClientId = CLIENT_ID,
                    ClientSecret = CLIENT_SECRET
                },
                SCOPES,
                "user", // Id handled by the DB
                CancellationToken.None
            ).Result;

            // Create the service.
            BaseClientService.Initializer initializer = new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = APP_NAME
            };

            DriveService service = new DriveService(initializer);
            
            //File body = new File();
            //body.Title = "My document";
            //body.Description = "A test document";
            //body.MimeType = "text/plain";

            //byte[] byteArray = System.IO.File.ReadAllBytes("../../document.txt");
            //System.IO.MemoryStream stream = new System.IO.MemoryStream(byteArray);

            FilesResource.ListRequest listRequest = service.Files.List();
            FileList files = listRequest.Execute();
            IEnumerable<File> daFiles = files.Items;

            Console.WriteLine("All the files on your Google Drive:\n");

            string testDLId = null;
            foreach (var item in daFiles)
            {
                string title = item.Title;
                Console.WriteLine(title);
                if (title.StartsWith("HUM205")) // Google Doc test = "ORM", Non Google file test = "HUM205"
                {
                    testDLId = item.Id;
                }
            }

            Console.WriteLine();

            /*
             * Need to somewhow check if the file being dl-ed is a Google Doc, Spreadsheet, or Presentation
             * If its one of the above, then we have to specifiy the appropriate extension since we're converting it, when using the normal dl link then can get the extension
             */

            // ---- Downloading Normal Files ---- //
            //FilesResource.GetRequest getRequest = service.Files.Get(testDLId);
            //File foundFile = getRequest.Execute();
            //string DLUri = foundFile.DownloadUrl;

            // ---- Downloading Google Files ---- //
            //foreach (var key in foundFile.ExportLinks.Keys)
            //{
            //    Console.WriteLine(key);
            //}
            //string DLUri = foundFile.ExportLinks["application/pdf"];

            // ---- Writing to local system file ---- //
            //var x = service.HttpClient.GetByteArrayAsync(DLUri);
            //while (!x.IsCompleted)
            //{
            //    Console.WriteLine("working...");
            //}

            //string DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            //byte[] arrBytes = x.Result;
            //System.IO.File.WriteAllBytes(DesktopPath + "/PulledFile3." + foundFile.FileExtension, arrBytes);

            // ---- Writing to local system file using StreamWriter (NOT TESTED) ---- //
            //using (System.IO.StreamWriter writer = new System.IO.StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) +  "/PulledFile.pdf"))
            //{
            //    writer.Write(storageString);
            //}

            // ---- Uploading Files ---- //
            //FilesResource.InsertMediaUpload request = service.Files.Insert(body, stream, "text/plain");
            //request.Upload();

            // ---- Other stuff ---- //
            //File file = request.ResponseBody;
            //Console.WriteLine("File id: " + file.Id);
            //Console.WriteLine("Press Enter to end this process.");
            Console.WriteLine("Done!");
            //Console.ReadLine();
        }


        // Reference method for downloading a file //

        /// <summary>
        /// Download a file
        /// Documentation: https://developers.google.com/drive/v2/reference/files/get
        /// </summary>
        /// <param name="_service">a Valid authenticated DriveService</param>
        /// <param name="_fileResource">File resource of the file to download</param>
        /// <param name="_saveTo">location of where to save the file including the file name to save it as.</param>
        /// <returns></returns>
        public static Boolean downloadFile(DriveService _service, File _fileResource, string _saveTo)
        {

            if (!String.IsNullOrEmpty(_fileResource.DownloadUrl))
            {
                try
                {
                    var x = _service.HttpClient.GetByteArrayAsync(_fileResource.DownloadUrl);
                    byte[] arrBytes = x.Result;
                    System.IO.File.WriteAllBytes(_saveTo, arrBytes);
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("An error occurred: " + e.Message);
                    return false;
                }
            }
            else
            {
                // The file doesn't have any content stored on Drive.
                return false;
            }
        }
    }
}
