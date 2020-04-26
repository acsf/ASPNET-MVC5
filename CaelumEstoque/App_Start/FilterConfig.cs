using System.Web.Mvc;

namespace CaelumEstoque.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //Verifca se o usuário está logado para toda a aplicação
            //Comentado para que não seja requisitado em toda aplicação
            //filters.Add(new AutorizacaoFilterAttribute());
        }
    }
}