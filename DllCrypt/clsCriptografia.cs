/* --------------------------------------------------------------------------------------------------------------------
Fernando Passaia - https://www.linkedin.com/pub/fernando-passaia/24/622/285 - https://www.facebook.com/fernando.passaia
Blog: fernandopassaia.wordpress.com - Email/Skype: fernandopassaia@futuradata.com.br - Cel/Whatsapp: (11)98104-9080
Para feedbacks - favor utilizar o GitHub - ou enviar através dos contatos acima.

Classe de interface para toda criptografia. Essa classe dará interface para uso da classe Crypt.
Possui dois métodos: Criptografar e Discriptografar que recebem a string e retornam a mesma criptografada.
Nota: Você pode alterar a _crypt.Key para mudar sua criptografia e torna-la unica facilmente.
 * Nota: Todos podem colaborar subindo suas melhorias, novos métodos e correções para esse projeto totalmente Opensource
 * e livre para uso de quem quiser em qualquer tipo de aplicação. Nota2: Por padrão, compila em C:\CSharp_BasicFramework
--------------------------------------------------------------------------------------------------------------------- */

using System;
using System.Collections.Generic;
using System.Text;

namespace DllCrypt
{
    public class clsCriptografia
    {
        #region Variaveis Internas da Classe
        string encryptedText = string.Empty;
        string decryptedText = string.Empty;
        string hashedText = string.Empty;
        #endregion

        #region Criptografar
        /// <summary>
        /// Recebe uma String e Retorna ela Criptografada
        /// </summary>
        /// <param name="Valor">String para ser Criptografada</param>
        /// <returns>Retorna String já criptografada</returns>
        public string Criptografar(string Valor)
        {
            string key = Valor;
            Crypt _crypt = new Crypt((CryptProvider)4);
            _crypt.Key = "";
            encryptedText = _crypt.Encrypt(Valor);
            return encryptedText; //adiciona um FD ao final, coloquei pra baguncar a vida de quem tentar descriptografar

        }//fim método Criptografar
        #endregion

        #region Descriptografar
        /// <summary>
        /// Recebe uma String Criptografada e retorna ela Descriptografada
        /// </summary>
        /// <param name="Valor">String para ser Criptografada</param>
        /// <returns>Retorna String já criptografada</returns>
        public string Descriptografar(string Valor)
        {
            string key = Valor; //retirando o A do final, coloquei pra confundir quem tentar descriptografar o que vier do banco
            Crypt _crypt = new Crypt((CryptProvider)4);
            _crypt.Key = "";
            decryptedText = _crypt.Decrypt(Valor);
            return decryptedText;
        }//fim método Descriptografar
        #endregion
    }//fim namespace
}//fim classe
