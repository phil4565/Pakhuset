using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;

namespace Enttec_Test
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            Thread t = new Thread(DataThread);
            t.Start();

            DMX.OpenDMX.setDmxValue(2, 255);
        }
        int manuel = 1;
        int BO = 0;
        int scene1 = 0;
        int scene2 = 0;
        bool scene3 = false;

        private void Main_Load(object sender, EventArgs e)
        {
            try
            {
                DMX.OpenDMX.start();
                if (DMX.OpenDMX.status == FT_STATUS.FT_DEVICE_NOT_FOUND)
                    toolStripStatusLabel1.Text = "No Enttec USB Device Found";
                else if (DMX.OpenDMX.status == FT_STATUS.FT_OK)
                {
                    toolStripStatusLabel1.Text = "Found DMX on USB";
                }
                else
                    toolStripStatusLabel1.Text = "Error Opening Device";
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp);
                toolStripStatusLabel1.Text = "Error Connecting to Enttec USB Device";
            }
        }

        public void BtnScene1_Click(object sender, EventArgs e)
        {
            if (DMX.OpenDMX.status == FT_STATUS.FT_DEVICE_NOT_FOUND)
                toolStripStatusLabel1.Text = "No Enttec USB Device Found";
            else
            {
                toolStripStatusLabel1.Text = "Found DMX on USB";
            }
            DMX.OpenDMX.setDmxValue(1, 0);
            if (manuel == 0)
            {
                if (scene1 == 1)
                {
                    btnScene1.BackColor = Color.LightGray;
                    scene1 = 0;
                }
                else
                {
                    btnScene1.BackColor = Color.Lime;
                    btnScene2.BackColor = Color.LightGray;
                    btnScene3.BackColor = Color.LightGray;
                    scene2 = 0;
                    DMX.OpenDMX.setDmxValue(1, 0);
                    Thread S1 = new Thread(Scene1Thread);
                    S1.Start();
                    scene1 = 1;
                    scene3 = false;
                }
            }
        }

        private void BtnScene2_Click(object sender, EventArgs e)
        {
            if (DMX.OpenDMX.status == FT_STATUS.FT_DEVICE_NOT_FOUND)
                toolStripStatusLabel1.Text = "No Enttec USB Device Found";
            else
                toolStripStatusLabel1.Text = "Found DMX on USB";
            DMX.OpenDMX.setDmxValue(1, 0);
            DMX.OpenDMX.writeData();
            if (manuel == 0)
            {
                if (scene2 == 1)
                {
                    btnScene2.BackColor = Color.LightGray;
                    scene2 = 0;
                }
                else
                {
                    btnScene2.BackColor = Color.Lime;
                    btnScene1.BackColor = Color.LightGray;
                    btnScene3.BackColor = Color.LightGray;
                    scene1 = 0;
                    Thread S2 = new Thread(Scene2Thread);
                    DMX.OpenDMX.setDmxValue(1, 0);
                    S2.Start();
                    scene2 = 1;
                    scene3 = false;
                }
            }
        }

        private void BtnScene3_Click(object sender, EventArgs e)
        {
            if (DMX.OpenDMX.status == FT_STATUS.FT_DEVICE_NOT_FOUND)
                toolStripStatusLabel1.Text = "No Enttec USB Device Found";
            else
                toolStripStatusLabel1.Text = "Found DMX on USB";
            if (scene3 == false)
            {
                scene1 = 0;
                scene2 = 0;
                btnScene3.BackColor = Color.Lime;
                btnScene1.BackColor = Color.LightGray;
                btnScene2.BackColor = Color.LightGray;
                DMX.OpenDMX.setDmxValue(1, 253);
                scene3 = true;
            }
            else
            {
                btnScene3.BackColor = Color.LightGray;
                DMX.OpenDMX.setDmxValue(1, 0);
                scene3 = false;
            }
        }

        private void TBR_Scroll(object sender, EventArgs e)
        {
            if (manuel == 1)
            {
                labelR.Text = Convert.ToString(TBR.Value);
                DMX.OpenDMX.setDmxValue(4, Convert.ToByte(TBR.Value));
                DMX.OpenDMX.setDmxValue(7, Convert.ToByte(TBR.Value));
                DMX.OpenDMX.setDmxValue(10, Convert.ToByte(TBR.Value));
                DMX.OpenDMX.setDmxValue(13, Convert.ToByte(TBR.Value));
            }
        }

        private void TBG_Scroll(object sender, EventArgs e)
        {
            if (manuel == 1)
            {
                labelG.Text = Convert.ToString(TBG.Value);
                DMX.OpenDMX.setDmxValue(5, Convert.ToByte(TBG.Value));
                DMX.OpenDMX.setDmxValue(8, Convert.ToByte(TBG.Value));
                DMX.OpenDMX.setDmxValue(11, Convert.ToByte(TBG.Value));
                DMX.OpenDMX.setDmxValue(14, Convert.ToByte(TBG.Value));
            }
        }

        private void TBB_Scroll(object sender, EventArgs e)
        {
            if (manuel == 1)
            {
                labelB.Text = Convert.ToString(TBB.Value);
                DMX.OpenDMX.setDmxValue(6, Convert.ToByte(TBB.Value));
                DMX.OpenDMX.setDmxValue(9, Convert.ToByte(TBB.Value));
                DMX.OpenDMX.setDmxValue(12, Convert.ToByte(TBB.Value));
                DMX.OpenDMX.setDmxValue(15, Convert.ToByte(TBB.Value));
            }
        }

        public void Manuelbtn_Click(object sender, EventArgs e)
        {
            if (manuel == 1)
            {
                Manuelbtn.BackColor = Color.LightGray;
                manuel = 0;
            }
            else
            {
                manuel = 1;
                btnScene1.BackColor = Color.LightGray;
                btnScene2.BackColor = Color.LightGray;
                btnScene3.BackColor = Color.LightGray;
                scene1 = 0;
                scene2 = 0;
                DMX.OpenDMX.setDmxValue(1, 0);
                Manuelbtn.BackColor = Color.Lime;
            }
        }

        public static void DataThread()
        {
            while (true)
            {
                DMX.OpenDMX.writeData();
                Thread.Sleep(50);
            }
        }

        public void Scene1Thread()
        {
            while (scene1 == 1)
            {
                DMX.OpenDMX.setDmxValue(5, 0);
                DMX.OpenDMX.setDmxValue(8, 0);
                DMX.OpenDMX.setDmxValue(11, 0);
                DMX.OpenDMX.setDmxValue(14, 0);
                DMX.OpenDMX.setDmxValue(6, 0);
                DMX.OpenDMX.setDmxValue(9, 0);
                DMX.OpenDMX.setDmxValue(12, 0);
                DMX.OpenDMX.setDmxValue(15, 0);
                DMX.OpenDMX.setDmxValue(4, 255);
                DMX.OpenDMX.setDmxValue(7, 255);
                DMX.OpenDMX.setDmxValue(10, 255);
                DMX.OpenDMX.setDmxValue(13, 255);
                Thread.Sleep(3000);
                if (scene1 == 0)
                {
                    break;
                }
                DMX.OpenDMX.setDmxValue(4, 0);
                DMX.OpenDMX.setDmxValue(7, 0);
                DMX.OpenDMX.setDmxValue(10, 0);
                DMX.OpenDMX.setDmxValue(13, 0);
                DMX.OpenDMX.setDmxValue(5, 255);
                DMX.OpenDMX.setDmxValue(8, 255);
                DMX.OpenDMX.setDmxValue(11, 255);
                DMX.OpenDMX.setDmxValue(14, 255);
                Thread.Sleep(3000);
                if (scene1 == 0)
                {
                    break;
                }
                DMX.OpenDMX.setDmxValue(5, 0);
                DMX.OpenDMX.setDmxValue(8, 0);
                DMX.OpenDMX.setDmxValue(11, 0);
                DMX.OpenDMX.setDmxValue(14, 0);
                DMX.OpenDMX.setDmxValue(6, 255);
                DMX.OpenDMX.setDmxValue(9, 255);
                DMX.OpenDMX.setDmxValue(12, 255);
                DMX.OpenDMX.setDmxValue(15, 255);
                Thread.Sleep(3000);
                if (scene1 == 0)
                {
                    break;
                }
            }
        }

        public void Scene2Thread()
        {
            byte r = 255;
            DMX.OpenDMX.setDmxValue(4, r);
            DMX.OpenDMX.setDmxValue(7, r);
            DMX.OpenDMX.setDmxValue(10, r);
            DMX.OpenDMX.setDmxValue(13, r);
            while (scene2 == 1)
            {
                DMX.OpenDMX.setDmxValue(5, 0);
                DMX.OpenDMX.setDmxValue(8, 0);
                DMX.OpenDMX.setDmxValue(11, 0);
                DMX.OpenDMX.setDmxValue(14, 0);
                DMX.OpenDMX.setDmxValue(6, 0);
                DMX.OpenDMX.setDmxValue(9, 0);
                DMX.OpenDMX.setDmxValue(12, 0);
                DMX.OpenDMX.setDmxValue(15, 0);
                for (byte b = 255; b > 0; b--)
                {
                    DMX.OpenDMX.setDmxValue(6, b);
                    DMX.OpenDMX.setDmxValue(9, b);
                    DMX.OpenDMX.setDmxValue(12, b);
                    DMX.OpenDMX.setDmxValue(15, b);
                    Thread.Sleep(50);
                    if (scene2 == 0)
                    {
                        break;
                    }
                }
                for (byte g = 0; g < 255; g++)
                {
                    DMX.OpenDMX.setDmxValue(5, g);
                    DMX.OpenDMX.setDmxValue(8, g);
                    DMX.OpenDMX.setDmxValue(11, g);
                    DMX.OpenDMX.setDmxValue(14, g);
                    Thread.Sleep(50);
                    if (scene2 == 0)
                    {
                        break;
                    }
                }

                for (byte b = 0; b < 255; b++)
                {
                    DMX.OpenDMX.setDmxValue(6, b);
                    DMX.OpenDMX.setDmxValue(9, b);
                    DMX.OpenDMX.setDmxValue(12, b);
                    DMX.OpenDMX.setDmxValue(15, b);
                    Thread.Sleep(50);
                    if (scene2 == 0)
                    {
                        break;
                    }
                }
               
                for (r = 255; r > 0; r--)
                {
                    DMX.OpenDMX.setDmxValue(4, r);
                    DMX.OpenDMX.setDmxValue(7, r);
                    DMX.OpenDMX.setDmxValue(10, r);
                    DMX.OpenDMX.setDmxValue(13, r);
                    Thread.Sleep(50);
                    if (scene2 == 0)
                    {
                        break;
                    }
                }
                
                for (byte g = 255; g > 0; g--)
                {
                    DMX.OpenDMX.setDmxValue(5, g);
                    DMX.OpenDMX.setDmxValue(8, g);
                    DMX.OpenDMX.setDmxValue(11, g);
                    DMX.OpenDMX.setDmxValue(14, g);
                    Thread.Sleep(50);
                    if (scene2 == 0)
                    {
                        break;
                    }
                }
                
                for (r = 0; r < 255; r++)
                {
                    DMX.OpenDMX.setDmxValue(4, r);
                    DMX.OpenDMX.setDmxValue(7, r);
                    DMX.OpenDMX.setDmxValue(10, r);
                    DMX.OpenDMX.setDmxValue(13, r);
                    Thread.Sleep(50);
                    if (scene2 == 0)
                    {
                        break;
                    }
                } 
            }
        }

        private void BObtn_Click(object sender, EventArgs e)
        {
            if (BO == 0)
            {
                BObtn.BackColor = Color.Lime;
                if (scene3 == true)
                {
                    DMX.OpenDMX.setDmxValue(1, 0);
                }
                DMX.OpenDMX.setDmxValue(2, 0);
                BO = 1;
            }
            else
            {
                if (scene3 == true)
                {
                    DMX.OpenDMX.setDmxValue(1, 253);
                }
                BObtn.BackColor = Color.LightGray;
                DMX.OpenDMX.setDmxValue(2, 255);
                BO = 0;
            }
        }
    }

    public enum FT_STATUS
    {
        FT_OK = 0,
        FT_INVALID_HANDLE,
        FT_DEVICE_NOT_FOUND,
        FT_DEVICE_NOT_OPENED,
        FT_IO_ERROR,
        FT_INSUFFICIENT_RESOURCES,
        FT_INVALID_PARAMETER,
        FT_INVALID_BAUD_RATE,
        FT_DEVICE_NOT_OPENED_FOR_ERASE,
        FT_DEVICE_NOT_OPENED_FOR_WRITE,
        FT_FAILED_TO_WRITE_DEVICE,
        FT_EEPROM_READ_FAILED,
        FT_EEPROM_WRITE_FAILED,
        FT_EEPROM_ERASE_FAILED,
        FT_EEPROM_NOT_PRESENT,
        FT_EEPROM_NOT_PROGRAMMED,
        FT_INVALID_ARGS,
        FT_OTHER_ERROR
    };
}