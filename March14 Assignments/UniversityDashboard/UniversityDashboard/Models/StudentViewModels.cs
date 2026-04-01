namespace UniversityDashboard.Models
{
    public class StudentCoursesVM
    {
        public int StudentId { get; set; }

        public string Name { get; set; } = "";
        public string Branch { get; set; } = "";

        public List<string> CourseTitles { get; set; } = new();

        public int CourseCount => CourseTitles.Count;
    }

    public class CourseDetailVM
    {
        public string Title { get; set; } = "";
        public int Credits { get; set; }
        public string Department { get; set; } = "";
        public string LatestGrade { get; set; } = "";
    }

    public class StudentDetailVM
    {
        public string Name { get; set; } = "";
        public string Branch { get; set; } = "";

        public List<CourseDetailVM> Courses { get; set; } = new();
    }
}