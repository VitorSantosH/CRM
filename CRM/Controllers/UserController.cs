using CRM.Entidades;
using CRM.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using static CRM.Models.Models;

namespace CRM.Controllers
{

    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("/fazer/login")]
        [IgnoreAntiforgeryToken] // Adicione esse atributo para ignorar antiforgery
        public async Task<IActionResult> Login([FromBody] Models.Models.LoginRequest LoginRequest)
        {
            Entidades.Usuario? User = await new MongoDBService().GetUser(LoginRequest.UserId);

            if (User != null && User.Password == LoginRequest.Password)
            {
                return Ok(User); // Retorna o usuário se os dados estão corretos
            }

            return Problem("Usuário não encontrado ou dados incorretos."); // Retorna um erro se não encontrou
        }


        [HttpPost]
        [Route("/create")]
        [IgnoreAntiforgeryToken] 
        public async Task<IActionResult> Create([FromForm] Usuario NewUser)
        {
            Entidades.Usuario? User = await new MongoDBService().InsertUser(NewUser);

            if (User != null)
            {
                return Ok(User); 
            }

            return Problem("Erro ao criar Usuário");
        }

        [HttpPost("update")]
        [IgnoreAntiforgeryToken] // Ignorar também para este endpoint
        public IActionResult UpdateUser(string userId, string nome, string telefone)
        {
            _userService.UpdateUserInfo(userId, nome, telefone);
            return Ok();
        }
    }
}
