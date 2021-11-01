using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace PlatinumFinance
{
    public partial class LoadingForm : Form
    {

        //Új thread definiálása
        Thread th;        

        //Betöltő képernyő
        public LoadingForm()
        {
            InitializeComponent();
            this.ControlBox = false;
            //Számláló elindítása
            this.timer1.Start();

        }


        private void LoadingForm_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Betöltő-csík léptetése
            progressBar1.Increment(1);
            if (this.progressBar1.Value == 40)
            {
                //Számláló leállítása
                timer1.Stop();
                //Ez a thread bezárása
                this.Close();
                //Új thread indítása
                th = new Thread(openform);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
                
            }
        }

        private void openform(Object obj)
        {
            //Form1 indítása
            Application.Run(new Form1());
        }
    }
}
