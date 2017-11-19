using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;


namespace MusicSpeech
{
    public partial class SignalClassification : Form
    {
        // JSON -------------------------------------------------------------------------------------------------
        public class MyJson
        {
            public lowlevel lowlevel { get; set; }
            public metadata metadata { get; set; }
            public rhythm rhythm { get; set; }
            public tonal tonal { get; set; }
        }

        public class lowlevel
        {
            public double average_loudness { get; set; }

            public barkbands_crest barkbands_crest { get; set; }
            public barkbands_flatness_db barkbands_flatness_db { get; set; }
            public barkbands_kurtosis barkbands_kurtosis { get; set; }
            public barkbands_skewness barkbands_skewness { get; set; }
            public barkbands_spread barkbands_spread { get; set; }
            public dissonance dissonance { get; set; }

            public double dynamic_complexity { get; set; }

            public erbbands_crest erbbands_crest { get; set; }
            public erbbands_flatness_db erbbands_flatness_db { get; set; }
            public erbbands_kurtosis erbbands_kurtosis { get; set; }
            public erbbands_skewness erbbands_skewness { get; set; }
            public erbbands_spread erbbands_spread { get; set; }
            public hfc hfc { get; set; }
            public melbands_crest melbands_crest { get; set; }
            public melbands_flatness_db melbands_flatness_db { get; set; }
            public melbands_kurtosis melbands_kurtosis { get; set; }
            public melbands_skewness melbands_skewness { get; set; }
            public melbands_spread melbands_spread { get; set; }
            public pitch_salience pitch_salience { get; set; }
            public silence_rate_20dB silence_rate_20dB { get; set; }
            public silence_rate_30dB silence_rate_30dB { get; set; }
            public silence_rate_60dB silence_rate_60dB { get; set; }
            public spectral_centroid spectral_centroid { get; set; }
            public spectral_complexity spectral_complexity { get; set; }
            public spectral_decrease spectral_decrease { get; set; }
            public spectral_energy spectral_energy { get; set; }
            public spectral_energyband_high spectral_energyband_high { get; set; }
            public spectral_energyband_low spectral_energyband_low { get; set; }
            public spectral_energyband_middle_high spectral_energyband_middle_high { get; set; }
            public spectral_energyband_middle_low spectral_energyband_middle_low { get; set; }
            public spectral_entropy spectral_entropy { get; set; }
            public spectral_flux spectral_flux { get; set; }
            public spectral_kurtosis spectral_kurtosis { get; set; }
            public spectral_rms spectral_rms { get; set; }
            public spectral_rolloff spectral_rolloff { get; set; }
            public spectral_skewness spectral_skewness { get; set; }
            public spectral_spread spectral_spread { get; set; }
            public spectral_strongpeak spectral_strongpeak { get; set; }
            public zerocrossingrate zerocrossingrate { get; set; }
        }


        public class barkbands_crest { public double max { get; set; } public double mean { get; set; } public double median { get; set; } public double min { get; set; } public double var { get; set; } }
        public class barkbands_flatness_db { public double max { get; set; } public double mean { get; set; } public double median { get; set; } public double min { get; set; } public double var { get; set; } }
        public class barkbands_kurtosis { public double max { get; set; } public double mean { get; set; } public double median { get; set; } public double min { get; set; } public double var { get; set; } }
        public class barkbands_skewness { public double max { get; set; } public double mean { get; set; } public double median { get; set; } public double min { get; set; } public double var { get; set; } }
        public class barkbands_spread { public double max { get; set; } public double mean { get; set; } public double median { get; set; } public double min { get; set; } public double var { get; set; } }
        public class dissonance { public double max { get; set; } public double mean { get; set; } public double median { get; set; } public double min { get; set; } public double var { get; set; } }
        public class erbbands_crest { public double max { get; set; } public double mean { get; set; } public double median { get; set; } public double min { get; set; } public double var { get; set; } }
        public class erbbands_flatness_db { public double max { get; set; } public double mean { get; set; } public double median { get; set; } public double min { get; set; } public double var { get; set; } }
        public class erbbands_kurtosis { public double max { get; set; } public double mean { get; set; } public double median { get; set; } public double min { get; set; } public double var { get; set; } }
        public class erbbands_skewness { public double max { get; set; } public double mean { get; set; } public double median { get; set; } public double min { get; set; } public double var { get; set; } }
        public class erbbands_spread { public double max { get; set; } public double mean { get; set; } public double median { get; set; } public double min { get; set; } public double var { get; set; } }
        public class hfc { public double max { get; set; } public double mean { get; set; } public double median { get; set; } public double min { get; set; } public double var { get; set; } }
        public class melbands_crest { public double max { get; set; } public double mean { get; set; } public double median { get; set; } public double min { get; set; } public double var { get; set; } }
        public class melbands_flatness_db { public double max { get; set; } public double mean { get; set; } public double median { get; set; } public double min { get; set; } public double var { get; set; } }
        public class melbands_kurtosis { public double max { get; set; } public double mean { get; set; } public double median { get; set; } public double min { get; set; } public double var { get; set; } }
        public class melbands_skewness { public double max { get; set; } public double mean { get; set; } public double median { get; set; } public double min { get; set; } public double var { get; set; } }
        public class melbands_spread { public double max { get; set; } public double mean { get; set; } public double median { get; set; } public double min { get; set; } public double var { get; set; } }
        public class pitch_salience { public double max { get; set; } public double mean { get; set; } public double median { get; set; } public double min { get; set; } public double var { get; set; } }
        public class silence_rate_20dB { public double max { get; set; } public double mean { get; set; } public double median { get; set; } public double min { get; set; } public double var { get; set; } }
        public class silence_rate_30dB { public double max { get; set; } public double mean { get; set; } public double median { get; set; } public double min { get; set; } public double var { get; set; } }
        public class silence_rate_60dB { public double max { get; set; } public double mean { get; set; } public double median { get; set; } public double min { get; set; } public double var { get; set; } }
        public class spectral_centroid { public double max { get; set; } public double mean { get; set; } public double median { get; set; } public double min { get; set; } public double var { get; set; } }
        public class spectral_complexity { public double max { get; set; } public double mean { get; set; } public double median { get; set; } public double min { get; set; } public double var { get; set; } }
        public class spectral_decrease { public double max { get; set; } public double mean { get; set; } public double median { get; set; } public double min { get; set; } public double var { get; set; } }
        public class spectral_energy { public double max { get; set; } public double mean { get; set; } public double median { get; set; } public double min { get; set; } public double var { get; set; } }
        public class spectral_energyband_high { public double max { get; set; } public double mean { get; set; } public double median { get; set; } public double min { get; set; } public double var { get; set; } }
        public class spectral_energyband_low { public double max { get; set; } public double mean { get; set; } public double median { get; set; } public double min { get; set; } public double var { get; set; } }
        public class spectral_energyband_middle_high { public double max { get; set; } public double mean { get; set; } public double median { get; set; } public double min { get; set; } public double var { get; set; } }
        public class spectral_energyband_middle_low { public double max { get; set; } public double mean { get; set; } public double median { get; set; } public double min { get; set; } public double var { get; set; } }
        public class spectral_entropy { public double max { get; set; } public double mean { get; set; } public double median { get; set; } public double min { get; set; } public double var { get; set; } }
        public class spectral_flux { public double max { get; set; } public double mean { get; set; } public double median { get; set; } public double min { get; set; } public double var { get; set; } }
        public class spectral_kurtosis { public double max { get; set; } public double mean { get; set; } public double median { get; set; } public double min { get; set; } public double var { get; set; } }
        public class spectral_rms { public double max { get; set; } public double mean { get; set; } public double median { get; set; } public double min { get; set; } public double var { get; set; } }
        public class spectral_rolloff { public double max { get; set; } public double mean { get; set; } public double median { get; set; } public double min { get; set; } public double var { get; set; } }
        public class spectral_skewness { public double max { get; set; } public double mean { get; set; } public double median { get; set; } public double min { get; set; } public double var { get; set; } }
        public class spectral_spread { public double max { get; set; } public double mean { get; set; } public double median { get; set; } public double min { get; set; } public double var { get; set; } }
        public class spectral_strongpeak { public double max { get; set; } public double mean { get; set; } public double median { get; set; } public double min { get; set; } public double var { get; set; } }
        public class zerocrossingrate { public double max { get; set; } public double mean { get; set; } public double median { get; set; } public double min { get; set; } public double var { get; set; } }

        public class metadata
        {

        }

        public class rhythm
        {
            public double beats_count { get; set; }
            public beats_loudness beats_loudness { get; set; }
            public double bpm { get; set; }
            public bpm_histogram_first_peak_bpm bpm_histogram_first_peak_bpm { get; set; }
            public bpm_histogram_first_peak_weight bpm_histogram_first_peak_weight { get; set; }
            public bpm_histogram_second_peak_bpm bpm_histogram_second_peak_bpm { get; set; }
            public bpm_histogram_second_peak_spread bpm_histogram_second_peak_spread { get; set; }
            public bpm_histogram_second_peak_weight bpm_histogram_second_peak_weight { get; set; }

        }

        public class beats_loudness { public double max { get; set; } public double mean { get; set; } public double median { get; set; } public double min { get; set; } public double var { get; set; } }
        public class bpm_histogram_first_peak_bpm { public double max { get; set; } public double mean { get; set; } public double median { get; set; } public double min { get; set; } public double var { get; set; } }
        public class bpm_histogram_first_peak_weight { public double max { get; set; } public double mean { get; set; } public double median { get; set; } public double min { get; set; } public double var { get; set; } }
        public class bpm_histogram_second_peak_bpm { public double max { get; set; } public double mean { get; set; } public double median { get; set; } public double min { get; set; } public double var { get; set; } }
        public class bpm_histogram_second_peak_spread { public double max { get; set; } public double mean { get; set; } public double median { get; set; } public double min { get; set; } public double var { get; set; } }
        public class bpm_histogram_second_peak_weight { public double max { get; set; } public double mean { get; set; } public double median { get; set; } public double min { get; set; } public double var { get; set; } }

        public class tonal
        {
            public double chords_changes_rate { get; set; }
            public double chords_number_rate { get; set; }
            public chords_strength chords_strength { get; set; }
            public hpcp_entropy hpcp_entropy { get; set; }
            public double key_strength { get; set; }
            public double tuning_diatonic_strength { get; set; }
            public double tuning_equal_tempered_deviation { get; set; }
            public double tuning_frequency { get; set; }
            public double tuning_nontempered_energy_ratio { get; set; }

        }

        public class chords_strength { public double max { get; set; } public double mean { get; set; } public double median { get; set; } public double min { get; set; } public double var { get; set; } }
        public class hpcp_entropy { public double max { get; set; } public double mean { get; set; } public double median { get; set; } public double min { get; set; } public double var { get; set; } }

        // ------------------------------------------------------------------------------------------------- JSON

        public SignalClassification()
        {
            InitializeComponent();
        }

        private void SignalClassification_Load(object sender, EventArgs e)
        {
            label1.Text = "Random forest:\n Result: \n Class 0 : \n Class 1 : \n Class 2 : \n Class 3 : \n Class 4 : \n Class 5 : \n Class 6 : \n Class 7 : \n Class 8 : \n Class 9 : \n";
        }


        double[,] TrainingSet;
        double[] y = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public static double[] y1;
       
        
        
        public void forest()
        {
            // Чтение CSV --------------------------------------------------------------------------------------------------
            char separator = ';';
            int flags = 0;

            // Случайный лес решений --------------------------------------------------------------------------------------------------
            var dForest = new alglib.decisionforest();
            var rep = new alglib.dfreport();
            int info;

            alglib.read_csv("../../data/train.csv", separator, flags, out TrainingSet);
            // ---------------------------------------------------------------------------------------------------Чтение CSV


            alglib.dfbuildrandomdecisionforestx1(TrainingSet, 950, 209, 10, 200, 1, 0.9, out info, out dForest, out rep);


            alglib.dfprocess(dForest, y1, ref y);


            // ---------------------------------------------------------------------------------------------------Случайный лес решений

            label1.Text = "Random forest:\n" + "Result: " + info.ToString() + "\n"
                                                    + "Class 0 : " + y[0].ToString() + "\n"
                                                    + "Class 1 : " + y[1].ToString() + "\n"
                                                    + "Class 2 : " + y[2].ToString() + "\n"
                                                    + "Class 3 : " + y[3].ToString() + "\n"
                                                    + "Class 4 : " + y[4].ToString() + "\n"
                                                    + "Class 5 : " + y[5].ToString() + "\n"
                                                    + "Class 6 : " + y[6].ToString() + "\n"
                                                    + "Class 7 : " + y[7].ToString() + "\n"
                                                    + "Class 8 : " + y[8].ToString() + "\n"
                                                    + "Class 9 : " + y[9].ToString() + "\n";

            //if (Array.IndexOf(y, y.Max()) == 0) label7.Text = "blues";
            //if (Array.IndexOf(y, y.Max()) == 1) label7.Text = "classical";
            //if (Array.IndexOf(y, y.Max()) == 2) label7.Text = "country";
            //if (Array.IndexOf(y, y.Max()) == 3) label7.Text = "disco";
            //if (Array.IndexOf(y, y.Max()) == 4) label7.Text = "hiphop";
            //if (Array.IndexOf(y, y.Max()) == 5) label7.Text = "jazz";
            //if (Array.IndexOf(y, y.Max()) == 6) label7.Text = "metal";
            //if (Array.IndexOf(y, y.Max()) == 7) label7.Text = "pop";
            //if (Array.IndexOf(y, y.Max()) == 8) label7.Text = "reggae";
            //if (Array.IndexOf(y, y.Max()) == 9) label7.Text = "rock";
        }


        private void button1_Click(object sender, EventArgs e)
        {
            // Чтение CSV --------------------------------------------------------------------------------------------------
            char separator = ';';
            int flags = 0;

            // Случайный лес решений --------------------------------------------------------------------------------------------------
            var dForest = new alglib.decisionforest();
            var rep = new alglib.dfreport();
            int info;

            alglib.read_csv("../../../data/trainRF.csv", separator, flags, out TrainingSet);

            alglib.dfbuildrandomdecisionforestx1(TrainingSet, 900, 37, 10, 150, 1, 0.1, out info, out dForest, out rep);


            string[] files1 = Directory.GetFiles(@"../../../data/testData");


            string add1 = "Blues" + "         " + "classical" + "         " + "country" + "         " + "disco" + "         " + "hiphop" + "         " + "jazz" + "         " + "metal" + "         " + "pop" + "         " + "reggae" + "         " + "rock" + "\n";

            File.AppendAllText(@"../../../data/music_forest_rezult.txt", add1);


            foreach (string file in files1)
            {

                string filename = file;

                label1.Text = filename;

                var json = File.ReadAllText(@filename).Replace("\n", " ");

                var result = JsonConvert.DeserializeObject<MyJson>(json);

                double[] y11 = {        result.lowlevel.average_loudness, 

                                            result.lowlevel.dissonance.mean,


                                            result.lowlevel.dynamic_complexity,

                                            result.lowlevel.hfc.mean,

                                            result.lowlevel.melbands_crest.max,
                                            result.lowlevel.melbands_crest.mean,

                                            result.lowlevel.melbands_flatness_db.max,
                                            result.lowlevel.melbands_flatness_db.mean,

                                            result.lowlevel.melbands_kurtosis.max,
                                            result.lowlevel.melbands_kurtosis.mean,

                                            result.lowlevel.melbands_skewness.max,
                                            result.lowlevel.melbands_skewness.mean,

                                            result.lowlevel.melbands_spread.max,
                                            result.lowlevel.melbands_spread.mean,

                                            result.lowlevel.pitch_salience.mean,

                                            result.lowlevel.silence_rate_20dB.mean,

                                            result.lowlevel.silence_rate_30dB.mean,
                                                                                                                                                                                                               
                                            result.lowlevel.silence_rate_60dB.mean,

                                            result.lowlevel.spectral_centroid.mean,

                                            result.lowlevel.spectral_complexity.mean,

                                            result.lowlevel.spectral_decrease.mean,

                                            result.lowlevel.spectral_energy.mean,

                                            result.lowlevel.spectral_energyband_high.mean,

                                            result.lowlevel.spectral_energyband_low.mean,

                                            result.lowlevel.spectral_energyband_middle_high.mean,

                                            result.lowlevel.spectral_energyband_middle_low.mean,

                                            result.lowlevel.spectral_entropy.mean,

                                            result.lowlevel.spectral_flux.mean,

                                            result.lowlevel.spectral_kurtosis.mean,

                                            result.lowlevel.spectral_rms.mean,

                                            result.lowlevel.spectral_rolloff.mean,

                                            result.lowlevel.spectral_skewness.mean,

                                            result.lowlevel.spectral_spread.mean,

                                            result.lowlevel.spectral_strongpeak.max,
                                            result.lowlevel.spectral_strongpeak.mean,
                                            result.lowlevel.spectral_strongpeak.min,

                                            result.lowlevel.zerocrossingrate.mean,

                                           // result.rhythm.beats_count,
                                           // result.rhythm.beats_loudness.mean,
                                           // result.rhythm.bpm,
                                           // result.rhythm.bpm_histogram_first_peak_bpm.mean,
                                           // result.rhythm.bpm_histogram_first_peak_weight.mean,
                                           // result.rhythm.bpm_histogram_second_peak_bpm.mean,
                                           // result.rhythm.bpm_histogram_second_peak_spread.mean,
                                           // result.rhythm.bpm_histogram_second_peak_weight.mean,

                                            //result.tonal.chords_changes_rate,
                                            //result.tonal.chords_number_rate,
                                            //result.tonal.chords_strength.mean,
                                            //result.tonal.hpcp_entropy.mean,
                                            //result.tonal.key_strength,
                                            //result.tonal.tuning_diatonic_strength,
                                            //result.tonal.tuning_equal_tempered_deviation,
                                            //result.tonal.tuning_frequency,
                                            //result.tonal.tuning_nontempered_energy_ratio
                          };
                y1 = y11;

                alglib.dfprocess(dForest, y1, ref y);


                // ---------------------------------------------------------------------------------------------------Случайный лес решений


                //----------------------------Запись в файл--------------------------------

                string add = file;

                add = add + "   " + y[0].ToString() + "           " + y[1].ToString() + "           " + y[2].ToString() + "           " + y[3].ToString() + "           " + y[4].ToString() + "           " + y[5].ToString() + "           " + y[6].ToString() + "           " + y[7].ToString() + "           " + y[8].ToString() + "           " + y[9].ToString() + "\n";

                File.AppendAllText(@"../../../data/music_forest_rezult.txt", add);

                label1.Text = "Random forest:\n" + "Result: " + info.ToString() + "\n"
                                                        + "Class 0 : " + y[0].ToString() + "\n"
                                                        + "Class 1 : " + y[1].ToString() + "\n"
                                                        + "Class 2 : " + y[2].ToString() + "\n"
                                                        + "Class 3 : " + y[3].ToString() + "\n"
                                                        + "Class 4 : " + y[4].ToString() + "\n"
                                                        + "Class 5 : " + y[5].ToString() + "\n"
                                                        + "Class 6 : " + y[6].ToString() + "\n"
                                                        + "Class 7 : " + y[7].ToString() + "\n"
                                                        + "Class 8 : " + y[8].ToString() + "\n"
                                                        + "Class 9 : " + y[9].ToString() + "\n";




                
            }
        }

        double[,] Speech_TrainingSet;
        double[] Speech_y = { 0, 0, 0 };
        double[] Speech_y2 = { 0, 0, 0 };
        public static double[] Speech_y1;

        private void button2_Click(object sender, EventArgs e)
        {
            // Чтение CSV --------------------------------------------------------------------------------------------------
            char separator = ';';
            int flags = 0;

            // Случайный лес решений --------------------------------------------------------------------------------------------------
            var dForest = new alglib.decisionforest();
            var rep = new alglib.dfreport();
            int info;

            alglib.read_csv("../../../data/train_speech_forest.csv", separator, flags, out Speech_TrainingSet);

            alglib.dfbuildrandomdecisionforestx1(Speech_TrainingSet, 69, 20, 3, 150, 1, 0.1, out info, out dForest, out rep);


            double[] y11 = { 196.464602717256, 176.744677229163, 125.08353265005, 60.5831933545139, 5.43772771850827, -24.5479444860171, -25.9131735026792, -7.39123684820563, 15.7948258401609, 29.9473097517081, 29.0897943537881, 16.4904128684023, 1.30258416810681, -7.35167463908725, -5.43464536162137, 4,42104356645431, 15.2528724104637, 20.2957931361363, 16.9132829693488, 7.74810091454172 
                           };

            Speech_y1 = y11;

            alglib.dfprocess(dForest, Speech_y1, ref Speech_y);


            // ---------------------------------------------------------------------------------------------------Случайный лес решений

            label2.Text = "Random forest:\n" + "Result: " + info.ToString() + "\n"
                                                + "Class 0 : " + Speech_y[0].ToString() + "\n"
                                                + "Class 1 : " + Speech_y[1].ToString() + "\n"
                                                + "Class 2 : " + Speech_y[2].ToString() + "\n";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int info;

            // Чтение CSV --------------------------------------------------------------------------------------------------
            char separator = ';';
            int flags = 0;

            alglib.read_csv("../../../data/train_speech_neuro.csv", separator, flags, out TrainingSet);

            double[] y11 = { 196.464602717256, 176.744677229163, 125.08353265005, 60.5831933545139, 5.43772771850827, -24.5479444860171, -25.9131735026792, -7.39123684820563, 15.7948258401609, 29.9473097517081, 29.0897943537881, 16.4904128684023, 1.30258416810681, -7.35167463908725, -5.43464536162137, 4,42104356645431, 15.2528724104637, 20.2957931361363, 16.9132829693488, 7.74810091454172 
                           };

            Speech_y1 = y11;

            // ---------------------------------------------------------------------------------------------------нейросеть

            var neuro = new alglib.multilayerperceptron();
            var neurorep = new alglib.mlpreport();

            alglib.mlpcreate1(20, 10, 3, out neuro);

            // alglib.mlpcreate0(7, 3, out neuro);

            alglib.mlptrainlm(neuro, TrainingSet, 69, 0.001, 2, out info, out neurorep);

            alglib.mlpprocess(neuro, Speech_y1, ref Speech_y2);

            label3.Text = "Neuro:\n" + "Result: " + info.ToString() + "\n"
                                                 + "Class 0 : " + Speech_y2[0].ToString() + "\n"
                                                 + "Class 1 : " + Speech_y2[1].ToString() + "\n"
                                                 + "Class 2 : " + Speech_y2[2].ToString() + "\n";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Чтение CSV --------------------------------------------------------------------------------------------------
            char separator = ';';
            int flags = 0;

            // Случайный лес решений --------------------------------------------------------------------------------------------------
            var dForest = new alglib.decisionforest();
            var rep = new alglib.dfreport();
            int info;

            alglib.read_csv("../../../data/trainNeuro.csv", separator, flags, out TrainingSet);


            string[] files1 = Directory.GetFiles(@"../../../data/testData");


            string add1 = "Blues" + "         " + "classical" + "         " + "country" + "         " + "disco" + "         " + "hiphop" + "         " + "jazz" + "         " + "metal" + "         " + "pop" + "         " + "reggae" + "         " + "rock" + "\n";

            File.AppendAllText(@"../../../data/music_neuro_rezult.txt", add1);


                // ---------------------------------------------------------------------------------------------------нейросеть

                alglib.read_csv("../../../data/trainNeuro.csv", separator, flags, out TrainingSet);

                var neuro = new alglib.multilayerperceptron();
                var neurorep = new alglib.mlpreport();

                alglib.mlpcreate1(37, 20, 10, out neuro);

                alglib.mlptrainlm(neuro, TrainingSet, 900, 0.001, 2, out info, out neurorep);

                files1 = Directory.GetFiles(@"../../../data/testData");


                add1 = "Blues" + "         " + "classical" + "         " + "country" + "         " + "disco" + "         " + "hiphop" + "         " + "jazz" + "         " + "metal" + "         " + "pop" + "         " + "reggae" + "         " + "rock" + "\n";

                File.AppendAllText(@"../../../data/music_forest_rezult.txt", add1);


                foreach (string file in files1)
                {

                    string filename = file;

                    label1.Text = filename;

                    var json = File.ReadAllText(@filename).Replace("\n", " ");

                    var result = JsonConvert.DeserializeObject<MyJson>(json);

                    double[] y11 = {        result.lowlevel.average_loudness, 

                                            result.lowlevel.dissonance.mean,


                                            result.lowlevel.dynamic_complexity,

                                            result.lowlevel.hfc.mean,

                                            result.lowlevel.melbands_crest.max,
                                            result.lowlevel.melbands_crest.mean,

                                            result.lowlevel.melbands_flatness_db.max,
                                            result.lowlevel.melbands_flatness_db.mean,

                                            result.lowlevel.melbands_kurtosis.max,
                                            result.lowlevel.melbands_kurtosis.mean,

                                            result.lowlevel.melbands_skewness.max,
                                            result.lowlevel.melbands_skewness.mean,

                                            result.lowlevel.melbands_spread.max,
                                            result.lowlevel.melbands_spread.mean,

                                            result.lowlevel.pitch_salience.mean,

                                            result.lowlevel.silence_rate_20dB.mean,

                                            result.lowlevel.silence_rate_30dB.mean,
                                                                                                                                                                                                               
                                            result.lowlevel.silence_rate_60dB.mean,

                                            result.lowlevel.spectral_centroid.mean,

                                            result.lowlevel.spectral_complexity.mean,

                                            result.lowlevel.spectral_decrease.mean,

                                            result.lowlevel.spectral_energy.mean,

                                            result.lowlevel.spectral_energyband_high.mean,

                                            result.lowlevel.spectral_energyband_low.mean,

                                            result.lowlevel.spectral_energyband_middle_high.mean,

                                            result.lowlevel.spectral_energyband_middle_low.mean,

                                            result.lowlevel.spectral_entropy.mean,

                                            result.lowlevel.spectral_flux.mean,

                                            result.lowlevel.spectral_kurtosis.mean,

                                            result.lowlevel.spectral_rms.mean,

                                            result.lowlevel.spectral_rolloff.mean,

                                            result.lowlevel.spectral_skewness.mean,

                                            result.lowlevel.spectral_spread.mean,

                                            result.lowlevel.spectral_strongpeak.max,
                                            result.lowlevel.spectral_strongpeak.mean,
                                            result.lowlevel.spectral_strongpeak.min,

                                            result.lowlevel.zerocrossingrate.mean,

                                           // result.rhythm.beats_count,
                                           // result.rhythm.beats_loudness.mean,
                                           // result.rhythm.bpm,
                                           // result.rhythm.bpm_histogram_first_peak_bpm.mean,
                                           // result.rhythm.bpm_histogram_first_peak_weight.mean,
                                           // result.rhythm.bpm_histogram_second_peak_bpm.mean,
                                           // result.rhythm.bpm_histogram_second_peak_spread.mean,
                                           // result.rhythm.bpm_histogram_second_peak_weight.mean,

                                            //result.tonal.chords_changes_rate,
                                            //result.tonal.chords_number_rate,
                                            //result.tonal.chords_strength.mean,
                                            //result.tonal.hpcp_entropy.mean,
                                            //result.tonal.key_strength,
                                            //result.tonal.tuning_diatonic_strength,
                                            //result.tonal.tuning_equal_tempered_deviation,
                                            //result.tonal.tuning_frequency,
                                            //result.tonal.tuning_nontempered_energy_ratio
                          };
                    y1 = y11;


                    alglib.mlpprocess(neuro, y1, ref y);


                    //----------------------------Запись в файл--------------------------------

                    string add = file;

                    add = add + "   " + y[0].ToString() + "           " + y[1].ToString() + "           " + y[2].ToString() + "           " + y[3].ToString() + "           " + y[4].ToString() + "           " + y[5].ToString() + "           " + y[6].ToString() + "           " + y[7].ToString() + "           " + y[8].ToString() + "           " + y[9].ToString() + "\n";

                    File.AppendAllText(@"../../../data/music_neuro_rezult.txt", add);

                    label1.Text = "Random forest:\n" + "Result: " + info.ToString() + "\n"
                                                            + "Class 0 : " + y[0].ToString() + "\n"
                                                            + "Class 1 : " + y[1].ToString() + "\n"
                                                            + "Class 2 : " + y[2].ToString() + "\n"
                                                            + "Class 3 : " + y[3].ToString() + "\n"
                                                            + "Class 4 : " + y[4].ToString() + "\n"
                                                            + "Class 5 : " + y[5].ToString() + "\n"
                                                            + "Class 6 : " + y[6].ToString() + "\n"
                                                            + "Class 7 : " + y[7].ToString() + "\n"
                                                            + "Class 8 : " + y[8].ToString() + "\n"
                                                            + "Class 9 : " + y[9].ToString() + "\n";


                }
            }
        }
    }

