using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
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
        const int TERM_OPTION1 = 1, TERM_OPTION2 = 3, TERM_OPTION3 = 5, TERM_OPTION4 = 7, MONTHS=12;
        const decimal ROI_CATEG1_TERM_OPT1 = 6.00m, ROI_CATEG1_TERM_OPT2 = 6.50m, ROI_CATEG1_TERM_OPT3 = 7.00m, ROI_CATEG1_TERM_OPT4 = 7.50m, ROI_CATEG2_TERM_OPT1 = 8.00m, ROI_CATEG2_TERM_OPT2 = 8.50m, ROI_CATEG2_TERM_OPT3 = 9.00m, ROI_CATEG2_TERM_OPT4 = 9.50m, ROI_CATEG3_TERM_OPT1 = 8.50m, ROI_CATEG3_TERM_OPT2 = 8.75m, ROI_CATEG3_TERM_OPT3 = 9.10m, ROI_CATEG3_TERM_OPT4 = 9.25m;

        string emailToFile, nameToFile, eirToFile, phoneNumToFile, loanRequestedToFile, emiToFile, tenureToFiles, roiToFile, trxID;



        decimal monthlyEMItoFile;
        string FILEPATH = @"D:\Assignments\Business Applicationn Programming\Assignment 3\Test.txt";

        

        const string ACTUAL_PASSOWRD = "123";
        const int MAX_ATTEMPTS = 3;
        int attemptCount = 1;

        private void LoginButton_Click(object sender, EventArgs e)
        {

            string enteredPassword;
            enteredPassword = PasswordTextBox.Text;

            if (!(enteredPassword.Equals("")))
            {
                if (enteredPassword.Equals(ACTUAL_PASSOWRD))
                {
                    InvestmentPanelScreen2.Visible = true;
                    SearchGroupBox.Visible = true;
                    SummaryGroupBox.Visible = true;
                }
                else if (attemptCount < MAX_ATTEMPTS)
                {
                    MessageBox.Show("Incorrect Password\n" + (MAX_ATTEMPTS - attemptCount) + " attempts out of " + MAX_ATTEMPTS + " remaining", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    PasswordTextBox.Focus();
                    PasswordTextBox.SelectAll();
                    attemptCount++;
                }
                else
                {
                    MessageBox.Show("Maximum Permissible attemps exhausted. Application will now Exit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("The Password Field cannot be Empty", "Password not Entered");
            }

            

        }
        private void DisplayButton_Click(object sender, EventArgs e)
        {
            decimal principalAmount, totalInterest, repaymentAmount;
            try
            {
                principalAmount = int.Parse(PrincipalTextBox.Text);
                InvestmentListBox.Items.Clear();
                
                if (principalAmount <= 40000)
                {
                    //Calculation of list variables for 1 Year Term
                    monthlyEMItoFile = MonthlyEMI(principalAmount, ROI_CATEG1_TERM_OPT1, TERM_OPTION1);
                    totalInterest = ((monthlyEMItoFile * MONTHS) - principalAmount);
                    repaymentAmount = principalAmount + totalInterest;
                    InvestmentListBox.Items.Add(TERM_OPTION1 + " Years\t\t" + ROI_CATEG1_TERM_OPT1 + "\t" + monthlyEMItoFile.ToString("0.00") + "\t" + totalInterest.ToString("0.00") + "\t" + repaymentAmount.ToString("0.00"));

                    //Calculation of list variables for 2 Year Term
                    monthlyEMItoFile = MonthlyEMI(principalAmount, ROI_CATEG1_TERM_OPT2, TERM_OPTION2);
                    totalInterest = ((monthlyEMItoFile * MONTHS) - principalAmount);
                    repaymentAmount = principalAmount + totalInterest;
                    InvestmentListBox.Items.Add(TERM_OPTION2 + " Years\t\t" + ROI_CATEG1_TERM_OPT2 + "\t" + monthlyEMItoFile.ToString("0.00") + "\t" + totalInterest.ToString("0.00") + "\t" + repaymentAmount.ToString("0.00"));

                    //Calculation of list variables for 3 Year Term
                    monthlyEMItoFile = MonthlyEMI(principalAmount, ROI_CATEG1_TERM_OPT3, TERM_OPTION3);
                    totalInterest = ((monthlyEMItoFile * MONTHS) - principalAmount);
                    repaymentAmount = principalAmount + totalInterest;

                    InvestmentListBox.Items.Add(TERM_OPTION3 + " Years\t\t" + ROI_CATEG1_TERM_OPT3 + "\t" + monthlyEMItoFile.ToString("0.00") + "\t" + totalInterest.ToString("0.00") + "\t" + repaymentAmount.ToString("0.00"));

                    //Calculation of list variables for 4 Year Term
                    totalInterest = ((monthlyEMItoFile * MONTHS) - principalAmount);
                    repaymentAmount = principalAmount + totalInterest;
                    monthlyEMItoFile = MonthlyEMI(principalAmount, ROI_CATEG1_TERM_OPT4, TERM_OPTION4);

                    InvestmentListBox.Items.Add(TERM_OPTION4 + " Years\t\t" + ROI_CATEG1_TERM_OPT4 + "\t" + monthlyEMItoFile.ToString("0.00") + "\t" + totalInterest.ToString("0.00") + "\t" + repaymentAmount.ToString("0.00"));

                }
                else if (principalAmount > 40000 && principalAmount <= 60000)
                {
                    //Calculation of list variables for 1 Year Term
                    monthlyEMItoFile = MonthlyEMI(principalAmount, ROI_CATEG2_TERM_OPT1, TERM_OPTION1);
                    totalInterest = (monthlyEMItoFile * 12);
                    repaymentAmount = principalAmount + totalInterest;
                    InvestmentListBox.Items.Add(TERM_OPTION1 + " Years\t\t" + ROI_CATEG2_TERM_OPT1 + "\t" + monthlyEMItoFile.ToString("0.00") + "\t" + totalInterest.ToString("0.00") + "\t" + repaymentAmount.ToString("0.00"));

                    //Calculation of list variables for 2 Year Term
                    monthlyEMItoFile = MonthlyEMI(principalAmount, ROI_CATEG2_TERM_OPT2, TERM_OPTION2);
                    totalInterest = (monthlyEMItoFile * 12);
                    repaymentAmount = principalAmount + totalInterest;
                    InvestmentListBox.Items.Add(TERM_OPTION2 + " Years\t\t" + ROI_CATEG2_TERM_OPT2 + "\t" + monthlyEMItoFile.ToString("0.00") + "\t" + totalInterest.ToString("0.00") + "\t" + repaymentAmount.ToString("0.00"));

                    //Calculation of list variables for 3 Year Term
                    monthlyEMItoFile = MonthlyEMI(principalAmount, ROI_CATEG2_TERM_OPT3, TERM_OPTION3);
                    totalInterest = (monthlyEMItoFile * 12);
                    repaymentAmount = principalAmount + totalInterest;

                    InvestmentListBox.Items.Add(TERM_OPTION3 + " Years\t\t" + ROI_CATEG2_TERM_OPT3 + "\t" + monthlyEMItoFile.ToString("0.00") + "\t" + totalInterest.ToString("0.00") + "\t" + repaymentAmount.ToString("0.00"));

                    //Calculation of list variables for 4 Year Term
                    monthlyEMItoFile = MonthlyEMI(principalAmount, ROI_CATEG2_TERM_OPT4, TERM_OPTION4);
                    totalInterest = (monthlyEMItoFile * 12);
                    repaymentAmount = principalAmount + totalInterest;


                    InvestmentListBox.Items.Add(TERM_OPTION4 + " Years\t\t" + ROI_CATEG2_TERM_OPT4 + "\t" + monthlyEMItoFile.ToString("0.00") + "\t" + totalInterest.ToString("0.00") + "\t" + repaymentAmount.ToString("0.00"));
                }
                else if (principalAmount > 60000 && principalAmount < 80000)
                {
                    //Calculation of list variables for 1 Year Term
                    monthlyEMItoFile = MonthlyEMI(principalAmount, ROI_CATEG3_TERM_OPT1, TERM_OPTION1);
                    totalInterest = (monthlyEMItoFile * 12);
                    repaymentAmount = principalAmount + totalInterest;
                    InvestmentListBox.Items.Add(TERM_OPTION1 + " Years\t\t" + ROI_CATEG3_TERM_OPT1 + "\t" + monthlyEMItoFile.ToString("0.00") + "\t" + totalInterest.ToString("0.00") + "\t" + repaymentAmount.ToString("0.00"));

                    //Calculation of list variables for 2 Year Term
                    monthlyEMItoFile = MonthlyEMI(principalAmount, ROI_CATEG3_TERM_OPT2, TERM_OPTION2);
                    totalInterest = (monthlyEMItoFile * 12);
                    repaymentAmount = principalAmount + totalInterest;
                    InvestmentListBox.Items.Add(TERM_OPTION2 + " Years\t\t" + ROI_CATEG3_TERM_OPT2 + "\t" + monthlyEMItoFile.ToString("0.00") + "\t" + totalInterest.ToString("0.00") + "\t" + repaymentAmount.ToString("0.00"));

                    //Calculation of list variables for 3 Year Term
                    monthlyEMItoFile = MonthlyEMI(principalAmount, ROI_CATEG3_TERM_OPT3, TERM_OPTION3);
                    totalInterest = (monthlyEMItoFile * 12);
                    repaymentAmount = principalAmount + totalInterest;

                    InvestmentListBox.Items.Add(TERM_OPTION3 + " Years\t\t" + ROI_CATEG3_TERM_OPT3 + "\t" + monthlyEMItoFile.ToString("0.00") + "\t" + totalInterest.ToString("0.00") + "\t" + repaymentAmount.ToString("0.00"));

                    //Calculation of list variables for 4 Year Term
                    monthlyEMItoFile = MonthlyEMI(principalAmount, ROI_CATEG3_TERM_OPT4, TERM_OPTION4);
                    totalInterest = (monthlyEMItoFile * 12);
                    repaymentAmount = principalAmount + totalInterest;


                    InvestmentListBox.Items.Add(TERM_OPTION4 + " Years\t\t" + ROI_CATEG3_TERM_OPT4 + "\t" + monthlyEMItoFile.ToString("0.00") + "\t" + totalInterest.ToString("0.00") + "\t" + repaymentAmount.ToString("0.00"));
                }
                else
                {
                    MessageBox.Show("Please enter a valid Loan amount in the range of 40K to 80K");
                    PrincipalTextBox.Focus();
                    PrincipalTextBox.SelectAll();
                }
            }
            catch
            {
                MessageBox.Show("Please enter a valid input");
                PrincipalTextBox.Focus();
                PrincipalTextBox.SelectAll();
            }
            
        }

        /*************************CODE FOR PROCEED BUTTON**********************************/
        private void InvestmentListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProceedButton.Enabled = true;
        }
        
        private void ProceedButton_Click(object sender, EventArgs e)
        {
            int schemeSelected;
            schemeSelected = InvestmentListBox.SelectedIndex;

            if (!schemeSelected.Equals(-1))
            {
                decimal principalAmount;
                InvestorDetailsGroupBox.Enabled = true;

                trxID = GenerateTRXid();
                TrxIDLabel.Text = trxID;
                try
                {
                    principalAmount = decimal.Parse(PrincipalTextBox.Text);
                    if (principalAmount < 40000)
                        switch (schemeSelected)
                        {
                            case 0:
                                roiToFile = ROI_CATEG1_TERM_OPT1.ToString();
                                tenureToFiles = (TERM_OPTION1 * MONTHS).ToString();
                                loanRequestedToFile = principalAmount.ToString();
                                break;
                            case 1:
                                roiToFile = ROI_CATEG1_TERM_OPT2.ToString();
                                tenureToFiles = (TERM_OPTION2 * MONTHS).ToString();
                                loanRequestedToFile = principalAmount.ToString();
                                break;
                            case 2:
                                roiToFile = ROI_CATEG1_TERM_OPT3.ToString();
                                tenureToFiles = (TERM_OPTION3 * MONTHS).ToString();
                                loanRequestedToFile = principalAmount.ToString();
                                break;
                            case 3:
                                roiToFile = ROI_CATEG1_TERM_OPT4.ToString();
                                tenureToFiles = (TERM_OPTION4 * MONTHS).ToString();
                                loanRequestedToFile = principalAmount.ToString();
                                break;
                        }
                    else if (principalAmount >= 40000 && principalAmount < 80000)
                    {
                        switch (schemeSelected)
                        {
                            case 0:
                                roiToFile = ROI_CATEG2_TERM_OPT1.ToString();
                                tenureToFiles = (TERM_OPTION1 * MONTHS).ToString();
                                loanRequestedToFile = principalAmount.ToString();
                                break;
                            case 1:
                                roiToFile = ROI_CATEG2_TERM_OPT2.ToString();
                                tenureToFiles = (TERM_OPTION2 * MONTHS).ToString();
                                loanRequestedToFile = principalAmount.ToString();
                                break;
                            case 2:
                                roiToFile = ROI_CATEG2_TERM_OPT3.ToString();
                                tenureToFiles = (TERM_OPTION3 * MONTHS).ToString();
                                loanRequestedToFile = principalAmount.ToString();
                                break;
                            case 3:
                                roiToFile = ROI_CATEG2_TERM_OPT4.ToString();
                                tenureToFiles = (TERM_OPTION4 * MONTHS).ToString();
                                loanRequestedToFile = principalAmount.ToString();
                                break;
                        }
                    }
                    else if (principalAmount >= 80000 && principalAmount < 100000)
                    {
                        switch (schemeSelected)
                        {
                            case 0:
                                roiToFile = ROI_CATEG3_TERM_OPT1.ToString();
                                tenureToFiles = (TERM_OPTION1 * MONTHS).ToString();
                                loanRequestedToFile = principalAmount.ToString();
                                break;
                            case 1:
                                roiToFile = ROI_CATEG3_TERM_OPT2.ToString();
                                tenureToFiles = (TERM_OPTION2 * MONTHS).ToString();
                                loanRequestedToFile = principalAmount.ToString();
                                break;
                            case 2:
                                roiToFile = ROI_CATEG3_TERM_OPT3.ToString();
                                tenureToFiles = (TERM_OPTION3 * MONTHS).ToString();
                                loanRequestedToFile = principalAmount.ToString();
                                break;
                            case 3:
                                roiToFile = ROI_CATEG3_TERM_OPT4.ToString();
                                tenureToFiles = (TERM_OPTION4 * MONTHS).ToString();
                                loanRequestedToFile = principalAmount.ToString();
                                break;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select a loan tenure plan");
                        InvestmentListBox.Focus();
                    }
                }
                catch
                {
                    MessageBox.Show("Please enter a valid value", "Error");
                    PrincipalTextBox.Focus();
                }



            }   
        }
        /********************************CODE FOR SUBMIT********************************/
        private void SubmitButton_Click(object sender, EventArgs e)
        {
            nameToFile = ClientNameTextBox.Text;
            eirToFile = EIRCodeTextBox.Text;
            phoneNumToFile = PhoneNumberTextBox.Text;
            emailToFile = EmailTextBox.Text;
            bool validationCheck, checkEmailFormat, checkContactFormat;
            checkEmailFormat = emailValidation(emailToFile);
            checkContactFormat=checkNumeric(phoneNumToFile);
            
            validationCheck=InvestorValidation();
            if (validationCheck)
            {
                DialogResult result;
                result = MessageBox.Show("Please check the below details. Click 'YES' if you wish to proceed \n" +
                    "ClientName: " + nameToFile + "\n" +
                    "Loan Requested: " + loanRequestedToFile + "\n" +
                    "Phone Number: " + phoneNumToFile + "\n" +
                    "Email: " + emailToFile + "\n" +
                    "Monthly EMI: " + monthlyEMItoFile + "\n" +
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

            
        }
        //*********************** CODE FOR SEARCH FUNCTIONALITY***********************/
        private void SearchButton_Click(object sender, EventArgs e)
        {
            SearchListBox.Items.Clear();
            if (TransactionIdRadioButton.Checked)
            {
                string searchTRXid;
                searchTRXid = SearchTextBox.Text;
                SearchByTrxId(searchTRXid);
            }
            else
            {
                string searchEmail;
                searchEmail = SearchTextBox.Text;
                
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
            SumaryListBox.Items.Clear();   
            SumaryListBox.Items.Add("Total Loaned:\t"+TotalAmountBorrowedCalculation().ToString());
            SumaryListBox.Items.Add("Total Interest:\t"+TotalInterestEarned().ToString());
            SumaryListBox.Items.Add("Average Loan:\t"+AverageAmountBorrowed().ToString());
            SumaryListBox.Items.Add("Average Interest:\t"+AverageInterestEarned().ToString());
            SumaryListBox.Items.Add("Average Months:\t"+AverageMonths().ToString());
        }
       
        private void ClearButtonInvestment_Click(object sender, EventArgs e)
        {
            InvestmentListBox.Items.Clear();
            PrincipalTextBox.Clear();
            TrxIDLabel.Text = "";
            ProceedButton.Enabled = false;
        }
        /****************************USER DEFINED FUNCTIONS**********************************/
        static decimal MonthlyEMI(decimal p, decimal r, int n)
        {
            decimal totalAmountPayable = 0, monthlyEMI, mf;
            decimal loanValue = p;
            mf = (1 + (r / 100));
            for (int i = 0; i < n; i++)
            {
                loanValue = (loanValue * mf);
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
                dataWriter.WriteLine(phoneNumToFile);
                dataWriter.WriteLine(loanRequestedToFile);
                dataWriter.WriteLine(monthlyEMItoFile);
                dataWriter.WriteLine(tenureToFiles);
                dataWriter.WriteLine(roiToFile+"%");
                
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
            TrxIDLabel.Text = "";
            ProceedButton.Enabled = false;
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
            StreamReader dataReader = new StreamReader(FILEPATH);
            using (dataReader)
            {
                string currentLine = dataReader.ReadLine();
                bool valueFound = false;
                while (valueFound == false)
                {
                    if (currentLine.Equals(trxIDvalue) && !dataReader.EndOfStream)
                    {
                        SearchListBox.Items.Add("Transaction ID:\t" + trxIDvalue);
                        SearchListBox.Items.Add("Customer Email:\t"+dataReader.ReadLine());
                        SearchListBox.Items.Add("Customer Name:\t"+dataReader.ReadLine());
                        SearchListBox.Items.Add("EIR CODE:\t"+dataReader.ReadLine());
                        SearchListBox.Items.Add("Phone Number:\t"+dataReader.ReadLine());
                        SearchListBox.Items.Add("Loan Sanctioned:\t"+dataReader.ReadLine());
                        SearchListBox.Items.Add("Monthly EMI:\t"+dataReader.ReadLine());
                        SearchListBox.Items.Add("Tenure in Months:\t"+dataReader.ReadLine());
                        SearchListBox.Items.Add("Rate of Interest:\t"+dataReader.ReadLine());
                        valueFound = true;
                    }
                    else if (!currentLine.Equals(trxIDvalue) && !dataReader.EndOfStream)
                    {
                        currentLine = dataReader.ReadLine();
                    }
                    else if (!currentLine.Equals(trxIDvalue) && dataReader.EndOfStream)
                    {
                        MessageBox.Show("Not found");
                        valueFound = true;
                    }
                }
                dataReader.Close();

            }
        }
        public void SearchByEmail(string emailValue)
        {
            string currentLine, trxID;
            StreamReader dataReader = new StreamReader(FILEPATH);
            bool recordsOver = false;
            bool recordFound = false;
            using (dataReader)
            {
                while (recordsOver == false)
                {
                    trxID = dataReader.ReadLine();
                    currentLine = dataReader.ReadLine();
                    if (currentLine == emailValue)
                    {
                        SearchListBox.Items.Add("Transaction ID:\t" + trxID);
                        SearchListBox.Items.Add("Customer Email:\t" + currentLine);
                        SearchListBox.Items.Add("Customer Name:\t" + dataReader.ReadLine());
                        SearchListBox.Items.Add("EIR CODE:\t" + dataReader.ReadLine());
                        SearchListBox.Items.Add("Phone Number:\t" + dataReader.ReadLine());
                        SearchListBox.Items.Add("Loan Sanctioned:\t" + dataReader.ReadLine());
                        SearchListBox.Items.Add("Monthly EMI:\t" + dataReader.ReadLine());
                        SearchListBox.Items.Add("Tenure in Months:\t" + dataReader.ReadLine());
                        SearchListBox.Items.Add("Rate of Interest:\t" + dataReader.ReadLine());
                        recordFound = true;
                    }
                    else if (!(currentLine == emailValue) && !dataReader.EndOfStream)
                    {
                        currentLine = dataReader.ReadLine();
                        currentLine = dataReader.ReadLine();
                        currentLine = dataReader.ReadLine();
                    }
                    else if (recordFound == false && dataReader.EndOfStream)
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
                dataReader.Close();
            }
            
        }
        public decimal TotalAmountBorrowedCalculation()
        {
            decimal totalAmountBorrowed = 0;
            StreamReader dataReader = new StreamReader(FILEPATH);
            bool recordsOver = false;
            using (dataReader)
            {
                while (recordsOver == false)
                {
                    if (!dataReader.EndOfStream)
                    {
                        for (int i = 1; i <= 5; i++)
                        {
                            dataReader.ReadLine();
                        }
                        totalAmountBorrowed = totalAmountBorrowed + decimal.Parse(dataReader.ReadLine());
                        for (int i = 1; i <= 4; i++)
                        {
                            dataReader.ReadLine();
                        }
                    }
                    else
                    {
                        recordsOver = true;
                    }
                }
            }
            return totalAmountBorrowed;
        }
        public decimal AverageAmountBorrowed()
        {
            decimal AvgAmountBorrowed = 0;
            int TransactionCount = 0;
            StreamReader dataReader = new StreamReader(FILEPATH);
            bool recordsOver = false;
            using (dataReader)
            {
                while (recordsOver == false)
                {
                    if (!dataReader.EndOfStream)
                    {
                        for (int i = 1; i <= 5; i++)
                        {
                            dataReader.ReadLine();
                        }
                        AvgAmountBorrowed = AvgAmountBorrowed + decimal.Parse(dataReader.ReadLine());
                        TransactionCount++;
                        for (int i = 1; i <= 4; i++)
                        {
                            dataReader.ReadLine();
                        }
                    }
                    else
                    {
                        recordsOver = true;
                    }

                }
                dataReader.Close();
                return AvgAmountBorrowed / TransactionCount;
            }
        }
        public decimal TotalInterestEarned()
        {
            decimal totalInterestEarned = 0;
            StreamReader dataReader = new StreamReader(FILEPATH);
            bool recordsOver = false;
            using (dataReader)
            {
                while (recordsOver == false)
                {
                    if (!dataReader.EndOfStream)
                    {
                        for (int i = 1; i <= 6; i++)
                        {
                            dataReader.ReadLine();
                        }
                        string currentValue=dataReader.ReadLine();
                        totalInterestEarned = totalInterestEarned + decimal.Parse(currentValue);
                        for (int i = 1; i <= 3; i++)
                        {
                            dataReader.ReadLine();
                        }
                    }
                    else
                    {
                        recordsOver = true;
                    }

                }
                dataReader.Close();
            }
            return totalInterestEarned;
        }
        public decimal AverageInterestEarned()
        {
            decimal AvgInterestEarned = 0;
            int TransactionCount = 0;
            StreamReader dataReader = new StreamReader(FILEPATH);
            bool recordsOver = false;
            using (dataReader)
            {
                while (recordsOver == false)
                {
                    if (!dataReader.EndOfStream)
                    {
                        for (int i = 1; i <= 6; i++)
                        {
                            dataReader.ReadLine();
                        }
                        AvgInterestEarned = AvgInterestEarned + decimal.Parse(dataReader.ReadLine());
                        TransactionCount++;
                        for (int i = 1; i <= 3; i++)
                        {
                            dataReader.ReadLine();
                        }
                    }
                    else
                    {
                        recordsOver = true;
                    }

                }
                dataReader.Close();
                return AvgInterestEarned / TransactionCount;
            }

        }
        public decimal AverageMonths()
        {
            decimal AvgMonths = 0;
            int TransactionCount = 0;
            StreamReader dataReader = new StreamReader(FILEPATH);
            bool recordsOver = false;
            using (dataReader)
            {
                while (recordsOver == false)
                {
                    if (!dataReader.EndOfStream)
                    {
                        for (int i = 1; i <= 7; i++)
                        {
                            string currentValue=dataReader.ReadLine();
                        }
                        AvgMonths = AvgMonths + decimal.Parse(dataReader.ReadLine());
                        TransactionCount++;
                        for (int i = 1; i <= 2; i++)
                        {
                            string currentValue=dataReader.ReadLine();
                        }
                    }
                    else
                    {
                        recordsOver = true;
                    }

                }
                dataReader.Close();
                return AvgMonths / TransactionCount;
            }

        }
        public bool InvestorValidation()
        {
            string Name=ClientNameTextBox.Text;
            string Eir = EIRCodeTextBox.Text;
            string phoneNum=PhoneNumberTextBox.Text;
            string email=EmailTextBox.Text;
            bool checkEmailFormat, checkContactFormat;
            checkEmailFormat = emailValidation(email);
            checkContactFormat = checkNumeric(phoneNum);
            int i = 0;
            bool validation=true;
            while (i ==0)
            {
                if (Name == "")
                {
                    MessageBox.Show("Please Enter a Name", "Name Required");
                    ClientNameTextBox.Focus();
                    validation = false;
                    break;
                }
                else if (Eir == "")
                {
                    MessageBox.Show("Please Enter an EIR Code", "EIR Required");
                    EIRCodeTextBox.Focus();
                    validation = false;
                    break;
                }
                else if (phoneNum == "")
                {

                    MessageBox.Show("Please Enter a Phone Number", "Contact Required");
                    PhoneNumberTextBox.Focus();
                    validation = false;
                    break;
                }
                else if(phoneNum!="" && checkContactFormat == false)
                {
                    MessageBox.Show("Enter valid contact number");
                    PhoneNumberTextBox.Focus();
                    validation = false;
                    break;
                }

                else if (email.Equals(""))
                {
                    MessageBox.Show("Please Enter an email", "Email Required");
                    PhoneNumberTextBox.Focus();
                    validation = false;
                    break;
                }
                else if (email!= "" && checkEmailFormat == false)
                {
                    MessageBox.Show("Enter valid Email Address");
                    EmailTextBox.Focus();
                    validation = false;
                    break;
                }
                else
                {
                    validation = true;
                    break;
                }
                
                
            }
            return validation;

        }

        public bool emailValidation(string emailEntered)
        {
            try
            {
                MailAddress checkMailFormat = new MailAddress(emailEntered);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool checkNumeric(string TestString)
        {
            try
            {
                int.Parse(TestString);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

