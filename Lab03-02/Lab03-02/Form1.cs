using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab03_02
{
    public partial class Form1 : Form
    {
        private RichTextBox richTextBox;
        private MenuStrip menuStrip;
        private ToolStrip toolStrip;
        private ToolStripComboBox cmbFonts, cmbSizes;
        public Form1()
        {
            InitializeComponent();
        }

        private void cmbFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null)
            {
                Font currentFont = richTextBox1.SelectionFont;
                string fontName = cmbFont.SelectedItem.ToString();
                richTextBox1.SelectionFont = new Font(fontName, currentFont.Size, currentFont.Style);
            }
        }

        private void cmbSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null)
            {
                Font currentFont = richTextBox1.SelectionFont;
                float fontSize = float.Parse(cmbSize.SelectedItem.ToString());
                richTextBox1.SelectionFont = new Font(currentFont.FontFamily, fontSize, currentFont.Style);
            }
        }

        private void cmbFont_Click(object sender, EventArgs e)
        {

        }

        private void địnhDạngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDlg = new FontDialog();
            fontDlg.ShowColor = true; // Cho phép thay đổi màu chữ
            fontDlg.ShowApply = false; // Ẩn nút Apply (không cần thiết)
            fontDlg.ShowEffects = true; // Cho phép hiệu ứng (in đậm, nghiêng, gạch chân, ...)
            fontDlg.Font = richTextBox1.SelectionFont; // Lấy font hiện tại của đoạn văn bản đã chọn
            fontDlg.Color = richTextBox1.SelectionColor; // Lấy màu chữ hiện tại của đoạn văn bản đã chọn

            // Hiển thị FontDialog
            if (fontDlg.ShowDialog() == DialogResult.OK)
            {
                // Cập nhật font và màu chữ cho đoạn văn bản được chọn
                if (richTextBox1.SelectionLength > 0) // Nếu có văn bản được chọn
                {
                    richTextBox1.SelectionFont = fontDlg.Font; // Áp dụng font
                    richTextBox1.SelectionColor = fontDlg.Color; // Áp dụng màu
                }
                else // Nếu không có văn bản nào được chọn
                {
                    MessageBox.Show("Vui lòng chọn văn bản để định dạng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        private void CreateNewFile()
        {
            if (!string.IsNullOrEmpty(richTextBox1.Text))
            {
                DialogResult result = MessageBox.Show("Bạn có muốn lưu nội dung hiện tại trước khi tạo văn bản mới?",
                                                      "Thông báo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    SaveFile();
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }
            }

            // Xóa nội dung hiện tại và thiết lập lại mặc định
            richTextBox1.Clear();
            richTextBox1.Font = new Font("Tahoma", 14); // Đặt font mặc định
        }
        private void btnNewfile_Click(object sender, EventArgs e)
        {
            CreateNewFile();
        }

        private void Newfile_Click(object sender, EventArgs e)
        {
            CreateNewFile();
        }
        private void SaveFile()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Rich Text Format (*.rtf)|*.rtf|Text Files (*.txt)|*.txt",
                Title = "Lưu nội dung văn bản"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (saveFileDialog.FileName.EndsWith(".rtf"))
                {
                    richTextBox1.SaveFile(saveFileDialog.FileName);
                }
                else
                {
                    System.IO.File.WriteAllText(saveFileDialog.FileName, richTextBox1.Text);
                }
                MessageBox.Show("Lưu tệp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void OpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Rich Text Format (*.rtf)|*.rtf|Text Files (*.txt)|*.txt",
                Title = "Mở tệp tin văn bản"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialog.FileName.EndsWith(".rtf"))
                {
                    richTextBox1.LoadFile(openFileDialog.FileName);
                }
                else
                {
                    richTextBox1.Text = System.IO.File.ReadAllText(openFileDialog.FileName);
                }
            }
        }


        private void SaveFile_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void ToggleFontStyle(FontStyle style)
        {
            if (richTextBox1.SelectionFont == null) return; // Kiểm tra nếu không có văn bản được chọn

            // Lấy font hiện tại của đoạn văn bản
            Font currentFont = richTextBox1.SelectionFont;
            FontStyle newFontStyle;

            // Kiểm tra kiểu font hiện tại đã có kiểu style chưa, nếu có thì tắt nó đi, nếu chưa thì bật
            if (currentFont.Style.HasFlag(style))
            {
                newFontStyle = currentFont.Style & ~style; // Tắt kiểu style
            }
            else
            {
                newFontStyle = currentFont.Style | style; // Bật kiểu style
            }

            // Áp dụng font mới cho đoạn văn bản được chọn
            richTextBox1.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
        }
        private void btnBold_Click(object sender, EventArgs e)
        {
            ToggleFontStyle(FontStyle.Bold);
        }

        private void btnItalic_Click(object sender, EventArgs e)
        {
            ToggleFontStyle(FontStyle.Italic);
        }

        private void btnUnderline_Click(object sender, EventArgs e)
        {
            ToggleFontStyle(FontStyle.Underline);
        }

        private void toolStripSavefile_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (FontFamily font in FontFamily.Families)
            {
                cmbFont.Items.Add(font.Name);
            }

            // Thiết lập font mặc định cho ComboBox (nếu cần)
            if (cmbFont.Items.Count > 0)
            {
                cmbFont.SelectedItem = "Tahoma"; // Đặt font mặc định
            }

            // Khởi tạo ComboBox cho kích thước font
            int[] fontSizes = { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
            foreach (int size in fontSizes)
            {
                cmbSize.Items.Add(size);
            }

            // Thiết lập kích thước font mặc định
            cmbSize.SelectedItem = 14;

            // Đặt nội dung mặc định cho RichTextBox
            richTextBox1.Text = "Chào mừng bạn đến với trình soạn thảo!";
            richTextBox1.Font = new Font("Tahoma", 14, FontStyle.Regular);
        }
    }
}
