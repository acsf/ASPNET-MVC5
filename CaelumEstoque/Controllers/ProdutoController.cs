using CaelumEstoque.DAO;
using CaelumEstoque.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaelumEstoque.Controllers
{
    public class ProdutoController : Controller
    {
        // GET: Produto
        public ActionResult Index()
        {
            ProdutosDAO dao = new ProdutosDAO();
            IList<Produto> produtos = dao.Lista();
            ViewBag.Produtos = produtos;
            return View();
        }

        public ActionResult Form()
        {
            CategoriasDAO dao = new CategoriasDAO();
            ViewBag.Produto = new Produto();
            ViewBag.Categorias = dao.Lista();            
            return View();
        }

        [HttpPost]
        public ActionResult Adiciona(Produto produto)
        {
            //Variável usada por addModelError
            //Serve para criar msg que não fazem parte do dicionário do modelState
            int idInfomatica = 1;

            if (produto.CategoriaId.Equals(idInfomatica) && produto.Preco < 20)
            {
                ModelState.AddModelError("produto.PrecoInvalido", "Produtos da Categoria Informática devem ser maior que 20 r$");
            }
            if (ModelState.IsValid)
            {
                ProdutosDAO dao = new ProdutosDAO();
                dao.Adiciona(produto);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Produto = produto;
                CategoriasDAO dao = new CategoriasDAO();
                ViewBag.Categorias = dao.Lista();
                return View("Form");
            }
        }

    }


}