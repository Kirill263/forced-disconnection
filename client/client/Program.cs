using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Client
{
    public static void Main()
    {
        byte[] bytes = new byte[1024];

        // Устанавливаем удаленную конечную точку для сокета
        IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

        // Создаем сокет TcpClient
        TcpClient client = new TcpClient();

        // Подключаемся к удаленному серверу
        client.Connect(remoteEP);
        Console.WriteLine("Подключено к серверу");

        // Получаем стрим (поток данных) для чтения и записи
        NetworkStream stream = client.GetStream();

        string data = null;
        while (true)
        {
            Console.Write("Вы: ");
            data = Console.ReadLine();

            // Отправляем данные на сервер
            byte[] msg = Encoding.ASCII.GetBytes(data);
            stream.Write(msg, 0, msg.Length);

            // Читаем ответ от сервера и отображаем его на экране
            int bytesRead = stream.Read(bytes, 0, bytes.Length);
            data = Encoding.ASCII.GetString(bytes, 0, bytesRead);
            Console.WriteLine("Сервер: {0}", data);
        }

        // Закрываем соединение
        client.Close();
    }
}

