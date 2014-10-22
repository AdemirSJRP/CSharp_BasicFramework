/* --------------------------------------------------------------------------------------------------------------------
Fernando Passaia - https://www.linkedin.com/pub/fernando-passaia/24/622/285 - https://www.facebook.com/fernando.passaia
Blog: fernandopassaia.wordpress.com - Email/Skype: fernandopassaia@futuradata.com.br - Cel/Whatsapp: (11)98104-9080
Para feedbacks - favor utilizar o GitHub - ou enviar através dos contatos acima.

Classe para validação de CPF e CNPJ.
 * Nota: Todos podem colaborar subindo suas melhorias, novos métodos e correções para esse projeto totalmente Opensource
 * e livre para uso de quem quiser em qualquer tipo de aplicação. Nota2: Por padrão, compila em C:\CSharp_BasicFramework
 * Caso o diretório não exista - efetue sua criação antes de abrir esse projeto e efetuar o Build.
--------------------------------------------------------------------------------------------------------------------- */

using System;
using System.Collections.Generic;

using System.Text;
using DllCrypt;

namespace DllValidacoes
{
    public class clsCpfCnpjValidacao
    {        
        #region Arruma Pontuação de CPF
        /// <summary>
        /// Arruma a Pontuação do CPF ou CNPJ (se vier virgula, ponto, etc, ele limpa, verifica se é válido e etc)
        /// </summary>
        /// <param name="cpf_cnpj">Cpf/Cnpj para ser verificado</param>
        /// <returns>Retorna o CPF/CNPJ validado e limpo</returns>
        public string pontuaCpf_CNPJ(string cpf_cnpj)
        {

            string textoLimpo = cpf_cnpj;

            textoLimpo = textoLimpo.Replace(".", "");
            textoLimpo = textoLimpo.Replace("-", "");
            textoLimpo = textoLimpo.Replace("/", "");
            textoLimpo = textoLimpo.Replace(",", "");

            int tam = textoLimpo.Length;
            string textoRetorno = textoLimpo;
            if (new clsNewContasMatematicas().verificaSeEInteiro(textoLimpo) == false)
            {
                return textoRetorno = "ERRO";
            }





            #region CPF
            if (textoLimpo.Length <= 11)
            {
                if (ValidaCPF(textoLimpo) == false)
                {
                    return textoRetorno = "ERROCPF";
                }

                textoRetorno = "";
                for (int i = 0; i < tam; i++)
                {
                    // string aux = (textoLimpo.Substring(i , 1)).ToString();
                    if (i == 3)
                    {
                        if (textoLimpo.Substring(i, 1).ToString() != ".")
                        {
                            textoRetorno = textoRetorno + ".";
                        }
                    }

                    if (i == 6)
                    {
                        if (textoLimpo.Substring(i, 1).ToString() != ".")
                        {
                            textoRetorno = textoRetorno + ".";
                        }
                    }

                    if (i == 9)
                    {
                        if (textoLimpo.Substring(i, 1).ToString() != "-")
                        {
                            textoRetorno = textoRetorno + "-";
                        }
                    }

                    textoRetorno = textoRetorno + textoLimpo.Substring(i, 1);

                }
            }
            #endregion
            else
            #region CNPJ
            {
                if (textoLimpo.Length >= 14)
                {
                    if (ValidaCNPJ(textoLimpo) == false)
                    {
                        return textoRetorno = "ERROCNPJ";
                    }


                    textoRetorno = "";

                    for (int i = 0; i < tam; i++)
                    {
                        // string aux = (textoLimpo.Substring(i , 1)).ToString();
                        if (i == 2)
                        {
                            if (textoLimpo.Substring(i, 1).ToString() != ".")
                            {
                                textoRetorno = textoRetorno + ".";
                            }
                        }

                        if (i == 5)
                        {
                            if (textoLimpo.Substring(i, 1).ToString() != ".")
                            {
                                textoRetorno = textoRetorno + ".";
                            }
                        }

                        if (i == 8)
                        {
                            if (textoLimpo.Substring(i, 1).ToString() != "/")
                            {
                                textoRetorno = textoRetorno + "/";
                            }
                        }

                        if (i == 12)
                        {
                            if (textoLimpo.Substring(i, 1).ToString() != "-")
                            {
                                textoRetorno = textoRetorno + "-";
                            }
                        }

                        textoRetorno = textoRetorno + textoLimpo.Substring(i, 1);

                    }
                }
            }
            #endregion
            return textoRetorno;

        }//fim método
        #endregion

        #region Validação de CPF
        /// <summary>
        /// Este método Valida o CPF
        /// </summary>
        /// <param name="vrCPF">String com o número do cpf</param>
        /// <returns>True para CPF válido - False para CPF não válido</returns>
        public bool ValidaCPF(string vrCPF)
        {

            string valor = vrCPF.Replace(".", "");
            valor = valor.Replace("-", "");

            if (valor.Length != 11)

                return false;

            bool igual = true;
            for (int i = 1; i < 11 && igual; i++)
                if (valor[i] != valor[0])
                    igual = false;
            if (igual || valor == "12345678909")
                return false;
            int[] numeros = new int[11];
            for (int i = 0; i < 11; i++)

                numeros[i] = int.Parse(

                  valor[i].ToString());

            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];
            int resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {

                if (numeros[9] != 0)

                    return false;

            }

            else if (numeros[9] != 11 - resultado)

                return false;



            soma = 0;

            for (int i = 0; i < 10; i++)

                soma += (11 - i) * numeros[i];



            resultado = soma % 11;



            if (resultado == 1 || resultado == 0)
            {

                if (numeros[10] != 0)

                    return false;

            }

            else

                if (numeros[10] != 11 - resultado)

                    return false;



            return true;

        }
        #endregion

        #region Validação de CNPJ
        /// <summary>
        /// Este método Valida o CNPJ 
        /// </summary>
        /// <param name="vrCNPJ">String com o número do CNPJ</param>
        /// <returns>True para CNPJ válido - False para CNPJ não válido</returns>
        public bool ValidaCNPJ(string vrCNPJ)
        {

            string CNPJ = vrCNPJ.Replace(".", "");
            CNPJ = CNPJ.Replace("/", "");
            CNPJ = CNPJ.Replace("-", "");

            int[] digitos, soma, resultado;
            int nrDig;
            string ftmt;
            bool[] CNPJOk;

            ftmt = "6543298765432";
            digitos = new int[14];
            soma = new int[2];
            soma[0] = 0;
            soma[1] = 0;
            resultado = new int[2];
            resultado[0] = 0;
            resultado[1] = 0;
            CNPJOk = new bool[2];
            CNPJOk[0] = false;
            CNPJOk[1] = false;

            try
            {
                for (nrDig = 0; nrDig < 14; nrDig++)
                {
                    digitos[nrDig] = int.Parse(
                        CNPJ.Substring(nrDig, 1));
                    if (nrDig <= 11)
                        soma[0] += (digitos[nrDig] *
                          int.Parse(ftmt.Substring(
                          nrDig + 1, 1)));
                    if (nrDig <= 12)
                        soma[1] += (digitos[nrDig] *
                          int.Parse(ftmt.Substring(
                          nrDig, 1)));
                }

                for (nrDig = 0; nrDig < 2; nrDig++)
                {
                    resultado[nrDig] = (soma[nrDig] % 11);
                    if ((resultado[nrDig] == 0) || (
                         resultado[nrDig] == 1))
                        CNPJOk[nrDig] = (
                        digitos[12 + nrDig] == 0);
                    else
                        CNPJOk[nrDig] = (
                        digitos[12 + nrDig] == (
                        11 - resultado[nrDig]));
                }
                return (CNPJOk[0] && CNPJOk[1]);
            }

            catch
            {
                return false;
            }
        }
        #endregion
    }//fim classe
}//fim namespace
