using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Services
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        public static List<string> gList  = new List<string>(); 

        
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            try
            {
                string[] readText = File.ReadAllLines(@"services.ini");
                    foreach (string s in readText)
                    {
                    gList.Add(s);
                    }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex);
            }
            // Suspend the screen.  
            Application.Run(new Form1());
        }
    }
}
