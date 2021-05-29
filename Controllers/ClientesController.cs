using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CadastroDeClientes.Data;
using CadastroDeClientes.Models;
using CadastroDeClientes.ViewModel;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using Newtonsoft.Json;

namespace CadastroDeClientes.Controllers
{
    public class ClientesController : Controller
    {
        private readonly Context _context;

        public ClientesController(Context context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("IdCliente"))) return RedirectToAction("Login", "Home");
            return View(await _context.Clientes.ToListAsync());
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Id_Cliente == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("IdCliente"))) return RedirectToAction("Login", "Home");

            if (HttpContext.Session.GetString("Nivel_De_Acesso") != "1") return RedirectToAction("Index", "Clientes");

            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cadastro cadastroModel)
        {
            var cliente = new Cliente();
            Cryptography cryptography = new Cryptography(MD5.Create());

            // Verifica se email existe
            if (EmailUsuarioExiste(cadastroModel.Email)) ModelState.AddModelError("Email", "O e-mail inserido já cadastrado!");

            // Verifica se o telefone existe
            if (TelefoneUsuarioExiste(cadastroModel.Telefone)) ModelState.AddModelError("Telefone", "O telefone inserido já cadastrado!");

            // Verifica se o CNPJ existe
            if (CNPJUsuarioExiste(cadastroModel.CNPJ)) ModelState.AddModelError("CNPJ", "O CNPJ inserido já cadastrado!");

            // Verifica se a senha a confirmação de senha são iguais
            if (!cryptography.HashVerify(cadastroModel.ConfirmarSenha, cadastroModel.Senha))
            {
                ModelState.AddModelError("Senha", "As senhas não correspondem.");
            }
            // Verifica força da senha
            else if (cliente.VerifyPasswordStrong(cadastroModel.Senha) < 3)
            {
                ModelState.AddModelError("Senha", "A segurança da senha é baixa, tente outra");
            }

            if (ModelState.IsValid)
            {
                cliente.Nivel_De_Acesso = cadastroModel.Nivel_De_Acesso;
                cliente.Razao_Social = cadastroModel.Razao_Social;
                cliente.Nome_Fantasia = cadastroModel.Nome_Fantasia;
                cliente.Telefone = cadastroModel.Telefone;
                cliente.CNPJ = cadastroModel.CNPJ;
                cliente.Email = cadastroModel.Email;
                cliente.Senha = cryptography.HashGenerate(cadastroModel.Senha);
                cliente.DataCreate = DateTime.Now;
                _context.Add(cliente);
                await _context.SaveChangesAsync();

                if(cadastroModel.Nivel_De_Acesso == 1)return RedirectToAction("PerfilAdmin" ,"Home");

                return RedirectToAction("Perfil", "Home");
            }


            return View();
        }

        //GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if(HttpContext.Session.GetString("Nivel_De_Acesso") != "1")
            {
                return RedirectToAction("Perfil", "Home");
            }
            if (id == null)
            {
                return NotFound();
            }

            
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Cliente,Nivel_De_Acesso,CNPJ,Razao_Social,Nome_Fantasia,Email,Telefone,DataUpdate,Senha")] Cliente cliente)
        {
            if (id != cliente.Id_Cliente)
            {
                return NotFound();
            }
            
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(cliente);
                        cliente.DataUpdate = DateTime.Now;
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ClienteExists(cliente.Id_Cliente))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Id_Cliente == id);
            
            if(cliente.Id_Cliente != 1)
            {
                return RedirectToAction("Perfil", "Home");
            }

            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Verifia se o Cliente Existe
        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.Id_Cliente == id);
        }

        // Verifica se o CNPJ já esta cadastrado
        private bool CNPJUsuarioExiste(string cnpj)
        {
            if (String.IsNullOrEmpty(cnpj)) return false;

            Cliente procurarCnpj = _context.Clientes.Where(m => m.CNPJ.Equals(cnpj)).FirstOrDefault();
            if (procurarCnpj != null) return true;
            return false;
        }

        // Verifica se um email já esta cadastrado
        private bool EmailUsuarioExiste(string email)
        {
            if (String.IsNullOrEmpty(email)) return false;

            Cliente procurarEmail = _context.Clientes.Where(m => m.Email.Equals(email)).FirstOrDefault();
            if (procurarEmail != null) return true;
            return false;
        }


        // Verifica se o telefone já está cadastrado
        private bool TelefoneUsuarioExiste(string telefone)
        {
            if (String.IsNullOrEmpty(telefone)) return false;

            Cliente procurarTelefone = _context.Clientes.Where(m => m.Telefone.Equals(telefone)).FirstOrDefault();
            if (procurarTelefone != null) return true;
            return false;
        }
    }
}
