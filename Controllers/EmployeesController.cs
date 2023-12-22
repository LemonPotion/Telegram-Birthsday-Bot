using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebApplication3.DataBase;
using System.IO;
using System.Text.Json;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmployeesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("id")]//GET id
        public IActionResult GetEmployeeById(int id) //IActionResult - результат выполнения контроллера
        {

            var employee = _context.entities.Find(id);//ищем по id

            if (employee == null)//если нулл то ошибка 404
            {
                return NotFound();//Код 404
            }

            return Ok(employee);//если найден то возвращаем Ok
        }

        [HttpPost]// POST добавить в бд
        public IActionResult CreateEmployee([FromBody] Employees employee)

        {
            try
            {
                if (employee == null)
                {
                    return BadRequest();
                }

                _context.entities.Add(employee);
                _context.SaveChanges();

                return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.id }, employee);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("id")] //PUT изменить в бд
        public IActionResult UpdateEmployee(int id, [FromBody] Employees updatedEmployee)
        {
            var employee = _context.entities.Find(id);

            if (employee == null)
            {
                return NotFound();
            }

            
            employee.name = updatedEmployee.name;
            employee.birth = updatedEmployee.birth;

            _context.SaveChanges();

            return Ok(employee);
        }

        [HttpDelete("id")]// Delete Удалить из бд
        public IActionResult DeleteEmployee(int id)
        {
            var employee = _context.entities.Find(id);

            if (employee == null)
            {
                return NotFound();
            }

            _context.entities.Remove(employee);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
