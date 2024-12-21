using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kiemtra.Models;

namespace Kiemtra
{
    public partial class frmSanPham : Form
    {
        public frmSanPham()
        {
            InitializeComponent();
        }

        public event Action<string> OnDataSendToParent;
        private void frmSanPham_Load(object sender, EventArgs e)
        {


            // Gắn sự kiện khi chọn dòng trong ListView
    lvSanPham.SelectedIndexChanged += lvSanPham_SelectedIndexChanged;

            try
            {
                Model1 context = new Model1();
                List<LoaiSP> listLoaiSP = context.LoaiSPs.ToList();

                // Gắn dữ liệu vào ComboBox
                FillLoaiSPCombobox(listLoaiSP);
                List<Sanpham> listsanpham = context.Sanphams.ToList();
                BindGrid(listsanpham);

            }
            
            catch (Exception ex) {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

                lvSanPham.View = View.Details; // Hiển thị dạng bảng
            lvSanPham.Columns.Clear();    // Xóa các cột cũ (nếu có)
            lvSanPham.Columns.Add("Mã SP", 100, HorizontalAlignment.Left);
            lvSanPham.Columns.Add("Tên SP", 200, HorizontalAlignment.Left);
            lvSanPham.Columns.Add("Ngày nhập", 150, HorizontalAlignment.Left);
            lvSanPham.Columns.Add("Loại SP", 100, HorizontalAlignment.Left);


        }

        private void FillLoaiSPCombobox(List<LoaiSP> listLoaiSP)
        {
            // Gắn danh sách dữ liệu vào ComboBox
            cmbLoaiSP.DataSource = listLoaiSP;
            cmbLoaiSP.DisplayMember = "TenLoai"; // Hiển thị tên loại sản phẩm
            cmbLoaiSP.ValueMember = "MaLoai";   // Giá trị của loại sản phẩm (mã)
            cmbLoaiSP.SelectedIndex = -1;       // Không chọn mục nào lúc đầu
        }

        private void btnSendDataToParent_Click(object sender, EventArgs e)
        {
            OnDataSendToParent?.Invoke("Dữ liệu từ Form con"); // Kích hoạt sự kiện
        }

        private void FillLoaiSPListView(List<LoaiSP> listloaisp)
        {
            // Xóa các mục hiện tại trong ListView
            lvSanPham.Items.Clear();

            // Thêm từng loại sản phẩm vào ListView
            foreach (var item in listloaisp)
            {
                ListViewItem lvItem = new ListViewItem(item.MaLoai); // Cột 1: MaLoai
                lvItem.SubItems.Add(item.TenLoai);                  // Cột 2: TenLoai

                lvSanPham.Items.Add(lvItem);
            }
        }

        private void BindGrid(List<Sanpham> listsanpham)
        {
            // Xóa tất cả các mục hiện có trong ListView
            lvSanPham.Items.Clear();

            // Thêm từng sản phẩm vào ListView
            foreach (var item in listsanpham)
            {
                ListViewItem lvItem = new ListViewItem(item.MaSP); // Cột 1: MaSP
                lvItem.SubItems.Add(item.TenSP);                  // Cột 2: TenSP
                lvItem.SubItems.Add(item.NgayNhap.ToString("dd/MM/yyyy")); // Cột 3: NgayNhap
                lvItem.SubItems.Add(item.MaLoai);                 // Cột 4: MaLoai

                lvSanPham.Items.Add(lvItem);
            }
        }

        private void btnADD_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaSP.Text) ||
        string.IsNullOrWhiteSpace(txtTenSP.Text) ||
        cmbLoaiSP.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra trùng mã sản phẩm
            foreach (ListViewItem item in lvSanPham.Items)
            {
                if (item.SubItems[0].Text == txtMaSP.Text)
                {
                    MessageBox.Show("Mã sản phẩm đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Thêm dữ liệu vào ListView
            ListViewItem newItem = new ListViewItem(txtMaSP.Text);
            newItem.SubItems.Add(txtTenSP.Text);
            newItem.SubItems.Add(dateTimePicker1.Value.ToString("dd/MM/yyyy"));
            newItem.SubItems.Add(cmbLoaiSP.Text);
            lvSanPham.Items.Add(newItem);

            // Xóa trắng các trường nhập
            txtMaSP.Clear();
            txtTenSP.Clear();
            cmbLoaiSP.SelectedIndex = -1;
            dateTimePicker1.Value = DateTime.Now;

            MessageBox.Show("Thêm sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnEDIT_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có dòng nào được chọn trong ListView không
            if (lvSanPham.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần chỉnh sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy sản phẩm được chọn
            ListViewItem selectedItem = lvSanPham.SelectedItems[0];
            string maSP = selectedItem.SubItems[0].Text;

            // Lấy dữ liệu từ database
            Model1 context = new Model1();
            Sanpham sanpham = context.Sanphams.FirstOrDefault(sp => sp.MaSP == maSP);

            if (sanpham == null)
            {
                MessageBox.Show("Không tìm thấy sản phẩm để chỉnh sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Cập nhật thông tin sản phẩm từ các ô nhập liệu
            sanpham.TenSP = txtTenSP.Text;
            sanpham.NgayNhap = dateTimePicker1.Value;
            sanpham.MaLoai = cmbLoaiSP.SelectedValue.ToString();

            // Lưu thay đổi vào database
            context.SaveChanges();

            // Cập nhật lại ListView
            BindGrid(context.Sanphams.ToList());

            // Xóa dữ liệu trên form nhập
            txtMaSP.Clear();
            txtTenSP.Clear();
            dateTimePicker1.Value = DateTime.Now;
            cmbLoaiSP.SelectedIndex = -1;

            MessageBox.Show("Cập nhật sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnDEL_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem người dùng đã chọn dòng nào trong ListView chưa
                if (lvSanPham.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn sản phẩm cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Lấy mã sản phẩm từ dòng được chọn
                string maSP = lvSanPham.SelectedItems[0].SubItems[0].Text;

                // Hỏi xác nhận trước khi xóa
                DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa sản phẩm có mã: {maSP} không?",
                                                      "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Kết nối đến cơ sở dữ liệu
                    Model1 context = new Model1();

                    // Tìm sản phẩm trong cơ sở dữ liệu
                    Sanpham sanpham = context.Sanphams.FirstOrDefault(sp => sp.MaSP == maSP);

                    if (sanpham != null)
                    {
                        // Xóa sản phẩm khỏi cơ sở dữ liệu
                        context.Sanphams.Remove(sanpham);
                        context.SaveChanges();

                        // Xóa sản phẩm khỏi ListView
                        lvSanPham.Items.Remove(lvSanPham.SelectedItems[0]);

                        MessageBox.Show("Xóa sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Sản phẩm không tồn tại trong cơ sở dữ liệu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindListView(List<Sanpham> sanphamList)
        {
            lvSanPham.Items.Clear();
            foreach (var sp in sanphamList)
            {
                ListViewItem item = new ListViewItem(sp.MaSP);
                item.SubItems.Add(sp.TenSP);
                item.SubItems.Add(dateTimePicker1.Value.ToString("dd/MM/yyyy"));
                item.SubItems.Add(sp.MaLoai);
                lvSanPham.Items.Add(item);
            }
        }


        private void btnSAVE_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra thông tin nhập vào
                if (string.IsNullOrWhiteSpace(txtMaSP.Text) ||
                    string.IsNullOrWhiteSpace(txtTenSP.Text) ||
                    string.IsNullOrWhiteSpace(cmbLoaiSP.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Lấy dữ liệu từ form
                string maSP = txtMaSP.Text.Trim();
                string tenSP = txtTenSP.Text.Trim();
                DateTime ngayNhap = dateTimePicker1.Value;
                string maLoai = cmbLoaiSP.SelectedValue.ToString(); // Lấy giá trị mã loại sản phẩm từ ComboBox

                // Kết nối đến cơ sở dữ liệu
                using (Model1 context = new Model1())
                {
                    // Kiểm tra sản phẩm có tồn tại trong cơ sở dữ liệu không (Cập nhật nếu có, thêm mới nếu không)
                    Sanpham sanpham = context.Sanphams.FirstOrDefault(sp => sp.MaSP == maSP);

                    if (sanpham == null)
                    {
                        // Nếu sản phẩm chưa tồn tại, thêm mới sản phẩm
                        sanpham = new Sanpham
                        {
                            MaSP = maSP,
                            TenSP = tenSP,
                            NgayNhap = ngayNhap,
                            MaLoai = maLoai
                        };

                        context.Sanphams.Add(sanpham);  // Thêm sản phẩm mới vào DbSet
                        MessageBox.Show("Thêm sản phẩm mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Nếu sản phẩm đã tồn tại, cập nhật thông tin sản phẩm
                        sanpham.TenSP = tenSP;
                        sanpham.NgayNhap = ngayNhap;
                        sanpham.MaLoai = maLoai;

                        MessageBox.Show("Cập nhật sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    // Lưu thay đổi vào cơ sở dữ liệu
                    context.SaveChanges();

                    // Cập nhật lại ListView (hoặc GridView) để hiển thị thông tin mới nhất
                    BindListView(context.Sanphams.ToList());
                }
            }
            catch (Exception ex)
            {
                // Bắt lỗi nếu có ngoại lệ và hiển thị thông báo lỗi
                MessageBox.Show($"Có lỗi xảy ra khi lưu dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnDSAVE_Click(object sender, EventArgs e)
        {
            try
            {
                // Xóa nội dung trên các TextBox và ComboBox
                txtMaSP.Clear();
                txtTenSP.Clear();
                dateTimePicker1.Value = DateTime.Now;
                if (cmbLoaiSP.Items.Count > 0)
                {
                    cmbLoaiSP.SelectedIndex = 0; // Đặt lại giá trị mặc định (dòng đầu tiên)
                }

                // Hiển thị thông báo xác nhận
                MessageBox.Show("Các thay đổi chưa lưu đã được hủy!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEXIT_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lvSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvSanPham.SelectedItems.Count > 0) // Kiểm tra có dòng được chọn
            {
                ListViewItem selectedItem = lvSanPham.SelectedItems[0];

                // Gán giá trị từ dòng được chọn sang các TextBox
                txtMaSP.Text = selectedItem.SubItems[0].Text;  // Mã SP
                txtTenSP.Text = selectedItem.SubItems[1].Text; // Tên SP
                dateTimePicker1.Value = DateTime.Parse(selectedItem.SubItems[2].Text); // Ngày nhập
                cmbLoaiSP.Text = selectedItem.SubItems[3].Text; // Loại SP
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy giá trị từ TextBox txtTimkiem
                string maSP = txtTimSP.Text.Trim();

                // Kiểm tra nếu chưa nhập mã sản phẩm
                if (string.IsNullOrEmpty(maSP))
                {
                    MessageBox.Show("Vui lòng nhập mã sản phẩm cần tìm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tìm kiếm sản phẩm theo Mã SP
                Model1 context = new Model1();
                Sanpham sanpham = context.Sanphams.FirstOrDefault(sp => sp.MaSP == maSP);

                if (sanpham != null)
                {
                    // Hiển thị thông tin sản phẩm qua MessageBox
                    string thongBao = $"Mã SP: {sanpham.MaSP}\n" +
                                      $"Tên SP: {sanpham.TenSP}\n" +
                                      $"Ngày nhập: {sanpham.NgayNhap.ToString("dd/MM/yyyy")}\n" +
                                      $"Loại SP: {sanpham.MaLoai}";

                    MessageBox.Show(thongBao, "Thông tin sản phẩm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Thông báo khi không tìm thấy
                    MessageBox.Show("Không tìm thấy sản phẩm với mã vừa nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
