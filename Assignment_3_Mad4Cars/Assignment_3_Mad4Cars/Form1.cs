﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
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
        const int TERM_OPTION1 = 1, TERM_OPTION2 = 3, TERM_OPTION3 = 5, TERM_OPTION4 = 7, MONTHS = 12;
        const decimal ROI_CATEG1_TERM_OPT1 = 6.00m, ROI_CATEG1_TERM_OPT2 = 6.50m, ROI_CATEG1_TERM_OPT3 = 7.00m, ROI_CATEG1_TERM_OPT4 = 7.50m, ROI_CATEG2_TERM_OPT1 = 8.00m, ROI_CATEG2_TERM_OPT2 = 8.50m, ROI_CATEG2_TERM_OPT3 = 9.00m, ROI_CATEG2_TERM_OPT4 = 9.50m, ROI_CATEG3_TERM_OPT1 = 8.50m, ROI_CATEG3_TERM_OPT2 = 8.75m, ROI_CATEG3_TERM_OPT3 = 9.10m, ROI_CATEG3_TERM_OPT4 = 9.25m;
        const string FILEPATH = "Customer_Data.txt";
        const string ACTUAL_PASSOWRD = "123";
        string emailToFile, nameToFile, eirToFile, phoneNumToFile, loanRequestedToFile, emiToFile, tenureToFiles, roiToFile, trxID;
        decimal monthlyEMItoFile;
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
                    SignOutButton.Visible = true;
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
                InvestmentListBox.Enabled = true;
                InvestmentListBox.Items.Clear();
                
                ModifySelectionButton.Enabled = false;

                if (principalAmount >=10000 && principalAmount<= 40000)
                {
                    //Calculation of list variables for 1 Year Term
                    monthlyEMItoFile = MonthlyEMI(principalAmount, ROI_CATEG1_TERM_OPT1, TERM_OPTION1);
                    totalInterest = ((monthlyEMItoFile * MONTHS) - principalAmount);
                    repaymentAmount = principalAmount + totalInterest;
                    InvestmentListBox.Items.Add(TERM_OPTION1 + " Years\t\t" + ROI_CATEG1_TERM_OPT1+" %" + "\t\t" + monthlyEMItoFile.ToString("C2"
                        ) + "\t\t" + totalInterest.ToString("C2") + "\t\t" + repaymentAmount.ToString("C2"));

                    //Calculation of list variables for 3 Year Term
                    monthlyEMItoFile = MonthlyEMI(principalAmount, ROI_CATEG1_TERM_OPT2, TERM_OPTION2);
                    totalInterest = ((monthlyEMItoFile * MONTHS) - principalAmount);
                    repaymentAmount = principalAmount + totalInterest;
                    InvestmentListBox.Items.Add(TERM_OPTION2 + " Years\t\t" + ROI_CATEG1_TERM_OPT2 + " %" + "\t\t" + monthlyEMItoFile.ToString("C2") + "\t\t" + totalInterest.ToString("C2") + "\t\t" + repaymentAmount.ToString("C2"));

                    //Calculation of list variables for 5 Year Term
                    monthlyEMItoFile = MonthlyEMI(principalAmount, ROI_CATEG1_TERM_OPT3, TERM_OPTION3);
                    totalInterest = ((monthlyEMItoFile * MONTHS) - principalAmount);
                    repaymentAmount = principalAmount + totalInterest;

                    InvestmentListBox.Items.Add(TERM_OPTION3 + " Years\t\t" + ROI_CATEG1_TERM_OPT3 + " %" + "\t\t" + monthlyEMItoFile.ToString("C2") + "\t\t" + totalInterest.ToString("C2") + "\t\t" + repaymentAmount.ToString("C2"));

                    //Calculation of list variables for 7 Year Term
                    monthlyEMItoFile = MonthlyEMI(principalAmount, ROI_CATEG1_TERM_OPT4, TERM_OPTION4);
                    totalInterest = ((monthlyEMItoFile * MONTHS) - principalAmount);
                    repaymentAmount = principalAmount + totalInterest;
                    

                    InvestmentListBox.Items.Add(TERM_OPTION4 + " Years\t\t" + ROI_CATEG1_TERM_OPT4 + " %" + "\t\t" + monthlyEMItoFile.ToString("C2") + "\t\t" + totalInterest.ToString("C2") + "\t\t" + repaymentAmount.ToString("C2"));

                }
                else if (principalAmount > 40000 && principalAmount <= 80000)
                {
                    //Calculation of list variables for 1 Year Term
                    monthlyEMItoFile = MonthlyEMI(principalAmount, ROI_CATEG2_TERM_OPT1, TERM_OPTION1);
                    totalInterest = (monthlyEMItoFile * 12);
                    repaymentAmount = principalAmount + totalInterest;
                    InvestmentListBox.Items.Add(TERM_OPTION1 + " Years\t\t" + ROI_CATEG2_TERM_OPT1 + " %" + "\t\t" + monthlyEMItoFile.ToString("C2") + "\t\t" + totalInterest.ToString("C2") + "\t\t" + repaymentAmount.ToString("C2"));

                    //Calculation of list variables for 2 Year Term
                    monthlyEMItoFile = MonthlyEMI(principalAmount, ROI_CATEG2_TERM_OPT2, TERM_OPTION2);
                    totalInterest = (monthlyEMItoFile * 12);
                    repaymentAmount = principalAmount + totalInterest;
                    InvestmentListBox.Items.Add(TERM_OPTION2 + " Years\t\t" + ROI_CATEG2_TERM_OPT2 + " %" + "\t\t" + monthlyEMItoFile.ToString("C2") + "\t\t" + totalInterest.ToString("C2") + "\t\t" + repaymentAmount.ToString("C2"));

                    //Calculation of list variables for 3 Year Term
                    monthlyEMItoFile = MonthlyEMI(principalAmount, ROI_CATEG2_TERM_OPT3, TERM_OPTION3);
                    totalInterest = (monthlyEMItoFile * 12);
                    repaymentAmount = principalAmount + totalInterest;

                    InvestmentListBox.Items.Add(TERM_OPTION3 + " Years\t\t" + ROI_CATEG2_TERM_OPT3 + " %" + "\t\t" + monthlyEMItoFile.ToString("C2") + "\t\t" + totalInterest.ToString("C2") + "\t\t" + repaymentAmount.ToString("C2"));

                    //Calculation of list variables for 4 Year Term
                    monthlyEMItoFile = MonthlyEMI(principalAmount, ROI_CATEG2_TERM_OPT4, TERM_OPTION4);
                    totalInterest = (monthlyEMItoFile * 12);
                    repaymentAmount = principalAmount + totalInterest;


                    InvestmentListBox.Items.Add(TERM_OPTION4 + " Years\t\t" + ROI_CATEG2_TERM_OPT4 + " %" + "\t\t" + monthlyEMItoFile.ToString("C2") + "\t\t" + totalInterest.ToString("C2") + "\t\t" + repaymentAmount.ToString("C2"));
                }
                else if (principalAmount > 80000 && principalAmount <= 100000)
                {
                    //Calculation of list variables for 1 Year Term
                    monthlyEMItoFile = MonthlyEMI(principalAmount, ROI_CATEG3_TERM_OPT1, TERM_OPTION1);
                    totalInterest = (monthlyEMItoFile * 12);
                    repaymentAmount = principalAmount + totalInterest;
                    InvestmentListBox.Items.Add(TERM_OPTION1 + " Years\t\t" + ROI_CATEG3_TERM_OPT1 + " %" + "\t\t" + monthlyEMItoFile.ToString("C2") + "\t\t" + totalInterest.ToString("C2") + "\t\t" + repaymentAmount.ToString("C2"));

                    //Calculation of list variables for 2 Year Term
                    monthlyEMItoFile = MonthlyEMI(principalAmount, ROI_CATEG3_TERM_OPT2, TERM_OPTION2);
                    totalInterest = (monthlyEMItoFile * 12);
                    repaymentAmount = principalAmount + totalInterest;
                    InvestmentListBox.Items.Add(TERM_OPTION2 + " Years\t\t" + ROI_CATEG3_TERM_OPT2 + " %" + "\t\t" + monthlyEMItoFile.ToString("C2") + "\t\t" + totalInterest.ToString("C2") + "\t\t" + repaymentAmount.ToString("C2"));

                    //Calculation of list variables for 3 Year Term
                    monthlyEMItoFile = MonthlyEMI(principalAmount, ROI_CATEG3_TERM_OPT3, TERM_OPTION3);
                    totalInterest = (monthlyEMItoFile * 12);
                    repaymentAmount = principalAmount + totalInterest;

                    InvestmentListBox.Items.Add(TERM_OPTION3 + " Years\t\t" + ROI_CATEG3_TERM_OPT3 + " %" + "\t\t" + monthlyEMItoFile.ToString("C2") + "\t\t" + totalInterest.ToString("C2") + "\t\t" + repaymentAmount.ToString("C2"));

                    //Calculation of list variables for 4 Year Term
                    monthlyEMItoFile = MonthlyEMI(principalAmount, ROI_CATEG3_TERM_OPT4, TERM_OPTION4);
                    totalInterest = (monthlyEMItoFile * 12);
                    repaymentAmount = principalAmount + totalInterest;
                    InvestmentListBox.Items.Add(TERM_OPTION4 + " Years\t\t" + ROI_CATEG3_TERM_OPT4 + " %" + "\t\t" + monthlyEMItoFile.ToString("C2") + "\t\t" + totalInterest.ToString("C2") + "\t\t" + repaymentAmount.ToString("C2"));
                }
                else
                {
                    MessageBox.Show("Please enter a valid Loan amount in the range of € 10000 to € 100000","Invalid Amount",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    PrincipalTextBox.Focus();
                    PrincipalTextBox.SelectAll();
                }
            }
            catch
            {
                MessageBox.Show("Please enter a valid input for loan amount","Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                PrincipalTextBox.Focus();
                PrincipalTextBox.SelectAll();
                DisplayButton.Enabled = true;
            }

        }

        private void SignOutButton_Click(object sender, EventArgs e)
        {
            InvestmentPanelScreen2.Visible = false;
            SearchGroupBox.Visible = false;
            SummaryGroupBox.Visible = false;
            PasswordTextBox.Clear();
            SignOutButton.Visible=false;
        }
        /*************************CODE FOR PROCEED BUTTON**********************************/
        private void InvestmentListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProceedButton.Enabled = true;
        }
        private void ProceedButton_Click(object sender, EventArgs e)
        {
            int schemeSelected;
            InvestmentListBox.Enabled = false;
            ProceedButton.Enabled = false;
            ModifySelectionButton.Enabled = true;
            DisplayButton.Enabled = false;
            SubmitButton.Enabled = true;
            ClientNameTextBox.Focus();
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
                                monthlyEMItoFile = MonthlyEMI(principalAmount, ROI_CATEG1_TERM_OPT1, TERM_OPTION1);
                                break;
                            case 1:
                                roiToFile = ROI_CATEG1_TERM_OPT2.ToString();
                                tenureToFiles = (TERM_OPTION2 * MONTHS).ToString();
                                loanRequestedToFile = principalAmount.ToString();
                                monthlyEMItoFile = MonthlyEMI(principalAmount, ROI_CATEG1_TERM_OPT2, TERM_OPTION2);
                                break;
                            case 2:
                                roiToFile = ROI_CATEG1_TERM_OPT3.ToString();
                                tenureToFiles = (TERM_OPTION3 * MONTHS).ToString();
                                loanRequestedToFile = principalAmount.ToString();
                                monthlyEMItoFile = MonthlyEMI(principalAmount, ROI_CATEG1_TERM_OPT3, TERM_OPTION3);
                                break;
                            case 3:
                                roiToFile = ROI_CATEG1_TERM_OPT4.ToString();
                                tenureToFiles = (TERM_OPTION4 * MONTHS).ToString();
                                loanRequestedToFile = principalAmount.ToString();
                                monthlyEMItoFile = MonthlyEMI(principalAmount, ROI_CATEG1_TERM_OPT4, TERM_OPTION4);
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
                                monthlyEMItoFile = MonthlyEMI(principalAmount, ROI_CATEG2_TERM_OPT1, TERM_OPTION1);
                                break;
                            case 1:
                                roiToFile = ROI_CATEG2_TERM_OPT2.ToString();
                                tenureToFiles = (TERM_OPTION2 * MONTHS).ToString();
                                loanRequestedToFile = principalAmount.ToString();
                                monthlyEMItoFile = MonthlyEMI(principalAmount, ROI_CATEG2_TERM_OPT2, TERM_OPTION2);
                                break;
                            case 2:
                                roiToFile = ROI_CATEG2_TERM_OPT3.ToString();
                                tenureToFiles = (TERM_OPTION3 * MONTHS).ToString();
                                loanRequestedToFile = principalAmount.ToString();
                                monthlyEMItoFile = MonthlyEMI(principalAmount, ROI_CATEG2_TERM_OPT3, TERM_OPTION3);
                                break;
                            case 3:
                                roiToFile = ROI_CATEG2_TERM_OPT4.ToString();
                                tenureToFiles = (TERM_OPTION4 * MONTHS).ToString();
                                loanRequestedToFile = principalAmount.ToString();
                                monthlyEMItoFile = MonthlyEMI(principalAmount, ROI_CATEG2_TERM_OPT4, TERM_OPTION4);
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
                                monthlyEMItoFile = MonthlyEMI(principalAmount, ROI_CATEG3_TERM_OPT1, TERM_OPTION1);
                                break;
                            case 1:
                                roiToFile = ROI_CATEG3_TERM_OPT2.ToString();
                                tenureToFiles = (TERM_OPTION2 * MONTHS).ToString();
                                loanRequestedToFile = principalAmount.ToString();
                                monthlyEMItoFile = MonthlyEMI(principalAmount, ROI_CATEG3_TERM_OPT2, TERM_OPTION2);
                                break;
                            case 2:
                                roiToFile = ROI_CATEG3_TERM_OPT3.ToString();
                                tenureToFiles = (TERM_OPTION3 * MONTHS).ToString();
                                loanRequestedToFile = principalAmount.ToString();
                                monthlyEMItoFile = MonthlyEMI(principalAmount, ROI_CATEG3_TERM_OPT3, TERM_OPTION3);
                                break;
                            case 3:
                                roiToFile = ROI_CATEG3_TERM_OPT4.ToString();
                                tenureToFiles = (TERM_OPTION4 * MONTHS).ToString();
                                loanRequestedToFile = principalAmount.ToString();
                                monthlyEMItoFile = MonthlyEMI(principalAmount, ROI_CATEG3_TERM_OPT4, TERM_OPTION4);
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
        /************************CODE FOR RESET BUTTON************************************/
        private void ResetButton_Click(object sender, EventArgs e)
        {
            ResetFormForNewTRX();
        }
        /********************************CODE FOR SUBMIT**********************************/
        private void SubmitButton_Click(object sender, EventArgs e)
        {
            nameToFile = ClientNameTextBox.Text;
            eirToFile = EIRCodeTextBox.Text;
            phoneNumToFile = PhoneNumberTextBox.Text;
            emailToFile = EmailTextBox.Text;
            bool validationCheck, checkEmailFormat, checkContactFormat;
            checkEmailFormat = emailValidation(emailToFile);
            checkContactFormat = checkNumeric(phoneNumToFile);
            validationCheck = InvestorValidation();
            if (validationCheck && InvestmentListBox.SelectedIndex != -1)
            {
                DialogResult result;
                result = MessageBox.Show("Do you wish to proceed with the below loan request? \n" +
                    "ClientName:\t " + nameToFile + "\n" +
                    "Loan Requested:\t " + loanRequestedToFile + "\n" +
                    "Phone Number:\t " + phoneNumToFile + "\n" +
                    "Email Address:\t " + emailToFile + "\n" +
                    "Monthly EMI:\t " + monthlyEMItoFile.ToString("C2") + "\n" +
                    "Rate of Interest:\t " + roiToFile+" %", "Please check details", MessageBoxButtons.YesNo,MessageBoxIcon.Question);

                if (result == DialogResult.Yes && trxID!=null)
                {
                    nameToFile = ClientNameTextBox.Text;
                    eirToFile = EIRCodeTextBox.Text;
                    phoneNumToFile = PhoneNumberTextBox.Text;
                    emailToFile = EmailTextBox.Text;
                    WriteToFile();
                    MessageBox.Show("Thank You!\n The loan has been processed successfully","Loan Sanctioned",MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetFormForNewTRX();
                }
                else if(result == DialogResult.Yes && trxID == null)
                {
                    MessageBox.Show("The transaction ID could not be generated. The customer data file is missing", "Unique ID generation failed!", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                }   
            }
        }
        //*********************** CODE FOR SEARCH FUNCTIONALITY***************************/
        private void SearchButton_Click(object sender, EventArgs e)
        {
            SearchListBox.Items.Clear();
            if (TransactionIdRadioButton.Checked)
            {
                string searchTRXid;
                searchTRXid = SearchTextBox.Text;
                if (checkNumeric(searchTRXid) == true)
                {
                    if (searchTRXid.Length == 5)
                    {
                        try
                        {
                            SearchByTrxId(searchTRXid);
                        }
                        catch
                        {
                            MessageBox.Show("The Customer Data File was not Found", "File Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("The transaction ID should contain 5 Numbers", "Invalid ID");
                    }
                }
                else
                {
                    MessageBox.Show("The trasaction ID should be numeric", "Invalid input");
                }
            }
            else
            {
                string searchEmail;
                searchEmail = SearchTextBox.Text;
                if (emailValidation(searchEmail) == true)
                {
                    try
                    {
                        SearchByEmail(searchEmail);
                    }
                    catch
                    {
                        MessageBox.Show("The Customer Data File was not Found", "File Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
                }
                else
                {
                    MessageBox.Show("Enter a valid email address","Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
            try
            {
                SumaryListBox.Items.Add("Total Loaned:\t" + TotalAmountBorrowedCalculation().ToString("C2"));
                SumaryListBox.Items.Add("Total Interest:\t" + TotalInterestEarned().ToString("C2"));
                SumaryListBox.Items.Add("Average Loan:\t" + AverageAmountBorrowed().ToString("C2"));
                SumaryListBox.Items.Add("Average Interest:\t" + AverageInterestEarned().ToString("C2"));
                SumaryListBox.Items.Add("Average Months:\t" + AverageMonths().ToString());
            }
            catch
            {
                MessageBox.Show("The Customer Data File was not Found", "File Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }  
        }
        private void ClearSummaryButton_Click(object sender, EventArgs e)
        {
            SumaryListBox.Items.Clear();
        }
        private void ModifySelectionButton_Click(object sender, EventArgs e)
        {
            InvestmentListBox.Enabled = true;
            ProceedButton.Enabled = false;
            SubmitButton.Enabled = false;
            DisplayButton.Enabled = true;
            ModifySelectionButton.Enabled = false;   
        }
        private void ExitButtonScr2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /***************************USER DEFINED FUNCTIONS*********************************/
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
                dataWriter.WriteLine(roiToFile + "%");
                dataWriter.WriteLine("********************");
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
            DisplayButton.Enabled = true;
            ModifySelectionButton.Enabled = false; 
        }
        public string GenerateTRXid()
        {
            string GeneratedTrxIDNumber, TrxIDinFile;
            try
            {
                StreamReader dataReader = new StreamReader(FILEPATH);
                Random trxID = new Random();
                GeneratedTrxIDNumber = trxID.Next(99999).ToString();
                using (dataReader)
                {
                    bool valueFound = false;
                    while (valueFound == false)
                    {
                        TrxIDinFile = dataReader.ReadLine();
                        if (GeneratedTrxIDNumber != TrxIDinFile && dataReader.EndOfStream == false)
                        {
                            for (int i = 0; i <= 8; i++)
                            {
                                dataReader.ReadLine();
                            }
                        }
                        else if (GeneratedTrxIDNumber != TrxIDinFile && dataReader.EndOfStream == true)
                        {
                            break;
                        }
                        else if (GeneratedTrxIDNumber == TrxIDinFile && dataReader.EndOfStream == false)
                        {
                            GeneratedTrxIDNumber = trxID.Next(99999).ToString();
                        }
                        else if (GeneratedTrxIDNumber == TrxIDinFile && dataReader.EndOfStream == true)
                        {
                            GeneratedTrxIDNumber = trxID.Next(99999).ToString();
                        }

                    }
                }
            }
            catch
            {
                StreamWriter dataWriter=new StreamWriter(FILEPATH);
                Random trxID = new Random();
                GeneratedTrxIDNumber = trxID.Next(99999).ToString();
            }
            return GeneratedTrxIDNumber;
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
                        SearchListBox.Items.Add("Customer Email:\t" + dataReader.ReadLine());
                        SearchListBox.Items.Add("Customer Name:\t" + dataReader.ReadLine());
                        SearchListBox.Items.Add("EIR CODE:\t" + dataReader.ReadLine());
                        SearchListBox.Items.Add("Phone Number:\t" + dataReader.ReadLine());
                        SearchListBox.Items.Add("Loan Sanctioned:\t" + dataReader.ReadLine());
                        SearchListBox.Items.Add("Monthly EMI:\t" + dataReader.ReadLine());
                        SearchListBox.Items.Add("Tenure in Months:\t" + dataReader.ReadLine());
                        SearchListBox.Items.Add("Rate of Interest:\t" + dataReader.ReadLine());
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
            bool recordsOver = false;
            bool recordFound = false;

            
                StreamReader dataReader = new StreamReader(FILEPATH);

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
                            SearchListBox.Items.Add(dataReader.ReadLine());
                            recordFound = true;
                        }
                        else if (!(currentLine == emailValue) && !dataReader.EndOfStream)
                        {
                            for (int i = 0; i <= 7; i++)
                            {
                                currentLine = dataReader.ReadLine();
                            }
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
            bool recordsOver = false;
            StreamReader dataReader = new StreamReader(FILEPATH);
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
                    dataReader.Close();
                }
            return totalAmountBorrowed;
        }
        public decimal AverageAmountBorrowed()
        {
            decimal AvgAmountBorrowed = 0;
            int TransactionCount = 0;
            bool recordsOver = false;
            StreamReader dataReader = new StreamReader(FILEPATH);
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
                }
            try
            {
                return AvgAmountBorrowed / TransactionCount;
            }
            catch
            {
                return 0;
            }
        }
        public decimal TotalInterestEarned()
        {
            decimal totalInterestEarned = 0;
            bool recordsOver = false;
            StreamReader dataReader = new StreamReader(FILEPATH);
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
                            string currentValue = dataReader.ReadLine();
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
            bool recordsOver = false;
            StreamReader dataReader = new StreamReader(FILEPATH);
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
                }
            return AvgInterestEarned / TransactionCount;
        }
        public decimal AverageMonths()
        {
            decimal AvgMonths = 0;
            int TransactionCount = 0;
            bool recordsOver = false;
            StreamReader dataReader = new StreamReader(FILEPATH);
                using (dataReader)
                {
                    while (recordsOver == false)
                    {
                        if (!dataReader.EndOfStream)
                        {
                            for (int i = 1; i <= 7; i++)
                            {
                                string currentValue = dataReader.ReadLine();
                            }
                            AvgMonths = AvgMonths + decimal.Parse(dataReader.ReadLine());
                            TransactionCount++;
                            for (int i = 1; i <= 2; i++)
                            {
                                string currentValue = dataReader.ReadLine();
                            }
                        }
                        else
                        {
                            recordsOver = true;
                        }
                    }
                    dataReader.Close();
                }
            return AvgMonths / TransactionCount;
        }
        public bool InvestorValidation()
        {
            string Name = ClientNameTextBox.Text;
            string Eir = EIRCodeTextBox.Text;
            string phoneNum = PhoneNumberTextBox.Text;
            string email = EmailTextBox.Text;
            bool checkEmailFormat, checkContactFormat;
            checkEmailFormat = emailValidation(email);
            checkContactFormat = checkNumeric(phoneNum);
            int i = 0;
            bool validation = true;
            while (i == 0)
            {
                if (Name == "")
                {
                    MessageBox.Show("Please Enter a Name", "Name Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ClientNameTextBox.Focus();
                    validation = false;
                    break;
                }
                else if (Eir == "")
                {
                    MessageBox.Show("Please Enter an EIR Code", "EIR Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    EIRCodeTextBox.Focus();
                    validation = false;
                    break;
                }
                else if (phoneNum == "")
                {
                    MessageBox.Show("Please Enter a Phone Number", "Contact Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    PhoneNumberTextBox.Focus();
                    validation = false;
                    break;
                }
                else if (phoneNum != "" && checkContactFormat == false)
                {
                    MessageBox.Show("Enter valid contact number","Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    PhoneNumberTextBox.Focus();
                    validation = false;
                    break;
                }
                else if (email.Equals(""))
                {
                    MessageBox.Show("Please Enter an email", "Email Required",MessageBoxButtons.OK, MessageBoxIcon.Error);
                    PhoneNumberTextBox.Focus();
                    validation = false;
                    break;
                }
                else if (email != "" && checkEmailFormat == false)
                {
                    MessageBox.Show("Enter valid Email Address","Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

