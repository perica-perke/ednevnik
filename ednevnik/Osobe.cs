using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;

namespace Ednevnik410b
{
    public partial class Osobe : Form
    {
        private int index = 0;
        private SqlDataAdapter adapter;
        private DataTable tabela;

        public Osobe()
        {
            InitializeComponent();
        }

        private void ApplyVisualStyle()
        {
            this.BackColor = Color.FromArgb(30, 30, 30);

            foreach (Control c in this.Controls)
                StyleControl(c);
        }

        private void StyleControl(Control c)
        {
            if (c is Button btn)
            {
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.BackColor = Color.FromArgb(45, 45, 48);
                btn.ForeColor = Color.White;
                btn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                btn.Height = 32;
            }

            if (c is TextBox tb)
            {
                tb.BackColor = Color.FromArgb(37, 37, 38);
                tb.ForeColor = Color.White;
                tb.BorderStyle = BorderStyle.FixedSingle;
                tb.Font = new Font("Segoe UI", 9F);
            }

            if (c is Label lbl)
            {
                lbl.ForeColor = Color.Gainsboro;
                lbl.Font = new Font("Segoe UI", 9F);
            }

            foreach (Control child in c.Controls)
                StyleControl(child);
        }

        private void LoadData()
        {
            using (SqlConnection veza = konekcija.povezi())
            {
                adapter = new SqlDataAdapter("SELECT * FROM osoba", veza);
                tabela = new DataTable();
                adapter.Fill(tabela);
            }
        }

        private void DisplayData()
        {
            if (tabela.Rows.Count == 0) return;

            button5.Enabled = index > 0;                    
            button6.Enabled = index < tabela.Rows.Count - 1; 

            DataRow r = tabela.Rows[index];

            textBox0.Text = r[0].ToString();
            textBox1.Text = r[1].ToString();
            textBox2.Text = r[2].ToString();
            textBox3.Text = r[3].ToString();
            textBox4.Text = r[4].ToString();
            textBox5.Text = r[5].ToString();
            textBox6.Text = r[6].ToString();
            textBox7.Text = r[7].ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
            DisplayData();
            ApplyVisualStyle();
        }

        private void button4_Click(object sender, EventArgs e) 
        {
            index = 0;
            DisplayData();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (index > 0)
            {
                index--;
                DisplayData();
            }
        }

        private void button6_Click(object sender, EventArgs e) 
        {
            if (index < tabela.Rows.Count - 1)
            {
                index++;
                DisplayData();
            }
        }

        private void button7_Click(object sender, EventArgs e) 
        {
            index = tabela.Rows.Count - 1;
            DisplayData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql =
                "INSERT INTO osoba (ime, prezime, adresa, jmbg, email, pass, uloga) VALUES (" +
                $"'{textBox1.Text}','{textBox2.Text}','{textBox3.Text}','{textBox4.Text}'," +
                $"'{textBox5.Text}','{textBox6.Text}',{textBox7.Text})";

            ExecuteSql(sql);

            LoadData();
            index = tabela.Rows.Count - 1;
            DisplayData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sql =
                "UPDATE osoba SET " +
                $"ime='{textBox1.Text}', " +
                $"prezime='{textBox2.Text}', " +
                $"adresa='{textBox3.Text}', " +
                $"jmbg='{textBox4.Text}', " +
                $"email='{textBox5.Text}', " +
                $"pass='{textBox6.Text}' " +
                $"WHERE id={textBox0.Text}";

            ExecuteSql(sql);

            LoadData();
            DisplayData();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Obrisati zapis?", "Potvrda",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            string sql = $"DELETE FROM osoba WHERE id={textBox0.Text}";
            ExecuteSql(sql);

            LoadData();

            if (tabela.Rows.Count == 0)
            {
                index = 0;
                ClearFields();
                return;
            }

            if (index >= tabela.Rows.Count)
                index = tabela.Rows.Count - 1;

            DisplayData();
        }

        private void ExecuteSql(string sql)
        {
            using (SqlConnection veza = konekcija.povezi())
            using (SqlCommand cmd = new SqlCommand(sql, veza))
            {
                veza.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void ClearFields()
        {
            textBox0.Clear();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
        }
    }
}
