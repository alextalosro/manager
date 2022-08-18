using DataAccessLayer;
using System;
using System.Linq;
using System.Data.Entity;
using System.Windows.Forms;
using System.Collections.Generic;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;

namespace EcolorProductionManager
{
    public partial class LogForm : Form
    {
        public LogForm()
        {
            InitializeComponent();
            // Set the view to show details.
            listView1.View = View.Details;
            // User is not allowed to edit item text.
            listView1.LabelEdit = false;
            // User is not allowed to rearrange columns.
            listView1.AllowColumnReorder = false;
            // Display grid lines.
            listView1.GridLines = true;

        }

        private void LogForm_Load(object sender, EventArgs e)
        {
            var logItems = new List<LogItem>();

            try
            {
                using (var ctx = new DatabaseContext())
                {
                    logItems = ctx.LogItems
                        .Include(u => u.User)
                        .ToList();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            
            foreach (var logItem in logItems)
            {
                string[] columns = { logItem.ActionExecutionTime.ToString(), logItem.User.Firstname + " " + logItem.User.Lastname, logItem.Action, logItem.Reason };
                ListViewItem item = new ListViewItem(columns);
                listView1.Items.Add(item);
            }

            //Auto resize columns
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var logItems = new List<LogItem>();

            try
            {
                using (var ctx = new DatabaseContext())
                {
                    logItems = ctx.LogItems
                        .Include(u => u.User)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            // Create excel file
            HSSFWorkbook workbook = new HSSFWorkbook();
            HSSFFont myFont = (HSSFFont)workbook.CreateFont();
            myFont.FontHeightInPoints = 12;
            myFont.FontName = "Tahoma";

            //Create new sheet
            ISheet Sheet = workbook.CreateSheet("Raport");

            //Creat The Headers of the excel
            IRow HeaderRow = Sheet.CreateRow(0);

            //Create The Actual Cells
            CreateCell(HeaderRow, 0, "Nr.Crt");
            CreateCell(HeaderRow, 1, "Data/Ora");
            CreateCell(HeaderRow, 2, "Utilizator");
            CreateCell(HeaderRow, 3, "Actiune");
            CreateCell(HeaderRow, 4, "Motiv");

            // This Where the Data row starts from
            int RowIndex = 1;
            //Iteration through owr collection
            foreach (var logItem in logItems)
            {
                //Creating the CurrentDataRow
                IRow CurrentRow = Sheet.CreateRow(RowIndex);

                CreateCell(CurrentRow, 0, RowIndex);
                CreateCell(CurrentRow, 1, logItem.ActionExecutionTime.ToString());
                CreateCell(CurrentRow, 2, logItem.User.Firstname + " " + logItem.User.Lastname);
                CreateCell(CurrentRow, 3, logItem.Action);
                CreateCell(CurrentRow, 4, logItem.Reason);
                RowIndex++;
            }

            // Auto sized all the affected columns
            int lastColumNum = Sheet.GetRow(0).LastCellNum;
            for (int i = 0; i <= lastColumNum; i++)
            {
                Sheet.AutoSizeColumn(i);
                GC.Collect();
            }

            // Write Excel to disk 
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "Custom Description";
            string selectedPath = "";
            string dateTimeNowNoWhiteSpace = String.Concat(DateTime.Now.ToString("dd:MM:yyyy:HH:mm").Where(c => !Char.IsWhiteSpace(c)));
            Regex pattern = new Regex("[/:]");
            string dateTimeProcessed = pattern.Replace(dateTimeNowNoWhiteSpace, "-");

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                selectedPath = folderBrowserDialog.SelectedPath;
            }

            string fullPath = Path.Combine(selectedPath, dateTimeProcessed + "interlock.xls");

            using (var fileData = new FileStream(fullPath, FileMode.CreateNew))
            {
                workbook.Write(fileData);
            }
        }

        private void CreateCell(IRow CurrentRow, int CellIndex, string Value, HSSFCellStyle Style = null)
        {
            ICell Cell = CurrentRow.CreateCell(CellIndex);
            Cell.SetCellValue(Value);
            if ( Style != null)
            {
                Cell.CellStyle = Style;
            }
        }

        private void CreateCell(IRow CurrentRow, int CellIndex, int Value, HSSFCellStyle Style = null)
        {
            ICell Cell = CurrentRow.CreateCell(CellIndex);
            Cell.SetCellValue(Value);
            if (Style != null)
            {
                Cell.CellStyle = Style;
            }
        }
    }
}
