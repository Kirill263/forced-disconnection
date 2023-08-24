using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Server
{
    public static void Main()
    {
        byte[] bytes = new byte[1024];
        string data = null;

        // Устанавливаем локальную конечную точку для сокета
        IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

        // Создаем сокет TcpListener
        TcpListener server = new TcpListener(localEndPoint);

        // Начинаем слушать входящие соединения
        server.Start();
        Console.WriteLine("Ожидание соединений...");

        // Входим в цикл для ожидания клиентского соединения
        TcpClient client = server.AcceptTcpClient();
        Console.WriteLine("Клиент подключился");

        // Получаем стрим (поток данных) для чтения и записи
        NetworkStream stream = client.GetStream();

        int i;

        // Читаем данные от клиента, пока есть что читать
        while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
        {
            // Преобразуем байты в строку
            data = Encoding.ASCII.GetString(bytes, 0, i);
            Console.WriteLine("Клиент: {0}", data);

            // Отправляем ответ клиенту
            byte[] msg = Encoding.ASCII.GetBytes(data);
            stream.Write(msg, 0, msg.Length);
            Console.WriteLine("Сервер отправил: {0}", data);
        }

        // Закрываем соединения
        client.Close();
        server.Stop();
    }
}

