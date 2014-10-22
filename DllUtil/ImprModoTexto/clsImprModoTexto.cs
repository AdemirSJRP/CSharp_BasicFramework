/* --------------------------------------------------------------------------------------------------------------------
Fernando Passaia - https://www.linkedin.com/pub/fernando-passaia/24/622/285 - https://www.facebook.com/fernando.passaia
Blog: fernandopassaia.wordpress.com - Email/Skype: fernandopassaia@futuradata.com.br - Cel/Whatsapp: (11)98104-9080
Para feedbacks - favor utilizar o GitHub - ou enviar através dos contatos acima.

Classe para impressão em impressoras de 40 colunas (impressoras de cupom não fiscal). Sua interface é clsImprModoTexto
Pode ser adaptada para usar em impressoras de mais colunas (como Epson Lx300 e outras). Impressão direta em porta COM1
ou LPT através do kernel (não usa nenhuma dll ou componente de nenhum fabricante - funciona perfeitamente!)
 * 
 * Nota: Todos podem colaborar subindo suas melhorias, novos métodos e correções para esse projeto totalmente Opensource
 * e livre para uso de quem quiser em qualquer tipo de aplicação. Nota2: Por padrão, compila em C:\CSharp_BasicFramework
--------------------------------------------------------------------------------------------------------------------- */

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Collections;
using System.Threading;
using DllCrypt;
using DllValidacoes;

namespace DllUtil.ImprModoTexto
{
    #region Construtor da Classe e Variaveis Internas
    public class clsImprModoTexto
    {
        #region Imprime Exemplo
        /// <summary>
        /// Imprime Texto de Exemplo
        /// </summary>        
        /// <param name="porta">Porta da Impressora (Lpt1, Com1, Com2)</param>
        /// <returns>Retorna True se Conseguir tudo, False se não</returns>
        public bool imprimeExemplo(string porta)
        {
            if (porta == "")
            {
                porta = "LPT1";
            }
            try
            {
                clsComunicacaoImprTexto imp = new clsComunicacaoImprTexto();
                imp.IniciarImpressao(porta);
                imp.ImpLFormatacao("DllUtil");
                imp.ImpLFormatacao("--------------------------");
                imp.ImpLFormatacao("Componente de impressao em modo texto");
                for (int i = 0; i < 6; i++)
                {
                    imp.ImpLFormatacao("Teste " + i.ToString());
                }
                imp.ImpLFormatacao(imp.NegritoOn + "Negrito ligado" + imp.NegritoOff);
                imp.ImpLFormatacao(imp.Expandido + "Expan.Ligado" + imp.ExpandidoNormal);
                imp.ImpLFormatacao(imp.Comprimido + "Compr.Ligado" + imp.ComprimidoNormal);
                imp.PulaLinha(10);
                imp.FinalizaImpressão();
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Finaliza Impressao (Pula linhas)
        public void FinalizaImpressao()
        {
            string porta = retornaPortaImpressora();
            if (porta == "")
            {
                porta = "LPT1";
            }
            clsComunicacaoImprTexto impr = new clsComunicacaoImprTexto();
            impr.IniciarImpressao(porta);
            impr.PulaLinha(10);
        }
        #endregion
        
        #region Finaliza Impressao (Pula linhas)
        public void FinalizaImpressao(string porta, int numeroLinhasPular)
        {            
            if (porta == "")
            {
                porta = "LPT1";
            }
            clsComunicacaoImprTexto impr = new clsComunicacaoImprTexto();
            int linhasPuladas = 0;
            while(linhasPuladas < numeroLinhasPular)
            {
                impr.IniciarImpressao(porta);
                impr.ImpLFormatacao("");
                impr.FinalizaImpressão();
                linhasPuladas++;
            }
        }
        #endregion

        #region Retorna Porta da Impressora
        /// <summary>
        /// Lê a configuração e retorna porta de impressora
        /// </summary>
        /// <returns>retorna a porta de impressora</returns>
        public string retornaPortaImpressora()
        {
            
            if (1 == 1) //aqui você deve criar sua lógica para recuperar a porta salva em algum lugar (banco, registro, xml, txt, etc)
            {               
                return "LPT1";
            }
            else
            {
                return "LPT1";
            }
        }
        #endregion
    }
    #endregion
}//fim namespace
