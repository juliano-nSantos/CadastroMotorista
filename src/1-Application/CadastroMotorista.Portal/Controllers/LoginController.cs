using CadastroMotorista.Domain.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CadastroMotorista.Portal.Controllers
{
    public class LoginController : Controller
    {
        private readonly IServiceLogin _serviceLogin;

        public LoginController(IServiceLogin serviceLogin)
        {
            _serviceLogin = serviceLogin;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Motorista");

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerificaLogin(LoginDto login)
        {
            UsuarioDto user;

            try
            {
                user = await _serviceLogin.ValidarLogin(login);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }

        private void Logar(UsuarioDto user)
        {
            var claim = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Nome),
                new Claim("Documento", user.CPF),
                new Claim("IdUsuario", user.IdUsuario.ToString()),
                new Claim("Permissao", user.Permissao),
                new Claim(ClaimTypes.Role, user.Permissao)
            };

            var identidadeUser = new ClaimsIdentity(claim, "Login");

            ClaimsPrincipal claimPrincipal = new ClaimsPrincipal(identidadeUser);

            var propriedadesAuthentication = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTime.Now.ToLocalTime().AddHours(1),
                IsPersistent = true
            };

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal, propriedadesAuthentication);
        }
    }
}
