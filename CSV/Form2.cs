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
            dataGridView1.DataSource = Form1.dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
                       
            
            Close();       

            
        }
               
    }
}
