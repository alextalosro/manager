using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

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
            //Query the db for username and password;
            string mainconn = ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            string sqlquery = "SELECT * FROM [dbo].[log_item]";
            sqlconn.Open();
            SqlCommand sqlCmd = new SqlCommand(sqlquery, sqlconn);
            SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            sqlCmd.ExecuteNonQuery();

            DataRow[] dr = dt.Select();

            foreach (DataRow row in dr)
            {
                string[] columns = { row.ItemArray[1].ToString(), row.ItemArray[2].ToString(), row.ItemArray[3].ToString(), row.ItemArray[4].ToString() };
                ListViewItem item = new ListViewItem(columns);
                listView1.Items.Add(item);
            }

            //Auto resize columns
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }
    }
}
