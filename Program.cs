using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinuxQCDQC
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Instantiate a new instance of SplashScreen
            Loading splash = new Loading(4);
            //Display the splash screen with a max value for the progress bar
            splash.ShowLoading(4);

            //Wait for the form to be loaded.
            while (!splash.Ready())
            {
                //Loop to wait for the splash screen to be ready before trying to
            }

            //Update the progress bar
            splash.UpdateProgress("Loading Step 1");
            //Perform action
            Thread.Sleep(1000); //Lets pretend this is doing something constructive
                                //Update the progress bar
            splash.UpdateProgress("Loading Step 2");
            //Perform action
            Thread.Sleep(1000); //Lets pretend this is doing something constructive
                                //Update the progress bar
            splash.UpdateProgress("Loading Step 3");
            //Perform action
            Thread.Sleep(1000); //Lets pretend this is doing something constructive
                                //Update the progress bar
            splash.UpdateProgress("Loading Step 4");
            //Perform action
            Thread.Sleep(1000); //Lets pretend this is doing something constructive
                                //Close the Splash Screen
            splash.CloseForm();

            //Launch your main application form

            Application.Run(new Dash());

        }
    }
}
