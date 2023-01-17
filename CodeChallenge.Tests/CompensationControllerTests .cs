
using System;
using System.Net;
using System.Net.Http;
using System.Text;

using CodeChallenge.Models;

using CodeCodeChallenge.Tests.Integration.Extensions;
using CodeCodeChallenge.Tests.Integration.Helpers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeCodeChallenge.Tests.Integration
{
  [TestClass]
  public class CompensationControllerTests
  {
    private static HttpClient _httpClient;
    private static TestServer _testServer;

    [ClassInitialize]
    // Attribute ClassInitialize requires this signature
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
    public static void InitializeClass(TestContext context)
    {
      _testServer = new TestServer();
      _httpClient = _testServer.NewClient();
    }

    [ClassCleanup]
    public static void CleanUpTest()
    {
      _httpClient.Dispose();
      _testServer.Dispose();
    }

    [TestMethod]
    public void CreateCompensation_Returns_Created()
    {
      // Arrange
      var employee = new Employee()
      {
        EmployeeId = "03aa1462-ffa9-4978-901b-7c001562cf6f",
        Department = "Engineering",
        FirstName = "Ringo",
        LastName = "Starr",
        Position = "Developer V",
      };
      var compensation = new Compensation()
      {
        CompensationId = "03aa1462-ffa9-4978-901b-7c001562cf6f",
        Employee = employee,
        Salary = 42000.50M,
        EffectiveDate = new DateTime(2022, 02, 14)
      };

      var requestContent = new JsonSerialization().ToJson(compensation);

      // Execute
      var putRequestTask = _httpClient.PostAsync($"api/compensation",
         new StringContent(requestContent, Encoding.UTF8, "application/json"));
      var putResponse = putRequestTask.Result;

      // Assert
      Assert.AreEqual(HttpStatusCode.Created, putResponse.StatusCode);
      var newCompensation = putResponse.DeserializeContent<Compensation>();
      Assert.IsNotNull(newCompensation);
      Assert.IsNotNull(newCompensation.Employee);
      Assert.AreEqual(newCompensation.Employee.EmployeeId, compensation.Employee.EmployeeId);
      Assert.AreEqual(newCompensation.Salary, compensation.Salary);
      Assert.IsNotNull(newCompensation.EffectiveDate);
    }

    [TestMethod]
    public void GetCompensation_Returns_Ok()
    {
      // Arrange
      var expectedCompensation = new Compensation()
      {
        Employee = new Employee()
        {
          EmployeeId = "03aa1462-ffa9-4978-901b-7c001562cf6f",
          Department = "Engineering",
          FirstName = "Ringo",
          LastName = "Starr",
          Position = "Developer V"
        },
        Salary = 42000.50M
      };

      // Execute
      var postRequestTask = _httpClient.GetAsync($"api/compensation/{expectedCompensation.Employee.EmployeeId}");
      var response = postRequestTask.Result;

      // Assert
      Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
      var compensation = response.DeserializeContent<Compensation>();
      Assert.IsNotNull(compensation);
      Assert.IsNotNull(compensation.Employee);
      Assert.AreEqual(compensation.Salary, Convert.ToDecimal(expectedCompensation.Salary));
    }

    [DataTestMethod]
    [DataRow("16a596ae-edd3-4847-99fe-c4518e82c86f")]
    [TestMethod]
    public void GetCompensation_Returns_NotFound(string employeeId)
    {
      // Execute
      var postRequestTask = _httpClient.GetAsync($"api/compensation/{employeeId}");
      var response = postRequestTask.Result;

      // Assert
      Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }
  }
}
