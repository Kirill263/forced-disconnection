using System;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        string mode = "runas";
        IntPtr result = ShellExecute(IntPtr.Zero, mode, "cmd.exe", "/K C:\\cports\\cports.exe /close 127.0.0.1 11000 * *", null, ShowCommands.SW_SHOWNORMAL);
        Console.WriteLine("Соединение разорвано");

        Environment.Exit(0);
    }

    [System.Runtime.InteropServices.DllImport("shell32.dll")] // Атрибут для импорта функции ShellExecute из библиотеки shell32.dll
    private static extern IntPtr ShellExecute(IntPtr hwnd, string lpOperation, string lpFile, string lpParameters, string lpDirectory, ShowCommands nShowCmd);

    private enum ShowCommands : int
    {
        SW_SHOWNORMAL = 1
    }
}
