using System;

class Program
{
    static void Main(string[] args)
    {
        // Tạo đối tượng StudentManager
        StudentManager manager = new StudentManager();

        // Nhập dữ liệu học sinh từ người dùng
        Console.WriteLine("Nhap danh sach hoc sinh:");
        manager.AddStudent();

        // a. In toàn bộ danh sách học sinh
        manager.PrintAllStudents();

        // b. Tìm học sinh có tuổi từ 15 đến 18
        manager.PrintStudentsInAgeRange(15, 18);

        // c. Tìm học sinh có tên bắt đầu bằng chữ "A"
        manager.PrintStudentsWithNameStartingWith('A');

        // d. Tính tổng tuổi
        manager.PrintTotalAge();

        // e. Tìm học sinh có tuổi lớn nhất
        manager.PrintOldestStudent();

        // f. Sắp xếp học sinh theo tuổi tăng dần
        manager.PrintStudentsSortedByAge();

        Console.WriteLine("\nKet chuong trinh.");
    }
}
