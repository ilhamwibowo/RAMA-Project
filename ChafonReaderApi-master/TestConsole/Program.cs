using Rmg.Chafon.ReaderApi.Ru5102;
using System;

var com = 0;
var readInput = "";
do
{
    Console.WriteLine("masukkan Com berapa Hardware berada! {1-9}");
    readInput = Console.ReadLine();

}while (!Int32.TryParse(readInput, out com) && com < 10 && com > 0);

try
{
    using var reader = await Ru5102Device.CreateAsync("COM" + com, 57600, 0x00);
    Console.WriteLine("Type 'End' to Close");
    Console.WriteLine("Type Any key to Scan");
    while (Console.ReadLine() != "End")
    {
        Console.WriteLine("Dekatkan Tag dengan Hardware untuk Membaca!\n");
        var prev = "";
        while (true)
        {
            var tags = string.Empty;
            await reader.InventoryAsync(null, tag => tags = tag);
            if (prev == tags) continue;
            var readResp = await reader.TryReadMemoryAsync(tags, 0, MemoryBank.Reserved, new AddressSegment(4, 4));
            prev = tags;
            if (tags != "")
            {
                Console.WriteLine($"Read : {tags}");
                break;
            }
            else
            {
                Console.WriteLine("Read NAK");
            }
        }
        Console.WriteLine("Type 'End' to Close");
        Console.WriteLine("Type Any key to ReScan");
    }
    
}
catch
{
    Console.WriteLine("Masukkan Hardwarenya");
    Console.ReadLine();
    System.Environment.Exit(0);
}
