#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace SQLiteAndDapper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private SQLiteConnectionStringBuilder builder = new SQLiteConnectionStringBuilder();
        private SQLiteConnection conn;

        private void Form1_Load(object sender, EventArgs e)
        {
            builder.DataSource = "./sample.db";
            builder.BusyTimeout = 5000;
            builder.DateTimeFormat = SQLiteDateFormats.InvariantCulture;
            builder.DateTimeKind = DateTimeKind.Local;
            builder.JournalMode = SQLiteJournalModeEnum.Wal;
            builder.SyncMode = SynchronizationModes.Off;
            builder.CacheSize = 4000;
            builder.Add("cache", "shared");

            string connStr = builder.ConnectionString;

            conn = new SQLiteConnection(connStr);
            conn.Open();

            RefreshGrid();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (null != conn)
            {
                conn.Close();
                conn.Dispose();
            }
            conn = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 200; i++)
            {

            }
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            dataGridView1.DataSource = null;

            dataGridView1.DataSource = null;
        }
    }
}
