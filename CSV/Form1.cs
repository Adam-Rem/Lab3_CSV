using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

namespace CSV
{
    public partial class Form1 : Form
    {
        private const string V = "Tytul,Autor,ID\n";
        public static string tytul, autor, id;
        public static int index,maxrow;
        public static DataTable dt = new DataTable();
        
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
            index = e.RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[index];
            tytul = selectedRow.Cells[0].Value.ToString();
            autor = selectedRow.Cells[1].Value.ToString();
            id = selectedRow.Cells[2].Value.ToString();

            
        } //wybieranie indeksu zaznaczonego

        private void label1_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog(this);


            File.WriteAllText(openFileDialog.FileName, V);
                
                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                if (item.Index<dataGridView1.RowCount-1)
                    {
                    string lines = item.Cells[0].Value.ToString() + ',' + item.Cells[1].Value.ToString() + ',' + item.Cells[2].Value.ToString() + '\n';

                    File.AppendAllText(openFileDialog.FileName, lines);
                    }
                
                }
            
        } //zapis

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog(this);
            dataGridView1.DataSource = readCSV(openFileDialog.FileName);
        } //odczytywanie pliku z file explorer

        private void button3_Click(object sender, EventArgs e)  
        {
          
            Hide();
            maxrow = dataGridView1.RowCount;
            Form2 form2 = new Form2();
            form2.ShowDialog();
            form2 = null;           // zamkniêcie formsa 2


            dataGridView1.DataSource = dt;
            DataGridViewRow selectedRow = dataGridView1.Rows[index];
            selectedRow.Cells[0].Value = tytul;
            selectedRow.Cells[1].Value = autor;
            selectedRow.Cells[2].Value = id;
            
            
            
            
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
}