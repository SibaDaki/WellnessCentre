using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SenwesAssignment_API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SenwesAssignment_API.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly LoadData _loadData;

        public EmployeeController(ILogger<EmployeeController> logger)
        {
            _logger = logger;
            _loadData = new LoadData();
        }

        /// <summary>
        /// Get all employees
        /// </summary>
        /// <returns>Returns a list of all employees</returns>
        [HttpGet]
        public IActionResult Get()
        {
            var employeeData = _loadData.LoadEmployeeData();
            return Ok(employeeData);
        }

        
        [Route("GetById/{empId}")]
        [HttpGet]
        public IActionResult GetById(int empId)
        {
            var employee = _loadData.LoadEmployeeData().Where(x => x.EmpID == empId).FirstOrDefault();
            return Ok(employee);
        }

       //[Route("Get/{Age}")]
        [HttpGet("GetByAge")]
        public IActionResult GetByAge()
        {
            //List<Employee> employees = new List<Employee>();
            var employee = _loadData.LoadEmployeeData().Where(x => x.Age > 30).ToList();
            return Ok(employee);
        }

        [HttpGet("GetHighestPaid")]
        public IActionResult GetHighestPaid()
        {
           
            var highestpaid = (from emp in _loadData.LoadEmployeeData()
                            orderby emp.Salary ascending
                            select emp).Take(10);
            return Ok(highestpaid);
        }
        [HttpGet("SearchByName/{name}")]
        public IActionResult GetByName(string name)
        {

            var nameList = _loadData.LoadEmployeeData();
            if (!string.IsNullOrEmpty(name))
                nameList = nameList.Where(emp => emp.FirstName.Contains(name));
           
            else if (!string.IsNullOrEmpty(name))
                    nameList = nameList.Where(emp => emp.LastName.Contains(name));
            else nameList = nameList.Where(emp => emp.City.Contains(name));
            return Ok(nameList);
        }
        [HttpGet("GetByFirstName")]
        public IActionResult GetFirstName()
        {

            var name= (from emp in _loadData.LoadEmployeeData()
                               where emp.FirstName == "Treasure"
                               select emp.Salary);
            return Ok(name);
        }

        [HttpGet("GetCity")]
        public IActionResult GetCity()
        {
                var city = (from emp in _loadData.LoadEmployeeData()
                            select emp.City);
                return Ok(city);
         }
    }
}
