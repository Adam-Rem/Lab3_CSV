using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using System.Xml;

namespace CSV
{
    

    public partial class Form1 : Form
    {
        
 
        public static int index,maxrow;
        public static DataTable dt = new DataTable();
        public static DataSet dataSet = new DataSet();
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }        

        public static DataTable readCSV(string filePath) 
        {         
            dt = new DataTable();
            foreach(var headerLine in File.ReadLines(filePath).Take(1))
            {
                foreach(var headerItem in headerLine.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    dt.Columns.Add(headerItem.Trim());
                }
            }
            foreach(var line in File.ReadLines(filePath).Skip(1))
            {
                dt.Rows.Add(line.Split(','));
            }
            
            return dt;
        } //funckja odczytywania pliku CSV

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog(this);
            dt.ToCSV(openFileDialog.FileName);      
            
        } //zapis do CSV

        private void button4_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog(this);
            dataSet.WriteXml(openFileDialog.FileName);
            
        }//zapis do XML

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog(this);
            dataSet.ReadXml(openFileDialog.FileName);
            dt = dataSet.Tables[0];
            dataGridView1.DataSource = dataSet.Tables[0];

        }//otwieranie XML

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog(this);
            dataGridView1.DataSource = readCSV(openFileDialog.FileName);
        } //odczytywanie pliku z file explorer

        private void button3_Click(object sender, EventArgs e)  
        {
          
            Hide();
            
            Form2 form2 = new Form2();

            form2.ShowDialog();
            form2 = null;           // zamkniêcie formsa 2


            dataGridView1.DataSource = dt;
            DataGridViewRow selectedRow = dataGridView1.Rows[index];
                       
            
            
            
            Show();
        } //zamykanie current forms i otwieranie okna edycji

        public static DataTable addRecord(DataTable dt)
        {
            if (index == maxrow-1)
            {
                dt.Rows.Add();
            }
            return dt;
        }  //dodawanie rowa

    }
    public static class CSVUtlity
    {
        public static void ToCSV(this DataTable dtDataTable, string strFilePath)
        {
            StreamWriter sw = new StreamWriter(strFilePath, false);
            //headers    
            for (int i = 0; i < dtDataTable.Columns.Count; i++)
            {
                sw.Write(dtDataTable.Columns[i]);
                if (i < dtDataTable.Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
            foreach (DataRow dr in dtDataTable.Rows)
            {
                for (int i = 0; i < dtDataTable.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        string value = dr[i].ToString();
                        if (value.Contains(','))
                        {
                            value = String.Format("\"{0}\"", value);
                            sw.Write(value);
                        }
                        else
                        {
                            sw.Write(dr[i].ToString());
                        }
                    }
                    if (i < dtDataTable.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }
    }
}