
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
  public class ReportingStructureControllerTests
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

    [DataTestMethod]
    [DataRow("16a596ae-edd3-4847-99fe-c4518e82c86f", 1)]
    [DataRow("03aa1462-ffa9-4978-901b-7c001562cf6f", 2)]
    [DataRow("c0c2293d-16bd-4603-8e08-638a9d18b22c", 0)]
    [DataRow("62c1084e-6e34-4630-93fd-9153afb65309", 0)]
    [DataRow("b7839309-3348-463b-a7e3-5de1c168beb3", 0)]
    public void GetReportingStructure_Returns_Correct_NumbeerofReports(string employeeId,  int expectedDirectReports)
    {
      // Execute
      var getRequestTask = _httpClient.GetAsync($"api/reporting/{employeeId}");
      var response = getRequestTask.Result;

      // Assert
      Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

      var reportingStructure = response.DeserializeContent<ReportingStructure>();
      Assert.IsNotNull(reportingStructure);
      Assert.IsNotNull(reportingStructure.Employee);
      Assert.AreEqual(reportingStructure.Employee.EmployeeId, employeeId);
      Assert.AreEqual(reportingStructure.NumberofReports, expectedDirectReports);
    }

  }
}
