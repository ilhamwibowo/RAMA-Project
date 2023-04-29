using System.Text;
using UHF;
namespace komkmk
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var com = 0;
            var readInput = "";
            string RaceId = "";
            string checkpointId = "";
            string token = "";
            do
            {
                Console.WriteLine("masukkan Com berapa Hardware berada! {1-9}");
                readInput = Console.ReadLine();

            } while (!Int32.TryParse(readInput, out com) && com < 10 && com > 0);
            {
                Console.WriteLine("Masukkan RaceId dengan Benar");
                RaceId = Console.ReadLine();
                Console.WriteLine("Masukkan checkpointId dari yang sesuai untuk hardware ini");
                checkpointId = Console.ReadLine();
                Console.WriteLine("Masukkan Token untuk akun Admin, (bisa didapatkan dengan Login pada WebApp)");
                token = Console.ReadLine();
            }
            Reader r = new Reader(com, RaceId, token, checkpointId);
            Console.WriteLine("sadasd");
            r.OpenComm();
            if (!r.portConnected)
            {
                Console.WriteLine("Gagal Menemukan Koneksi ke Hardwarenya");
                Console.ReadLine();
                System.Environment.Exit(0);
            }
            Thread tRead = new Thread(new ThreadStart(r.ReadInventory)); tRead.Start(); tRead.IsBackground = true; 
            Thread tSend = new Thread(new ThreadStart(r.SendScanned)); tSend.Start(); tSend.IsBackground = true;
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine(string.Join("\n", r.EPClist.Select(x => x.Key + "=" + x.Value).ToArray()));
            Thread.Sleep(1000);
            /*
                        Sender sender = new Sender();
                        sender.PostScanResult("fauzani", "asd", "asd").Wait();*/

        }
    }
}