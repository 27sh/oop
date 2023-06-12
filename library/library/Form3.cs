using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace library
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();

            dataGridView1.DataSource = DataManager.Users;
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (txtId.Text == "")
                MessageBox.Show("Id를 입력해야 합니다");
            else if (DataManager.Users.Exists((x) => int.Parse(txtId.Text) == x.Id))
                MessageBox.Show("사용자 Id 가 겹칩니다");
            else
            {
                User user = new User();
                user.Id = int.Parse(txtId.Text);
                user.Name = txtName.Text;
                DataManager.Users.Add(user);

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = DataManager.Users;
                DataManager.Save();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            User user = DataManager.Users.Single((x) => int.Parse(txtId.Text) == x.Id);
            user.Name = txtName.Text;

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = DataManager.Users;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            User user = DataManager.Users.Single((x) => int.Parse(txtId.Text) == x.Id);
            DataManager.Users.Remove(user);

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = DataManager.Users;
        }
    }
}
