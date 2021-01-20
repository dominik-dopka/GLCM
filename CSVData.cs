using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLCM
{
    public class CSVData
    {
        private DataTable dataTable;
        private int id = 1;

        public CSVData()
        {
            dataTable = new DataTable();

            dataTable.Columns.Add("ID", typeof(int));
            dataTable.Columns.Add("NAME", typeof(string));
            dataTable.Columns.Add("PATH", typeof(string));

            //test data
            //dataTable.Rows.Add(getId(), "Test", "Sciezka");
            //dataTable.Rows.Add(getId(), "Test2", "Sciezka2");
        }

        public DataTable getDataTable()
        {
            return dataTable;
        }

        private int getId()
        {
            return id++;
        }

        public void AddRow(string name, string path)
        {
            dataTable.Rows.Add(getId(), name, path);
        }

        public void ExportToCSV(string filename)
        {
            StreamWriter streamWriter = new StreamWriter(
                Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\" + filename, false);

            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                streamWriter.Write(dataTable.Columns[i]);
                if (i < dataTable.Columns.Count - 1)
                {
                    streamWriter.Write(",");
                }
            }

            streamWriter.Write(streamWriter.NewLine);

            foreach (DataRow dataRow in dataTable.Rows)
            {
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dataRow[i]))
                    {
                        string value = dataRow[i].ToString();
                        if (value.Contains(','))
                        {
                            value = String.Format("\"{0}\"", value);
                            streamWriter.Write(value);
                        }
                        else
                        {
                            streamWriter.Write(dataRow[i].ToString());
                        }
                    }
                    if (i < dataTable.Columns.Count - 1)
                    {
                        streamWriter.Write(",");
                    }
                }
                streamWriter.Write(streamWriter.NewLine);
            }

            streamWriter.Close();
        }
    }
}
