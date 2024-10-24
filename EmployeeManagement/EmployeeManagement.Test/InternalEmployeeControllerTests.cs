using EmployeeManagement.Business;
using EmployeeManagement.Controllers;
using EmployeeManagement.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Test
{
    public class InternalEmployeeControllerTests
    {
        private readonly InternalEmployeesController _internalEmployeesController;

        public InternalEmployeeControllerTests()
        {
            var employeeServiceMock = new Mock<IEmployeeService>();
            employeeServiceMock
                .Setup(m => m.FetchInternalEmployeesAsync())
                .ReturnsAsync(new List<InternalEmployee>()
                {
                    new InternalEmployee("Megan", "Jones", 2, 3000, false, 2),
                    new InternalEmployee("Jaimy", "Johnson", 3, 3400, true, 1),
                    new InternalEmployee("Anne", "Adams", 3, 4000, false, 3)
                });

            _internalEmployeesController = new InternalEmployeesController(
                employeeServiceMock.Object, null);

        }
        [Fact]
        public async Task GetInternalEmployees_GetAction_MustReturnOkObjectResult()
        {
            // Arrange

            // Act
            var result = await _internalEmployeesController.GetInternalEmployees();

            // Assert
            var actionResult = Assert
             .IsType<ActionResult<IEnumerable<Models.InternalEmployeeDto>>>(result);
            Assert.IsType<OkObjectResult>(actionResult.Result);

        }

        [Fact]
        public async Task GetInternalEmployees_GetAction_MustReturnIEnumerableOfInternalEmployeeDtoAsModelType()
        {
            // Arrange

            // Act 
            var result = await _internalEmployeesController.GetInternalEmployees();

            // Assert
            var actionResult = Assert
                .IsType<ActionResult<IEnumerable<Models.InternalEmployeeDto>>>(result);

            Assert.IsAssignableFrom<IEnumerable<Models.InternalEmployeeDto>>(
                ((OkObjectResult)actionResult.Result).Value);
        }

    }
}
