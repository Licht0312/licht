using System;
using System.Collections.Generic;
using System.Linq;
using QLHS;

public class StudentManager
{
    private List<Student> students;

    public StudentManager()
    {
        students = new List<Student>();
    }

    // Phương thức thêm học sinh vào danh sách
    public void AddStudent()
    {
        Console.WriteLine("Nhap so luong hoc sinh :");
        int count;

        // Kiểm tra nhập số lượng học sinh hợp lệ
        while (!int.TryParse(Console.ReadLine(), out count) || count <= 0)
        {
            Console.WriteLine("Vui long nhap so nguyen duong hop le.");
        }

        for (int i = 0; i < count; i++)
        {
            Console.WriteLine($"\nNhap thong tin hoc sinh thu {i + 1}:");

            int id;
            while (true)
            {
                Console.Write("Id: ");
                if (int.TryParse(Console.ReadLine(), out id) && id > 0)
                    break;
                Console.WriteLine("Vui long nhap 1 so nguyen duong cho Id.");
            }

            Console.Write("Ten: ");
            string name = Console.ReadLine();

            int age;
            while (true)
            {
                Console.Write("Tuoi: ");
                if (int.TryParse(Console.ReadLine(), out age) && age > 0)
                    break;
                Console.WriteLine("Vui long nhap 1 so nguyen duong cho Tuổi.");
            }

            // Thêm học sinh vào danh sách
            students.Add(new Student(id, name, age));
        }

        Console.WriteLine("\nĐa them hoc sinh thanh cong!");
    }


    // a. In danh sách toàn bộ học sinh
    public void PrintAllStudents()
    {
        Console.WriteLine("\na. Danh sach toan bo hoc sinh:");
        foreach (var student in students)
        {
            Console.WriteLine($"Id: {student.Id}, Name: {student.Name}, Age: {student.Age}");
        }
    }

    // b. Tìm học sinh có tuổi từ 15 đến 18
    public void PrintStudentsInAgeRange(int minAge, int maxAge)
    {
        var result = students.Where(s => s.Age >= minAge && s.Age <= maxAge);
        Console.WriteLine($"\nb. Hoc sinh co tuoi tu {minAge} den {maxAge}:");
        foreach (var student in result)
        {
            Console.WriteLine($"Id: {student.Id}, Name: {student.Name}, Age: {student.Age}");
        }
    }

    // c. Tìm học sinh có tên bắt đầu bằng chữ "A"
    public void PrintStudentsWithNameStartingWith(char startLetter)
    {
        // Tìm kiếm học sinh có tên bắt đầu bằng chữ cái (không phân biệt hoa/thường)
        var result = students.Where(s => s.Name.StartsWith(startLetter.ToString(), StringComparison.OrdinalIgnoreCase));

        Console.WriteLine($"\nc. Hoc sinh co ten bat dau bang chu'{startLetter}':");

        // Kiểm tra danh sách kết quả
        if (!result.Any())
        {
            Console.WriteLine($"Khong co hoc sinh nao co ten bat dau bang chu '{startLetter}'.");
        }
        else
        {
            // In thông tin học sinh
            foreach (var student in result)
            {
                Console.WriteLine($"Id: {student.Id}, Name: {student.Name}, Age: {student.Age}");
            }
        }
    }

    // d. Tính tổng tuổi của tất cả học sinh
    public void PrintTotalAge()
    {
        int totalAge = students.Sum(s => s.Age);
        Console.WriteLine($"\nd. Tong  tuoi cua tat ca hoc sinh: {totalAge}");
    }

    // e. Tìm học sinh có tuổi lớn nhất
    public void PrintOldestStudent()
    {
        // Kiểm tra danh sách rỗng
        if (!students.Any())
        {
            Console.WriteLine("\ne. Khong co hoc sinh nao trong danh sach.");
            return;
        }

        // Tìm tuổi lớn nhất
        int maxAge = students.Max(s => s.Age);

        // Lọc học sinh có tuổi lớn nhất
        var oldestStudents = students.Where(s => s.Age == maxAge);

        Console.WriteLine("\ne. Hoc sinh co tuoi lon nhat:");
        foreach (var student in oldestStudents)
        {
            Console.WriteLine($"Id: {student.Id}, Name: {student.Name}, Age: {student.Age}");
        }
    }

    // f. Sắp xếp danh sách theo tuổi tăng dần
    public void PrintStudentsSortedByAge()
    {
        var sortedStudents = students.OrderBy(s => s.Age);
        Console.WriteLine("\nf. Danh sach hoc sinh theo tuoi tang dan:");
        foreach (var student in sortedStudents)
        {
            Console.WriteLine($"Id: {student.Id}, Name: {student.Name}, Age: {student.Age}");
        }
    }
}