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

        [HttpGet("GetList")]
        public IActionResult GetList() //IActionResult - результат выполнения контроллера
        {
            var employees = _context.entities.ToList();

            // Путь к директории проекта
            var projectDirectory = Directory.GetCurrentDirectory();

            // Путь к файлу JSON в директории проекта
            var jsonFilePath = Path.Combine(projectDirectory, "employees.json");

            try
            {
                // Сериализация списка в формат JSON и запись в файл
                var json = JsonSerializer.Serialize(employees);
                System.IO.File.WriteAllText(jsonFilePath, json);

                // Возвращаем успешный результат
                return Ok(employees);
            }
            catch (Exception ex)
            {
                // Обработка возможных ошибок и возвращение ответа с ошибкой
                return StatusCode(500, $"Error: {ex.Message}");
            }

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
