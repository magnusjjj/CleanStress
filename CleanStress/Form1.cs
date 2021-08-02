using Python.Runtime;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CleanStress
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            int i = 0;
            while (true) { 
                ReloadScript(i++);
            }
        }

        object reloadlock = new object();
        private IntPtr pythonthreadstate;
        private dynamic module;

        public void ReloadScript(int num)
        {

            //PythonEngine.DebugGIL = true;

            lock (reloadlock)
            {

                if (PythonEngine.IsInitialized) // Only run this code if python has actually started.
                {
                    PythonEngine.EndAllowThreads(pythonthreadstate);
                    PythonEngine.Shutdown();
                }

                Environment.SetEnvironmentVariable("PYTHONNET_PYDLL", "python38.dll");

                PythonEngine.Initialize();


                using (Py.GIL()) // The GIL is the Global Interpreter Lock. This is so that we don't touch python while it's running.
                {
                    dynamic sys = Py.Import("sys");
                    module = Py.Import("testscript");
                    module.start(num);
                }

                pythonthreadstate = PythonEngine.BeginAllowThreads();
            }
        }
    }
}
