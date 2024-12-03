using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaiTapVeNhaBuoi4
{
    public partial class frmGridView : Form
    {
        private List<NhanVien> danhSachNV = new List<NhanVien>();
        public frmGridView()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Tải dữ liệu mẫu
            danhSachNV.Add(new NhanVien { MSNV = "NV001", TenNV = "Nguyễn Thị Thu Hiền", LuongCB = 8500000 });
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = danhSachNV;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                var nhanVien = (NhanVien)dataGridView1.CurrentRow.DataBoundItem;
                danhSachNV.Remove(nhanVien);
                dataGridView1.DataSource = null; // Cập nhật lại DataGridView
                dataGridView1.DataSource = danhSachNV;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            NhanVienForm form = new NhanVienForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                danhSachNV.Add(form.NhanVienMoi);
                dataGridView1.DataSource = null; // Cập nhật lại DataGridView
                dataGridView1.DataSource = danhSachNV;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                var nhanVien = (NhanVien)dataGridView1.CurrentRow.DataBoundItem;
                NhanVienForm form = new NhanVienForm(nhanVien);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    dataGridView1.DataSource = null; // Cập nhật lại DataGridView
                    dataGridView1.DataSource = danhSachNV;
                }
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
