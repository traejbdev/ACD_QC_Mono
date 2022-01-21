using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using LinuxQCDQC;
using System.Threading;

namespace LinuxQCDQC
{
    public partial class Loading : Form
    {
        public Loading(int max)
        {
            InitializeComponent();
            progressBar1.Minimum = 0;
            progressBar1.Maximum = max;
        }
        private delegate void CloseDelegate();
        private delegate void UpdateDelegate(string txt);

        private Loading _LoadingInstance;

        //The initializer for the Loading Class

        //Private method used to display the splash creen form.
        private void ShowForm(object max)
        {
            _LoadingInstance = new Loading(Convert.ToInt32(max));
            Application.Run(_LoadingInstance);
        }

        //Public method that spawns a new thread and calls the ShowForm() method to lauch the splash screen in the new thread.
        public void ShowLoading(int max)
        {
            if (_LoadingInstance != null)
                return;
            Thread thread = new Thread(ShowForm);
            thread.IsBackground = true;
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start(max);
        }

        //Public bool to check if the splash screen is ready.
        public bool Ready()
        {
            return (_LoadingInstance != null) && (_LoadingInstance.Created);
        }

        //Public method used to update the progress bar progress and text from your main application thread.
        public void UpdateProgress(string txt)
        {
            _LoadingInstance.Invoke(new UpdateDelegate(UpdateProgressInternal), txt);

        }

        //Public method used to close the splash screen from your main application thread.
        public void CloseForm()
        {
            _LoadingInstance.Invoke(new CloseDelegate(CloseFormInternal));
        }

        //The private method invoked by the delegate to update the progress bar.
        private void UpdateProgressInternal(string txt)
        {
            _LoadingInstance.progressBar1.Value++;
            _LoadingInstance.progressBar1.Text = txt;
        }

        //The private method invoked by the delegate to close the splash screen.
        private void CloseFormInternal()
        {
            _LoadingInstance.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Big dog");
        }

        private void Loading_Load(object sender, EventArgs e)
        {
            

            Setup set = new Setup();
            if(set.Check_Tech_Records() == true)
            {
                return;
                
            }
            if (set.Check_Tech_Records() == false)
            {
                load_label.Text = "Loading Setup...";
                if (set.Init_1() == true)
                {
                    load_label.Text = "Tech Record Database Made...";
                    if (set.Init_2() == true)
                    {
                        load_label.Text = "Profile Photo Reference Database Made...";
                        this.Hide();
                        Dash da = new Dash();

                        return;

                    }
                }
                else
                {
                    MessageBox.Show("Install bad");
                }

            }
            else
            {

            }
        }
    }
}
