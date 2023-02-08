using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AtosAula10.Models;

// acesso a API
using System.Net.Http;
using System.Net.Http.Headers;

// uso da API
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AtosAula10.Controllers
{
    public class AlunosController : Controller
    {
        // obj de acesso
        HttpClient client;

        public AlunosController()
        {
            if (client == null)
            {
                client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:44394/");
                client.DefaultRequestHeaders.Accept.Add(new   MediaTypeWithQualityHeaderValue("application/json"));
            }
        }

        public async Task<ActionResult> Listar()
        {
            try
            {
                // procurar o serviço
                var response = await client.GetAsync("api/alunos/ListarAlunos");

                // se respondeu
                if (response.IsSuccessStatusCode)
                {
                    var resultado = await 
                          response.Content.ReadAsStringAsync();

                    var lista = 
                        JsonConvert.DeserializeObject<Alunos[]>   (resultado).ToList();

                    return View(lista);
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }

            }
            catch (Exception erro)
            {
                return View("erro");
            }
        }





        // GET: Alunos
        public ActionResult Index()
        {
            return View();
        }
    }
}