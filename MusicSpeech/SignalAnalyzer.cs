using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections; //для работы с ArrayList
using Un4seen.Bass; //для работы с Bass.Net
using Un4seen.Bass.Misc; //для работы с ArrayList.Net
using System.IO;
using System.Numerics;
using MathNet.Numerics.IntegralTransforms;
using MathNet.Numerics.Signals;
using WaveletLibrary;


namespace MusicSpeech
{
    public partial class SignalAnalyzer : Form
    {
        public SignalAnalyzer()
        {
            InitializeComponent();
        }

        private void SignalAnalyzer_Load(object sender, EventArgs e)
        {
            //------------------------------------------------Инициализация Bass.Net-----------------------------------------------------------------------
           // initDefaultDevice = Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
            //Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
        }



        OpenFileDialog OpenedFile = new OpenFileDialog();//выбираем файл

        private void button1_Click(object sender, EventArgs e)
        {
            //-----------------------------------------------Открытие файла-----------------------------------------------------------------------
            OpenedFile.Filter = "wav Files|*.wav";//Фильтр - только wav файлы
            OpenedFile.ShowDialog();
            label1.ForeColor = Color.Green;
            label1.Text = OpenedFile.FileName;
        }


        bool initDefaultDevice = false;
        private int stream;

        private void button2_Click_1(object sender, EventArgs e)
        {
            //------------------------------------------------Воспроизведение-----------------------------------------------------------------------
            if (OpenedFile.FileName != "")
            {
                if (initDefaultDevice && Bass.BASS_ChannelIsActive(stream) == BASSActive.BASS_ACTIVE_STOPPED)
                {
                    stream = Bass.BASS_StreamCreateFile(OpenedFile.FileName, 0, 0, BASSFlag.BASS_DEFAULT);
                    if (stream != 0)
                    {
                       // Bass.BASS_ChannelPlay(stream, true);
                    }
                } 
            }
            else
            {
                MessageBox.Show("Файл не выбран");
            }         
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //------------------------------------------------Стоп-----------------------------------------------------------------------
            Bass.BASS_ChannelStop(stream);
        }


        //------------------------------------------------Получить амплитуды сигнала-----------------------------------------------------------------------
        byte[] buffer = new byte[4];
        ArrayList Amp = new ArrayList();
        int volume = 0;//Будем использовать для хранения каждого 2400 сэмпла
        int currentVolume = 0;//Вспомогательный элемент
        
        int frames; 
       
        public void amplitudes()
        {

            Amp.Clear();//очищаем массив от предыдущих данных

            if (OpenedFile.FileName != "")//Если выбран какой-нибудь файл
            {
                FileStream fs = new FileStream(OpenedFile.FileName, FileMode.Open, FileAccess.Read);
                fs.Seek(44, SeekOrigin.Begin);//Перематываем 44 байта от начала файла - заголовок
                bool end = false;
                while (!end)
                {
                    for (int i = 0; i < 48000 / 2048; i++)
                    {//Берём каждый 2400 сэмпл, или 20 сэмплов в секунду для файла с качеством
                        //звука 48000
                        if (fs.Read(buffer, 0, 4) == 4)//Считываем 4 байта в buffer
                        {//если кол-во считанных байт меньше 4, значит, найден конец файла, заканчиваем чтение
                            currentVolume = BitConverter.ToInt16(buffer, 0);
                            if (volume < currentVolume)//Из 2400 берём самое большое значение,
                                volume = currentVolume;//чтобы случайно не пропустить пик
                        }
                        else
                        {
                            end = true;
                            break;
                        }
                    }
                    Amp.Add(volume);//Добавляем в массив
                    volume = 0;//Обнуляем
                }

            }
        }


        private bool isPowerOfTwo(int x) /* Является ли степенью двойки */
        {
            while (((x % 2) == 0) && x > 1)
                x /= 2;
            return (x == 1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (OpenedFile.FileName != "")
            {
                amplitudes();
                Amp.RemoveAt(0);

                label2.Text = "Семплов: " + Amp.Count.ToString();

                if (!(isPowerOfTwo(Amp.Count))) //Дополним массив до степени двойки
                {
                    do
                    {
                        Amp.Add(0);
                    }
                    while (!(isPowerOfTwo(Amp.Count)));
                }

                frames = Amp.Count / 512; //Число фреймов

                label3.Text = "Ближайшая степень двойки: " + Amp.Count.ToString();
                label4.Text = "Фреймов: " + frames.ToString();

                pictureBox1.Width = Amp.Count;//подгоняем ширину графика под кол-во данных
                Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);//создаём в оперативной памяти новый рисунок
                Graphics gr = Graphics.FromImage(bmp);

                Pen RedPen = new Pen(Color.IndianRed, 1);
                Pen IndigoPen = new Pen(Color.Indigo, 1);

                for (int i = 0; i < Amp.Count; i++)
                {
                    gr.DrawLine(RedPen, i, bmp.Height, i, bmp.Height - bmp.Height * Convert.ToInt32(Amp[i]) / 20000);
                }
                for (int i = 0; i < Amp.Count; i = i + 512)
                {
                    gr.DrawLine(IndigoPen, new Point(i, 0), new Point(i, 100));
                }


                pictureBox1.Image = bmp;
            }
            else
            {
                MessageBox.Show("Файл не выбран");
            }
        }

        //------------------------------------------------Очистка и нормализация-----------------------------------------------------------------------
        private void button5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Amp.Count; i++)
            {
                if (Convert.ToInt32(Amp[i]) < 20000000) { Amp.RemoveAt(i); }
            }

            if (!(isPowerOfTwo(Amp.Count))) //Дополним массив до степени двойки
            {
                do
                {
                    Amp.Add(0);
                }
                while (!(isPowerOfTwo(Amp.Count)));
            }

            frames = Amp.Count / 512; //Число фреймов

            label3.Text = "Ближайшая степень двойки: " + Amp.Count.ToString();
            label4.Text = "Фреймов: " + frames.ToString();
            //---------------Очистка и нормализация----------------------------------

            double[] abs_wav_buf = new double[Amp.Count];

            for (int i = 0; i < Amp.Count; i++)
                if (Convert.ToInt32(Amp[i]) < 0) abs_wav_buf[i] = -Convert.ToInt32(Amp[i]);   //приводим все значения амплитуд к абсолютной величине 
                else abs_wav_buf[i] = Convert.ToInt32(Amp[i]);                    //для определения максимального пика

            double max = -10000;

            for (int i = 0; i < abs_wav_buf.Length; i++)
            {

                if (abs_wav_buf[i] > max)
                {
                    // найден больший элемент
                    max = abs_wav_buf[i];
                }
            }

            double k = 0.0001 * max;        //получаем коэффициент нормализации      

            for (int i = 0; i < Amp.Count; i++)    //записываем нормализованные значения в исходный массив амплитуд
            {
                Amp[i] = Convert.ToInt32(Amp[i]) * k;
            }

            pictureBox2.Width = Amp.Count;//подгоняем ширину графика под кол-во данных
            Bitmap bmp = new Bitmap(pictureBox2.Width, pictureBox2.Height);//создаём в оперативной памяти новый рисунок
            Graphics gr = Graphics.FromImage(bmp);

            Pen VioletPen = new Pen(Color.BlueViolet, 1);
            Pen RedPen = new Pen(Color.Red, 1);

            for (int i = 0; i < Amp.Count; i++)
            {
                gr.DrawLine(VioletPen, i, bmp.Height, i, bmp.Height - bmp.Height * Convert.ToInt32(Amp[i]) / 20000);
            }
            for (int i = 0; i < Amp.Count; i = i + 256)
            {
                gr.DrawLine(RedPen, new Point(i, 0), new Point(i, 200));
            }

            pictureBox2.Image = bmp;
        }

        //------------------------------------------------Преобразование Фурье-----------------------------------------------------------------------
        double[,] FFT_mass_1;
        ArrayList FFT_mass = new ArrayList();
        ArrayList FFT_mass_Window = new ArrayList();
       
        private double[,] set_frames(double[,] frame_mass)
        {
            int k = 0;
            for (int i = 0; i < frames; i++)
            { //Деление на неперекрывающиеся фреймы
                for (int j = 0; j < 512; j++)
                {
                    frame_mass[i, j] = Convert.ToInt32(Amp[k]);
                    k++;
                }
            }
            return frame_mass;
        }


        private void button6_Click(object sender, EventArgs e)
        {
            //---------------Фурье----------------------------------
            FFT_mass.Clear();
            FFT_mass_Window.Clear();

            double[,] frame_mass = new double[frames, 512];  //массив всех фреймов по 512 

            set_frames(frame_mass);

            //Hamming_window(frame_mass, frames);

            FFT_mass_1 = new double[frames, 512];

            Complex[] fft_1 = new Complex[512];

            for (int i = 0; i < frames; i++)
            {
                for (int j = 0; j < 512; j++) fft_1[j] = frame_mass[i, j];

                Transform.FourierForward(fft_1, FourierOptions.Matlab);

                for (int j = 0; j < 256; j++)
                {
                    FFT_mass.Add(fft_1[j].Magnitude);
                }

                for (int j = 0; j < 512; j++)
                {
                    FFT_mass_1[i, j] = fft_1[j].Magnitude;
                }
            }

            pictureBox3.Width = FFT_mass.Count;//подгоняем ширину графика под кол-во данных
            Bitmap bmp = new Bitmap(pictureBox3.Width, pictureBox3.Height);//создаём в оперативной памяти новый рисунок
            Graphics gr = Graphics.FromImage(bmp);

            Pen AquaPen = new Pen(Color.Aqua, 1);
            Pen RedPen = new Pen(Color.Red, 1);

            for (int i = 0; i < FFT_mass.Count; i++)
            {
                gr.DrawLine(AquaPen, i, bmp.Height, i, bmp.Height - bmp.Height * Convert.ToInt32(FFT_mass[i]) / 200000);
            }
            for (int i = 0; i < FFT_mass.Count; i = i + 256)
            {
                gr.DrawLine(RedPen, new Point(i, 0), new Point(i, 200));
            }

            pictureBox3.Image = bmp;
        }


        private void Hamming_window(double[,] frames, int count_frames)
        {
            double omega = 2.0 * Math.PI / (2048f);
            for (int i = 0; i < count_frames; i++)
                for (int j = 0; j < count_frames; j++)
                    frames[i, j] = (0.54 - 0.46 * Math.Cos(omega * (j))) * frames[i, j];
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //---------------Фурье----------------------------------
            FFT_mass_Window.Clear();

            double[,] frame_mass = new double[frames, 512];  //массив всех фреймов по 512 

            set_frames(frame_mass);

            FFT_mass_1 = new double[frames, 512];

            Complex[] fft_1 = new Complex[512];

            for (int i = 0; i < frames; i++)
            {
                for (int j = 0; j < 512; j++) fft_1[j] = frame_mass[i, j];

                Transform.FourierForward(fft_1, FourierOptions.Matlab);

                for (int j = 0; j < 512; j++)
                {
                    FFT_mass_1[i, j] = fft_1[j].Magnitude;
                }
            }

            Hamming_window(FFT_mass_1, frames);

            for (int i = 0; i < frames; i++)
            {
                for (int j = 0; j < 512; j++)
                {
                    FFT_mass_Window.Add(FFT_mass_1[i, j]);
                }
            }

            pictureBox3.Width = FFT_mass_Window.Count;//подгоняем ширину графика под кол-во данных
            Bitmap bmp = new Bitmap(pictureBox3.Width, pictureBox3.Height);//создаём в оперативной памяти новый рисунок
            Graphics gr = Graphics.FromImage(bmp);

            Pen AquaPen = new Pen(Color.Aqua, 1);
            Pen RedPen = new Pen(Color.Red, 1);

            for (int i = 0; i < FFT_mass_Window.Count; i++)
            {
                gr.DrawLine(AquaPen, i, bmp.Height, i, bmp.Height - bmp.Height * Convert.ToInt32(FFT_mass_Window[i]) / 200000);
            }
            for (int i = 0; i < FFT_mass_Window.Count; i = i + 256)
            {
                gr.DrawLine(RedPen, new Point(i, 0), new Point(i, 200));
            }

            pictureBox3.Image = bmp;
        }


        //------------------------------------------------Вейвлет-----------------------------------------------------------------------
        private void button7_Click(object sender, EventArgs e)
        {
            //---------------Вейвлет----------------------------------

            double[,] frame_mass = new double[frames, 512];  //массив всех фреймов по 512 

            set_frames(frame_mass);
            Hamming_window(frame_mass, frames);

            var dataMatrix = new Matrix(frame_mass);
            var transform = new WaveletTransform(new HaarLift(), 2);

            dataMatrix = transform.DoForward(dataMatrix);


            string wavelet_str = dataMatrix.ToString();

            double[] wavelet_mass = wavelet_str.Split(new char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(n => double.Parse(n)).ToArray();


            pictureBox4.Width = wavelet_mass.Length;//подгоняем ширину графика под кол-во данных
            Bitmap bmp = new Bitmap(pictureBox4.Width, pictureBox4.Height);//создаём в оперативной памяти новый рисунок
            Graphics gr = Graphics.FromImage(bmp);

            Pen VioletPen = new Pen(Color.BlueViolet, 1);
            Pen RedPen = new Pen(Color.Red, 1);

            for (int i = 0; i < wavelet_mass.Length; i++)
            {
                gr.DrawLine(VioletPen, i, bmp.Height, i, bmp.Height - bmp.Height * Convert.ToInt32(wavelet_mass[i]) / 10000);
            }
            for (int i = 0; i < wavelet_mass.Length; i = i + 256)
            {
                gr.DrawLine(RedPen, new Point(i, 0), new Point(i, 200));
            }


            pictureBox4.Image = bmp;
        }


        //------------------------------------------------Мощность сигнала-----------------------------------------------------------------------
        private void button9_Click(object sender, EventArgs e)
        {
            double w = 0;

            for (int i = 0; i < FFT_mass.Count; i++)
            {
                w = w + Math.Pow(Math.Abs(Convert.ToInt32(FFT_mass[i])), 2);
            }
            label5.Text = "Средняя мощность: " + w.ToString();
        }

        //------------------------------------------------Кепстр-----------------------------------------------------------------------
        double[] Cepstral;

        private void button10_Click(object sender, EventArgs e)
        {
            Cepstral = new double[frames];

            double[] Cepstral_mass = new double[frames];         //массив наборов для каждого фрейма

            for (int i = 0; i < frames; i++)
            {
                for (int j = 0; j < 512; j++)
                {
                    Cepstral_mass[i] += FFT_mass_1[i, j];
                }

            }

            for (int i = 0; i < frames; i++)
            {
                if (Cepstral_mass[i] != 0) Cepstral_mass[i] = Math.Log(Cepstral_mass[i], Math.E);

            }


            MessageBox.Show("Cepstral: " + Cepstral_mass[0] + " " + Cepstral_mass[1] + " " + Cepstral_mass[2] + " " + Cepstral_mass[3] + " " + Cepstral_mass[4]);
        }


        //------------------------------------------------MFCC-----------------------------------------------------------------------
        int[] filter_points = {6,18,31,46,63,82,103,127,154,184,218,
                              257,299,348,402,463,531,608,695,792,901,1023};//массив опорных точек для фильтрации спекрта фрейма
        double[,] H = new double[20, 1024];     //массив из 20-ти фильтров для каждого MFCC

        double[] MFCC = new double[20];     //массив MFCC для данной речевой выборки   <<<<<<<<<<<<<<<<<<<<

        private void button11_Click(object sender, EventArgs e)
        {
            double[,] MFCC_mass = new double[frames, 20];         //массив наборов MFCC для каждого фрейма

            //***********   Расчет гребенчатых фильтров спектра:    *************
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 256; j++)
                {
                    if (j < filter_points[i]) H[i, j] = 0;
                    if ((filter_points[i] <= j) & (j <= filter_points[i + 1]))
                        H[i, j] = ((double)(j - filter_points[i]) / (filter_points[i + 1] - filter_points[i]));
                    if ((filter_points[i + 1] <= j) & (j <= filter_points[i + 2]))
                        H[i, j] = ((double)(filter_points[i + 2] - j) / (filter_points[i + 2] - filter_points[i + 1]));
                    if (j > filter_points[i + 2]) H[i, j] = 0;
                }

            for (int k = 0; k < frames; k++)
            {
                //**********    Применение фильтров и логарифмирование энергии спектра для каждого фрейма   ***********
                double[] S = new double[20];

                for (int i = 0; i < 20; i++)
                {
                    for (int j = 0; j < 512; j++)
                    {
                        S[i] += Math.Pow(FFT_mass_1[k, j], 2) * H[i, j];
                    }
                    if (S[i] != 0) S[i] = Math.Log(S[i], Math.E);

                }

                //**********    DCT и массив MFCC для каждого фрейма на выходе     ***********
                for (int l = 0; l < 20; l++)
                    for (int i = 0; i < 20; i++) MFCC_mass[k, l] += S[i] * Math.Cos(Math.PI * l * ((i * 0.5) / 20));
            }


            //***********   Рассчет конечных MFCC для всей речевой выборки    ***********       
            for (int i = 0; i < 20; i++)
            {
                for (int k = 0; k < frames; k++) MFCC[i] += MFCC_mass[k, i];
                MFCC[i] = MFCC[i] / frames;
            }

            //using (System.IO.StreamWriter file =
            //    new System.IO.StreamWriter(@"..\..\mfcc", true))
            //{
            //    file.WriteLine(MFCC[0] + " " + MFCC[1] + " " + MFCC[2] + " " + MFCC[3] + " " + MFCC[4] + " " + MFCC[5]
            //    + " " + MFCC[6] + " " + MFCC[7] + " " + MFCC[8] + " " + MFCC[9] + " " + MFCC[10]
            //    + " " + MFCC[11] + " " + MFCC[12] + " " + MFCC[13] + " " + MFCC[14] + " " + MFCC[15]
            //    + " " + MFCC[16] + " " + MFCC[17] + " " + MFCC[18] + " " + MFCC[19]);
            //}

            MessageBox.Show("MFCC: " + MFCC[0] + " " + MFCC[1] + " " + MFCC[2] + " " + MFCC[3] + " " + MFCC[4] + " " + MFCC[5]
                + " " + MFCC[6] + " " + MFCC[7] + " " + MFCC[8] + " " + MFCC[9] + " " + MFCC[10]
                + " " + MFCC[11] + " " + MFCC[12] + " " + MFCC[13] + " " + MFCC[14] + " " + MFCC[15]
                + " " + MFCC[16] + " " + MFCC[17] + " " + MFCC[18] + " " + MFCC[19]);
        }

    }
    
}
