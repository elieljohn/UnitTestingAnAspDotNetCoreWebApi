using EmployeeManagement.Business;
using EmployeeManagement.Services.Test;
using Moq;

namespace EmployeeManagement.Test
{
    public class MoqTests
    {
        [Fact]
        public void FetchInternalEmployee_EmployeeFetched_SuggestedBonusMustBeCalculated()
        {
            // Arrange
            var employeeManagementTestDataRepository =
              new EmployeeManagementTestDataRepository();
            //var employeeFactory = new EmployeeFactory();
            var employeeFactoryMock = new Mock<EmployeeFactory>();
            var employeeService = new EmployeeService(
                employeeManagementTestDataRepository,
                employeeFactoryMock.Object);

            // Act 
            var employee = employeeService.FetchInternalEmployee(
                Guid.Parse("72f2f5fe-e50c-4966-8420-d50258aefdcb"));

            // Assert  
            Assert.Equal(400, employee.SuggestedBonus);
        }
    }
}
