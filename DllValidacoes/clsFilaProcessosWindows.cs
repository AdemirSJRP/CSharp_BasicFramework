/* --------------------------------------------------------------------------------------------------------------------
Fernando Passaia - https://www.linkedin.com/pub/fernando-passaia/24/622/285 - https://www.facebook.com/fernando.passaia
Blog: fernandopassaia.wordpress.com - Email/Skype: fernandopassaia@futuradata.com.br - Cel/Whatsapp: (11)98104-9080
Para feedbacks - favor utilizar o GitHub - ou enviar através dos contatos acima.

Classe que lida com os processos do Windows.
 * Nota: Todos podem colaborar subindo suas melhorias, novos métodos e correções para esse projeto totalmente Opensource
 * e livre para uso de quem quiser em qualquer tipo de aplicação. Nota2: Por padrão, compila em C:\CSharp_BasicFramework
--------------------------------------------------------------------------------------------------------------------- */

using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace DllValidacoes
{
    #region Classe estática clsFilaProcessosWindows
    public static class clsFilaProcessosWindows
    {
        #region "Método estático que lista os processos aberto do Windows"
        /// <summary>
        /// Lista todos os processos
        /// </summary>
        /// <returns>retorna a lista com as propriedades do processo</returns>
        public static List<PropriedadesProcessos> ListaProcessos()
        {
            List<PropriedadesProcessos> lstProcessos = new List<PropriedadesProcessos>();

            Process[] Processos = Process.GetProcesses();

            foreach (var item in Processos)
            {
                lstProcessos.Add(new PropriedadesProcessos { Nome = item.ProcessName, ID = item.Id.ToString(), Titulo_Pagina = item.MainWindowTitle });
            }

            //var query = (from o in lstProcessos
              //           select o).OrderBy(p => p.ID);
            return lstProcessos;
        }
        #endregion

        #region "Método estático que inicia um processo no Windows"
        /// <summary>
        /// Inicia um novo processo
        /// </summary>
        /// <param name="Nome">Nome do novo processo</param>
        public static void IniciarProcesso(string Nome)
        {
            Process.Start(Nome);
        }
        #endregion

        #region "Método estático que finaliza um processo no Windows por ID"
        /// <summary>
        /// Finaliza um processo pelo ID
        /// </summary>
        /// <param name="IDPROC">Id do processo que será finalizado</param>
        public static void FinalizaProcessoID(string IDPROC)
        {
            foreach (var proc in Process.GetProcesses())
            {
                if (proc.Id.ToString() == IDPROC)
                {
                    proc.Kill();
                }

            }
        }
        #endregion

        #region Método estático para finalizar processo por nome
        public static void finalizarProcessoPorNome(string nome) {                                    
            string id = "";      
            Process[] Processos = Process.GetProcesses();

            foreach (var item in Processos)
            {
                if( nome == item.ProcessName){
                    id = item.Id.ToString();
                    FinalizaProcessoID(id);
                }                
            }          

        }
        #endregion

        #region Método estático para finalizar Programa
        public static void finalizarPrograma()
        {  
            string nome = System.Environment.CommandLine.ToString();// ExitCode.ToString();

            while (nome.IndexOf(@"\") != -1)
            {
                nome= nome.Substring(nome.IndexOf(@"\") + 1);
                //MessageBox.Show(nome);
            }
            nome = nome.Substring(0, nome.IndexOf(".exe"));
            //MessageBox.Show(nome);

            finalizarProcessoPorNome(nome);
        }
        #endregion
    }
    #endregion

    #region Classe Utilizada para armazenar dados dos Processos do windows
    public class PropriedadesProcessos
    {
        public string Nome
        { get; set; }

        public string ID
        { get; set; }

        public string Titulo_Pagina
        { get; set; }
    }
    #endregion
}
