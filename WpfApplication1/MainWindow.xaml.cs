using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using PointOfSale.Objects;

namespace PointOfSale
{
    public partial class MainWindow : Window
    {
        //Initialize bills
        List<Bill> BillsList = new List<Bill>();

        public MainWindow()
        {
            //Stock the bills with the default values
            Restock();
            InitializeComponent();
        }

        //Gets the total amount of money currently available and shows the all the bills details
        private void Get_Inventory(object sender, RoutedEventArgs e)
        {           
            Output.Text = "Inventory total: $" + GetTotalAmountOfMoney() + "\n";
            GetBillsAndAmountLeft();
        }

        //Updates the display so the user can see what they entered in
        private void Update_Display(object sender, RoutedEventArgs e)
        {
            string str = ((Button)sender).Tag.ToString();
            Display.Text = Display.Text + str;
        }

        //Withdraws money (if there aren't errors) and clears the display
        private void Withdraw_Money(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(Display.Text))
            {
                Output.Text = "Failure: Please enter an amount to withdraw.\n";
            }
            else
            {
                Withdraw(Convert.ToInt32(Display.Text));
                Display.Text = "";
            }
        }

        //Clears the display
        private void Clear_Display(object sender, RoutedEventArgs e)
        {
            Display.Text = "";
        }

        //Removes the last entered in character from the display
        private void Backspace(object sender, RoutedEventArgs e)
        {
            if (Display.Text.Length > 0)
            { 
                Display.Text = Display.Text.Remove(Display.Text.Length - 1);
            }
        }

        //Restocks the money back to the default values
        private void Restock_Money(object sender, RoutedEventArgs e)
        {
            Restock();
            Output.Text = "";
            GetBillsAndAmountLeft();
        }

        //Withdraws money
        private void Withdraw(int amountToWithdraw) {
            //Get total amount of money in the machine
            int totalAmountOfMoney = GetTotalAmountOfMoney();
            //Save amount wanting to withdraw
            int amountLeftToWithdraw = amountToWithdraw;
            //Instantiate a list of bills that is identical to the current bills if the amount is not able to be withdrawn
            List<Bill> OriginalBillsList = new List<Bill>();
            foreach(Bill bill in BillsList)
            {
                Bill newBill = new Bill(bill.Name, bill.Denomination, bill.Amount);
                OriginalBillsList.Add(newBill);
            }
            //If funds are not available
            if(amountToWithdraw > totalAmountOfMoney)
            {
                Output.Text = "Failure: Insufficient funds.\n";
                GetBillsAndAmountLeft();
            }
            else
            {
                foreach(Bill bill in BillsList)
                {
                    //Process withdraw amount
                    while((amountLeftToWithdraw >= bill.Denomination) && (bill.Amount != 0))
                    {
                        amountLeftToWithdraw = amountLeftToWithdraw - bill.Denomination;
                        bill.Amount--;
                    }
                }
                //If all of the bills are used but there is still a balance
                if(amountLeftToWithdraw != 0)
                {
                    //Undo the removal of bills/put back to original amount of bills
                    BillsList = new List<Bill>(OriginalBillsList);
                    Output.Text = "Failure: Insufficient funds.\n";
                    GetBillsAndAmountLeft();
                }
                else
                {
                    Output.Text = "Success: Dispensed $" + amountToWithdraw + "\n";
                    GetBillsAndAmountLeft();
                }
            }
        }

        //Returns the total amount of money left
        private int GetTotalAmountOfMoney()
        {
            int totalAmountOfMoney = 0;
            foreach(Bill bill in BillsList)
            {
                totalAmountOfMoney = totalAmountOfMoney + (bill.Denomination * bill.Amount);
            }
            return totalAmountOfMoney;
        }

        //Outputs the details of each bill
        private void GetBillsAndAmountLeft()
        {
            Output.Text = Output.Text + "Machine Balance: $" + GetTotalAmountOfMoney() + "\n";
            foreach(Bill bill in BillsList)
            {
                Output.Text = Output.Text + "Denomination: $" + bill.Denomination + " - Amount: " + bill.Amount + "\n";
            }
        }

        //Clears the bills list and adds each bill in with default values
        private void Restock()
        {
            BillsList.Clear();
            BillsList.Add(new Bill("100", 100, 10));
            BillsList.Add(new Bill("50", 50, 10));
            BillsList.Add(new Bill("20", 20, 10));
            BillsList.Add(new Bill("10", 10, 10));
            BillsList.Add(new Bill("5", 5, 10));
            BillsList.Add(new Bill("1", 1, 10));
        }
    }
}
