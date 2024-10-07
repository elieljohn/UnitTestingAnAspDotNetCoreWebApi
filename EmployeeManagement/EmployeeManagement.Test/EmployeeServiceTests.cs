﻿using EmployeeManagement.Business;
using EmployeeManagement.Services.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EmployeeManagement.Test
{
    public class EmployeeServiceTests
    {
        [Fact]
        public void CreateInternalEmployee_InternalEmployeeCreated_MustHaveAttendedFirstObligatoryCourse_WithObject()
        {
            // Arrange
            var employeeManagementTestDataRepository =
              new EmployeeManagementTestDataRepository();
            var employeeService = new EmployeeService(
                employeeManagementTestDataRepository,
                new EmployeeFactory());
            var obligatoryCourse = employeeManagementTestDataRepository
                .GetCourse(Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"));

            // Act
            var internalEmployee = employeeService
                .CreateInternalEmployee("Brooklyn", "Cannon");

            // Assert
            Assert.Contains(obligatoryCourse, internalEmployee.AttendedCourses);
        }

        [Fact]
        public void CreateInternalEmployee_InternalEmployeeCreated_MustHaveAttendedFirstObligatoryCourse_WithPredicate()
        {
            // Arrange
            var employeeManagementTestDataRepository =
              new EmployeeManagementTestDataRepository();
            var employeeService = new EmployeeService(
                employeeManagementTestDataRepository,
                new EmployeeFactory());

            // Act
            var internalEmployee = employeeService
                .CreateInternalEmployee("Brooklyn", "Cannon");

            // Assert
            Assert.Contains(internalEmployee.AttendedCourses,
                course => course.Id == Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"));
        }

        [Fact]
        public void CreateInternalEmployee_InternalEmployeeCreated_MustHaveAttendedSecondObligatoryCourse_WithPredicate()
        {
            // Arrange
            var employeeManagementTestDataRepository =
                new EmployeeManagementTestDataRepository();
            var employeeService = new EmployeeService(
                employeeManagementTestDataRepository,
                new EmployeeFactory());

            // Act
            var internalEmployee = employeeService.CreateInternalEmployee("Brooklyn", "Cannon");

            // Assert
            Assert.Contains(internalEmployee.AttendedCourses,
                course => course.Id == Guid.Parse("1fd115cf-f44c-4982-86bc-a8fe2e4ff83e"));
        }

        [Fact]
        public void CreateInternalEmployee_InternalEmployeeCreated_AttendedCoursesMustMatchObligatoryCourses()
        {
            // Arrange
            var employeeManagementTestDataRepository =
                new EmployeeManagementTestDataRepository();
            var employeeService = new EmployeeService(
                employeeManagementTestDataRepository,
                new EmployeeFactory());
            var obligatoryCourses = employeeManagementTestDataRepository
                .GetCourses(
                    Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"),
                    Guid.Parse("1fd115cf-f44c-4982-86bc-a8fe2e4ff83e"));

            // Act
            var internalEmployee = employeeService.CreateInternalEmployee("Brooklyn", "Cannon");

            // Assert
            Assert.Equal(obligatoryCourses, internalEmployee.AttendedCourses);
        }

        [Fact]
        public void CreateInternalEmployee_InternalEmployeeCreated_AttendedCoursesMustNotBeNew()
        {
            // Arrange
            var employeeManagementTestDataRepository =
                new EmployeeManagementTestDataRepository();
            var employeeService = new EmployeeService(
                employeeManagementTestDataRepository,
                new EmployeeFactory());

            // Act
            var internalEmployee = employeeService.CreateInternalEmployee("Brooklyn", "Cannon");

            // Assert
            //foreach (var course in internalEmployee.AttendedCourses)
            //{
            //    Assert.False(course.IsNew);
            //}
            Assert.All(internalEmployee.AttendedCourses,
                course => Assert.False(course.IsNew));
        }
    }
}
