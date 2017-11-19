using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;

namespace MusicSpeech
{
    public partial class MakeDataRetrieval : Form
    {
        public MakeDataRetrieval()
        {
            InitializeComponent();
            textBox1.Text = @"C:\Users\Dingo\Music\Audials\GTZAN_mp3\";
        }


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

        private void button1_Click(object sender, EventArgs e)
        {
            string Folder = textBox1.Text + comboBox1.Text + @"\";

            string code = "0";

            string DataRetrievalName = "../../../data/trainRF.csv";

            if (radioButton1.Checked)
            {
                if (String.Equals(comboBox1.Text, "blues")) code = "0";
                if (String.Equals(comboBox1.Text, "classical")) code = "1";
                if (String.Equals(comboBox1.Text, "country")) code = "2";
                if (String.Equals(comboBox1.Text, "disco")) code = "3";
                if (String.Equals(comboBox1.Text, "hiphop")) code = "4";
                if (String.Equals(comboBox1.Text, "jazz")) code = "5";
                if (String.Equals(comboBox1.Text, "metal")) code = "6";
                if (String.Equals(comboBox1.Text, "pop")) code = "7";
                if (String.Equals(comboBox1.Text, "reggae")) code = "8";
                if (String.Equals(comboBox1.Text, "rock")) code = "9";

                DataRetrievalName = "../../../data/trainRF.csv";
            }

            if (radioButton2.Checked)
            {
                if (String.Equals(comboBox1.Text, "blues"))     code = "1;0;0;0;0;0;0;0;0;0";
                if (String.Equals(comboBox1.Text, "classical")) code = "0;1;0;0;0;0;0;0;0;0";
                if (String.Equals(comboBox1.Text, "country"))   code = "0;0;1;0;0;0;0;0;0;0";
                if (String.Equals(comboBox1.Text, "disco"))     code = "0;0;0;1;0;0;0;0;0;0";
                if (String.Equals(comboBox1.Text, "hiphop"))    code = "0;0;0;0;1;0;0;0;0;0";
                if (String.Equals(comboBox1.Text, "jazz"))      code = "0;0;0;0;0;1;0;0;0;0";
                if (String.Equals(comboBox1.Text, "metal"))     code = "0;0;0;0;0;0;1;0;0;0";
                if (String.Equals(comboBox1.Text, "pop"))       code = "0;0;0;0;0;0;0;1;0;0";
                if (String.Equals(comboBox1.Text, "reggae"))    code = "0;0;0;0;0;0;0;0;1;0";
                if (String.Equals(comboBox1.Text, "rock"))      code = "0;0;0;0;0;0;0;0;0;1";

                DataRetrievalName = "../../../data/trainNeuro.csv";
            }


            string[] files = Directory.GetFiles(@Folder, "*.txt");// папка с файлами


            foreach (string way in files) // извлекаем все файлы и кидаем их в список
            {
                var json = File.ReadAllText(@way).Replace("\n", " ");

                var result = JsonConvert.DeserializeObject<MyJson>(json);

                File.AppendAllText(@DataRetrievalName,

                                                        result.lowlevel.average_loudness.ToString() + ";"

                                                        + result.lowlevel.dissonance.mean.ToString() + ";"


                                                        + result.lowlevel.dynamic_complexity.ToString() + ";"

                                                        + result.lowlevel.hfc.mean.ToString() + ";"


                                                        + result.lowlevel.melbands_crest.max.ToString() + ";"
                                                        + result.lowlevel.melbands_crest.mean.ToString() + ";"
                                                        //+ result.lowlevel.melbands_crest.median.ToString() + ";"
                                                        //+ result.lowlevel.melbands_crest.min.ToString() + ";"
                                                        //+ result.lowlevel.melbands_crest.var.ToString() + ";"

                                                        + result.lowlevel.melbands_flatness_db.max.ToString() + ";"
                                                        + result.lowlevel.melbands_flatness_db.mean.ToString() + ";"
                                                        //+ result.lowlevel.melbands_flatness_db.median.ToString() + ";"
                                                        //+ result.lowlevel.melbands_flatness_db.min.ToString() + ";"
                                                        //+ result.lowlevel.melbands_flatness_db.var.ToString() + ";"

                                                        + result.lowlevel.melbands_kurtosis.max.ToString() + ";"
                                                        + result.lowlevel.melbands_kurtosis.mean.ToString() + ";"
                                                        //+ result.lowlevel.melbands_kurtosis.median.ToString() + ";"
                                                        //+ result.lowlevel.melbands_kurtosis.min.ToString() + ";"
                                                        //+ result.lowlevel.melbands_kurtosis.var.ToString() + ";"

                                                        + result.lowlevel.melbands_skewness.max.ToString() + ";"
                                                        + result.lowlevel.melbands_skewness.mean.ToString() + ";"
                                                        //+ result.lowlevel.melbands_skewness.median.ToString() + ";"
                                                        //+ result.lowlevel.melbands_skewness.min.ToString() + ";"
                                                        //+ result.lowlevel.melbands_skewness.var.ToString() + ";"

                                                        + result.lowlevel.melbands_spread.max.ToString() + ";"
                                                        + result.lowlevel.melbands_spread.mean.ToString() + ";"
                                                        //+ result.lowlevel.melbands_spread.median.ToString() + ";"
                                                        //+ result.lowlevel.melbands_spread.min.ToString() + ";"
                                                        //+ result.lowlevel.melbands_spread.var.ToString() + ";"


                                                        + result.lowlevel.pitch_salience.mean.ToString() + ";"

                                                        + result.lowlevel.silence_rate_20dB.mean.ToString() + ";"

                                                        + result.lowlevel.silence_rate_30dB.mean.ToString() + ";"

                                                        + result.lowlevel.silence_rate_60dB.mean.ToString() + ";"

                                                        + result.lowlevel.spectral_centroid.mean.ToString() + ";"

                                                        + result.lowlevel.spectral_complexity.mean.ToString() + ";"

                                                        + result.lowlevel.spectral_decrease.mean.ToString() + ";"

                                                        + result.lowlevel.spectral_energy.mean.ToString() + ";"

                                                        + result.lowlevel.spectral_energyband_high.mean.ToString() + ";"

                                                        + result.lowlevel.spectral_energyband_low.mean.ToString() + ";"

                                                        + result.lowlevel.spectral_energyband_middle_high.mean.ToString() + ";"

                                                        + result.lowlevel.spectral_energyband_middle_low.mean.ToString() + ";"

                                                        + result.lowlevel.spectral_entropy.mean.ToString() + ";"

                                                        + result.lowlevel.spectral_flux.mean.ToString() + ";"

                                                        + result.lowlevel.spectral_kurtosis.mean.ToString() + ";"

                                                        + result.lowlevel.spectral_rms.mean.ToString() + ";"

                                                        + result.lowlevel.spectral_rolloff.mean.ToString() + ";"

                                                        + result.lowlevel.spectral_skewness.mean.ToString() + ";"

                                                        + result.lowlevel.spectral_spread.mean.ToString() + ";"

                                                        + result.lowlevel.spectral_strongpeak.max.ToString() + ";"

                                                        + result.lowlevel.spectral_strongpeak.mean.ToString() + ";"

                                                        + result.lowlevel.spectral_strongpeak.min.ToString() + ";"

                                                        + result.lowlevel.zerocrossingrate.mean.ToString() + ";"

                    //  + result.rhythm.beats_count.ToString() + ";"
                    //  + result.rhythm.beats_loudness.mean.ToString() + ";"
                    //  + result.rhythm.bpm.ToString() + ";"
                    //  + result.rhythm.bpm_histogram_first_peak_bpm.mean.ToString() + ";"
                    //  + result.rhythm.bpm_histogram_first_peak_weight.mean.ToString() + ";"
                    //  + result.rhythm.bpm_histogram_second_peak_bpm.mean.ToString() + ";"
                    //  + result.rhythm.bpm_histogram_second_peak_spread.mean.ToString() + ";"
                    //  + result.rhythm.bpm_histogram_second_peak_weight.mean.ToString() + ";"

                    //  + result.tonal.chords_changes_rate.ToString() + ";"
                    //  + result.tonal.chords_number_rate.ToString() + ";"
                    //  + result.tonal.chords_strength.mean.ToString() + ";"
                    //  + result.tonal.hpcp_entropy.mean.ToString() + ";"
                    //  + result.tonal.key_strength.ToString() + ";"
                    //  + result.tonal.tuning_diatonic_strength.ToString() + ";"
                    //  + result.tonal.tuning_equal_tempered_deviation.ToString() + ";"
                    //  + result.tonal.tuning_frequency.ToString() + ";"
                    //  + result.tonal.tuning_nontempered_energy_ratio.ToString() + ";"

                                            + code + "\n");
            }
        }

        //------------------------------------------------Создать новую выборку-----------------------------------------------------------------------
        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                File.Delete("../../../data/trainRF.csv");
                File.Create("../../../data/trainRF.csv");
            }
            if (radioButton2.Checked)
            {
                File.Delete("../../../data/trainNeuro.csv");
                File.Create("../../../data/trainNeuro.csv");
            }
        }

        private void MakeDataRetrieval_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
        }
    }
}
