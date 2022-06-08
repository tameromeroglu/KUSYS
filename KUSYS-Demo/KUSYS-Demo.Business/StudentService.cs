using KUSYS_Demo.Business.DTOs;
using KUSYS_Demo.Business.Enums;
using KUSYS_Demo.Business.Query;
using KUSYS_Demo.Business.Results;
using KUSYS_Demo.Business.Utils;
using KUSYS_Demo.Data.Contexts;
using KUSYS_Demo.Data.Extensions;
using KUSYS_Demo.Entity;
using KUSYS_Demo.Entity.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS_Demo.Business
{
    public class StudentService
    {
        private readonly IDbContextFactory<KusysContext> _factory;
        private readonly StudentQuery _querySchema;
        private readonly UserManager<User> _userManager;

        public StudentService(IDbContextFactory<KusysContext> factory, StudentQuery querySchema, UserManager<User> userManager)
        {
            _factory = factory;
            _querySchema = querySchema;
            _userManager = userManager;
        }

        public async Task<DataResult<List<StudentDto>>> GetAllStudents()
        {
            using var context = _factory.CreateDbContext();

            var query = context.Students.AsQueryable();

            var data = await _querySchema.SelectStudentList(query).ToListAsync();

            return new DataResult<List<StudentDto>>(ResultStatusEnum.Success,data);
        }


        public async Task<DataResult<StudentDetailDto>> GetStudentDetailById(int id)
        {
            using var context = _factory.CreateDbContext();

            var query = context.Students.AsQueryable();

            var data = await _querySchema.SelectStudentDetail(query).FirstOrDefaultAsync(x => x.Id == id);

            return new DataResult<StudentDetailDto>(ResultStatusEnum.Success, data);
        }

      

        public async Task<DataResult<List<StudentsCoursesDto>>> GetAllStudentsCourses()
        {
            using var context = _factory.CreateDbContext();

            var query = context.Students.AsQueryable();

            var data = await _querySchema.SelectStudentCourse(query).ToListAsync();

            return new DataResult<List<StudentsCoursesDto>>(ResultStatusEnum.Success, data);
        }

        public async Task<DataResult<StudentsCoursesDto>> GetStudentCourseById(int id)
        {
            using var context = _factory.CreateDbContext();

            var query = context.Students.AsQueryable();

            var data = await _querySchema.SelectStudentCourse(query).FirstOrDefaultAsync(x => x.Id == id);

            return new DataResult<StudentsCoursesDto>(ResultStatusEnum.Success, data);
        }


        public async Task<DataResult<List<StudentsCoursesDto>>> GetStudentCourseByUsername(string username)
        {
            using var context = _factory.CreateDbContext();

            var query = context.Students.AsQueryable();

            var data = await _querySchema.SelectStudentCourse(query).ToListAsync();

            data = data.Where(x => RemoveDiacriticsTool.RemoveDiacritics(x.FirstName + x.LastName).Replace(" ","").ToLower() == username).ToList();

            return new DataResult<List<StudentsCoursesDto>>(ResultStatusEnum.Success, data);
        }

        public async Task<DataResult<List<CourseDto>>> GetAllCourses()
        {
            using var context = _factory.CreateDbContext();

            var query = context.Courses.AsQueryable();


            var data = await _querySchema.SelectCourse(query).ToListAsync();

            return new DataResult<List<CourseDto>>(ResultStatusEnum.Success, data);
        }

        public async Task<DataResult<Student>> NewStudent(NewStudentDto newStudent)
        {
            using var context = _factory.CreateDbContext();

            var transactionWrapper = context.BeginTransactionWrapper();

            try
            {
                var student = new Student
                {
                    FirstName = newStudent.FirstName,
                    LastName = newStudent.LastName,
                    BirthDate = newStudent.BirthDate,
                    PhoneNumber = newStudent.PhoneNumber,
                    Adress = newStudent.Adress
                };

                var studentAddResult = await context.AddAsyncExt(student);

                if (studentAddResult == null)
                {
                    return new DataResult<Student>(ResultStatusEnum.Error, "Data Save Failed", null);
                }

                var username = RemoveDiacriticsTool.RemoveDiacritics(newStudent.FirstName + newStudent.LastName);


                var user = new User
                {
                    UserName = username.ToLower(),
                    NormalizedUserName = username.ToUpper(),
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                user.PasswordHash = CreatePasswordHash(user, username.ToLower());

                var addUserResult = await _userManager.CreateAsync(user);

                if (!addUserResult.Succeeded)
                {
                    return new DataResult<Student>(ResultStatusEnum.Error, "User Save Failed", null);

                }


                transactionWrapper.Commit();

                return new DataResult<Student>(ResultStatusEnum.Success, studentAddResult);

            }
            catch (Exception ex)
            {
                transactionWrapper.Rollback();

                return new DataResult<Student>(ResultStatusEnum.Error, ex.Message, null);
            }
            finally
            {
                transactionWrapper.DisposeTransaction();
            }

        }


        public async Task<DataResult<Student>> UpdateStudent(UpdateStudentDto updateStudent)
        {
            using var context = _factory.CreateDbContext();

            var transactionWrapper = context.BeginTransactionWrapper();

            try
            {
                var student = await context.Students.FirstOrDefaultAsync(x => x.Id == updateStudent.Id);

                if (student == null)
                {
                    return new DataResult<Student>(ResultStatusEnum.Error, "Student Not Found", null);

                }
         
                student.PhoneNumber = updateStudent.PhoneNumber;
                student.Adress = updateStudent.Adress;

                var studentUpdateResult = await context.UpdateAsyncExt(student);

                if (studentUpdateResult == null)
                {
                    return new DataResult<Student>(ResultStatusEnum.Error, "Data Update Failed", null);
                }

                transactionWrapper.Commit();

                return new DataResult<Student>(ResultStatusEnum.Success, studentUpdateResult);

            }
            catch (Exception ex)
            {
                transactionWrapper.Rollback();

                return new DataResult<Student>(ResultStatusEnum.Error, ex.Message, null);
            }
            finally
            {
                transactionWrapper.DisposeTransaction();
            }

        }


        public async Task<DataResult<string>> UpdateStudentCourse(StudentsCoursesDto studentsCoursesDto)
        {
            using var context = _factory.CreateDbContext();

            var transactionWrapper = context.BeginTransactionWrapper();

            try
            {
                var student = await context.Students.FirstOrDefaultAsync(x => x.Id == studentsCoursesDto.Id);

                if (student == null)
                {
                    return new DataResult<string>(ResultStatusEnum.Error, "Student Not Found");

                }

                var course = await context.Courses.FirstOrDefaultAsync(x => x.CourseId == studentsCoursesDto.CourseId);

                if (course == null)
                {
                    return new DataResult<string>(ResultStatusEnum.Error, "Course Not Found");

                }

                student.CourseId = course.Id;

                var studentUpdateResult = await context.UpdateAsyncExt(student);

                if (studentUpdateResult == null)
                {
                    return new DataResult<string>(ResultStatusEnum.Error, "Data Update Failed");
                }

                transactionWrapper.Commit();

                return new DataResult<string>(ResultStatusEnum.Success, "");

            }
            catch (Exception ex)
            {
                transactionWrapper.Rollback();

                return new DataResult<string>(ResultStatusEnum.Error, ex.Message);
            }
            finally
            {
                transactionWrapper.DisposeTransaction();
            }

        }

        public async Task<DataResult<string>> DeleteStudent(int id)
        {
            using var context = _factory.CreateDbContext();

            var student = await context.Students.FirstOrDefaultAsync(x => x.Id == id);

            if (student == null)
            {
                return new DataResult<string>(ResultStatusEnum.Error, "Student Not Found");
            }

            var deleteStudentResult = await context.DeleteAsyncExt(student);

            if (!deleteStudentResult)
            {
                return new DataResult<string>(ResultStatusEnum.Error, "Data Delete Failed");
            }

            return new DataResult<string>(ResultStatusEnum.Success, "Successful");
        }





      

        private string CreatePasswordHash(User user, string password)
        {
            var passwordHasher = new PasswordHasher<User>();
            return passwordHasher.HashPassword(user, password);
        }
    }
}
