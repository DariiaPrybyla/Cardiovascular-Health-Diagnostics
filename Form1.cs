using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Отримання параметрів comboBox
            int R_interval = comboBox1.SelectedIndex;
            int concomitant_signs = comboBox2.SelectedIndex;

            // Перевірка на заповнення всіх полів
            if (string.IsNullOrEmpty(textBox1.Text) ||
                string.IsNullOrEmpty(textBox2.Text) ||
                (R_interval == -1 || concomitant_signs == -1))
            {
                MessageBox.Show("Будь ласка, заповніть всі поля.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Отримання параметрів textBox
            double heart_rate = double.Parse(textBox1.Text);
            double duration_qrs = double.Parse(textBox2.Text);

            //Аналіз даних
            string diagnosis = "Відхилень не визначено."; // Змінна для зберігання діагнозу


            if (heart_rate > 100)
            {
                if (duration_qrs < 0.11)
                {
                    //Синусова, передсердна та вузлова тахікардія
                    if (R_interval == 0 && concomitant_signs == 1)
                        diagnosis = "Синусова, передсердна та вузлова тахікардія.";
                    
                    //Мерехтіння передсердь
                    else if (R_interval == 1 && concomitant_signs == 2)
                        diagnosis = "Мерехтіння передсердь.";
                }

                //Шлуночкова тахікардія
                else if (duration_qrs >= 0.11 && ((R_interval == 0 && concomitant_signs == 3) || R_interval == 1))
                    diagnosis = "Шлуночкова тахікардія.";
            }

            else if (heart_rate < 60)
            {
                //Мерехтіння передсердь з аберантним проведенням по шлуночкам
                if (R_interval == 1 && concomitant_signs == 11)
                    diagnosis = "Мерехтіння передсердь з аберантним проведенням по шлуночкам.";

                else if (duration_qrs < 0.11)
                {
                    if (R_interval == 0)
                    {
                        //Синусова брадикардія
                        if (concomitant_signs == 4)
                            diagnosis = "Синусова брадикардія.";

                        //АВ – блокада
                        else if (concomitant_signs == 5 || concomitant_signs == 6)
                            diagnosis = "АВ–блокада.";
                        
                        //АВ-вузловий ритм
                        else if (concomitant_signs == 7)
                            diagnosis = "АВ-вузловий ритм.";
                    }

                    //Мерехтіння передсердь
                    else if (R_interval == 1 && concomitant_signs == 8)
                        diagnosis = "Мерехтіння передсердь.";
                    
                    //АВ - блокада типу Венкебака
                    else if (concomitant_signs == 9)
                        diagnosis = "АВ-блокада типу Венкебака.";
                }

                //Ідіоветрикулярний ритм
                else if (duration_qrs >= 0.11 && R_interval == 0 && concomitant_signs == 10)
                    diagnosis = "Ідіоветрикулярний ритм.";
            }

            // Відображення результату
            label7.Text = diagnosis;
        }
    }
}
