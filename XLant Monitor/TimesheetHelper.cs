using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XLant_Monitor
{
    public partial class TimesheetHelper : Form
    {
        public TimesheetHelper()
        {
            InitializeComponent();
        }

        private void GoBtn_Click(object sender, EventArgs e)
        {
            string start = StartDateBox.Text;
            string end = EndDateBox.Text;
            string user = Environment.UserName;

            DataTable xlReader = XLant.XLSQL.ReturnTable("EXECUTE [XLant].[dbo].[ActionsLog] @user = '" + user + "' ,@start = '" + start + "',@end = '" + end + "'");

            ResultsGrid.DataSource = xlReader;
            ResultsGrid.Columns[0].ReadOnly = true;
            ResultsGrid.Columns[1].ReadOnly = true;
            ResultsGrid.Columns[3].ReadOnly = true;
            DataGridViewTextBoxColumn hoursColumn = new DataGridViewTextBoxColumn();
            hoursColumn.HeaderText = "Hours";
            hoursColumn.ReadOnly = false;
            hoursColumn.ValueType = typeof(decimal);
            ResultsGrid.Columns.Add(hoursColumn);
            ResultsGrid.Columns.Add(CreateSelectColumn("Service"));
            ResultsGrid.Columns.Add(CreateSelectColumn("Analysis"));
            ResultsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            ResultsGrid.MultiSelect = true;
            ResultsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            ResultsGrid.Update();
        }

        private DataGridViewComboBoxColumn CreateSelectColumn(string columnType)
        {
            DataGridViewComboBoxColumn _column = new DataGridViewComboBoxColumn();
            _column.HeaderText = columnType;
            return _column;
        }

        private void ResultsGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
           
            if (e.ColumnIndex == 4)
            {
                if (ResultsGrid[e.ColumnIndex, e.RowIndex].Value != null)
                {
                    DataGridViewComboBoxCell updateCell = this.ResultsGrid[e.ColumnIndex + 1, e.RowIndex] as DataGridViewComboBoxCell;
                    decimal result = 0;
                    if (decimal.TryParse(ResultsGrid[e.ColumnIndex, e.RowIndex].Value.ToString(), out result) && result > 0)
                    {
                        DataTable xlReader = new DataTable();
                        string param = ResultsGrid[1, e.RowIndex].Value.ToString();
                        param = param.Substring(0, param.IndexOf("-") - 1);
                        xlReader = XLant.XLSQL.ReturnTable("Select * from [dbo].[TS_ServiceLines]('" + param + "')");
                        updateCell.DisplayMember = "ServTitle";
                        updateCell.ValueMember = "ServIndex";
                        updateCell.DataSource = xlReader;
                        updateCell.Value = xlReader.Rows[0][0].ToString();
                    }
                    else
                    {
                        updateCell.Value = "";
                        updateCell.DataSource = null;
                        updateCell = this.ResultsGrid[e.ColumnIndex + 2, e.RowIndex] as DataGridViewComboBoxCell;
                        updateCell.Value = "";
                        updateCell.DataSource = null;
                    }
                }
            }
        }

        private void ResultsGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                DataTable xlReader = new DataTable();
                DataGridViewComboBoxCell updateCell = this.ResultsGrid[e.ColumnIndex + 1, e.RowIndex] as DataGridViewComboBoxCell;
                string param = ResultsGrid[5, e.RowIndex].Value.ToString();
                xlReader = XLant.XLSQL.ReturnTable("SELECT * FROM [dbo].[TS_AnalysisCodes] ('" + param + "')");
                if (xlReader.Rows.Count > 0)
                {
                    updateCell.DisplayMember = "chargename";
                    updateCell.ValueMember = "chargecode";
                    updateCell.DataSource = xlReader;
                    updateCell.Value = xlReader.Rows[0]["chargecode"].ToString();   
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string user = Environment.UserName;
            List<bool> results = new List<bool>();
            decimal testDecimal = 0;
            foreach(DataGridViewRow row in ResultsGrid.Rows)
            {
                if (row.Cells[4].Value != null)
                {
                    if (decimal.TryParse(row.Cells[4].Value.ToString(), out testDecimal) && testDecimal > 0)
                    {
                        List<SqlParameter> paramList = new List<SqlParameter>();
                        string client = row.Cells[1].Value.ToString();
                        string clientcode = client.Substring(0, client.ToString().IndexOf('-') - 1);
                        DateTime date = (DateTime)row.Cells[3].Value;
                        string desc = row.Cells[2].Value.ToString();
                        decimal hours = testDecimal;
                        string service = row.Cells[5].Value.ToString();
                        string analysis = row.Cells[6].Value.ToString();
                        paramList.Add(new SqlParameter("@clientcode", clientcode));
                        paramList.Add(new SqlParameter("@date", date));
                        paramList.Add(new SqlParameter("@narrative", desc));
                        paramList.Add(new SqlParameter("@staffuser", user));
                        paramList.Add(new SqlParameter("@hours", hours));
                        paramList.Add(new SqlParameter("@service", service));
                        paramList.Add(new SqlParameter("@analysis", analysis));

                        bool result = XLant.XLSQL.RunCommand("Execute [Xlant].[dbo].[TS_AddTimesheetEntry2] @Clientcode, @date, @hours, @narrative, @staffuser, @service, @analysis", paramList);
                        results.Add(result);
                    }
                }
            }
            MessageBox.Show(results.Count().ToString() + " entries added to timesheet");
            ResultsGrid.DataSource = null;
        }


    }
}
