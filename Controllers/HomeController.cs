using CadastroDeClientes.Data;
using CadastroDeClientes.Models;
using CadastroDeClientes.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace CadastroDeClientes.Controllers
{
    public class HomeController : Controller
    {
        private readonly Context _context;


        public HomeController(Context context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            //Verifica a session 
            //<return>se true vai para o perfilAdmin ou Perfil, dependendo do nivel de acesso. se nao vai para a tela de login
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("IdCliente")))
            {
                if(HttpContext.Session.GetString("Nivel_De_Acesso") == "1")
                {
                    RedirectToAction("PerfilAdmin", "Home");
                }
                else
                {
                    RedirectToAction("Perfil", "Home");
                }
            }
                return View();
        }

        [HttpGet]
        public IActionResult Perfil()
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("IdCliente")))return RedirectToAction(nameof(Login));
                
            int id = int.Parse(HttpContext.Session.GetString("IdCliente"));
            int Nivel = int.Parse(HttpContext.Session.GetString("Nivel_De_Acesso"));
       
            Cliente cliente = _context.Clientes.Find(id);
            if(Nivel == 1)
            {
                return RedirectToAction(nameof(PerfilAdmin));
            }

            
            return View(cliente);
        }
        public IActionResult PerfilAdmin()
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("IdCliente"))) 
                if (HttpContext.Session.GetString("Nivel_De_Acesso") != "1") return RedirectToAction(nameof(Perfil));

            int id = int.Parse(HttpContext.Session.GetString("IdCliente"));

            Cliente cliente = _context.Clientes.Find(id);
            return View(cliente);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        //Efetuar o login do usuário
        public IActionResult Login(string email, string senha)
        {
            Cryptography cryptography = new Cryptography(MD5.Create());
            string passwordCript = cryptography.HashGenerate(senha);

            Cliente cliente = _context.Clientes.Where(p => p.Email.Equals(email) && p.Senha.Equals(passwordCript)).FirstOrDefault();
            if (cliente == null)
            {
                ViewBag.Erro = "Não foi possível realizar login. Dados incorretos!";
                return View();
            }


            StartSessionLogin(cliente);
            return RedirectToAction(nameof(Perfil));
        }


        //Deslogando o usuário - Remove as sessions existentes
        public void Logout()
        {

            HttpContext.Session.Remove("Nivel_De_Acesso");
            HttpContext.Session.Remove("NomeFantasia");
            HttpContext.Session.Remove("EmailCliente");
            HttpContext.Session.Remove("IdCliente");
            HttpContext.Session.Remove("CNPJCliente");
            HttpContext.Session.Remove("TelefoneCliente");
            Response.Redirect("Index");
        }


        //Inicia a session
        private void StartSessionLogin(Cliente cliente)
        {
            HttpContext.Session.SetString("Nivel_De_Acesso", (cliente.Nivel_De_Acesso).ToString());
            HttpContext.Session.SetString("NomeFantasia", cliente.Nome_Fantasia);
            HttpContext.Session.SetString("EmailCliente", cliente.Email);
            HttpContext.Session.SetString("CNPJCliente", cliente.CNPJ);
            HttpContext.Session.SetString("TelefoneCliente", cliente.Telefone);
            HttpContext.Session.SetString("IdCliente", cliente.Id_Cliente.ToString());

            HttpContext.Session.SetString("Adicionar", cliente.Adicionar.ToString());
            HttpContext.Session.SetString("Editar", cliente.Editar.ToString());
            HttpContext.Session.SetString("Excluir", cliente.Deletar.ToString());
        }
    }
}
