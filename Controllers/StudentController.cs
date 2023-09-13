 
using Microsoft.AspNetCore.Mvc;
using DAL_PRACTICE.Models;
using System.Data.SqlClient;
using DAL_PRACTICE.App_Code;
using System.Data;
using System.Collections.Generic;
 
namespace DAL_PRACTICE.Controllers
{
    public class StudentController : Controller
    {
        private readonly IDAL _dal;
        public StudentController(IDAL dal)
        {
            _dal = dal;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Addstu(Student stu)
        {
            int i = 0;
            string query = @"insert into practice2 (Name,Email,Password,Course) values (@Name,@Email,@Password,@Course);";
            if (stu.Id > 0)
            {
                  query = @"UPDATE practice2 SET Name = @Name, Email= @Email,   Password = @Password, Course= @Course WHERE Id = @Id; ";
            }
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Id",stu.Id),
                new SqlParameter("@Name",stu.Name),
                new SqlParameter("@Email",stu.Email),
                new SqlParameter("@Password",stu.Password),
                new SqlParameter("@Course",stu.Course)

            };
            i=_dal.Execute(query, param);

            return Json(i);

        }
        public IActionResult Details()
        {
            List<Student> studentslist = new List<Student>();
            DataTable dt =new DataTable();
            string query = "Select * from practice2";
            dt = _dal.Get(query);
            foreach (DataRow row in dt.Rows)
            {
                studentslist.Add(new Student
                {
         
                     Id = row["Id"] is DBNull ? 0 : Convert.ToInt32(row["Id"]),
                    Name = row["Name"] is DBNull ? String.Empty : row["Name"].ToString(),
                    Email = row["Email"] is DBNull ? String.Empty : row["Email"].ToString(),
                    Password = row["Password"] is DBNull ? String.Empty : row["Password"].ToString(),
                    Course = row["Course"] is DBNull ? String.Empty : row["Course"].ToString(), 
          
                });
            }
            return View(studentslist);


        }
        public IActionResult Delete(int id)
        {
            int i = 0;
            string query = "Delete from practice2 where Id = @Id";
            SqlParameter[] param =
            {
                new SqlParameter("@Id", id)
            };
            i= _dal.Execute(query, param);
            return Json(i);
        }
        public IActionResult Empedit(int id)
        {
            Student stu = new Student();
            try
            {
                if (id > 0)
                {
                    DataTable dt = new DataTable();
                    string sqlquery = "Select * from practice2 where id = @Id";
                    SqlParameter[] param =
                    {
                        new SqlParameter ("@Id",id)
                     };
                        dt = _dal.Get(sqlquery, param);
                     if(dt!=null && dt.Rows.Count>0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            stu.Name = dr["Name"] is DBNull ? String.Empty : (dr["Name"]).ToString();
                            stu.Email = dr["Email"] is DBNull ? String.Empty : (dr["Email"]).ToString();
                            stu.Password = dr["Password"] is DBNull ? String.Empty : (dr["Password"]).ToString();
                            stu.Course = dr["Course"] is DBNull ? String.Empty : (dr["Course"]).ToString();
                        }
                    }
                }
              
            }
            catch (Exception)
            {

                throw;
            }
            return View(stu);
        }
        public IActionResult StudentDetails(int id)
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM practice2 WHERE Id = @Id";
            SqlParameter[] param =
            {
        new SqlParameter("@Id", id)
    };

            dt = _dal.Get(query, param);

            if (dt.Rows.Count == 1)
            {
                DataRow row = dt.Rows[0];

                var student = new Student
                {
                    Id = row["Id"] is DBNull ? 0 : Convert.ToInt32(row["Id"]),
                    Name = row["Name"] is DBNull ? String.Empty : row["Name"].ToString(),
                    Email = row["Email"] is DBNull ? String.Empty : row["Email"].ToString(),
                    Password = row["Password"] is DBNull ? String.Empty : row["Password"].ToString(),
                    Course = row["Course"] is DBNull ? String.Empty : row["Course"].ToString(),
                };

                return View(student);
            }
            else
            {
                // Handle the case where the student with the given id was not found
                return NotFound();
            }
        }


    }
}
