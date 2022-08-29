using CadastroMotorista.Domain.Dtos;
using CadastroMotorista.Domain.Interfaces.Services;
using CadastroMotorista.Domain.Models;
using CadastroMotorista.Portal.Models;
using Jarvis.Utils.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CadastroMotorista.Portal.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class MotoristaController : Controller
    {
        private readonly IService<MotoristaDto> _service;

        public MotoristaController(IService<MotoristaDto> service)
        {
            _service = service;
        }

        public IActionResult Index()
        {            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(FiltroBusca filtro)
        {
            var retornoBusca = new RetornoBuscaViewModel();

            retornoBusca.ListMotoristas = await _service.BuscarPorFiltroAsync(filtro);

            if (string.IsNullOrEmpty(_service.MensagemErro))
                return PartialView("_Resultado", retornoBusca);
            else
            {
                TempData["error"] = _service.MensagemErro;
                return RedirectToAction("Index");
            }                
        }

        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Adiciona(MotoristaDto motorista)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Dados incorretos!";                
                return RedirectToAction ("Index");
            }

            try
            {
                if (await _service.AdicionarAsync(motorista))
                {
                    TempData["success"] = "Motorista cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["error"] = "Erro ao cadastrar o motorista. Favor tentar novamente!";
                    return RedirectToAction("Adicionar");
                }                   
            }
            catch(Exception ex)
            {
                TempData["error"] = ex.Message.Split("!")[0];
                return RedirectToAction ("Adicionar");
            }          
        }

        public async Task<IActionResult> Visualizar()
        {
            string[] uri = HttpContext.Request.Path.ToUriComponent().Split("/");
            int Id = JarvisEncrypt.DecryptId(uri[3]);

            var motorista = await _service.BuscarPorCodigoAsync(Id);

            return View(motorista);
        }

        public async Task<IActionResult> Editar()
        {
            string[] uri = HttpContext.Request.Path.ToUriComponent().Split("/");
            int Id = JarvisEncrypt.DecryptId(uri[3]);

            var motorista = await _service.BuscarPorCodigoAsync(Id);

            return View(motorista);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(MotoristaDto dto)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Dados incorretos!";
                return RedirectToAction("Editar");
            }               

            try
            {
                if (await _service.AtualizarAsync(dto))
                {
                    TempData["success"] = "Motorista editado com sucesso!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["error"] = "Erro ao editar o motorista. Favor tentar novamente!";
                    return RedirectToAction("Editar");
                }                    
            }
            catch(Exception ex)
            {
                TempData["error"] = ex.Message.Split("!")[0];
                return RedirectToAction("Editar");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Deletar(int Id)
        {
            if (Id <= 0)
            {
                TempData["error"] = "Erro ao localizar o motorista. Favor tentar novamente!";
                return RedirectToAction("Index");
            }                

            try
            {
                if (await _service.DeletarAsync(Id))
                {
                    TempData["success"] = "Motorista excluido com sucesso!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["error"] = "Erro ao excluir o motorista. Favor tentar novamente!";
                    return RedirectToAction("Index");
                }
                    
            }
            catch(Exception ex)
            {
                TempData["error"] = ex.Message.Split("!")[0];
                return RedirectToAction("Index");
            }           
        }
    }
}
