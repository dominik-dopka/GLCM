using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GLCM
{
    public partial class DataTableForm : Form
    {
        private CSVData csv;
        private DataTable dataTable;
        private string filename;

        public DataTableForm(CSVData csv, DataTable dataTable, string filename)
        {
            this.csv = csv;
            this.dataTable = dataTable;
            this.filename = filename;
            InitializeComponent();
        }

        private void DataTableForm_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dataTable;
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            //TODO
            csv.ExportToCSV(filename, dataTable);
            MessageBox.Show("Table exported to CSV on Desktop");
        }
    }
}
