using Eq2Grau.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Eq2Grau.Controllers{
    public class HomeController : Controller{

        /// <summary>
        /// calcula as raízes de uma eq. 2º Grau
        /// </summary>
        /// <param name="A">parâmetro do x^2</param>
        /// <param name="B">parâmetro do x</param>
        /// <param name="C">parametro independente</param>
        /// <returns>vista</returns>

        public IActionResult Index(string A,string B,string C){

            /*
             * Algortimo:
             * 1- ler os parêmetros A,B,C
             * 2- validar se os parâmetros são válidos
             *      2.1- A,B e C são números 
             *      2.2- A!= 0
             * 3- se tudo estiver bem, posso fazer o algoritmo
             *      3.1- calcular o DELTA
             *      3.2- Se DELTA>=0, raízes reais
             *          3.2.1- calcular raízes reais
             *      3.3- Se DELTA<0, raízes complexas conjugadas
             *          3.3.1- calcular raízes complexas
             *      3.4- informar o utilizador das raízes calculadas         
             *    senão, notificar o utlizador de como corrigir
             */

            //2.1 
            if (!float.TryParse(A, out float a)) {
                //A não é número
                ModelState.AddModelError("A", "O valor introduzido deve ser um número.");
                return View();
            }
            if(!float.TryParse(B, out float b)){
                //B não é número
                ModelState.AddModelError("B", "O valor introduzido deve ser um número.");
                return View();
            }
            if(!float.TryParse(C, out float c)){
                //C não é número
                ModelState.AddModelError("C", "O valor introduzido deve ser um número.");
                return View();
            }

            //2.2
            if(a == 0){
                ModelState.AddModelError("A", "O valor introduzido deve ser diferente de 0.");
                return View();
            }

            //3.1
            float delta = (b * b) - (4 * a * c);

            //3.2
            if (delta >= 0)
            {
                //3.2.1
                float x1 = (-b + MathF.Sqrt(delta)) / (2 * a);
                float x2 = (-b - MathF.Sqrt(delta)) / (2 * a);

                //3.4
                ViewBag.X1 = x1;
                ViewBag.X2 = x2;
            }else{
                //3.3
                //3.3.1

                float parteReal = -b / (2 * a);
                float parteImag = MathF.Sqrt(-delta)/(2*a);

                //3.4
                ViewBag.X1 = $"{parteReal} +{ parteImag}i";
                ViewBag.X2 = $"{parteReal} - {parteImag}i";

            }

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
    }
}
