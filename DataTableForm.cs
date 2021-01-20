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

        public DataTableForm(CSVData csv)
        {
            this.csv = csv;
            InitializeComponent();
        }

        private void DataTableForm_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = csv.getDataTable();
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            //TODO
            csv.ExportToCSV("test.csv");
            MessageBox.Show("Table exported to CSV on Desktop");
        }
    }
}
