using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace Sayı_bulma_oyunu                       // Written by Recep Oğuzhan Şenoğlu
                                                // Recep Oğuzhan Şenoğlu tarafından yazılmıştır
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Gerekli değişkenleri yaratıyoruz.
        int num_of_prediction;
        int num_of_prediction1;
        int number_to_find;
        int score;
        string nickname = "rec";
        int second = 10;
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int text_lenght = txt_guess.TextLength;
            if (text_lenght.ToString() == comboBox1.SelectedItem.ToString())
            {
                int guess_number = Convert.ToInt32(txt_guess.Text);
                guess_control(guess_number);
                // Girdiğimiz tahminlerimizi guess_control isimli metoda parametre olarak verip tahmini kontrol ediyoruz
                txt_guess.Clear();
                num_of_prediction -= 1;
                label12.Text = "Your remaining life: " + num_of_prediction.ToString();
                second = 10;
                if (num_of_prediction == 0)
                {
                    panel2.Visible = true;
                    label12.Visible = false;
                    label13.Text = "You have lost :(";
                    score = 0;
                    Write_file(score, nickname); //Skoru ve ismi Write_file metodunu çağırarak metin belgesine yazdırıyoruz.
                }
                if (guess_number == number_to_find) // Doğru ise
                {
                    score = 31 - (num_of_prediction1 - num_of_prediction);
                    panel2.Visible = true;
                    label12.Visible = false;
                    Write_file(score, nickname);
                }
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Sayının kaç basamaklı olduğunu belirliyoruz
            if (txt_num_of_predictions.Text != "") btnstart.Enabled = true;
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex > -1)
            {
                btnstart.Enabled = true;
            }
        }
        private void btnstart_Click(object sender, EventArgs e)
        {
            nickname = txtnick.Text;
            panel1.Visible = false;
            //Start butonuna basınca create_number metodunu çağırarak rastgele bir sayı oluşturuyoruz.
            number_to_find = create_number();
            timer1.Enabled = true;
            timer1.Start();
            num_of_prediction = Convert.ToInt32(txt_num_of_predictions.Text);
            num_of_prediction1 = num_of_prediction;
            label12.Text = "Your remaining life: " + txt_num_of_predictions.Text;
            if (nickname == "rec.the.engineer") label3.Text = "number: " + number_to_find.ToString();
        }
        private void guess_control(int num) // Kontrol Metodu (Uzun olduğuna bakmayın bayâ bir işlevli
        {
            int num1 = num;
            int number_to_find1 = number_to_find;
            int b = 0;
            // Sayıyı basamaklarına ayırdık.
            label11.Text = (num1 % 10).ToString();
            label10.Text = (num1 / 10 % 10).ToString();
            label9.Text = (num1 / 100 % 10).ToString();
            label8.Text = (num1 / 1000 % 10).ToString();
            if (num1 / 1000 > 9)
            {
                label7.Text = (num1 / 10000 % 10).ToString();
                b++;
            }
            else if (num1 / 10000 > 9)
            {
                label6.Text = (num1 / 100000 % 10).ToString();
                b++;
            }
            else label6.Text = "";

            int[] our_digits = new int[4 + b];
            int[] game_digits = new int[4 + b];

            for (int i = 0; i < 4 + b; i++)
            {
                our_digits[i] = num1 % 10;
                num1 /= 10;
                game_digits[i] = number_to_find1 % 10;
                number_to_find1 /= 10;
            }

            label6.BackColor = Color.Red;
            label7.BackColor = Color.Red;
            label8.BackColor = Color.Red;
            label9.BackColor = Color.Red;
            label10.BackColor = Color.Red;
            label11.BackColor = Color.Red;

            // Bu kısımda da sayının yeri yanlışsa ama varsa turkuaz, yeri de doğruysa arkaplanı yeşil yapıyoruz.
            for (int j = 0; j < 4 + b; j++)
            {
                for (int k = 0; k < 4 + b; k++)
                {
                    if (our_digits[k] == game_digits[j])
                    {
                        switch (k)
                        {
                            case 0:
                                label11.BackColor = Color.Cyan;
                                break;
                            case 1:
                                label10.BackColor = Color.Cyan;
                                break;
                            case 2:
                                label9.BackColor = Color.Cyan;
                                break;
                            case 3:
                                label8.BackColor = Color.Cyan;
                                break;
                            case 4:
                                label7.BackColor = Color.Cyan;
                                break;
                            case 5:
                                label6.BackColor = Color.Cyan;
                                break;
                        }
                    }
                }
            }
            // Eğer sayı var ve yeri de doğru ise yeşil yapıyoruz.
            for (int l = 0; l < 4 + b; l++)
            {
                if (our_digits[l] == game_digits[l])
                {
                    switch (l)
                    {
                        case 0:
                            label11.BackColor = Color.Green;
                            break;
                        case 1:
                            label10.BackColor = Color.Green;
                            break;
                        case 2:
                            label9.BackColor = Color.Green;
                            break;
                        case 3:
                            label8.BackColor = Color.Green;
                            break;
                        case 4:
                            label7.BackColor = Color.Green;
                            break;
                        case 5:
                            label6.BackColor = Color.Green;
                            break;
                    }
                }
            }
        }
        private int create_number() // Random sayı ürettiğimiz metod.
        {
            while (true)
            {
                if (checkBox1.Checked == true) // Tekrarlı sayılara izin var mı yok mu kontrol metodu
                {
                    if (comboBox1.SelectedIndex == 0) // 4 basamak
                    {
                        Random random = new Random();
                        int number = random.Next(1000, 9999);
                        return number;
                    }
                    else if (comboBox1.SelectedIndex == 1) // 5 basamak
                    {
                        Random random = new Random();
                        int number = random.Next(10000, 99999);
                        return number;
                    }
                    else if (comboBox1.SelectedIndex == 2) // 6 basamak
                    {
                        Random random = new Random();
                        int number = random.Next(100000, 999999);
                        return number;
                    }
                }
                else if (checkBox1.Checked != true) // Tekrarlı sayıya izin yok ise
                {
                    int digit_number = (comboBox1.SelectedIndex + 4);
                    int return_number = 0;
                    int[] digit_numbers = new int[digit_number];
                    int kk = 1;
                    int a = 0;
                    int number;
                    while (a < digit_number)
                    {
                        bool break_while = false;
                        Random random = new Random();
                        if (kk == 1)
                        {
                            number = random.Next(0, 9);
                            digit_numbers[a] = number;
                            return_number += kk * number;
                            a += 1;
                            kk *= 10;
                            continue;
                        }
                        else if (kk != 1 && a != digit_number - 1)
                        {
                            number = random.Next(0, 9);
                            for (int j = 0; j < a; j++)
                            {
                                if (number == digit_numbers[j])
                                {
                                    break_while = true;
                                    continue;
                                }
                            }
                            if (break_while != true)
                            {
                                digit_numbers[a] = number;
                                return_number += kk * number;
                                kk *= 10;
                                a += 1;
                                continue;
                            }

                            if (break_while == true) continue;
                        }
                        if (a == digit_number - 1)
                        {
                            number = random.Next(1, 9);
                            for (int j = 0; j < a; j++)
                            {
                                if (number == digit_numbers[j])
                                {
                                    break_while = true;
                                    continue;
                                }
                            }
                            if (break_while != true)
                            {
                                digit_numbers[a] = number;
                                return_number += kk * number;
                                kk *= 10;
                                a += 1;
                                continue;
                            }
                            if (break_while == true) continue;
                        }
                    }
                    return return_number;
                }
            }
        }
        string dir = Directory.GetCurrentDirectory(); // Dosya yolu
        private void Write_file(int score, string nick) // Dosya işlemleri kısmı.
        {
            string file_path = dir + "\\Scores.txt";
            if (File.Exists(file_path))
            {
                StreamWriter SW = File.AppendText(file_path);
                SW.WriteLine(nick + "'s score: " + score);
                SW.Close();
            }
            else
            {
                FileStream fs = new FileStream(file_path, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(nick + "'s score: " + score);
                sw.Flush();
                sw.Close();
                fs.Close();
            }
        }       
        private void timer1_Tick(object sender, EventArgs e) // Süre kısmı
        {
            second--;
            if (second < 0)
            {
                second = 10;
                num_of_prediction--;
                txt_guess.Clear();
                label12.Text = "Your remaining life: " + num_of_prediction.ToString();
            }
            label5.Text = "Your time: " + second.ToString() + " second";
            if (num_of_prediction == 0)
            {
                txt_guess.Enabled = false;
                timer1.Stop();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
        private void txt_num_of_predictions_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void txt_guess_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
// Written by Recep Oğuzhan Şenoğlu
// Recep Oğuzhan Şenoğlu tarafından yazılmıştır

