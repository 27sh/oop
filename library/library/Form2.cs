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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            dataGridView1.DataSource = DataManager.Books;
        }



        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Book book = dataGridView1.CurrentRow.DataBoundItem as Book;
            txtIsbn.Text = book.Isbn;
            txtName.Text = book.Name;
            txtPage.Text = book.Page.ToString();
            txtPublisher.Text = book.Publisher;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (DataManager.Books.Exists((x) => x.Isbn == txtIsbn.Text))
                MessageBox.Show("이미 존재하는 도서입니다");
            else
            {
                Book book = new Book();
                book.Isbn = txtIsbn.Text;
                book.Name = txtName.Text;
                book.Publisher = txtPublisher.Text;
                book.Page = int.Parse(txtPage.Text);
                DataManager.Books.Add(book);

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = DataManager.Books;
                DataManager.Save();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtIsbn.Text == "")
                MessageBox.Show("Isbn을 입력해야 합니다");
            else
            {
                Book book = DataManager.Books.Single((x) => x.Isbn == txtIsbn.Text);
                book.Name = txtName.Text;
                book.Publisher = txtPublisher.Text;
                book.Page = int.Parse(txtPage.Text);

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = DataManager.Books;
                DataManager.Save();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtIsbn.Text == "")
                MessageBox.Show("Isbn을 입력해야 합니다");
            else
            {
                Book book = DataManager.Books.Single((x) => x.Isbn == txtIsbn.Text);

                DataManager.Books.Remove(book);

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = DataManager.Books;
                DataManager.Save();
            }
        }
    }
}
