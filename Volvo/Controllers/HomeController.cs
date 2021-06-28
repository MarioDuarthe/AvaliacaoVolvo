using Entidades.EF;
using Entidades.Models.Cadastros;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Volvo.Models;

namespace Volvo.Controllers
{
    public class HomeController : Controller
    {
        Contexto db = new Contexto();

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
 

            return View();


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

        public string BuscaCaminhao()
        {

            using (Contexto db = new Contexto())
            {
       
                var caminhao = db.Caminhoes
                .Select(c => new { c.Id, c.Modelo, c.Ano_Fabricacao, c.Ano_Modelo, c.Qtde_Estoque }).ToList();
                return Newtonsoft.Json.JsonConvert.SerializeObject(caminhao);

            }
        }
        public string SomarCaminhao(int idModelo)
        {
            using (Contexto db = new Contexto())
            {
                var caminhao = db.Caminhoes.Find(idModelo);
                caminhao.Qtde_Estoque ++;
                db.Entry(caminhao);
                db.SaveChanges();
            }
            var ret = new
            {
                Status = 0,
                Mensagem = "Inclusão Com Sucesso!"
            };

            return Newtonsoft.Json.JsonConvert.SerializeObject(ret);

        }
        public string SomarEditCaminhao(int idModelo, int qtde)
        {
            using (Contexto db = new Contexto())
            {
                var caminhao = db.Caminhoes.Find(idModelo);
                caminhao.Qtde_Estoque = caminhao.Qtde_Estoque + qtde;
                db.Entry(caminhao);
                db.SaveChanges();
            }
            var ret = new
            {
                Status = 0,
                Mensagem = "Inclusão Com Sucesso!"
            };

            return Newtonsoft.Json.JsonConvert.SerializeObject(ret);

        }
        public string SubtrairCaminhao(int idModelo)
        {
            using (Contexto db = new Contexto())
            {
                var caminhao = db.Caminhoes.Find(idModelo);
                caminhao.Qtde_Estoque--;
                db.Entry(caminhao);
                db.SaveChanges();
            }
            var ret = new
            {
                Status = 0,
                Mensagem = "Retirada Com Sucesso!"
            };

            return Newtonsoft.Json.JsonConvert.SerializeObject(ret);

        }
        public string ExcluirCaminhao(int idModelo)
        {
            using (Contexto db = new Contexto())
            {
                var caminhao = db.Caminhoes.Find(idModelo);
                db.Remove(caminhao);
                db.SaveChanges();
            }
            var ret = new
            {
                Status = 0,
                Mensagem = "Exclusão Com Sucesso!"
            };

            return Newtonsoft.Json.JsonConvert.SerializeObject(ret);

        }
        public string GravarCaminhao(string modelo, string anoFabricacao, string anoModelo)
        {
            var obj = new Caminhao();
            obj.Modelo = modelo;
            obj.Ano_Fabricacao = anoFabricacao;
            obj.Ano_Modelo = anoModelo;
            obj.Qtde_Estoque = 1;


            using (Contexto db = new Contexto())
            {
                var caminhao = db.Caminhoes
               .Where(c => c.Modelo == modelo && c.Ano_Fabricacao == anoFabricacao && c.Ano_Modelo == anoModelo)
               .Select(c => new { c.Id, c.Modelo, c.Ano_Fabricacao, c.Ano_Modelo, c.Qtde_Estoque }).ToList();

                if(caminhao.Count != 0)
                {
                    SomarCaminhao(caminhao[0].Id);
                }
                else
                {
                    db.Caminhoes.Add(obj);
                    db.SaveChanges();
                }

            }
            var ret = new
            {
                Status = 0,
                Mensagem = "Cadatrado Com Sucesso!"
            };

            return Newtonsoft.Json.JsonConvert.SerializeObject(ret);

        }
        public string BuscaCaminhaoId(int idModelo)
        {
            using (Contexto db = new Contexto())
            {

                var caminhao = db.Caminhoes
                .Where(c => c.Id == idModelo)
                .Select(c => new { c.Id, c.Modelo, c.Ano_Fabricacao, c.Ano_Modelo, c.Qtde_Estoque }).ToList();
                return Newtonsoft.Json.JsonConvert.SerializeObject(caminhao);

            }
        }
        public string EditarCaminhao(int idModelo, string modelo, string anoFabricacao, string anoModelo)
        {
            using (Contexto db = new Contexto())
            {
                var existeCaminhao = db.Caminhoes
                .Where(c => c.Modelo == modelo && c.Ano_Fabricacao == anoFabricacao && c.Ano_Modelo == anoModelo)
                .Select(c => new { c.Id, c.Modelo, c.Ano_Fabricacao, c.Ano_Modelo, c.Qtde_Estoque }).ToList();

                if (existeCaminhao.Count != 0)
                {
                    var json = BuscaCaminhaoId(idModelo);
                    var caminhaoAlterar = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Caminhao>>(json);
                    SomarEditCaminhao(existeCaminhao[0].Id, caminhaoAlterar[0].Qtde_Estoque);
                    ExcluirCaminhao(idModelo);
                }
                else
                {
                    var caminhaoEdit = db.Caminhoes.Find(idModelo);
                    caminhaoEdit.Modelo = modelo;
                    caminhaoEdit.Ano_Fabricacao = anoFabricacao;
                    caminhaoEdit.Ano_Modelo = anoModelo;
                    db.Entry(caminhaoEdit);
                    db.SaveChanges();
                }
            }
            var ret = new
            {
                Status = 0,
                Mensagem = "Alteração Com Sucesso!"
            };

            return Newtonsoft.Json.JsonConvert.SerializeObject(ret);

        }
    }

}
