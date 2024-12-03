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
    public partial class NhanVienForm : Form
    {
        public NhanVien NhanVienMoi { get; private set; }
        public NhanVienForm()
        {
            InitializeComponent();
        }
        public NhanVienForm(NhanVien nhanVien) : this()
        {
            // Hiển thị dữ liệu lên form nếu sửa
            txtMSNV.Text = nhanVien.MSNV;
            txtTenNV.Text = nhanVien.TenNV;
            txtLuongCB.Text = nhanVien.LuongCB.ToString();
            NhanVienMoi = nhanVien;
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (NhanVienMoi == null)
                NhanVienMoi = new NhanVien();

            NhanVienMoi.MSNV = txtMSNV.Text;
            NhanVienMoi.TenNV = txtTenNV.Text;
            NhanVienMoi.LuongCB = decimal.Parse(txtLuongCB.Text);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void NhanVienForm_Load(object sender, EventArgs e)
        {

        }
    }
}
