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
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Globalization;
using System.Threading;

namespace capitals
{
    public partial class Form1 : Form
    {
            SpeechSynthesizer S = new SpeechSynthesizer();
            Choices list = new Choices();
            //public Boolean cap = false;
            public Form1()
        {
            CultureInfo ci = new CultureInfo("fr-FR");
            SpeechRecognitionEngine rec = new SpeechRecognitionEngine(ci);
            list.Add(File.ReadAllLines(@"C:\Users\thinkpad\Desktop\cap.txt"));
            Grammar gr = new Grammar(new GrammarBuilder(list));
            try
            {
                rec.RequestRecognizerUpdate();
                rec.LoadGrammarAsync(gr);
                rec.SpeechRecognized += Rec_SpeechRecognized;
                rec.SetInputToDefaultAudioDevice();
                rec.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch { return; }
            S.SelectVoiceByHints(VoiceGender.Female);
            S.Volume = 100;
            InitializeComponent();
        }
        public void say(string c)
        {
            S.SpeakAsync(c);
            textBox2.AppendText(c + "\n");
        }
        private void Rec_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string res = e.Result.Text;
            //if (cap)
            //{
                if (res.Equals(res.ToUpper()))
                {
                    int l = 0;
                    StreamReader sr = new StreamReader(@"C:\Users\thinkpad\Desktop\cap.txt");
                    List<string> lines = new List<string>();
                    while (lines.Contains(res) == false)
                    {
                        lines.Add(sr.ReadLine());
                    
                        l++;

                    }
                    sr.Close();
                    say(lines[l - 2]);
                    label2.ForeColor = Color.Red;
                    /*Thread.Sleep(1000);
                    Process.Start("https://www.google.tn/search?q=" + lines[l - 2]);*/
                    //cap = false;
                }
                else
                {
                    int l = 0;
                    StreamReader sr = new StreamReader(@"C:\Users\thinkpad\Desktop\cap.txt");
                    List<string> lines = new List<string>();
                    while (lines.Contains(res) == false)
                    {
                        lines.Add(sr.ReadLine());
                        l++;

                    }
                    lines.Add(sr.ReadLine());
                    say(res + " est la capitale de " + lines[l]);
                    sr.Close();
                    //cap = false;
                }
            
           //}
           /*if (res == "capitale")
           {
               cap = true;
           }*/
           textBox1.AppendText(res + "\n");
        }
            private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        
    }
}
