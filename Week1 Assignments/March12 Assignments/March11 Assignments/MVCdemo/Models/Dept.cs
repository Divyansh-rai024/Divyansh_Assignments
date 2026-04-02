namespace MVCdemo.Models
{
    public class Dept
    {
        public int DeptID { get; set; }
        public string DeptName { get; set; }

        //Master-side collection
        public List<Employee_> Employees { get; set; } = new List<Employee_>();
    }
}
