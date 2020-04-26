using CaelumEstoque.DAO;
using CaelumEstoque.Filtros;
using CaelumEstoque.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CaelumEstoque.Controllers
{
    [AutorizacaoFilter]
    public class ProdutoController : Controller
    {
        // GET: Produto
        [Route("produtos", Name = "ListaProdutos")]
        public ActionResult Index()
        {
            ProdutosDAO dao = new ProdutosDAO();
            var produtos = dao.Lista();
            return View(produtos);
        }

        public ActionResult Form()
        {
            ViewBag.Produto = new Produto();

            CategoriasDAO dao = new CategoriasDAO();
            IList<CategoriaDoProduto> categorias = dao.Lista();
            ViewBag.Categorias = categorias;
            return View(categorias);
        }


        [ValidateAntiForgeryToken, HttpPost]
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
        [Route("produtos/{id}", Name = "DetalhesProduto")]
        public ActionResult Detalhes(int id)
        {
            ProdutosDAO dao = new ProdutosDAO();
            Produto produto = dao.BuscaPorId(id);
            ViewBag.Produto = produto;
            return View();
        }

        public ActionResult DecrementaQtd(int id)
        {
            ProdutosDAO dao = new ProdutosDAO();
            Produto produto = dao.BuscaPorId(id);
            produto.Quantidade--;
            dao.Atualiza(produto);
            //return RedirectToAction("Index");
            return Json(produto);
        }

    }


}