using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSV
{
    public partial class Form2 : Form
    {
       
        public Form2()
        {
            InitializeComponent();
            textBox1.Text = Form1.autor;
            textBox2.Text = Form1.tytul;
            textBox3.Text = Form1.id;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1.autor = textBox1.Text;
            Form1.tytul = textBox2.Text;
            Form1.id = textBox3.Text;
           
            Form1.addRecord(Form1.dt);
            
            
            Close();

           

            
        }
               
    }
}
