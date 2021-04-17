using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            int seconds = 1;

            DoMouseClick(seconds);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        static void DoMouseClick(int sleepTime)
        {
            while (true)
            {
                uint MOUSEEVENTF_LEFTDOWN = 0x02;
                uint MOUSEEVENTF_LEFTUP = 0x04;

                var point = GetCursorPosition();

                //Call the imported function with the cursor's current position
                //uint X = (uint)System.Windows.Forms.Cursor.Position.X;
                //uint Y = (uint)System.Windows.Forms.Cursor.Position.Y;

                uint X = (uint)point.X;
                uint Y = (uint)point.Y;

                mouse_event(MOUSEEVENTF_LEFTDOWN, X, Y, 0, 0);
                mouse_event(MOUSEEVENTF_LEFTUP, X, Y, 0, 0);

                System.Threading.Thread.Sleep(sleepTime * 1000);
            }

        }



        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public static implicit operator Point(POINT point)
            {
                return new Point(point.X, point.Y);
            }
        }

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        public static Point GetCursorPosition()
        {
            POINT lpPoint;
            GetCursorPos(out lpPoint);
            return lpPoint;
        }
    }
}
