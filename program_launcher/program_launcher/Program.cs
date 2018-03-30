using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace program_launcher
{
    class Program
    {
        static void Main(string[] args)
        {


            bool flag_run = false;
            List<string> programNames = new List<string>();


            foreach (string arg in args)
            {

                #region Starting ARGZ

                if (string.IsNullOrEmpty(arg))
                {

                }
                else if (arg.Equals("run"))
                {
                    flag_run = true;
                }
                else
                {
                    programNames.Add(arg);
                }

                #endregion


            }



            bool program_loop = true;
            while (program_loop)
            {
                if ((flag_run == true))
                {
                    run_program(programNames);
                    program_loop = false;

                }


                string inputText = string.Empty;
                string[] parsedText = new string[0];

                if (program_loop == true)
                {
                    inputText = Console.ReadLine();
                    parsedText = utilities.parse_string_spaces(inputText);



                    if ((parsedText[0].Equals("run")) && (parsedText.Length >= 2))
                    {
                        List<string> programList = new List<string>();
                        foreach (string name in parsedText)
                        {
                            if (!name.Equals("run"))
                            {
                                programList.Add(name);
                            }
                        }


                        run_program(programList);

                        program_loop = false;
                    }

                    if (parsedText[0].Equals("help"))
                    {
                        Console.WriteLine("======================================\r\nCommand List\r\n======================================");
                        Console.WriteLine("help\r\n This will diplay this command list\r\n------");
                        Console.WriteLine("run {program.name}\r\nThis will run the selected program\r\n------");
                        Console.WriteLine("exit | quit | stop\r\n This quit the program\r\n------");

                        Console.WriteLine("\r\n======================================\r\n");
                    }

                    if ((parsedText[0].Equals("exit")) || (parsedText[0].Equals("quit")) || (parsedText[0].Equals("stop")))
                    {
                        program_loop = false;
                        break;
                    }


                }

            }
        }


        static public void run_program(List<string> programsToRun)
        {

            foreach (string name in programsToRun)
            {
                bool failBool = false;

                try
                {
                    var p = new Process();

                    //Team ViewerCheck
                    if (name.ToLower().Equals("teamviewer.exe"))
                    {
                        p.StartInfo.WorkingDirectory = "C:\\Program Files (x86)\\TeamViewer";
                    }



                    p.StartInfo.FileName = name;
                    p.Start();

                }
                catch (Exception e)
                {
                    failBool = true;
                    Console.WriteLine(String.Concat("Error Launching: ", name, "\r\n", e.ToString()));
                }
                finally
                {
                    if (failBool == false)
                    {
                        Console.WriteLine(String.Concat("Launched: ", name));

                    }

                }
            }


        }
        static public void run_program(List<string> programsToRun, string DirectoryToUse)
        {

            foreach (string name in programsToRun)
            {
                bool failBool = false;

                try
                {
                    var p = new Process();

                    p.StartInfo.WorkingDirectory = DirectoryToUse;

                    p.StartInfo.FileName = name;
                    p.Start();

                }
                catch (Exception e)
                {
                    failBool = true;
                    Console.WriteLine(String.Concat("Error Launching: ", name, "\r\n", e.ToString()));
                }
                finally
                {
                    if (failBool == false)
                    {
                        Console.WriteLine(String.Concat("Launched: ", name));

                    }

                }
            }


        }

    }



    public class utilities
    {


        #region File Functions

        #region Load File
        static public byte[] LoadBytesFromFile(string FileName)
        {
            byte[] fileBytes;

            try
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @FileName);

                FileStream filePathStream = new FileStream(filePath,
                FileMode.Open,
                FileAccess.Read);
                int sizeP = (int)filePathStream.Length;

                fileBytes = new byte[sizeP];
                sizeP = filePathStream.Read(fileBytes, 0, sizeP);

                filePathStream.Close();

                return fileBytes;
            }
            catch (Exception e)
            {
                Console.WriteLine("===========================================\r\nERROR\r\n===========================================");
                Console.WriteLine(e);
                Console.WriteLine("\r\n===========================================\r\n");
                Console.WriteLine("Maybe missing files?");
                Console.WriteLine("\r\n===========================================\r\n");
                fileBytes = new byte[0];

                return fileBytes;
            }

        }
        static public byte[] LoadBytesFromFile(string FileName, string FilePath)
        {
            byte[] fileBytes;

            try
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FilePath, @FileName);

                FileStream filePathStream = new FileStream(filePath,
                FileMode.Open,
                FileAccess.Read);
                int sizeP = (int)filePathStream.Length;

                fileBytes = new byte[sizeP];
                sizeP = filePathStream.Read(fileBytes, 0, sizeP);

                filePathStream.Close();

                return fileBytes;
            }
            catch (Exception e)
            {
                Console.WriteLine("===========================================\r\nERROR\r\n===========================================");
                Console.WriteLine(e);
                Console.WriteLine("\r\n===========================================\r\n");
                Console.WriteLine("Maybe missing files?");
                Console.WriteLine("\r\n===========================================\r\n");
                fileBytes = new byte[0];

                return fileBytes;
            }

        }



        static public string LoadFromFile(string FileName)
        {
            try
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @FileName);

                FileStream filePathStream = new FileStream(filePath,
                FileMode.Open,
                FileAccess.Read);
                int sizeP = (int)filePathStream.Length;
                byte[] fileBytes = new byte[sizeP];
                sizeP = filePathStream.Read(fileBytes, 0, sizeP);
                filePathStream.Close();

                string FileString = Encoding.ASCII.GetString(fileBytes);

                return FileString;
            }
            catch (Exception e)
            {
                Console.WriteLine("===========================================\r\nERROR\r\n===========================================");
                Console.WriteLine(e);
                Console.WriteLine("\r\n===========================================\r\n");
                Console.WriteLine("Maybe missing files?");
                Console.WriteLine("\r\n===========================================\r\n");

                return string.Empty;
            }

        }
        static public string LoadFromFile(string FileName, string FilePath)
        {
            try
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FilePath, @FileName);

                FileStream filePathStream = new FileStream(filePath,
                FileMode.Open,
                FileAccess.Read);
                int sizeP = (int)filePathStream.Length;
                byte[] fileBytes = new byte[sizeP];
                sizeP = filePathStream.Read(fileBytes, 0, sizeP);
                filePathStream.Close();

                string FileString = Encoding.ASCII.GetString(fileBytes);

                return FileString;
            }
            catch (Exception e)
            {
                Console.WriteLine("===========================================\r\nERROR\r\n===========================================");
                Console.WriteLine(e);
                Console.WriteLine("\r\n===========================================\r\n");
                Console.WriteLine("Maybe missing files?");
                Console.WriteLine("\r\n===========================================\r\n");

                return string.Empty;
            }

        }


        static public string[] LoadLinesFromFile(string FileName)
        {
            string[] fileResults = new string[0];

            try
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @FileName);

                fileResults = File.ReadAllLines(filePath);


                return fileResults;
            }
            catch (Exception e)
            {
                Console.WriteLine("===========================================\r\nERROR\r\n===========================================");
                Console.WriteLine(e);
                Console.WriteLine("\r\n===========================================\r\n");
                Console.WriteLine("Maybe missing files?");
                Console.WriteLine("\r\n===========================================\r\n");

                return fileResults;
            }

        }
        static public string[] LoadLinesFromFile(string FileName, string FilePath)
        {
            string[] fileResults = new string[0];

            try
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FilePath, @FileName);

                fileResults = File.ReadAllLines(filePath);


                return fileResults;
            }
            catch (Exception e)
            {
                Console.WriteLine("===========================================\r\nERROR\r\n===========================================");
                Console.WriteLine(e);
                Console.WriteLine("\r\n===========================================\r\n");
                Console.WriteLine("Maybe missing files?");
                Console.WriteLine("\r\n===========================================\r\n");

                return fileResults;
            }

        }
        #endregion

        #region Make File
        static public void MakeFile(string FileName, string FilePath)
        {
            MakeFile(FileName, FilePath, String.Empty);
        }
        static public void MakeFile(string FileName, string FilePath, byte[] FileData)
        {
            string newFilePath = System.IO.Path.Combine(FilePath, FileName);

            if (!File.Exists(newFilePath))
            {

                // Create a file to write to.
                using (FileStream sw = File.Create(AppDomain.CurrentDomain.BaseDirectory + newFilePath))
                {
                    sw.Write(FileData, 0, FileData.Length);

                }
            }


        }
        static public void MakeFile(string FileName, string FilePath, string FileData)
        {
            // Old way
            //string[] lines_Strings = { FileData };
            //System.IO.File.WriteAllLines(AppDomain.CurrentDomain.BaseDirectory + newFilePath, lines_Strings);

            string newFilePath = System.IO.Path.Combine(FilePath, FileName);

            if (!File.Exists(newFilePath))
            {
                using (StreamWriter sw = File.CreateText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory + newFilePath)))
                {
                    sw.WriteLine(FileData);
                }
            }
        }
        static public void MakeFile(string FileName, string FilePath, string[] FileData)
        {
            string newFilePath = System.IO.Path.Combine(FilePath, FileName);
            if (!File.Exists(newFilePath))
            {
                using (StreamWriter sw = File.CreateText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, newFilePath)))
                {
                    foreach (string line in FileData)
                    {
                        sw.WriteLine(line);
                    }
                }
            }
        }
        #endregion

        #region Write to File
        static public void WriteToFile(string FileName, string FilePath, string FileData)
        {
            string newFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FilePath, FileName);

            if (File.Exists(newFilePath))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.AppendText(newFilePath))
                {
                    sw.WriteLine(FileData);

                }
            }


        }
        static public void WriteToFile(string FileName, string FilePath, string[] FileData)
        {
            string newFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FilePath, FileName);

            if (File.Exists(newFilePath))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.AppendText(newFilePath))
                {
                    foreach (string line in FileData)
                    {
                        sw.WriteLine(line);

                    }

                }
            }


        }
        #endregion

        #region OverWrite the File
        static public void OverWriteToFile(string FileName, string FilePath, byte[] FileData)
        {
            string newFilePath = System.IO.Path.Combine(FilePath, FileName);

            if (File.Exists(newFilePath))
            {

                // Create a file to write to.
                using (FileStream sw = File.Create(AppDomain.CurrentDomain.BaseDirectory + newFilePath))
                {
                    sw.Write(FileData, 0, FileData.Length);

                }
            }


        }

        static public void OverWriteToFile(string FileName, string FilePath, string FileData)
        {
            string newFilePath = System.IO.Path.Combine(FilePath, FileName);

            if (File.Exists(newFilePath))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(AppDomain.CurrentDomain.BaseDirectory + newFilePath))
                {
                    sw.WriteLine(FileData);

                }
            }


        }
        static public void OverWriteToFile(string FileName, string FilePath, string[] FileData)
        {
            string newFilePath = System.IO.Path.Combine(FilePath, FileName);

            if (File.Exists(newFilePath))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(AppDomain.CurrentDomain.BaseDirectory + newFilePath))
                {
                    foreach (string line in FileData)
                    {
                        sw.WriteLine(line);

                    }

                }
            }


        }
        #endregion


        #region Does File Exist
        static public bool DoesFileExist(string FileName, string FilePath)
        {
            bool doesFileExist = false;
            string newFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FilePath, FileName);

            if (File.Exists(newFilePath))
            {
                doesFileExist = true;
            }

            return doesFileExist;
        }
        #endregion

        #region Delete File

        static public void DeleteFile(string FileName, string FilePath)
        {
            string newFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FilePath, FileName);
            if (File.Exists(newFilePath))
            {

                File.Delete(newFilePath);

            }
        }

        #endregion

        #region Make Folder

        public static void MakeFolder(string folderName)
        {
            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folderName);
            System.IO.Directory.CreateDirectory(path);

        }
        #endregion

        #region Does Folder Exist
        static public bool DoesFolderExist(string folderPath)
        {
            bool doesFolderExist = false;

            if (Directory.Exists(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folderPath)))
            {
                doesFolderExist = true;
            }

            return doesFolderExist;
        }
        #endregion

        #region Delete Folder

        static public void DeleteFolder(string FileName, string FilePath)
        {
            string newFilePath = System.IO.Path.Combine(FilePath, FileName);
            if (Directory.Exists(newFilePath))
            {

                Directory.Delete(newFilePath);

            }
        }

        #endregion


        #endregion


        #region Get DateTime Online

        public static DateTime GetNistTime()
        {
            DateTime dateTime = DateTime.MinValue;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://nist.time.gov/actualtime.cgi?lzbc=siqm9b");
            request.Method = "GET";
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; Trident/6.0)";
            request.ContentType = "application/x-www-form-urlencoded";
            request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore); //No caching
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                StreamReader stream = new StreamReader(response.GetResponseStream());
                string html = stream.ReadToEnd();//<timestamp time=\"1395772696469995\" delay=\"1395772696469995\"/>
                string time = Regex.Match(html, @"(?<=\btime="")[^""]*").Value;
                double milliseconds = Convert.ToInt64(time) / 1000.0;
                dateTime = new DateTime(1970, 1, 1).AddMilliseconds(milliseconds).ToLocalTime();
            }

            return dateTime;
        }

        #endregion


        #region Parse String Spaces

        static public string[] parse_string_spaces(string input)
        {
            string[] results = new string[0];

            char[] delimiterChars = { ' ' };

            results = input.Split(delimiterChars);

            return results;
        }

        #endregion



    }


}
