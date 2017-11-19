using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MusicSpeech
{
    public partial class TextClassification : Form
    {
        public TextClassification()
        {
            InitializeComponent();
        }


        /// <summary>
        /// //////////////ТЕКСТ
        /// </summary>
        double[,] TrainingSet;
        double[] y = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public static double[] y1;

        string[] song;
        string[] vocabulary;


        public static double[] textAnalys;
        public static double[] text;


        public void forestText()
        {
            //-----------------------------текст из поля ввода--------------------------------

            string VocabularyTxt = "../../../data/vocabulary.txt";
            vocabulary = File.ReadAllLines(VocabularyTxt);
            double[] text = new double[vocabulary.Length];
            Char[] separators = { ' ', ',', '.', '-', '\n' };
            song = richTextBox1.Text.Split(separators);
            int i = 0;
            double[] textAnalys = { 0, 0 };

            foreach (string str in vocabulary)
            {
                foreach (string str1 in song)
                {
                    if (string.Equals(str, str1))
                    {
                        text[i] = text[i] + 1;
                    }
                }
                i++;
            }
            //----------------------------...................................текст из поля ввода


            // Чтение CSV --------------------------------------------------------------------------------------------------
            char separator = ';';
            int flags = 0;

            // Случайный лес решений --------------------------------------------------------------------------------------------------
            var dForest1 = new alglib.decisionforest();
            var rep = new alglib.dfreport();
            int info;

            alglib.read_csv("../../../data/trainText.csv", separator, flags, out TrainingSet);
            // ---------------------------------------------------------------------------------------------------Чтение CSV

            alglib.dfbuildrandomdecisionforestx1(TrainingSet, 40, 2945, 2, 200, 1, 0.9, out info, out dForest1, out rep);


            alglib.dfprocess(dForest1, text, ref textAnalys);

            label1.Text = "Random forest:\n" + "Result: " + info.ToString() + "\n"
                                        + "Веселый : " + textAnalys[0].ToString() + "\n"
                                        + "Грустный : " + textAnalys[1].ToString() + "\n";
            //  + "Романтический : " + textAnalys[2].ToString() + "\n"
            //  + "Философский : " + textAnalys[3].ToString() + "\n"
            // + "Политический : " + textAnalys[4].ToString() + "\n"
            // + "Бессмысленный : " + textAnalys[5].ToString() + "\n";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            forestText();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //-----------------------------Словарь в массив строк--------------------------------
            string VocabularyTxt = "../../../data/vocabulary.txt";
            vocabulary = File.ReadAllLines(VocabularyTxt);
            //----------------------------...................................Словарь в массив строк

            int[] analys = new int[vocabulary.Length];



            //-----------------------------Текст песни в массив строк--------------------------------
            Char[] separators = { ' ', ',', '.', '-', '\n' };
            song = richTextBox1.Text.Split(separators);
            //----------------------------...................................Текст песни в массив строк


            //-----------------------------сравнение текста со словарем--------------------------------
            int i = 0;

            foreach (string str in vocabulary)
            {
                foreach (string str1 in song)
                {
                    if (string.Equals(str, str1))
                    {
                        analys[i] = analys[i] + 1;
                    }
                }
                i++;
            }
            //----------------------------...................................сравнение текста со словарем

            string add = "";

            foreach (int a in analys)
            {
                add = add + a.ToString() + ';';
            }

            File.AppendAllText(@"../../../data/trainText.csv", add + textBox1.Text + "\n");
        }
    }
}
