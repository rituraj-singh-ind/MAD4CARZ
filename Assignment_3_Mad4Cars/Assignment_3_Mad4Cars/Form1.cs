using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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

        string emailToFile, nameToFile, eirToFile, phoneNumToFile, loanRequestedToFile, emiToFile, tenureToFiles, roiToFile, trxID;

        

        decimal monthlyEMI;
        string FILEPATH= @"D:\Assignments\Business Applicationn Programming\Assignment 3\Test.txt";
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
            decimal principalAmount,totalInterest,repaymentAmount;

            principalAmount = int.Parse(PrincipalTextBox.Text);
            InvestmentListBox.Items.Clear();

            if (principalAmount <= 40000)
            {
                //Calculation of list variables for 1 Year Term
                monthlyEMI = MonthlyEMI(principalAmount, ROI_CATEG1_TERM_OPT1, TERM_OPTION1);
                totalInterest = ((monthlyEMI * 12)-principalAmount);
                repaymentAmount = principalAmount + totalInterest;
                InvestmentListBox.Items.Add(TERM_OPTION1 + " Years\t\t" + ROI_CATEG1_TERM_OPT1 + "\t" + monthlyEMI.ToString("0.00") + "\t" + totalInterest.ToString("0.00") + "\t" + repaymentAmount.ToString("0.00"));

                //Calculation of list variables for 2 Year Term
                monthlyEMI = MonthlyEMI(principalAmount, ROI_CATEG1_TERM_OPT2, TERM_OPTION2);
                totalInterest = ((monthlyEMI * 12)-principalAmount);
                repaymentAmount = principalAmount + totalInterest;
                InvestmentListBox.Items.Add(TERM_OPTION2 + " Years\t\t" + ROI_CATEG1_TERM_OPT2 + "\t" + monthlyEMI.ToString("0.00") + "\t" + totalInterest.ToString("0.00") + "\t" + repaymentAmount.ToString("0.00"));

                //Calculation of list variables for 3 Year Term
                monthlyEMI = MonthlyEMI(principalAmount, ROI_CATEG1_TERM_OPT3, TERM_OPTION3);
                totalInterest = ((monthlyEMI * 12)-principalAmount);
                repaymentAmount = principalAmount + totalInterest;

                InvestmentListBox.Items.Add(TERM_OPTION3 + " Years\t\t" + ROI_CATEG1_TERM_OPT3 + "\t" + monthlyEMI.ToString("0.00") + "\t" + totalInterest.ToString("0.00") + "\t" + repaymentAmount.ToString("0.00"));

                //Calculation of list variables for 4 Year Term
                totalInterest = ((monthlyEMI * 12)-principalAmount);
                repaymentAmount = principalAmount + totalInterest;
                monthlyEMI = MonthlyEMI(principalAmount, ROI_CATEG1_TERM_OPT4, TERM_OPTION4);
                
                InvestmentListBox.Items.Add(TERM_OPTION4 + " Years\t\t" + ROI_CATEG1_TERM_OPT4 + "\t" + monthlyEMI.ToString("0.00") + "\t" + totalInterest.ToString("0.00") + "\t" + repaymentAmount.ToString("0.00"));

            }
            else if(principalAmount > 40000 && principalAmount <= 60000)
            {
                //Calculation of list variables for 1 Year Term
                monthlyEMI = MonthlyEMI(principalAmount, ROI_CATEG2_TERM_OPT1, TERM_OPTION1);
                totalInterest = (monthlyEMI * 12);
                repaymentAmount = principalAmount + totalInterest;
                InvestmentListBox.Items.Add(TERM_OPTION1 + " Years\t\t" + ROI_CATEG2_TERM_OPT1 + "\t" + monthlyEMI.ToString("0.00") + "\t" + totalInterest.ToString("0.00") + "\t" + repaymentAmount.ToString("0.00"));

                //Calculation of list variables for 2 Year Term
                monthlyEMI = MonthlyEMI(principalAmount, ROI_CATEG2_TERM_OPT2, TERM_OPTION2);
                totalInterest = (monthlyEMI * 12);
                repaymentAmount = principalAmount + totalInterest;
                InvestmentListBox.Items.Add(TERM_OPTION2 + " Years\t\t" + ROI_CATEG2_TERM_OPT2 + "\t" + monthlyEMI.ToString("0.00") + "\t" + totalInterest.ToString("0.00") + "\t" + repaymentAmount.ToString("0.00"));

                //Calculation of list variables for 3 Year Term
                monthlyEMI = MonthlyEMI(principalAmount, ROI_CATEG2_TERM_OPT3, TERM_OPTION3);
                totalInterest = (monthlyEMI * 12);
                repaymentAmount = principalAmount + totalInterest;

                InvestmentListBox.Items.Add(TERM_OPTION3 + " Years\t\t" + ROI_CATEG2_TERM_OPT3 + "\t" + monthlyEMI.ToString("0.00") + "\t" + totalInterest.ToString("0.00") + "\t" + repaymentAmount.ToString("0.00"));

                //Calculation of list variables for 4 Year Term
                monthlyEMI = MonthlyEMI(principalAmount, ROI_CATEG2_TERM_OPT4, TERM_OPTION4);
                totalInterest = (monthlyEMI * 12);
                repaymentAmount = principalAmount + totalInterest;
                

                InvestmentListBox.Items.Add(TERM_OPTION4 + " Years\t\t" + ROI_CATEG2_TERM_OPT4 + "\t" + monthlyEMI.ToString("0.00") + "\t" + totalInterest.ToString("0.00") + "\t" + repaymentAmount.ToString("0.00"));
            }
            else if(principalAmount>60000 && principalAmount<80000)
            {
                //Calculation of list variables for 1 Year Term
                monthlyEMI = MonthlyEMI(principalAmount, ROI_CATEG3_TERM_OPT1, TERM_OPTION1);
                totalInterest = (monthlyEMI * 12);
                repaymentAmount = principalAmount + totalInterest;
                InvestmentListBox.Items.Add(TERM_OPTION1 + " Years\t\t" + ROI_CATEG3_TERM_OPT1 + "\t" + monthlyEMI.ToString("0.00") + "\t" + totalInterest.ToString("0.00") + "\t" + repaymentAmount.ToString("0.00"));

                //Calculation of list variables for 2 Year Term
                monthlyEMI = MonthlyEMI(principalAmount, ROI_CATEG3_TERM_OPT2, TERM_OPTION2);
                totalInterest = (monthlyEMI * 12);
                repaymentAmount = principalAmount + totalInterest;
                InvestmentListBox.Items.Add(TERM_OPTION2 + " Years\t\t" + ROI_CATEG3_TERM_OPT2 + "\t" + monthlyEMI.ToString("0.00") + "\t" + totalInterest.ToString("0.00") + "\t" + repaymentAmount.ToString("0.00"));

                //Calculation of list variables for 3 Year Term
                monthlyEMI = MonthlyEMI(principalAmount, ROI_CATEG3_TERM_OPT3, TERM_OPTION3);
                totalInterest = (monthlyEMI * 12);
                repaymentAmount = principalAmount + totalInterest;

                InvestmentListBox.Items.Add(TERM_OPTION3 + " Years\t\t" + ROI_CATEG3_TERM_OPT3 + "\t" + monthlyEMI.ToString("0.00") + "\t" + totalInterest.ToString("0.00") + "\t" + repaymentAmount.ToString("0.00"));

                //Calculation of list variables for 4 Year Term
                monthlyEMI = MonthlyEMI(principalAmount, ROI_CATEG3_TERM_OPT4, TERM_OPTION4);
                totalInterest = (monthlyEMI * 12);
                repaymentAmount = principalAmount + totalInterest;
                

                InvestmentListBox.Items.Add(TERM_OPTION4 + " Years\t\t" + ROI_CATEG3_TERM_OPT4 + "\t" + monthlyEMI.ToString("0.00") + "\t" + totalInterest.ToString("0.00") + "\t" + repaymentAmount.ToString("0.00"));
            }
            else
            {
                MessageBox.Show("Please enter a valid Loan amount in the range of 40K to 80K");
                PrincipalTextBox.Focus();
            }
        }
        /*************************CODE FOR PROCEED BUTTON**********************************/
        private void ProceedButton_Click(object sender, EventArgs e)
        {
            int schemeSelected;
            schemeSelected = InvestmentListBox.SelectedIndex;

            if (!schemeSelected.Equals(-1))
            {
                decimal principalAmount;
                InvestorDetailsGroupBox.Enabled = true;

                trxID = GenerateTRXid();
                TrxIDLabel.Text=trxID;
                principalAmount = decimal.Parse(PrincipalTextBox.Text);
                if (principalAmount < 40000)
                switch (schemeSelected)
                {
                    case 0:
                        roiToFile=ROI_CATEG1_TERM_OPT1.ToString();
                        tenureToFiles = TERM_OPTION1.ToString();
                        loanRequestedToFile = principalAmount.ToString();
                        break;
                    case 1:
                            roiToFile=ROI_CATEG1_TERM_OPT2.ToString();
                            tenureToFiles = TERM_OPTION2.ToString();
                            loanRequestedToFile=principalAmount.ToString();
                            break;
                        case 2:
                            roiToFile=ROI_CATEG1_TERM_OPT3.ToString();
                            tenureToFiles = TERM_OPTION3.ToString();
                            loanRequestedToFile = principalAmount.ToString();
                            break;
                        case 3:
                            roiToFile = ROI_CATEG1_TERM_OPT4.ToString();
                            tenureToFiles=TERM_OPTION4.ToString();
                            loanRequestedToFile = principalAmount.ToString();
                            break;
                }
                else if(principalAmount>=40000 && principalAmount<80000)
                {
                    switch (schemeSelected)
                    {
                        case 0:
                            roiToFile = ROI_CATEG2_TERM_OPT1.ToString();
                            tenureToFiles = TERM_OPTION1.ToString();
                            loanRequestedToFile = principalAmount.ToString();
                            break;
                        case 1:
                            roiToFile = ROI_CATEG2_TERM_OPT2.ToString();
                            tenureToFiles = TERM_OPTION2.ToString();
                            loanRequestedToFile = principalAmount.ToString();
                            break;
                        case 2:
                            roiToFile = ROI_CATEG2_TERM_OPT3.ToString();
                            tenureToFiles = TERM_OPTION3.ToString();
                            loanRequestedToFile = principalAmount.ToString();
                            break;
                        case 3:
                            roiToFile = ROI_CATEG2_TERM_OPT4.ToString();
                            tenureToFiles = TERM_OPTION4.ToString();
                            loanRequestedToFile = principalAmount.ToString();
                            break;
                    }
                }
                else if(principalAmount>=80000 && principalAmount<100000)
                {
                    switch (schemeSelected)
                    {
                        case 0:
                            roiToFile = ROI_CATEG3_TERM_OPT1.ToString();
                            tenureToFiles = TERM_OPTION1.ToString();
                            loanRequestedToFile = principalAmount.ToString();
                            break;
                        case 1:
                            roiToFile = ROI_CATEG3_TERM_OPT2.ToString();
                            tenureToFiles = TERM_OPTION2.ToString();
                            loanRequestedToFile = principalAmount.ToString();
                            break;
                        case 2:
                            roiToFile = ROI_CATEG3_TERM_OPT3.ToString();
                            tenureToFiles = TERM_OPTION3.ToString();
                            loanRequestedToFile = principalAmount.ToString();
                            break;
                        case 3:
                            roiToFile = ROI_CATEG3_TERM_OPT4.ToString();
                            tenureToFiles = TERM_OPTION4.ToString();
                            loanRequestedToFile = principalAmount.ToString();
                            break;
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a loan tenure plan");
                InvestmentListBox.Focus();  
            }
        }
        /********************************CODE FOR SUBMIT********************************/
        private void SubmitButton_Click(object sender, EventArgs e)
        {
            nameToFile = ClientNameTextBox.Text;
            eirToFile = EIRCodeTextBox.Text;
            phoneNumToFile = PhoneNumberTextBox.Text;
            emailToFile = EmailTextBox.Text;

            DialogResult result;
            result = MessageBox.Show("Please check the below details. Click 'YES' if you wish to proceed \n" +
                "ClientName: " + nameToFile +"\n"+
                "Loan Requested: " + loanRequestedToFile + "\n" +
                "Phone Number: " + phoneNumToFile + "\n" +
                "Email: " + emailToFile + "\n" +
                "Monthly EMI: " + monthlyEMI + "\n" +
                "Rate of Interest:" + roiToFile, "Please check details", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
                {
                    nameToFile = ClientNameTextBox.Text;
                    eirToFile = EIRCodeTextBox.Text;
                    phoneNumToFile = PhoneNumberTextBox.Text;
                    emailToFile = EmailTextBox.Text;
                    WriteToFile();
                    ResetFormForNewTRX();
                }
            else
                {
                    //pass
                }       
        }
        //*********************** CODE FOR SEARCH FUNCTIONALITY***********************/
        private void SearchButton_Click(object sender, EventArgs e)
        {
            if (TransactionIdRadioButton.Checked)
            {
                string searchTRXid;
                searchTRXid= SearchTextBox.Text;
                SearchByTrxId(searchTRXid);
            }
            else
            {
                string searchEmail;
                searchEmail= SearchTextBox.Text;
                SearchListBox.Items.Clear();
                SearchByEmail(searchEmail);
            }

        }
        private void ClearButtonSearch_Click(object sender, EventArgs e)
        {
            SearchListBox.Items.Clear();
            TransactionIdRadioButton.Checked = true;
            SearchTextBox.Clear();

        }
        /***********************************CODE FOR SUMMARY**********************************/

        private void SummaryButton_Click(object sender, EventArgs e)
        {


        }
        /****************************USER DEFINED FUNCTIONS**********************************/
        private void ClearButtonInvestment_Click(object sender, EventArgs e)
        {
            InvestmentListBox.Items.Clear();
        }

        static decimal MonthlyEMI(decimal p, decimal r, int n)
        {
            decimal totalAmountPayable = 0, monthlyEMI, mf;
            decimal loanValue = p;
            mf = (1 + (r / 100));
            for (int i = 0; i < n; i++)
            {
                loanValue = (loanValue *mf );
                totalAmountPayable = loanValue;
            }
            monthlyEMI = (totalAmountPayable / 12);
            return monthlyEMI;
        }
        public void WriteToFile()
        { 
            StreamWriter dataWriter = File.AppendText(FILEPATH);
            using (dataWriter)
            {
                dataWriter.WriteLine(trxID);
                dataWriter.WriteLine(emailToFile);
                dataWriter.WriteLine(nameToFile);
                dataWriter.WriteLine(eirToFile);
                dataWriter.WriteLine("**********");
                dataWriter.Close();
            }
        }
        public void ResetFormForNewTRX()
        {
            PrincipalTextBox.Clear();
            InvestmentListBox.Items.Clear();
            ClientNameTextBox.Clear();
            EIRCodeTextBox.Clear();
            PhoneNumberTextBox.Clear();
            EmailTextBox.Clear();
            TrxIDLabel.Text="";
        }

        public string GenerateTRXid()
        {
            Random trxID = new Random();
            string TrxIDNumber;
            TrxIDNumber = trxID.Next(99999).ToString();
            return TrxIDNumber;
        }

        public void SearchByTrxId(string trxIDvalue)
        {
            StreamReader dataReader=new StreamReader(FILEPATH);
            using (dataReader)
            {
                string currentLine = dataReader.ReadLine();
                bool valueFound=false;
                while (valueFound==false)
                {
                    if (currentLine.Equals(trxIDvalue) && !dataReader.EndOfStream)
                    {
                        SearchListBox.Items.Add(trxIDvalue);
                        valueFound=true;
                    }
                    else if(!currentLine.Equals(trxIDvalue) && !dataReader.EndOfStream)
                    {
                        currentLine = dataReader.ReadLine();
                    }
                    else if(!currentLine.Equals(trxIDvalue) && dataReader.EndOfStream)
                    {
                        MessageBox.Show("Not found");
                        valueFound = true;
                    }
                }
                
            }
        }
        public void SearchByEmail(string emailValue)
        {
            string currentLine, trxID;
            StreamReader dataReader = new StreamReader(FILEPATH);
            bool recordsOver = false;
            bool recordFound=false;
            using (dataReader)
            {
                while (recordsOver == false)
                {
                    trxID = dataReader.ReadLine();
                    currentLine = dataReader.ReadLine();
                    if (currentLine==emailValue)
                    {
                        SearchListBox.Items.Add(trxID);
                        SearchListBox.Items.Add(emailValue);
                        SearchListBox.Items.Add(dataReader.ReadLine());
                        SearchListBox.Items.Add(dataReader.ReadLine());
                        SearchListBox.Items.Add(dataReader.ReadLine());
                        recordFound = true;
                    }
                    else if(!(currentLine==emailValue) && !dataReader.EndOfStream)
                    {
                        currentLine=dataReader.ReadLine();
                        currentLine=dataReader.ReadLine();
                        currentLine=dataReader.ReadLine();
                    }
                    else if (recordFound==false && dataReader.EndOfStream)
                    {
                        MessageBox.Show("Not found");
                        SearchTextBox.Focus();
                        recordsOver = true;
                    }
                    else if (recordFound == true && dataReader.EndOfStream)
                    {
                        recordsOver = true;
                    }
                }
            }
        }
    }
}

