using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using UHF;
using Nito.AsyncEx;
namespace komkmk
{
    class Reader
    {
        byte Ant = 0;
        int TagNum = 0;
        int Totallen = 0;
        byte[] EPC = new byte[5000];
        byte MaskMem = 0;
        byte[] MaskAdr = new byte[2];
        byte MaskLen = 0;
        byte[] MaskData = new byte[100];
        byte MaskFlag = 0;
        byte Target = 0x0;
        byte Qvalue = 0x0;
        byte Session = 0xff;
        byte tidAddr = 0x1;
        byte tidLen = 0xF;
        byte TIDFlag = 0x0;
        byte InAnt = 0x80;
        byte Scantime = 0;
        byte FastFlag = 1;
        int portNum = 3;
        public bool portConnected = false;
        byte fComAdr = 0xFF;
        byte fBaud = 5;
        int FrmPortIndex = 0;

        public Dictionary<string, int> EPClist = new Dictionary<string, int>();
        public List<string> rfidlist = new List<string>();
        public List<string> sentrfid = new List<string>();
        private static readonly HttpClient client = new HttpClient();

        private string RaceId;
        private string Token;
        private string CheckpointId;
        public Reader(int portnum, string raceId, string token, string checkpointId)
        {
            portNum = portnum;
            RaceId = raceId;
            Token = token;
            CheckpointId = checkpointId;
        }
        public void OpenComm()
        {
            int fCmdRet = RWDev.OpenComPort(portNum, ref fComAdr, fBaud, ref FrmPortIndex);
            portConnected =  fCmdRet == 0;
        }
        public static string ByteArrayToHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0'));

            return sb.ToString().ToUpper();
        }
        public void ReadInventory()
        {
            if (!portConnected) return;
            while (true)
            {

                int ret = RWDev.Inventory_G2(ref fComAdr, Qvalue, Session, MaskMem, MaskAdr, MaskLen, MaskData, MaskFlag, tidAddr, tidLen, TIDFlag, Target, InAnt, Scantime, FastFlag, EPC, ref Ant, ref Totallen, ref TagNum, FrmPortIndex);
                if (TagNum > 1)
                {
                    //Console.WriteLine("Thread");
                }
                for (int i = 0; i < TagNum; i++)
                {
                    string EpcString = ByteArrayToHexString(EPC.Skip(i * 14).Skip(1).Take(12).ToArray());
                    int count;
                    if (!EPClist.TryGetValue(EpcString, out count))
                    {
                        count = 1;
                        EPClist.Add(EpcString, count);
                    }
                    else
                    {
                        EPClist[EpcString] = count+1;
                    }
                    if (!rfidlist.Contains(EpcString) && !sentrfid.Contains(EpcString) ){
                        Console.WriteLine("Found Tag :" + EpcString);
                        rfidlist.Add(EpcString); 
                    };
                }
            }
        }
        public void SendScanned()
        {
            while (true)
            {
                foreach ( string r in rfidlist.ToList() )
                {
                    bool success = AsyncContext.Run(() => PostScanResult(r));
                    if (success) 
                    {
                        Console.WriteLine("Sent : " + r);
                        sentrfid.Add(r); 
                    }
                    else
                    {
                        Console.WriteLine("Failed :" + r);
                    }
                }
                lock (rfidlist)
                {
                    rfidlist = rfidlist.Except(sentrfid).ToList();
                }
            }
        }
        string MyDictionaryToJson(Dictionary<string, string> dict)
        {
            var entries = dict.Select(d =>
                string.Format("\"{0}\": \"{1}\"", d.Key, string.Join(",", d.Value)));
            return "{" + string.Join(",", entries) + "}";
        }
        async Task<bool> PostScanResult(string RFID)
        {
            var apilink = "https://ramadevelopment.fly.dev/api/checkpoint/scan/";
            var data = new Dictionary<string, string>
           {
               {"rfid", RFID },
               {"raceId", RaceId }
           };
            var content = new StringContent(MyDictionaryToJson(data), Encoding.UTF8, "application/json");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Clear();
            Console.WriteLine(MyDictionaryToJson(data));
            Console.WriteLine("asdd");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            var response = await client.PostAsync(apilink + CheckpointId, content);
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            return response.IsSuccessStatusCode;
        }
    }
}
