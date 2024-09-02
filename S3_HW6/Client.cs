using SetDz6.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SetDz6
{
    internal class Client
    {
        private readonly string _name;
        private readonly IMessageSource _messageSource;
        private readonly IPEndPoint _peerEndPoint;
        public Client(IMessageSource messageSource, IPEndPoint peerEndPoint, string name)
        {
            _messageSource = messageSource;
            _peerEndPoint = peerEndPoint;
            _name = name;
        }
        private void Registor()
        {
            var messageJson = new MessageUDP() 
            { Command = Command.Register, 
              FromName = _name
            };
            _messageSource.SendMessage(messageJson, _peerEndPoint);
        }
        public void ClientSendler()
        {
            while (true)
            {
                Console.WriteLine("Enter message");
                string text = Console.ReadLine();
                Console.WriteLine("Введите имя кому отправляем");
                string name = Console.ReadLine();
                if (string.IsNullOrEmpty(name))    // если пользователь не ввел имя кому отправить сообщение
                {
                    continue;
                }
                var messageJson = new MessageUDP()
                {
                    Text = text,
                    FromName = _name,
                    ToName = name,
                };
                _messageSource.SendMessage(messageJson, _peerEndPoint);
            }
        } 
        public void ClientListner()
        {
            Registor();
            IPEndPoint ep = new IPEndPoint(_peerEndPoint.Address, _peerEndPoint.Port);
            while (true)
            {                
                MessageUDP message = _messageSource.ReceiveMessage(ref ep);
                Console.WriteLine(message.ToString());
            }
        }
    }
}
