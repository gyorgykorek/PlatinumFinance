using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;


namespace PlatinumFinance
{
    public partial class Form1 : Form
    {   
        //Lista az egyenlegről
        List<Balance> egyenleg = new List<Balance>();
        //Lista kiadásokról és bevételekről
        List<Expense> kiadas = new List<Expense>();
        
        public Form1()
        {
            
            InitializeComponent();
            //File beolvasása
            beolvas();
            //Egyenleg beolvasása
            beolvasEgyenleg();
            //Listview kiiratása
            kiir();
            //Listview frissítése
            listView1_Refresh();
            //menüszalag
            menuStrip2.BackColor = Color.Maroon;
            menuStrip2.ForeColor = Color.White;
            //listview részletek
            listView1.View = View.Details;
            listView1.GridLines = true;
            //Szülő-Utódosztály
            ConceptChild child = new ConceptChild();
            child.print();

        }
        private void beolvas() //kiadás/bevételek lista feltoltese 
        {
            StreamReader input = new StreamReader("finance.csv", Encoding.Default);
            while (!input.EndOfStream)
            {
                string[] adatok = input.ReadLine().Split(';');
                Expense e = new Expense(adatok[0], Convert.ToInt32(adatok[1]), adatok[2], adatok[3], adatok[4]);
                kiadas.Add(e);
            }
            input.Close();
        }

        public void kiir() //listview-ra iratas
        {

            listView1.Items.Clear(); //elemek törlése
            for (int i = 0; i < kiadas.Count; i++)
            {
                //Listview coloumn subitems-re való szétválogatása listából
                string combindedString = string.Join(";", kiadas[i]);
                string[] splitter = combindedString.Split(';');
                Console.WriteLine(combindedString);
                ListViewItem item = new ListViewItem(new[] { splitter[0], splitter[1], splitter[2], splitter[3], splitter[4] });
                listView1.Items.Add(item);


                
            }
            //aktuális egyenleg kiiratása
            for (int i = 0; i < egyenleg.Count; i++)
            {
                Form1text = egyenleg[i].ToString();
                textBox6.Text = Form1text;
            }
        }
        //Listview frissítése
        public void listView1_Refresh()
        {
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                //alapból zöld
                listView1.Items[i].ForeColor = Color.Green;

                //ha típus (Listview items) "-" akkor piros, kiadás
                if (listView1.Items[i].ToString().Contains("-"))
                {
                    listView1.Items[i].ForeColor = Color.Maroon;
                }

            }
        }

        //egyenleg beolvasása fájlból
        private void beolvasEgyenleg() 
        {
            StreamReader input = new StreamReader("Balance.csv", Encoding.Default);
            while (!input.EndOfStream)
            {
                string[] adatok = input.ReadLine().Split(';');
                Balance e = new Balance(Convert.ToInt32(adatok[0]));
                egyenleg.Add(e);
            }
            input.Close();

        }

        //Egyenleg átadása további formoknak
        private static string form1text;
        public static string Form1text
        {
            get { return form1text; }
            set { form1text = value; }
        }

        //átlátszóság állíthatósága
        private void trackBar1_Scroll_1(object sender, EventArgs e)
        {
            this.Opacity = trackBar1.Value * 0.01;
        }


        //Dark mód beállításai
        private void darkModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (darkModeToolStripMenuItem.Text == "Dark mode")
            {
                this.BackColor = Color.Black;
                label1.ForeColor = Color.White;
                label2.ForeColor = Color.White;
                label3.ForeColor = Color.White;
                label4.ForeColor = Color.White;
                labelkiadasgyakorisag.ForeColor = Color.White;
                label7.ForeColor = Color.White;
                label9.ForeColor = Color.White;
                listView1.BackColor = Color.Black;
                checkBox1.ForeColor = Color.Gray;
                darkModeToolStripMenuItem.Text = "Világos mód";

            }

            //Light mód beállításai
            else if (darkModeToolStripMenuItem.Text == "Világos mód")
            {
                this.BackColor = Color.White;
                label1.ForeColor = Color.Black;
                label2.ForeColor = Color.Black;
                label3.ForeColor = Color.Black;
                label4.ForeColor = Color.Black;
                labelkiadasgyakorisag.ForeColor = Color.Black;
                label7.ForeColor = Color.Black;
                label9.ForeColor = Color.Black;
                listView1.BackColor = Color.Gainsboro;
                checkBox1.ForeColor = Color.Black;
                darkModeToolStripMenuItem.Text = "Dark mode";
            }

        }
        //átlátszóság-állító elrejtése/feltüntetése
        private void átlátszóságToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (trackBar1.Visible == false)
            {
                trackBar1.Visible = true;
            }
            else if (trackBar1.Visible == true)
            {
                trackBar1.Visible = false;
            }
        }

        //Gyakoriság checkbox, új mezőt nyit fel
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (labelkiadasgyakorisag.Visible == false)
            {
                labelkiadasgyakorisag.Visible = true;
                textBox4.Visible = true;
            }
            else
            {
                labelkiadasgyakorisag.Visible = false;
                textBox4.Visible = false;
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //KIADÁS/BEVÉTEL mentése 
        private void buttonKiadasmentes_Click(object sender, EventArgs e)
        {
            //Csak számot fogadjon el
            int parsedValue;
            if (!int.TryParse(textBox1.Text, out parsedValue))
            {
                //ha nem számot ad meg
                MessageBox.Show("Csak számot adhatsz meg!");
                return;

            }
            //ha helyes
            else
            {
                //kiadás mentése, rdoibutton vizsgálata
                if (radioButton1.Checked == true)
                {
                    Expense newexp = new Expense("-", Convert.ToInt32(textBox1.Text), textBox2.Text, textBox3.Text, textBox4.Text);
                    MessageBox.Show("Kiadások mentése sikeres!");
                    //kiadás kivonása az aktuális egyenlegből
                    int refreshBalance = Convert.ToInt32(textBox6.Text) - Convert.ToInt32(textBox1.Text);
                    textBox6.Text = refreshBalance + "";
                    Balance refe = new Balance(Convert.ToInt32(refreshBalance));
                    egyenleg.Add(refe);
                    //textboxok ürítése
                    textBox1.Text = String.Empty;
                    textBox2.Text = String.Empty;
                    textBox3.Text = String.Empty;
                    textBox4.Text = String.Empty;
                    //hozzáadás
                    kiadas.Add(newexp);
                    //kiiratás és frissítés
                    kiir();
                    listView1_Refresh();

                }
                else
                {   //bevétel mentése, rdoibutton vizsgálata
                    Expense newinc = new Expense("+", Convert.ToInt32(textBox1.Text), textBox2.Text, textBox3.Text, textBox4.Text);
                    MessageBox.Show("Bevételek mentése sikeres!");
                    //bevétel hozzáadása az aktuális egyenleghez
                    int refreshBalance = Convert.ToInt32(textBox6.Text) + Convert.ToInt32(textBox1.Text);
                    textBox6.Text = refreshBalance + "";
                    Balance refe = new Balance(Convert.ToInt32(refreshBalance));
                    egyenleg.Add(refe);
                    //textboxok ürítése
                    textBox1.Text = String.Empty;
                    textBox2.Text = String.Empty;
                    textBox3.Text = String.Empty;
                    textBox4.Text = String.Empty;
                    //hozzáadás
                    kiadas.Add(newinc);
                    //kiiratás és frissítés
                    kiir();
                    listView1_Refresh();
                }
            }


        }
        //Finance.csv/Balance.csv frissitese bezaraskor
        private void Form1_FormClosing(object sender, FormClosingEventArgs e) 
        {
            //figyelmeztetés, biztos?
            DialogResult dlgresult = MessageBox.Show("Biztosan bezárod az alkalmazást?",
                            "PlatinumFinance 1.0.0",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Information);
            if (dlgresult == DialogResult.No)
            {
                e.Cancel = true;

            }
            else
            {
                StreamWriter output = new StreamWriter("finance.csv", false, Encoding.Default); 
                foreach (Expense item in kiadas)
                {
                    output.WriteLine(item.ToString());

                }
                output.Close();
                StreamWriter outputbalance = new StreamWriter("Balance.csv", false, Encoding.Default);
                foreach (Balance item in egyenleg)
                {
                    outputbalance.WriteLine(item.ToString());

                }
                outputbalance.Close();
            }


        }

        //- és + jel alapján szűrhetünk bevételre vagy kiadásra
        private void txt_Search_TextChanged(object sender, System.EventArgs e)
        {
            if (textBox5.Text != "")
            {
                for (int i = listView1.Items.Count - 1; i >= 0; i--)
                {
                    var item = listView1.Items[i];
                    if (item.Text.ToLower().Contains(textBox5.Text.ToLower()))
                    {

                    }
                    else
                    {
                        listView1.Items.Remove(item);
                    }
                }
                if (listView1.SelectedItems.Count == 1)
                {
                    //listView1.Focus();
                }
            }
            else
                //kiírás, frissítés
                kiir();
                listView1_Refresh();


        }
        //Listview coloumn headerek formázása
        private void listView1_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.Maroon,e.Bounds);
            e.DrawText();
        }
        //Egyenlegmódosításának lehettősége
        private void egyenlegMegadásaToolStripMenuItem_Click(object sender, EventArgs e)
        {     
            BalanceForm egyenlegWin = new BalanceForm(this);
            egyenlegWin.Show();
        }
        //Egyenleg érték átadása Balanceform
        public void AddNewbalance(int newBalancevalue)
        {
            Balance e = new Balance(Convert.ToInt32(newBalancevalue));
            egyenleg.Add(e);
        }

        //Dupla klikk esetén listview itemnél lehetőség van törölni a rekordot
        public void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int sindex = 0;
            
            if (listView1.SelectedItems.Count > 0)
            {
                //selected index eltárolása
                object var2 = listView1.Items.IndexOf(listView1.SelectedItems[0]);
                sindex = Convert.ToInt32(var2);
                Console.WriteLine(sindex);
            }
            //Figyelmeztető ablak a törlésről
            var confirmation = MessageBox.Show("Törlöd a kiválasztott elemet?\n"+ listView1.SelectedItems[0].SubItems[0].Text + listView1.SelectedItems[0].SubItems[1].Text + ", "+ listView1.SelectedItems[0].SubItems[2].Text + ", " + listView1.SelectedItems[0].SubItems[3].Text, "Elem törlése", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmation == DialogResult.Yes)
            {

                for (int i = listView1.SelectedItems.Count - 1; i >= 0; i--)
                {
                    ListViewItem itm = listView1.SelectedItems[i];
                    
                }
                //selected item törlése listviewből és listából is!!
                listView1.SelectedItems[0].Remove();
                kiadas.RemoveAt(sindex);
                MessageBox.Show("Törlés sikeres","Törlés...");
            }
        
            else
                //semmit nem csinál
            MessageBox.Show("Nem történt változás...","Visszavonás..");
        }

        //Listview header formázáshoz
        private void listView1_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        //standard kinézet visszaállítása
        private void visszaállításToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(64, 64, 64);
            label1.ForeColor = Color.White;
            label2.ForeColor = Color.White;
            label3.ForeColor = Color.White;
            label4.ForeColor = Color.White;
            labelkiadasgyakorisag.ForeColor = Color.White;
            label7.ForeColor = Color.White;
            label9.ForeColor = Color.White;
            listView1.BackColor = Color.LightGray;
            checkBox1.ForeColor = Color.Gray;
        }

        //printscreen készítése , riport lehetőség
        private void button1_Click(object sender, EventArgs e)
        {
            Rectangle bounds = this.Bounds;
            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
                }
                //útvonal
                bitmap.Save("C://PlatinumFinanceReport.jpg", ImageFormat.Jpeg);
                MessageBox.Show("Sikeres mentés: \nC://PlatinumFinanceReport.jpg", "Kép mentése...",MessageBoxButtons.OK,MessageBoxIcon.Question);
            }
        }

        //Kapcsolat menüpont, Linkedin profilom megnyitása
        private void fejlesztőWeboldalaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.linkedin.com/in/gy%C3%B6rgy-korek/");
            MessageBox.Show("LinkedIn megnyitása.", "Kapcsolat", MessageBoxButtons.OK, MessageBoxIcon.Question);

        }

        //Dokumentáció pdf  megnyitása
        private void dokumentációToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("PF_readme.pdf");
            MessageBox.Show("Dokumentáció megnyitása.", "Segítség", MessageBoxButtons.OK, MessageBoxIcon.Question);

        }
    }
}

