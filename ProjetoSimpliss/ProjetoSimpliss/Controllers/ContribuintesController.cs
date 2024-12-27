using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoSimpliss.Data;
using ProjetoSimpliss.Models;
using ProjetoSimpliss.viewModels;
using ProjetoSimpliss.ViewModels;

namespace ProjetoSimpliss.Controllers
{
    public class ContribuintesController : Controller
    {
        private readonly ProjetoSimplissContext _context;

        public ContribuintesController(ProjetoSimplissContext context)
        {
            _context = context;
        }

        // GET: Contribuintes
        public IActionResult Index()
        {
            //var projetoSimplissContext = _context.Contribuintes;
            //return View(await projetoSimplissContext.ToListAsync());

            //var contribuintes = await _context.Contribuintes.ToListAsync();
            //return View(contribuintes);

            var resultado = _context.ContribuintesBeneficios
                .Join(
                    _context.Beneficios,
                    cb => cb.BeneficioId,
                    b => b.Id,
                    (cb, b) => new { cb, b }
                )
                .Join(
                    _context.Contribuintes,
                    cb_b => cb_b.cb.ContribuinteId,
                    c => c.Id,
                    (cb_b, c) => new viewModels.ContribuinteShowModel

                    {
                        Id = c.Id,
                        CNPJ = c.CNPJ,
                        RazaoSocial = c.RazaoSocial,
                        DataAbertura = c.DataAbertura,
                        RegimeTributacao = c.RegimeTributacao,
                        nomeBeneficio = cb_b.b.NomeBeneficio

                    }).ToList();

                    return View(resultado);

        }

        // GET: Contribuintes/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound(); 
            }

            var resultado = _context.ContribuintesBeneficios
                .Join(
                    _context.Beneficios,
                    cb => cb.BeneficioId,
                    b => b.Id,
                    (cb, b) => new { cb, b }
                )
                .Join(
                    _context.Contribuintes,
                    cb_b => cb_b.cb.ContribuinteId,
                    c => c.Id,
                    (cb_b, c) => new viewModels.ContribuinteShowModel
                    {
                        Id = c.Id,
                        CNPJ = c.CNPJ,
                        RazaoSocial = c.RazaoSocial,
                        DataAbertura = c.DataAbertura,
                        RegimeTributacao = c.RegimeTributacao,
                        nomeBeneficio = cb_b.b.NomeBeneficio
                    }
                )
                .Where(model => model.Id == id) 
                .FirstOrDefault(); 

            if (resultado == null)
            {
                return NotFound(); 
            }

            return View(resultado);
        }

        // GET: Contribuintes/Create
        public IActionResult CreateWithBeneficios()
        {
            var beneficios = _context.Beneficios
        .Select(b => new ViewModels.BeneficioCheckboxViewModel
        {
            Id = b.Id,
            NomeBeneficio = b.NomeBeneficio,
            IsSelected = false
        }).ToList();

            var viewModel = new ContribuinteCreateViewModel
            {
                Beneficios = beneficios
            };

            return View("CreateWithBeneficios", viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateWithBeneficios(ContribuinteCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Garantir que a data está no formato correto e com Kind UTC
                var dataAbertura = DateTime.SpecifyKind(viewModel.DataAbertura, DateTimeKind.Utc);

                // Criar o contribuinte
                var contribuinte = new Contribuintes
                {
                    CNPJ = viewModel.CNPJ,
                    RazaoSocial = viewModel.RazaoSocial,
                    DataAbertura = dataAbertura,
                    RegimeTributacao = viewModel.RegimeTributacao,
                };

                _context.Contribuintes.Add(contribuinte);
                await _context.SaveChangesAsync();

                foreach (int idBeneficio in viewModel.SelectedBeneficios)
                {
                    // Criar os vínculos com os benefícios
                    var beneficiosSelecionados = new ContribBeneficio
                    {
                        ContribuinteId = contribuinte.Id,
                        BeneficioId = idBeneficio,
                        DataVinculo = DateTime.UtcNow
                    };

                    _context.ContribuintesBeneficios.AddRange(beneficiosSelecionados);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            
            var beneficiosFromDb = _context.Beneficios
                .Select(b => new ViewModels.BeneficioCheckboxViewModel
                {
                    Id = b.Id,
                    NomeBeneficio = b.NomeBeneficio
                }).ToList();

            // marcar benefício como selecionado (IsSelected) no lado cliente
            foreach (var beneficio in beneficiosFromDb)
            {
                beneficio.IsSelected = viewModel.SelectedBeneficios.Contains(beneficio.Id);
            }

            // Atribui a lista de benefícios à ViewModel
            viewModel.Beneficios = beneficiosFromDb;

            return View("CreateWithBeneficios", viewModel);
        }


        // POST: Contribuintes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        // GET: Contribuintes/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            
                var item = _context.Contribuintes.Find(id);
                if (item == null)
                {
                    return NotFound();
                }
                return View(item);

        }

        // POST: Contribuintes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Contribuintes item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var itemNovo = _context.Contribuintes.Find(id);
                    if(itemNovo == null)
                    {
                        return HttpNotFound();
                    }

                    var dataAbertura = DateTime.SpecifyKind(item.DataAbertura, DateTimeKind.Utc);

                    itemNovo.CNPJ = item.CNPJ;
                    itemNovo.RazaoSocial = item.RazaoSocial;
                    itemNovo.DataAbertura = dataAbertura;
                    itemNovo.RegimeTributacao = item.RegimeTributacao;

                    _context.Update(itemNovo);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    
                    return View(item);
                }
            }
            return View(item);
        }

        private IActionResult HttpNotFound()
        {
            throw new NotImplementedException();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound(); 
            }

            // Encontrar o contribuinte na tabela `Contribuintes`
            var contribuinte = _context.Contribuintes.FirstOrDefault(c => c.Id == id);

            if (contribuinte == null)
            {
                return NotFound();
            }

            // Encontrar os registros na tabela `ContribuintesBeneficios` relacionados ao contribuinte
            var registrosBeneficios = _context.ContribuintesBeneficios
                .Where(cb => cb.ContribuinteId == id)
                .ToList();

            // Remove os registros da tabela `ContribuintesBeneficios`
            if (registrosBeneficios.Any())
            {
                _context.ContribuintesBeneficios.RemoveRange(registrosBeneficios);
            }

            // Remove o registro da tabela `Contribuintes`
            _context.Contribuintes.Remove(contribuinte);

            // Salva as alterações no banco
            _context.SaveChanges();

            return RedirectToAction("Index"); // Redireciona para a lista principal após deletar.
        }

        public IActionResult CalculoImposto()
        {
            var resultado = _context.ContribuintesBeneficios
            .Join(
                _context.Beneficios,
                cb => cb.BeneficioId,
                b => b.Id,
                (cb, b) => new { cb, b }
            )
            .Join(
                _context.Contribuintes,
                cb_b => cb_b.cb.ContribuinteId,
                c => c.Id,
                (cb_b, c) => new viewModels.ContribuinteShowModel

                {
                    Id = c.Id,
                    CNPJ = c.CNPJ,
                    RazaoSocial = c.RazaoSocial,
                    DataAbertura = c.DataAbertura,
                    RegimeTributacao = c.RegimeTributacao,
                    nomeBeneficio = cb_b.b.NomeBeneficio,
                    PorcentagemDesconto = cb_b.b.PorcentagemDesconto

                }).ToList();

            return View(resultado);
        }


        private bool ContribuintesExists(int id)
        {
            return _context.Contribuintes.Any(e => e.Id == id);
        }



    }
}
