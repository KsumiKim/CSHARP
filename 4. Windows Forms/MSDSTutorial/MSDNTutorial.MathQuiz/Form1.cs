using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSDNTutorial.MathQuiz
{
    public partial class Form1 : Form
    {
        Random randomizer = new Random();

        int addend1;
        int addend2;

        int minuend;
        int subtrahend;

        int multiplecand;
        int multiplier;

        int dividend;
        int divisor;

        int timeLeft;

        public void StartTheQuiz()
        {
            addend1 = randomizer.Next(51);
            addend2 = randomizer.Next(51);

            lblPlusLeft.Text = addend1.ToString();
            lblPlusRight.Text = addend2.ToString();

            sum.Value = 0;

            minuend = randomizer.Next(1, 101);
            subtrahend = randomizer.Next(1, minuend);
            lblMinusLeft.Text = minuend.ToString();
            lblMinusRight.Text = subtrahend.ToString();

            difference.Value = 0;

            multiplecand = randomizer.Next(2, 11);
            multiplier = randomizer.Next(2, 11);
            lblTimesLeft.Text = multiplecand.ToString();
            lblTimesRight.Text = multiplier.ToString();
            product.Value = 0;

            divisor = randomizer.Next(2, 11);
            int temporaryQuotient = randomizer.Next(2, 11);
            dividend = divisor * temporaryQuotient;
            lblDividedLeft.Text = dividend.ToString();
            lblDividedRight.Text = divisor.ToString();
            quotient.Value = 0;

            timeLeft = 30;
            lblTime.Text = "30 seconds";
            timer.Start();

        }

        private bool CheckTheAnswer()
        {
            if ((addend1 + addend2 == sum.Value) && 
                (minuend - subtrahend == difference.Value) && 
                (multiplecand * multiplier == product.Value) &&
                (dividend / divisor == quotient.Value))
                return true;
            else
                return false;
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            StartTheQuiz();
            btnStart.Enabled = false;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (CheckTheAnswer())
            {
                timer.Stop();
                MessageBox.Show("You Got All the Answers Right!", "Congratulations!!");
                btnStart.Enabled = true;
            }
            else if (timeLeft > 0)
            {
                timeLeft--;
                lblTime.Text = timeLeft + " seconds";
            }
            else
            {
                timer.Stop();
                lblTime.Text = "Time's Up!";
                MessageBox.Show("You didn't finish in time.", "Sorry!");
                sum.Value = addend1 + addend2;
                difference.Value = minuend - subtrahend;
                product.Value = multiplecand * multiplier;
                quotient.Value = dividend / divisor;
                btnStart.Enabled = true;
            }
        }

        private void answer_Enter(object sender, EventArgs e)
        {
            NumericUpDown answerBox =
                sender as NumericUpDown;

            if (answerBox != null)
            {
                int lengthOfAnswer = answerBox.Value.ToString().Length;
                answerBox.Select(0, lengthOfAnswer);
            }
        }
    }
}
