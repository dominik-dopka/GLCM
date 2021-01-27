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
        private DataTable parametersDataTable;
        private DataTable matrixDataTable;
        private int id;

        public CSVData()
        {
            parametersDataTable = new DataTable();
            id = 1;

            parametersDataTable.Columns.Add("NAME", typeof(string));
            parametersDataTable.Columns.Add("ENERGY", typeof(double));
            parametersDataTable.Columns.Add("ENTROPY", typeof(double));
            parametersDataTable.Columns.Add("CORRELATION", typeof(double));
            parametersDataTable.Columns.Add("INVERSE DIFFERENCE MOMENT", typeof(double));
            parametersDataTable.Columns.Add("INERTIA", typeof(double));
        }

        public void createMatrixTable(float[,] matrix)
        {
            matrixDataTable = new DataTable();
            matrixDataTable.Columns.Add("X", typeof(int));

            for (int i = 0; i < matrix.GetUpperBound(0) + 1; i++)
            {
                int numberI = i + 1; 
                matrixDataTable.Columns.Add(numberI.ToString(), typeof(float));
            }

            for (int j = 0; j < matrix.GetUpperBound(1) + 1; j++)
            {
                int numberJ = j + 1;

                DataRow dataRow = matrixDataTable.NewRow();
                dataRow["X"] = numberJ;

                for (int k = 0; k < matrix.GetUpperBound(0) + 1; k++)
                {
                    int numberK = k + 1;
                    dataRow[numberK.ToString()] = matrix[k, j];
                }

                matrixDataTable.Rows.Add(dataRow);
            }
        }

        public DataTable getMatrixDataTable()
        {
            return matrixDataTable;
        }

        public DataTable getParametersDataTable()
        {
            return parametersDataTable;
        }

        private int getId()
        {
            return id++;
        }

        public void AddRow(string name, double energy, double entropy, double correlation, 
                double inverseDifferenceMoment, double inertia)
        {
            parametersDataTable.Rows.Add(name, energy, entropy, correlation, inverseDifferenceMoment, inertia);
        }

        public void ExportToCSV(string filename, DataTable dataTable)
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
