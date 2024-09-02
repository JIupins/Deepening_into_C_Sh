using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace S3_HW1
{
    internal class Chat
    {


        public static void Server()
        {
            IPEndPoint localEP = new IPEndPoint(IPAddress.Any, 0);
            UdpClient ucl = new UdpClient(12345);
            Console.WriteLine("Сервер ожидает сообщение от клиента");

            while (true)
            {
                try
                {
                    byte[] buffer = ucl.Receive(ref localEP);
                    string str1 = Encoding.UTF8.GetString(buffer);

                    Massage? somemassage = Massage.FromJson(str1);
                    if (somemassage != null)
                    {
                        Console.WriteLine(somemassage.ToString());
                        Massage newmassage = new Massage("server", "сообщение полученно");
                        string js = newmassage.ToJson();
                        byte[] bytes = Encoding.UTF8.GetBytes(js);
                        ucl.Send(bytes, localEP);
                    }
                    else
                    {
                        Console.WriteLine("Некорректное сообщение");
                    }
                }
                catch (Exception e) { Console.WriteLine(e.Message); }
            }
        }

        public static void Client(string nik)
        {
            IPEndPoint localEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12345);
            UdpClient ucl = new UdpClient();



            while (true)
            {
                Console.WriteLine("Введите сообщение");
                string text = Console.ReadLine();
                if (string.IsNullOrEmpty(text))
                {
                    break;
                }
                Massage newmassage = new Massage(nik, text);
                string js = newmassage.ToJson();
                byte[] bytes = Encoding.UTF8.GetBytes(js);
                ucl.Send(bytes, localEP);

                byte[] buffer = ucl.Receive(ref localEP);
                string str1 = Encoding.UTF8.GetString(buffer);
                Massage? somemassage = Massage.FromJson(str1);
                Console.WriteLine(somemassage);


            }

        }
    }

}
