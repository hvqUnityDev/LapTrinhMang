using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TCP_FW_Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Title = "Bài 1 TCPClient";

            //cung cấp địa chỉ ip của máy client
            IPAddress _ipaddress = IPAddress.Parse("192.168.1.6");

            //tạo IPEndPoint
            IPEndPoint _ipendpoint = new IPEndPoint(_ipaddress, 9000);

            //tạo socket cho client
            Socket _socketclient = new Socket(SocketType.Stream, ProtocolType.Tcp);
            _socketclient.Connect(_ipendpoint);

            byte[] _data = new byte[1024];
            int _recv = _socketclient.Receive(_data);
            string _s = Encoding.UTF8.GetString(_data, 0, _recv);
            Console.WriteLine($"server gửi:{_s}");
            string _input;

            while (true)
            {
                _input = Console.ReadLine();

                //chuyển _input thành mảng byte
                _data = new byte[1024];
                _data = Encoding.UTF8.GetBytes(_input);
                _socketclient.Send(_data, _data.Length, SocketFlags.None);
                if (_input.ToUpper().Equals("QUIT")) break;

                //new lại mảng byte để đảm bảo loại bỏ hoàn toàn dữ liệu cũ,
                _data = new byte[1024];
                _recv = _socketclient.Receive(_data);
                _s = Encoding.UTF8.GetString(_data, 0, _recv);
                Console.WriteLine($"server gửi:{_s}");
            }

            _socketclient.Disconnect(true);
            _socketclient.Close();
        }
    }
}
