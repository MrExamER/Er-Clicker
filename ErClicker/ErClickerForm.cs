using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.ComponentModel;
//using System.Media;


namespace ErClicker
{
    public partial class ErClickerForm : Form
    {
        //DllImport for clicker event
        [DllImport("user32.dll")]
        static extern void mouse_event(int examleftright, int RCM, int PRC, int PLC, int examupdown);
        //DllImport for hotkey
        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(Keys vKey);
        //some cmd
        int cmdec;
        bool start = true;
        int interval;
        public ErClickerForm()
        {
            InitializeComponent();
        }
        //Right click
        public void Rightclick(Point RC)
        {
            mouse_event(0x0008, RC.X, RC.Y, 0, 0);
            mouse_event(0x0010, RC.X, RC.Y, 0, 0);
        }
        //Left click
        public void Leftclick(Point LC)
        {
            mouse_event(0x0002, LC.X, LC.Y, 0, 0);
            mouse_event(0x0004, LC.X, LC.Y, 0, 0);
        }
        // send true/flase
        public int sendyesorno()
        {
            if (ExamErClickerRight.Checked == true)
            {
                cmdec = 0;
            }
            else if (ExamErClickerLeft.Checked == true)
            {
                cmdec = 1;
            }
            else
            {
                cmdec = 2;
            }
            // just back if 0
            return cmdec;

        }
        // do nothing
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            TopMost = false;
        }
        //over all app
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            TopMost = true;
        }
        //About ErClicker
        private void button3_Click(object sender, EventArgs e)
        {     
            MessageBox.Show("Er-Clicker v1.0 | 2022" +
    "\nit auto clicker for silkroad pvp" +
    "\nUP Stats/silk/mastery so fast, You can use it for other games that's needed more then one click!" +
    "\nFor more or update visit [ mrexamer.blogspot.com ]" +
    "\nSupport us with a word of thanks." +
    "\nMr Exam ♥", "About ER-Clicker", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        // exit
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            //nothing
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            //nothing
        }
        //link I
        private void exambloglink_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://mrexamer.blogspot.com/2023/04/ER-Clicker.html");
        }
        //link II
        private void examchlink_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://youtu.be/gd6vE4yAnxI");
        }
        //on start + background active
        private void ErClickerForm_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            Thread EC = new Thread(ErClicker);
            backgroundWorker2.RunWorkerAsync();
            EC.Start();
        }
        //rest ZzZzZz CPU
        private void ErClicker()
        {
            Thread.Sleep(2);
        }
        // timer
        private void timer1_Tick(object sender, EventArgs e)
        {
            //RC
            if (cmdec == 0)
            {
                Rightclick(new Point(MousePosition.X, MousePosition.Y));
            }
            //LC
            else
            {
                Leftclick(new Point(MousePosition.X, MousePosition.Y));
            }

        }
        // on close
        private void ErClickerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you wanna Exit ?", "Exit!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
            Thread.Sleep(1);
        }
        // background "hoykey" it MButton = mouse scroll button you can change it as you want
        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (GetAsyncKeyState(Keys.MButton) < 0)
                {
                    if (sendyesorno() == 2)
                    {
                        MessageBox.Show("Choose Left/Right click then start!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        if (numericUpDown1.Value == 0)
                        {
                            MessageBox.Show("Set the time mate!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            interval = (int)numericUpDown1.Value;
                            interval *= 1;
                            timer1.Interval = interval;
                            start = true;
                            timer1_Tick(sender, e);
                            if (!start)
                            {
                                timer1.Start();
                            }
                            Thread.Sleep(1);
                        }
                        Thread.Sleep(interval);
                    }
                    Thread.Sleep(1);
                }Thread.Sleep(1);
            }//END
        }
    }
}


