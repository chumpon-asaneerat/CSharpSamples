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

using Dapper;

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

        private void RefreshGrid()
        {
            dataGridView1.DataSource = null;
            List<MyRecord> items;
            if (null != conn)
            {
                items = conn.Query<MyRecord>("Select * From MyRecord").ToList();
            }
            else
            {
                items = new List<MyRecord>();
            }

            dataGridView1.DataSource = items;
        }

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
            if (null == conn) return;
            //List<MyRecord> items = new List<MyRecord>();
            for (int i = 0; i < 20000; i++)
            {
                MyRecord inst = new MyRecord();
                inst.Data = "Data-" + i.ToString("D5");
                inst.DOB = DateTime.Now;
                conn.Execute("INSERT INTO MyRecord(Data, DOB) VALUES(@Data, @DOB)", inst);
            }
            
            RefreshGrid();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (null == conn) return;
            conn.Execute("DELETE FROM MyRecord");
            RefreshGrid();
        }
    }

    public class MyRecord
    {
        public int Id { get; set; }
        public string Data { get; set; }
        public DateTime DOB { get; set; }
    }
}
