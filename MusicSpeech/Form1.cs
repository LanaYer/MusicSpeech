using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MusicSpeech
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MakeDataRetrieval MakeDataRetrievalForm = new MakeDataRetrieval();
            MakeDataRetrievalForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SignalClassification SignalClassificationForm = new SignalClassification();
            SignalClassificationForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TextClassification TextClassificationForm = new TextClassification();
            TextClassificationForm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SignalAnalyzer SignalAnalyzerForm = new SignalAnalyzer();
            SignalAnalyzerForm.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
