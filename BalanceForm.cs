using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlatinumFinance
{
    //form öröklése,érték átadás
    public partial class BalanceForm : Form
    {
        private int parsedValue;

        public Form1 Main { get; set; }
        public BalanceForm(Form1 main)
        {
            InitializeComponent();
            //érték átadása
            textBox1.Text = Form1.Form1text;
            this.Main = main;

        }

        //egyenleg érték frissítése, módosítása
        private void buttonKiadasmentes_Click(object sender, EventArgs e)
        {
            //csak szám lehetséges
            if (!int.TryParse(textBox2.Text, out parsedValue))
            {
                MessageBox.Show("Csak számot adhatsz meg!");
                return;

            }
            else
            {
                //formok közötti
                Main.AddNewbalance(Convert.ToInt32(textBox2.Text));
                Main.kiir();
                Main.listView1_Refresh();
                //textbox ürítés
                textBox1.Text = textBox2.Text;
                textBox2.Text = String.Empty;
            }
                
        }

        private void BalanceForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }
    }
}
