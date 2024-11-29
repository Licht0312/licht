using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ListViewDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeListView();
        }
        private void InitializeListView()
        {
            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.GridLines = true;

            // Thêm các cột vào ListView
            listView1.Columns.Add("LastName", 150);
            listView1.Columns.Add("FirstName", 150);
            listView1.Columns.Add("Phone", 150);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tạo một ListViewItem mới
            ListViewItem item = new ListViewItem(txtLastName.Text);
            item.SubItems.Add(txtFirstName.Text);
            item.SubItems.Add(txtPhone.Text);

            // Thêm item vào ListView
            listView1.Items.Add(item);

            // Xóa dữ liệu TextBox sau khi thêm
            ClearTextBoxes();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn dòng cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Xóa Item được chọn
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                listView1.Items.Remove(item);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui long chon dong can sua!", "Thong bao!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy item được chọn
            ListViewItem selectedItem = listView1.SelectedItems[0];

            // Cập nhật giá trị mới
            selectedItem.SubItems[0].Text = txtLastName.Text;
            selectedItem.SubItems[1].Text = txtFirstName.Text;
            selectedItem.SubItems[2].Text = txtPhone.Text;

            // Xóa dữ liệu TextBox sau khi sửa
            ClearTextBoxes();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listView1.SelectedItems[0];
                txtLastName.Text = selectedItem.SubItems[0].Text;
                txtFirstName.Text = selectedItem.SubItems[1].Text;
                txtPhone.Text = selectedItem.SubItems[2].Text;
            }
        }
            private void ClearTextBoxes()
        {
            txtLastName.Clear();
            txtFirstName.Clear();
            txtPhone.Clear();
        }
    

    }
}
