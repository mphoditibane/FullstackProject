using FullStack.Api.Data;
using FullStack.Api.Modals;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FullStack.Api.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeesController : Controller
    {
        private readonly FullStackDBContext _fullStackDBContext;

        public EmployeesController(FullStackDBContext fullStackDBContext) {

            _fullStackDBContext = fullStackDBContext;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
          var employees =  await _fullStackDBContext.EmployeeTable.ToListAsync();

            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employees employeeRequest) 
        {
            employeeRequest.Id = Guid.NewGuid();

            await _fullStackDBContext.EmployeeTable.AddAsync(employeeRequest);
            await _fullStackDBContext.SaveChangesAsync();
            return Ok(employeeRequest);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id) 
        {
            var employee = await _fullStackDBContext.EmployeeTable.FirstOrDefaultAsync(x => x.Id == id);

            if(employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPut]
        [Route("{id:Guid}")]

        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, Employees updateEmployeeRequest)
        {
           var employee = await _fullStackDBContext.EmployeeTable.FindAsync(id);

           if(employee == null) 
           {
                return NotFound();
           }

           employee.Name = updateEmployeeRequest.Name;
           employee.Email = updateEmployeeRequest.Email;
           employee.Phone = updateEmployeeRequest.Phone;
           employee.Salary = updateEmployeeRequest.Salary;
           employee.Department = updateEmployeeRequest.Department;

           await _fullStackDBContext.SaveChangesAsync();

            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id) 
        {
            var employee = await _fullStackDBContext.EmployeeTable.FindAsync(id);

            if (employee == null) {
                return NotFound();
            }

             _fullStackDBContext.EmployeeTable.Remove(employee);
            await _fullStackDBContext.SaveChangesAsync();

            return Ok(employee);
        }
    }
}
