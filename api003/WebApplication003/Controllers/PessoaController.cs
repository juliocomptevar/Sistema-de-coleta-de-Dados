using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApplication003.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PessoaController : ControllerBase
    {

        private readonly ILogger<PessoaController> _logger;

        public PessoaController(ILogger<PessoaController> logger)
        {

        }

        [HttpPost]
        public PessoaResponse ProcessarInformacoesPessoa([FromBody] PessoaRequest request)

        {

            var anoAtual = DateTime.Now.Year;
            var idade = anoAtual - request.Datanascimento.Year;

            var imc = Math.Round(request.Peso / (request.Altura * request.Altura), 2);
            var classificacao = "";


            if (imc < (decimal)18.5)
            {
                classificacao = "Você está abaixo do peso ideal";
            }
            else if (imc >= (decimal)18.5 && imc <= (decimal)24.99)
            {
                classificacao = "Peso normal";
            }

            else if (imc >= (decimal)25.00 && imc <= (decimal)29.99)
            {
                classificacao = "Pré-Obesidade";
            }
            else if (imc >= (decimal)30.00 && imc <= (decimal)34.99)
            {
                classificacao = "Obesidade Grau I";
            }
            else if (imc >= (decimal)35.00 && imc <= (decimal)39.99)
            {
                classificacao = "Obesidade Grau II";
            }
            else
            {
                classificacao = "Obesidade Grau III";
            }


            var aliquota = 7.5;
            if (request.Salario <= 1212)
            {
                aliquota = 7.5;
            }
            else if (request.Salario >= 1212.01 && request.Salario <= 2427.35)
            {
                aliquota = 9;
            }
            else if (request.Salario >= 2427 && request.Salario <= 3641.03)
            {
                aliquota = 12;
            }
            else
            {
                aliquota = 14;
            }

                var inss = (request.Salario * aliquota) / 100;
                var salarioliquido = request.Salario - inss;

                var dollar = (decimal)5.14;
                var saldoDollar = Math.Round(request.Saldo / dollar, 2);

                var resposta = new PessoaResponse();
                resposta.SaldoDollar = saldoDollar;
                resposta.Aliquota = aliquota;
                resposta.SalarioLiquido = salarioliquido;
                resposta.Classificacao = classificacao;
                resposta.Idade = idade;
                resposta.Imc = imc;
                resposta.Inss = inss;
                resposta.Nome = request.Nome;

                return resposta;

            }

        }

    }

    


   
