using System.ComponentModel;
using System.Net;

internal class Program
{
    static string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    const string imageUrl_1 = @"https://exactpublicity.com/wp-content/uploads/2019/07/Summer.jpg";
    const string imageUrl_2 = @"https://media.cnn.com/api/v1/images/stellar/prod/210316134738-02-wisdom-project-summer.jpg";
    private static async Task Main(string[] args)
    {
        // Donwload File using HttpClient

        //HttpClient client = new HttpClient();

        // Way1 
        /* HttpRequestMessage message = new HttpRequestMessage()
         {
             Method = HttpMethod.Get,
             RequestUri = new Uri(imageUrl_1),
         };
         var response = await client.SendAsync(message);
         Console.WriteLine($"Status : {response.StatusCode}");
         using (FileStream fs = new FileStream(desktopPath + "/image.jpg", FileMode.Create))
         {
             await response.Content.CopyToAsync(fs);
         }*/

        // way 2
        /*byte[] buffer = await client.GetByteArrayAsync(imageUrl_2);
        File.WriteAllBytes(desktopPath + "/" + Path.GetFileName(imageUrl_2), buffer);*/

        // Donwload File using WebClient
        //WebClient client = new WebClient();

        // sync donwload
        /*client.DownloadFile(imageUrl_1, Path.Combine(desktopPath, Path.GetFileName(imageUrl_1)));
        Console.WriteLine("File loaded");*/

        //async donwload
        Console.WriteLine("File loading ... ");

        //DonwloadFileAsync(@"https://ash-speed.hetzner.com/100MB.bin");
        DonwloadFileAsync(imageUrl_1);
        Console.ReadKey();

    }
    private static async void DonwloadFileAsync(string address)
    {
        WebClient client = new WebClient();

        client.DownloadFileCompleted += ClientDownloadFileCompleted;
        client.DownloadProgressChanged += ClientDownloadProgressChanged;
        
        string fileName = Path.GetFileName(address);

        await client.DownloadFileTaskAsync(address, Path.Combine(desktopPath, fileName));

        // cancel donwload
        //client.CancelAsync();

        Console.WriteLine($"{fileName} - file loaded!");
    }

    private static void ClientDownloadProgressChanged(object? sender, DownloadProgressChangedEventArgs e)
    {
        Console.WriteLine($"Downloading .... {Math.Round((float)e.BytesReceived / 1024 / 1024,2),-10} MB of {Math.Round((float)e.TotalBytesToReceive / 1024 / 1024,2),-10} MB {e.ProgressPercentage,-10}%");
    }
    private static void ClientDownloadFileCompleted(object? sender, AsyncCompletedEventArgs e)
    {
        if (e.Cancelled)
        {
            Console.WriteLine("File downloading was cancelled");
        }
        else
        {
            Console.WriteLine("File downloaded succesfully");
        }
    }
}