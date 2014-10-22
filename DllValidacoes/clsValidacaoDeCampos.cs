/* --------------------------------------------------------------------------------------------------------------------
Fernando Passaia - https://www.linkedin.com/pub/fernando-passaia/24/622/285 - https://www.facebook.com/fernando.passaia
Blog: fernandopassaia.wordpress.com - Email/Skype: fernandopassaia@futuradata.com.br - Cel/Whatsapp: (11)98104-9080
Para feedbacks - favor utilizar o GitHub - ou enviar através dos contatos acima.

Classe que valida diversos tipos de informações email, telefone, cep, retira assentos entre outros.
 * Nota: Todos podem colaborar subindo suas melhorias, novos métodos e correções para esse projeto totalmente Opensource
 * e livre para uso de quem quiser em qualquer tipo de aplicação. Nota2: Por padrão, compila em C:\CSharp_BasicFramework
--------------------------------------------------------------------------------------------------------------------- */

using System;
using System.Collections.Generic;

using System.Text;
using DllCrypt;

namespace DllValidacoes
{
    public class clsValidacaoDeCampos
    {
        #region Validação de E-mail
        /// <summary>
        /// Esse método valida E-mail (verifica a presença de @ na string)
        /// </summary>
        /// <param name="email">E-mail para ser Validado</param>
        /// <returns>True para válido - False para não válido</returns>
        public bool ValidaEmail(string email)
        {
            int tam = email.Length;
            bool teste = false;

            for (int i = 0; i < tam; i++)
            {
                if (email.Substring(i, 1) == "@")
                {
                    teste = true;
                }
            }

            return teste;

        }
        #endregion

        #region Validação de Telefone
        /// <summary>
        /// Esse método valida telefones (verifica se está no formato (11)4640-2833 
        /// </summary>
        /// <param name="telefone">Telefone para ser Validado</param>
        /// <returns>True para válido - False para não válido</returns>
        public bool ValidaTelefone(string telefone)
        {
            string textoLimpo = telefone;

            //textoLimpo = textoLimpo.Replace("(", "");
            //textoLimpo = textoLimpo.Replace(")", "");
            //textoLimpo = textoLimpo.Replace("-", "");

            int contador = textoLimpo.Length;

            if (contador == 13) //se length for 13, esta no formato (11)4640-2481
            {
                return true;
            }
            else
            {
                return false;
            }

        }//fim método que valida telefone

        #endregion

        #region Validação de CEP
        /// <summary>
        /// Esse método valida CEP (verifica se está no formato 08573-180)
        /// </summary>
        /// <param name="cep">Cep para ser Validado</param>
        /// <returns>True para válido - False para não válido</returns>
        public bool ValidaCep(string cep)
        {
            string textoLimpo = cep;

            textoLimpo = textoLimpo.Replace("(", "");
            textoLimpo = textoLimpo.Replace(")", "");
            textoLimpo = textoLimpo.Replace("-", "");

            int contador = textoLimpo.Length;

            if (contador == 8) //se length for 7, está no formado 08573180 
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        #endregion

        #region Retira Assentos e Caraters
        public string retiraPontuacao(string variavel)
        {
            string variavel2 = variavel;
            variavel2 = variavel2.Replace("ç", "c");
            variavel2 = variavel2.Replace("Ç", "C");
            variavel2 = variavel2.Replace("Ã", "A");
            variavel2 = variavel2.Replace("ã", "a");
            variavel2 = variavel2.Replace("õ", "o");
            variavel2 = variavel2.Replace("Õ", "O");
            variavel2 = variavel2.Replace("Í", "I");
            variavel2 = variavel2.Replace("í", "I");
            variavel2 = variavel2.Replace("ü", "u");
            variavel2 = variavel2.Replace("Ü", "U");
            variavel2 = variavel2.Replace("Á", "A");
            variavel2 = variavel2.Replace("á", "a");
            variavel2 = variavel2.Replace("à", "a");
            variavel2 = variavel2.Replace("ú", "u");
            variavel2 = variavel2.Replace("Ú", "U");
            variavel2 = variavel2.Replace("À", "A");
            variavel2 = variavel2.Replace(",", "");
            variavel2 = variavel2.Replace(".", "");
            variavel2 = variavel2.Replace("/", "");
            variavel2 = variavel2.Replace("|", "");
            variavel2 = variavel2.Replace(@"\", "");
            variavel2 = variavel2.Replace("[", "");
            variavel2 = variavel2.Replace("]", "");
            variavel2 = variavel2.Replace(")", "");
            variavel2 = variavel2.Replace("(", "");
            variavel2 = variavel2.Replace("%", "");
            variavel2 = variavel2.Replace("�", " ");
            variavel2 = variavel2.Replace("-", "");
            return variavel2;
        }
        #endregion

        #region Valida SQL
        /// <summary>
        /// Tira Aspas da String - Chamar Rotina para limpar string Antes de montar uma SQL pra jogar no banco
        /// </summary>
        /// <param name="texto">String para serem retiradas as Aspas</param>
        /// <returns>Retorna SQL que tem que ser inserida no banco</returns>
        public string validaSQL(string texto)
        {
            return texto = texto.Replace("'", "");

        }
        #endregion

        #region Substitui Virgula por Ponto
        /// <summary>
        /// Tira Aspas da String - Chamar Rotina para limpar string Antes de montar uma SQL pra jogar no banco
        /// </summary>
        /// <param name="texto">String para serem retiradas as Aspas</param>
        /// <returns></returns>
        public string substituiVirgulaPorPonto(string texto)
        {
            return texto = texto.Replace(",", ".");
        }
        #endregion

        #region Substitui Virgula por Ponto
        /// <summary>
        /// Tira Aspas da String - Chamar Rotina para limpar string Antes de montar uma SQL pra jogar no banco
        /// </summary>
        /// <param name="texto">String para serem retiradas as Aspas</param>
        /// <returns></returns>
        public string substituiPontoPorVirgula(string texto)
        {
            return texto = texto.Replace(".", ",");
        }
        #endregion

        #region Limitar Texto
        /// <summary>
        /// Limita o tamanho da STRING - recebe um inteiro e a string, e limita o tamanho dela
        /// </summary>
        /// <param name="texto">Texto a ser limitado</param>
        /// <param name="tam">Quantidades de caracteres que o texto deve ser limitado</param>
        /// <returns>Retorna Texto Limitado</returns>
        public string limitarTexto(string texto, int tam)
        {
            string textoRetorno = texto;
            if (texto.Length > tam)
            {
                textoRetorno = "";
                for (int i = 0; i < tam; i++)
                {
                    textoRetorno = textoRetorno + texto.Substring(i, 1);
                }
            }

            return textoRetorno;
        }
        #endregion

        #region Alongar Texto
        /// <summary>
        /// Alonga o tamanho do texto preenchendo o restante com espaço em branco
        /// </summary>
        /// <param name="texto">Texto a ser limitado</param>
        /// <param name="tam">Quantidades de caracteres que o texto deve sair após caracters</param>
        /// <returns>Retorna Texto Alongado</returns>
        public string alongarTexto(string texto, int tam)
        {
            string textoRetorno = texto;
            if (texto.Length < tam)
            {
                for (int i = 0; i < tam; i++)
                {
                    textoRetorno = textoRetorno + " ";
                }
            }

            return textoRetorno;
        }
        #endregion

        #region Verifica Virgula (Ponto)
        /// <summary>
        /// Função que Verifica se Existe virgula ou ponto em uma STRING
        /// </summary>
        /// <param name="texto">Recebe a String e verifica se a mesma tem ponto ou virgula</param>
        /// <returns>Retorna True se tiver, false se não tiver</returns>
        public bool verificaVirgula(string texto)
        {
            int tam = texto.Length;
            for (int i = 0; i < tam; i++)
            {
                if (texto.Substring(i, 1) == "," || texto.Substring(i, 1) == ".")
                {
                    return true;
                }
            }
            return false;

        }//fim verificarVirgula
        #endregion

        #region Verifica se valor é uma Data Válida (ex: 11/11/2008 ou 11/11/2008 11:11:11 - se for 1/1/08 ele retorna erro - apenas dd/MM/aaaa hh:mm:ss)
        /// <summary>
        /// Verifica se a String enviada é uma Data Válida (no formado dd/mm/aa ou dd/mm/aa hh:mm:ss - quaisquer outros
        /// formatos retornam erro) (ex: d/m/aa ou d/m/aaaa)
        /// </summary>
        /// <param name="data">data a ser verificada</param>
        /// <returns>retorna TRUE se for uma data Válida, false se não</returns>
        public bool verificaSeEData(string data)
        {



            try
            {
                DateTime teste = Convert.ToDateTime(data);
            }
            catch
            {
                return false; //retorna false logo no inicio se der pau na conversão
            }//senão, ele irá continuar testando no código a seguir

            //verifica o .Length do campo em busca do tamanho exato!
            int dataLength = data.Length;

            if (dataLength == 10 || dataLength == 16 || dataLength == 17 || dataLength == 18 || dataLength == 19 || dataLength == 20)
            {
                return true;
            }

            else
            {
                return false;
            }
        }
        #endregion

        #region Verifica se a Data Enviada é menor que a Data Atual
        /// <summary>
        /// Verifica se a String enviada é uma Data Menor que a Data Atual
        /// </summary>
        /// <param name="data">data a ser verificada</param>
        /// <returns>retorna TRUE se for menor que a atual, false se não</returns>
        public bool verificaSeEDataEMenorQueAtual(string data)
        {
            if (Convert.ToDateTime(data) < DateTime.Now.Subtract(new TimeSpan(1, 0, 0, 0)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Retorna Data Atual sem nenhuma pontuação
        /// <summary>
        /// Retorna a Data sem Barras nem : - Limpa, ou seja, se hoje for 02/02/2008 11:11
        /// ela retornará 02022008-111100 (dia, mês, ano, hora, minuto,segundo)
        /// </summary>
        /// <returns>retorna data sem pontuação</returns>
        public string retornaDataSemPontosEBarras()
        {
            string dia = DateTime.Now.Day.ToString();
            string mês = DateTime.Now.Month.ToString();
            string ano = DateTime.Now.Year.ToString();
            string hora = DateTime.Now.Hour.ToString();
            string minuto = DateTime.Now.Minute.ToString();
            string segundo = DateTime.Now.Second.ToString();

            string data = dia + mês + ano + hora + minuto + segundo;
            return data;
        }
        #endregion
    }//fim classe
}//fim namespace
