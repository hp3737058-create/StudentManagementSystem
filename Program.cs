
using System.Text.Json;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Data
{
    public class StudentStore
    {
        private readonly string _filePath;

        public StudentStore(IWebHostEnvironment environment)
        {
            string folder = Path.Combine(
                environment.ContentRootPath, "App_Data");

            Directory.CreateDirectory(folder);

            _filePath = Path.Combine(folder, "students.json");
        }

        public List<Student> GetAll()
        {
            if (!File.Exists(_filePath))
                return new List<Student>();

            string json = File.ReadAllText(_filePath);

            return JsonSerializer.Deserialize<List<Student>>(json)
                   ?? new List<Student>();
        }

        public void SaveAll(List<Student> students)
        {
            string json = JsonSerializer.Serialize(
                students,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                });

            File.WriteAllText(_filePath, json);
        }
    }
}
