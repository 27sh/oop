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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            label11.Text = DataManager.Books.Count.ToString();
            label12.Text = DataManager.Users.Count.ToString();
            label13.Text = DataManager.Books.Where((x) => x.IsBorrowed).Count().ToString();
            label14.Text = DataManager.Books.Where((x) =>
            { return x.IsBorrowed && x.BorrowedAt.AddDays(7) < DateTime.Now; }).Count().ToString();

            dataGridView1.DataSource = DataManager.Books;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void bookBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtIsbn.Text == "")
                MessageBox.Show("Isbn을 입력해주세요");
            else if (txtUserId.Text == "")
                MessageBox.Show("사용자 Id를 입력해주세요");
            else
            {
                Book book = DataManager.Books.Single((x) => x.Isbn == txtIsbn.Text);
                if (book.IsBorrowed)
                    MessageBox.Show("이미 대여 중인 도서입니다");
                else
                {
                    User user = DataManager.Users.Single((x) => x.Id.ToString() == txtUserId.Text);
                    book.UserId = user.Id;
                    book.UserName = user.Name;
                    book.IsBorrowed = true;
                    book.BorrowedAt = DateTime.Now;

                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = DataManager.Books;
                    DataManager.Save();

                    MessageBox.Show("/" + book.Name + "/이 " + user.Name + "/님께 대여되었습니다");

                    label13.Text = DataManager.Books.Where((x) => x.IsBorrowed).Count().ToString();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtIsbn.Text == "")
                MessageBox.Show("Isbn을 입력해주세요");
            else
            {
                Book book = DataManager.Books.Single((x) => x.Isbn == txtIsbn.Text);
                if (book.IsBorrowed)
                {

                    User user = DataManager.Users.Single((x) => x.Id.ToString() == book.UserId.ToString());
                    book.UserId = 0;
                    book.UserName = "";
                    book.IsBorrowed = false;
                    book.BorrowedAt = new DateTime();

                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = DataManager.Books;
                    DataManager.Save();

                    if (book.BorrowedAt.AddDays(7) > DateTime.Now)
                        MessageBox.Show("/" + book.Name + "/이 연체 상태로 반납되었습니다");
                    else
                        MessageBox.Show("/" + book.Name + "/이 반납되었습니다");

                    label13.Text = DataManager.Books.Where((x) => x.IsBorrowed).Count().ToString();
                }
                else
                    MessageBox.Show("대여 상태가 아닙니다");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Book book = dataGridView1.CurrentRow.DataBoundItem as Book;
                txtIsbn.Text = book.Isbn;
                txtName.Text = book.Name;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void 도서관리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new Form2();
            form.ShowDialog();
        }

        private void 사용자관리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form3().ShowDialog();
        }
    }
}
