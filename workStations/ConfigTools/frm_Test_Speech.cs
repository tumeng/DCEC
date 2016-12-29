using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Rmes.Public;

namespace Rmes.Workstation.ConfigTools
{
    public partial class frm_Test_Speech : Form
    {
        private RSpeechLib speech;
        public frm_Test_Speech()
        {
            InitializeComponent();
            speech = new RSpeechLib();
            comboBox1.DataSource = speech.InstalledVoices;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            if (comboBox1.Items.Count > 0)
            {
                if (comboBox1.Items.Contains(speech.Voice))
                    comboBox1.SelectedItem = speech.Voice;
                else
                    comboBox1.SelectedIndex = 0; 
            }
            comboBox1.SelectedIndexChanged += new EventHandler(comboBox1_SelectedIndexChanged);

            numericUpDown1.Maximum = 100;
            numericUpDown1.Minimum = 0;
            numericUpDown1.Increment = 1;
            numericUpDown1.Value = speech.Volume;
            numericUpDown1.ValueChanged += new EventHandler(numericUpDown1_ValueChanged);

            numericUpDown2.Maximum = 10;
            numericUpDown2.Minimum = -10;
            numericUpDown2.Increment = 1;
            numericUpDown2.Value = speech.Rate;
            numericUpDown2.ValueChanged += new EventHandler(numericUpDown2_ValueChanged);

            button1.Click += new EventHandler(button1_Click);
        }

        void button1_Click(object sender, EventArgs e)
        {
            string val = textBox1.Text;
            speech.Speak(val);
            //throw new NotImplementedException();
        }

        void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex < 0) return;
            speech.Voice = comboBox1.SelectedItem.ToString();
            //throw new NotImplementedException();
        }

        void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            speech.Rate = (int)numericUpDown2.Value;
            //throw new NotImplementedException();
        }

        void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            speech.Volume = (int)numericUpDown1.Value;
            //throw new NotImplementedException();
        }

        private void frm_Test_Speech_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((MDIParent1)this.MdiParent).测试SpeechLibToolStripMenuItem.Enabled = true;
        }
    }
}
