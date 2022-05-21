namespace SushiShop.Services
{
    internal class FileService : IFileService

    {
        private string PATH = @"/Users/alexey/CSharp.DiplomProject/SushiShop/SushiShop/log/" + " " +
                    DateTime.Now.ToString("yyyymmdd") + ".txt";
        
        public void log_function(string error)
        {
            StreamWriter sw;
            FileInfo log_file = new FileInfo(@"/Users/alexey/CSharp.DiplomProject/SushiShop/SushiShop/log/" + " " + DateTime.Now.ToString("yyyymmdd") + "[1]" + ".txt");
            
            try
            {
                long fileByteSize = log_file.Length;
                if (fileByteSize >= 245760)
                {
                    int i=1;
                    File.Move(@"/Users/alexey/CSharp.DiplomProject/SushiShop/SushiShop/log/" + " " + DateTime.Now.ToString("yyyymmdd") + "1" + ".txt", @"/Users/alexey/CSharp.DiplomProject/SushiShop/SushiShop/log/" + " " + DateTime.Now.ToString("yyyymmdd") + "[" + (++i) +"]" + ".txt");
                }
                sw = log_file.AppendText();
                sw.WriteLine(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss\t"));
                sw.WriteLine(error);
                sw.Close();
            }
            
            catch
            {
                StreamWriter sw_e;
                FileInfo log_error = new FileInfo(@"/Users/alexey/CSharp.DiplomProject/SushiShop/SushiShop/log/test1.log");
                
                sw_e = log_file.AppendText();
                sw_e.WriteLine(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss\t"));
                sw_e.WriteLine(error);
                sw_e.Close();
            }
        }
        public async Task WriteLog(string message)
        {
            using (StreamWriter sw = new StreamWriter(PATH, true, System.Text.Encoding.Default))
            {
                await sw.WriteLineAsync(message);
            }
        }
        
        public async Task ReadLogs()
        {
            using (StreamReader sr = new StreamReader(PATH))
            {
                Console.WriteLine(await sr.ReadToEndAsync());
            }
        }
    }
}