using KUSYS_Demo.Business;
using KUSYS_Demo.Business.DTOs;
using KUSYS_Demo.Business.Enums;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Xunit;

namespace KUSYS_Demo.Test
{
    public class StudentServiceTest
    {

        private readonly StudentService _studentService;

        public StudentServiceTest()
        {
            //var webApplicationFactory = new WebApplicationFactory<Program>();

            //using (var scope = webApplicationFactory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            //{
            //    _studentService = scope.ServiceProvider.GetRequiredService<StudentService>();
            //}

            var factory = new ApplicationFactory();

            using (var scope = factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                _studentService = scope.ServiceProvider.GetRequiredService<StudentService>();
            }
        }


        [Fact]
        public async void Test_GetAllCourses()
        {

            // Arrange

            var expectCoursesList = new List<CourseDto>()
            {
                 new CourseDto
                {
                    CourseId = "CSI101",
                    CourseName = "Introduction to Computer Science"
                },
                new CourseDto
                {
                    CourseId = "CSI102",
                    CourseName = "Algorithms"
                },
                new CourseDto
                {
                    CourseId = "MAT101",
                    CourseName = "Calculus"
                },
                new CourseDto
                {
                    CourseId = "PHY101",
                    CourseName = "Physics"
                }
            };



            // Act

            var result = await _studentService.GetAllCourses();


            // Assert

            Assert.NotNull(result.Data);

            Assert.Equal(4, result.Data.Count);

            Assert.Equal(ResultStatusEnum.Success, result.ResultStatus);

            Assert.Equal(expectCoursesList[0].CourseName, result.Data[0].CourseName);


        }


        [Fact]
        public async void Test_NewStudent()
        {
            // Arrange

            var expectStudentUpdateDto = new UpdateStudentDto
            {
                Id = 1,
                PhoneNumber = "7777777777",
                Adress = "Faroe Adalarý"

            };

            // Act

            var updateStudentResult = await _studentService.UpdateStudent(expectStudentUpdateDto);

            // Assert

            Assert.NotNull(updateStudentResult.Data);

            Assert.Equal(ResultStatusEnum.Success, updateStudentResult.ResultStatus);

            Assert.Equal(expectStudentUpdateDto.PhoneNumber, updateStudentResult.Data.PhoneNumber);

            Assert.Equal(expectStudentUpdateDto.Adress, updateStudentResult.Data.Adress);

        }

    }
}