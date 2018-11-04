using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Threading;

namespace PlayTheMusic
{
    public partial class Form1 : Form
    {
        SpeechSynthesizer ss = new SpeechSynthesizer();
        PromptBuilder pb = new PromptBuilder();
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();
        SpeechRecognitionEngine sre2 = new SpeechRecognitionEngine();
        Choices clist = new Choices();
        Choices clist2 = new Choices();
        
        
        string[] files, paths, files2, paths2, files3, paths3, files4, paths4;

        public Form1()
        {
            InitializeComponent();
            ss.SpeakAsync("Welcome to play the music. Let me know your mood           ");
            timer1.Start();
            clist.Add(new string[] { "Happy", "Sad", "Romantic","Instrumental","No"});
            clist2.Add(new string[] { "Yes", "No" });
            Grammar gr2 = new Grammar(new GrammarBuilder(clist2));
            Grammar gr = new Grammar(new GrammarBuilder(clist));
            

            try
            {
                sre.RequestRecognizerUpdate();
                sre.LoadGrammar(gr);
                sre.SpeechRecognized += sre_SpeechRecognized;
                sre.SetInputToDefaultAudioDevice();
                sre.RecognizeAsync(RecognizeMode.Multiple);

                sre2.RequestRecognizerUpdate();
                sre2.LoadGrammar(gr2);
                sre2.SpeechRecognized += sre2_SpeechRecognized;
                sre2.SetInputToDefaultAudioDevice();
                sre2.RecognizeAsync(RecognizeMode.Multiple);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        void sre2_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Text.ToString())
            {
                case "Yes":
                    sre.Recognize();
                    break;

                case "No":
                    axWindowsMediaPlayer1.Ctlcontrols.play();
                    sre.RecognizeAsyncStop();
                    break;

            }
        }

        
        void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Text.ToString())
            {
                case "Happy":
                    button5.Visible = true;
                    listBox1.Visible = true;

                    button6.Visible = false;
                    listBox2.Visible = false;

                    button7.Visible = false;
                    listBox3.Visible = false;

                    button8.Visible = false;
                    listBox4.Visible = false;

                    axWindowsMediaPlayer1.Ctlcontrols.stop();
                    sre.RecognizeAsyncStop();
                    break;

                case "Sad":
                    button5.Visible = false;
                    listBox1.Visible = false;

                    button6.Visible = true;
                    listBox2.Visible = true;

                    button7.Visible = false;
                    listBox3.Visible = false;

                    button8.Visible = false;
                    listBox4.Visible = false;

                    axWindowsMediaPlayer1.Ctlcontrols.stop();
                    sre.RecognizeAsyncStop();
                    break;

                case "Romantic":
                    button5.Visible = false;
                    listBox1.Visible = false;

                    button6.Visible = false;
                    listBox2.Visible = false;

                    button7.Visible = true;
                    listBox3.Visible = true;

                    button8.Visible = false;
                    listBox4.Visible = false;

                    axWindowsMediaPlayer1.Ctlcontrols.stop();
                    sre.RecognizeAsyncStop();
                    break;

                case "Instrumental":
                    button5.Visible = false;
                    listBox1.Visible = false;

                    button6.Visible = false;
                    listBox2.Visible = false;

                    button7.Visible = false;
                    listBox3.Visible = false;

                    button8.Visible = true;
                    listBox4.Visible = true;

                    axWindowsMediaPlayer1.Ctlcontrols.stop();
                    sre.RecognizeAsyncStop();
                    break;

                case "No":
                    axWindowsMediaPlayer1.Ctlcontrols.play();
                    break;


                /*case "open chrome":
                Process.Start("chrome", "http://www.youtube.com");
                break;
                case "close":
                    Application.Exit();
                    break;*/


            }
            txtContents.Text += e.Result.Text.ToString() + Environment.NewLine;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //Enable Happy mode
            button5.Visible = true;
            listBox1.Visible = true;
            
            button6.Visible = false;
            listBox2.Visible = false;
            
            button7.Visible = false;
            listBox3.Visible = false;
            
            button8.Visible = false;
            listBox4.Visible = false;
            
            axWindowsMediaPlayer1.Ctlcontrols.stop();

        }
        private void button5_Click(object sender, EventArgs e)
        {
            //happy mode song selection
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Multiselect = true;

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                files = openFileDialog1.SafeFileNames;  //save only the names
                paths = openFileDialog1.FileNames;      //save the full paths
                for (int i = 0; i < files.Length; i++)
                {
                    listBox1.Items.Add(files[i]);     //Add songs to the listbox
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //happy mode
            axWindowsMediaPlayer1.URL=paths[listBox1.SelectedIndex];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Enable Sad Mode
            button5.Visible = false;
            listBox1.Visible = false;

            button6.Visible = true;
            listBox2.Visible = true;

            button7.Visible = false;
            listBox3.Visible = false;

            button8.Visible = false;
            listBox4.Visible = false;

            axWindowsMediaPlayer1.Ctlcontrols.stop();
          

        }

        private void button6_Click(object sender, EventArgs e)
        {
            //sad mode song selection

            OpenFileDialog openFileDialog2 = new OpenFileDialog();
            openFileDialog2.Multiselect = true;

            if (openFileDialog2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                files2 = openFileDialog2.SafeFileNames;  //save only the names
                paths2 = openFileDialog2.FileNames;      //save the full paths
                for (int i = 0; i < files2.Length; i++)
                {
                    listBox2.Items.Add(files2[i]);     //Add songs to the listbox
                }
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //sad mode
            axWindowsMediaPlayer1.URL = paths2[listBox2.SelectedIndex];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Romantic Mode
            button5.Visible = false;
            listBox1.Visible = false;

            button6.Visible = false;
            listBox2.Visible = false;

            button7.Visible = true;
            listBox3.Visible = true;

            button8.Visible = false;
            listBox4.Visible = false;

            axWindowsMediaPlayer1.Ctlcontrols.stop();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //Romantic mode song selection
            OpenFileDialog openFileDialog3 = new OpenFileDialog();
            openFileDialog3.Multiselect = true;

            if (openFileDialog3.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                files3 = openFileDialog3.SafeFileNames;  //save only the names
                paths3 = openFileDialog3.FileNames;      //save the full paths
                for (int i = 0; i < files3.Length; i++)
                {
                    listBox3.Items.Add(files3[i]);     //Add songs to the listbox
                }
            }
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Romantic mode
            axWindowsMediaPlayer1.URL = paths3[listBox3.SelectedIndex];
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            //Instrumenatal
            button5.Visible = false;
            listBox1.Visible = false;

            button6.Visible = false;
            listBox2.Visible = false;

            button7.Visible = false;
            listBox3.Visible = false;

            button8.Visible = true;
            listBox4.Visible = true;

            axWindowsMediaPlayer1.Ctlcontrols.stop();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //Instrumental mode song selection
            OpenFileDialog openFileDialog4 = new OpenFileDialog();
            openFileDialog4.Multiselect = true;

            if (openFileDialog4.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                files4 = openFileDialog4.SafeFileNames;  //save only the names
                paths4 = openFileDialog4.FileNames;      //save the full paths
                for (int i = 0; i < files4.Length; i++)
                {
                    listBox4.Items.Add(files4[i]);     //Add songs to the listbox
                }
            }
        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Instrumental mode
            axWindowsMediaPlayer1.URL = paths4[listBox4.SelectedIndex];
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ss.SpeakAsync("Want to change the playlist");
            sre.Recognize();

        }

        
            
        }
        
    }
