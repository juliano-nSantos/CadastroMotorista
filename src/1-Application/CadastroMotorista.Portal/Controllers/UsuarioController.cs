using CadastroMotorista.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroMotorista.Portal.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IServiceUsuario _serviceUsuario;

        public UsuarioController(IServiceUsuario serviceUsuario)
        {
            _serviceUsuario = serviceUsuario;
        }

        public async Task<IActionResult> Perfil()
        {
            
            var id = Convert.ToInt32(User.Claims.Where(c => c.Type.Equals("IdUsuario")).First().Value);

            var usuario = await _serviceUsuario.GetUserById(id);
            
            return View(usuario);
        }
    }
}
