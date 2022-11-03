using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment_3_Mad4Cars
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        const int TERM_OPTION1 = 1, TERM_OPTION2 = 3, TERM_OPTION3 = 5, TERM_OPTION4 = 7;
        const decimal ROI_CATEG1_TERM_OPT1 = 6.00m, ROI_CATEG1_TERM_OPT2 =6.50m, ROI_CATEG1_TERM_OPT3 =7.00m, ROI_CATEG1_TERM_OPT4 =7.50m, ROI_CATEG2_TERM_OPT1 = 8.00m, ROI_CATEG2_TERM_OPT2 = 8.50m, ROI_CATEG2_TERM_OPT3 = 9.00m, ROI_CATEG2_TERM_OPT4 = 9.50m, ROI_CATEG3_TERM_OPT1 = 8.50m, ROI_CATEG3_TERM_OPT2 = 8.75m, ROI_CATEG3_TERM_OPT3 = 9.10m, ROI_CATEG3_TERM_OPT4 = 9.25m;
        

        

        const string ACTUAL_PASSOWRD = "123";
        const int MAX_ATTEMPTS = 3;
        int attemptCount = 1;
        private void LoginButton_Click(object sender, EventArgs e)
        {
            
            string enteredPassword;
            enteredPassword = PasswordTextBox.Text;

            if (enteredPassword.Equals(ACTUAL_PASSOWRD))
            {
                InvestmentPanelScreen2.Visible = true;
                SearchGroupBox.Visible = true;
                SummaryGroupBox.Visible = true;
            }
            else if (attemptCount < MAX_ATTEMPTS)
            {
                MessageBox.Show("Incorrect Password\n" + (MAX_ATTEMPTS- attemptCount) + " attempts out of "+ MAX_ATTEMPTS+" remaining", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                PasswordTextBox.Focus();
                PasswordTextBox.SelectAll();
                attemptCount++;
            }
            else
            {
                MessageBox.Show("Maximum Permissible attemps exhausted. Application will now Exit","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                this.Close();
            }

        }
        private void DisplayButton_Click(object sender, EventArgs e)
        {
            decimal principalAmount=0.00m,monthlyInterest=0.00m,totalInterest=0.00m,repaymentAmount=0.00m;

            principalAmount = int.Parse(PrincipalTextBox.Text);

            if (principalAmount < 40000)
            {
                //Calculation of list variables for 1 Year Term
                monthlyInterest = MonthlyInterest(principalAmount, ROI_CATEG1_TERM_OPT1, TERM_OPTION1);
                totalInterest = monthlyInterest * 12;
                repaymentAmount=principalAmount+totalInterest;
                InvestmentListBox.Items.Add(TERM_OPTION1 + " Years\t" + ROI_CATEG1_TERM_OPT1 + "\t" + monthlyInterest.ToString("0.00") + "\t" + totalInterest.ToString("0.00") + "\t" + repaymentAmount.ToString("0.00"));

                //Calculation of list variables for 2 Year Term
                monthlyInterest = MonthlyInterest(principalAmount, ROI_CATEG1_TERM_OPT2, TERM_OPTION2);
                totalInterest = monthlyInterest * 12;
                repaymentAmount = principalAmount + totalInterest;
                InvestmentListBox.Items.Add(TERM_OPTION2 + " Years\t" + ROI_CATEG1_TERM_OPT2 + "\t" + monthlyInterest.ToString("0.00") + "\t" + totalInterest.ToString("0.00") + "\t" + repaymentAmount.ToString("0.00"));

                //Calculation of list variables for 3 Year Term
                monthlyInterest = MonthlyInterest(principalAmount, ROI_CATEG1_TERM_OPT3, TERM_OPTION3);
                totalInterest = monthlyInterest * 12;
                repaymentAmount = principalAmount + totalInterest;

                InvestmentListBox.Items.Add(TERM_OPTION3 + " Years\t" + ROI_CATEG1_TERM_OPT3 + "\t" + monthlyInterest.ToString("0.00") + "\t" + totalInterest.ToString("0.00") + "\t" + repaymentAmount.ToString("0.00"));

                //Calculation of list variables for 4 Year Term
                totalInterest = monthlyInterest * 12;
                repaymentAmount = principalAmount + totalInterest;
                monthlyInterest = MonthlyInterest(principalAmount, ROI_CATEG1_TERM_OPT4, TERM_OPTION4);
                
                InvestmentListBox.Items.Add(TERM_OPTION4 + " Years\t" + ROI_CATEG1_TERM_OPT4 + "\t" + monthlyInterest.ToString("0.00") + "\t" + totalInterest.ToString("0.00") + "\t" + repaymentAmount.ToString("0.00"));

            }



        }
        private void ProceedButton_Click(object sender, EventArgs e)
        {
            int schemeSelected;
            schemeSelected = InvestmentListBox.SelectedIndex;

            if (!schemeSelected.Equals(-1))
            {
                

            }
            else
            {
                MessageBox.Show("Please select a loan tenure plan");
                InvestmentListBox.Focus();  
            }

        }
        /*********************************************************************************/

        static decimal FirstSlabLoanProcessing(decimal requestedLoan)
        {

            return 1;
        }
        static decimal MonthlyInterest(decimal p, decimal r, int n)
        {
            decimal totalInterestPayable = 0, monthlyInterest;
            for (int i = 0; i < n; i++)
            {
                decimal loanValue = p;
                loanValue = loanValue * (1 + (r/100));
                totalInterestPayable = loanValue-p;

            }
            monthlyInterest = (totalInterestPayable / 12);
            return monthlyInterest;

        }


    }
}

