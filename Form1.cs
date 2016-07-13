using System;
using System.Drawing;
using System.Windows.Forms;
using System.Text;
using System.IO;
using System.Media;

namespace fantasyspill2
{
    public partial class Form1 : Form

    {
        StreamReader read;
        SoundPlayer sp;
        string b;
        


        
        public Form1()
        {
            InitializeComponent();
            sp = new SoundPlayer();     // новый звуковой объект
            sp.Stream = Properties.Resources.mario; // берем из ресурсов звуковой файл марио
            

        }


        
       

        private void Form1_Load(object sender, EventArgs e)
        {

            this.Text = "Fantasyspill";

            textBox1.Multiline = true; //
            textBox1.Clear ();
          

            button1.Text = "Start Game";
            button1.TabIndex = 0;

            button2.Text = "Start Timer";
            label2.Text = null;

            button3.Text = "Lagre spill"; //сохранить игру
            button4.Text = "Lagring spill"; // запустить сохраненную игру
            
                      
            
           // StartGame (); // метод начала игры

        }

       


        void StartGame() // начало игры
        {
           

                        
            try
            {
                read = new StreamReader("fantasy5001.txt"); // объект читает текст
                textBox1.Text = read.ReadLine(); // записывает строку в текстбокс1

            }

            catch (Exception situation)
            {

                MessageBox.Show(situation.Message, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);           
            
            }


            //ReadNextWord(); // метод 

        }


        void ReadNextWord()
        {
            button3.Text = "Lagre spill";
            textBox1.Text = read.ReadLine();
            if (read.EndOfStream == true)
            {
                button1.Text = "Game over!";
            }
        
        }
         


        private void button1_Click(object sender, EventArgs e)
        {

            if (button1.Text == "Start Game")
            {
                button1.Text = "Next ord";
                StartGame();
                return;            
            }
            else if (button1.Text == "Next ord")
            {
                ReadNextWord();
            }

            else if (button1.Text == "Game over!")
            {
                read.Close();
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label2.Text = null;
            timer1.Interval =120000; // 120000
            timer1.Enabled = true; // таймер запущен
            button2.Text = "Time going now!";
        }

        private void timer1_Tick(object sender, EventArgs e) // обработчик события окончания игры
        {
            label2.Text = "TIME IS LAST!";
            sp.Play();
            //SystemSounds.Beep.Play();
            timer1.Enabled = false;
            button2.Text = "Start Timer";
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show("Точно выйти?", "Внимание",  //метод диалогового окна с кнопками да и нет.
                           MessageBoxButtons.YesNo,
                           MessageBoxIcon.Question);
            if (result != DialogResult.Yes) // если result неравно "да", задание о выходе отменяется.
                e.Cancel = true; // следует ли отменить событие..в данном случае следует
        }

        private void button3_Click(object sender, EventArgs e)
        {
            b = textBox1.Text;
            File.WriteAllText ("save.txt", b);
            button3.Text = "Ferdig!";
            
        }


        private void button4_Click(object sender, EventArgs e)
        {
            
            StreamReader ak = File.OpenText("save.txt");
            string s = ak.ReadLine ();
            if (s != null)
            {
                button1.Text = "Next ord";
                try
                {
                    read = new StreamReader("fantasy5001.txt"); // объект читает текст
                    while (read.EndOfStream == false) // пока не достигнут конец строки.
                    {
                        string a = read.ReadLine();
                        if (a == s)
                        {
                            textBox1.Text = a;
                            break;
                        }
                    }


                }

                catch (Exception situation)
                {

                    MessageBox.Show(situation.Message, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }

                ReadNextWord();
            }

            else
            {
                StartGame();
            }

        }

       
       

        

        // ///////////////////////////////////////////////////////////////////////////////////
        
       

        
       


    }
}
