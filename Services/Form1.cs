using System;
using System.ServiceProcess;
using System.Windows.Forms;

namespace Services
{
    public partial class Form1 : Form
    {
        private CheckBox chkbox = new System.Windows.Forms.CheckBox();

        public Form1()
        {
            InitializeComponent();
        }
        private void Chk_onClick(object sender, System.EventArgs e)
        {
            CheckBox CurrentChkBox = (CheckBox)sender;
            using (ServiceController service = new ServiceController(CurrentChkBox.Name))
            {
                switch (service.Status)
                {
                    case ServiceControllerStatus.Running:
                        service.Stop();
                        service.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(0, 0, 20));
                        break;
                    case ServiceControllerStatus.Stopped:
                        service.Start();
                        service.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(0, 0, 20));
                        break;
                }
            }


            foreach (CheckBox control in flowLayoutPanel1.Controls)
            {
                ServiceController service = new ServiceController(control.Name);
               // CheckBox chk = (CheckBox)sender;

                switch (service.Status)
                {
                    case ServiceControllerStatus.Running:
                        control.Text = control.Name + " Running";
                        control.Checked = true;
                        break;
                    case ServiceControllerStatus.Stopped:
                        control.Text = control.Name + " Stopped";
                        control.Checked = false;
                        break;
                }

            }
        }


        private void AddItem(object sender, System.EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            foreach (string el in Program.gList)
            {
                CheckBox chkbox = new System.Windows.Forms.CheckBox
                {
                    Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0),
                    Name = el,

                    AutoSize = true
                };
                ServiceController sc = new ServiceController(el);
                switch (sc.Status)
                {
                    case ServiceControllerStatus.Running:
                        chkbox.Text = el + " Running";
                        chkbox.Checked = true;
                        break;
                    case ServiceControllerStatus.Stopped:
                        chkbox.Text = el + " Stopped";
                        chkbox.Checked = false;
                        break;
                    case ServiceControllerStatus.Paused:
                        chkbox.Text = el + " Paused";
                        break;
                    case ServiceControllerStatus.StopPending:
                        chkbox.Text = el + " Stopping";
                        break;
                    case ServiceControllerStatus.StartPending:
                        chkbox.Text = el + " Starting";
                        break;
                    default:
                        chkbox.Text = el + " Status Changing";
                        break;
                }
                chkbox.Click += new System.EventHandler(Chk_onClick);
                flowLayoutPanel1.Controls.Add(chkbox);
            }
        }
    }
    //this.checkBox1 = new System.Windows.Forms.CheckBox();
}
