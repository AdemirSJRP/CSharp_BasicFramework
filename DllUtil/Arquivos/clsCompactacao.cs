/* --------------------------------------------------------------------------------------------------------------------
Fernando Passaia - https://www.linkedin.com/pub/fernando-passaia/24/622/285 - https://www.facebook.com/fernando.passaia
Blog: fernandopassaia.wordpress.com - Email/Skype: fernandopassaia@futuradata.com.br - Cel/Whatsapp: (11)98104-9080
Para feedbacks - favor utilizar o GitHub - ou enviar através dos contatos acima.

Classe de Compactação ou descompactação de arquivos (formato ZIP)
Essa classe em geral foi programada entre 2008 e 2010 e funciona perfeitamente - mas precisa de revisões/melhorias.
 * 
 * Nota: Todos podem colaborar subindo suas melhorias, novos métodos e correções para esse projeto totalmente Opensource
 * e livre para uso de quem quiser em qualquer tipo de aplicação. Nota2: Por padrão, compila em C:\CSharp_BasicFramework
--------------------------------------------------------------------------------------------------------------------- */

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace DllUtil.Arquivos
{
    public class clsCompactacao
    { 
        #region Compactacao de Arquivos
        /// <summary>
        /// Compacta um Arquivo em Formato .ZIP
        /// </summary>
        /// <param name="arquivoOrigemDescompactado">Caminho do Arquivo de Origem</param>
        /// <param name="arquivoDestinoCompactado">Caminho do Arquivo de Destino</param>
        /// <returns>Retorna "OK" se conseguir, 'ARQUIVO ORIGEM NÃO EXISTE' se não achar o arquivo de origem,
        /// e 'ERRO NA COMPACTACAO' caso aconteça algum problema dentro do método</returns>
        public string compactarArquivo(string arquivoOrigemDescompactado, string arquivoDestinoCompactado)
        {
             //verifica se o arquivo existe - se não existir
             if (File.Exists(arquivoOrigemDescompactado) == false)
             {
                 return "ARQUIVO ORIGEM NÃO EXISTE!";
             }
             // Create um Array de Bytes para Armazenar o Arquivo em Memória
             byte[] buffer = null;
             FileStream sourceStream = null;
             FileStream destinationStream = null;
             GZipStream compressedStream = null;

             try            
             {
                 // Le o arquivo dentro do Array de Bytes
                 sourceStream = new FileStream(arquivoOrigemDescompactado, FileMode.Open, FileAccess.Read, FileShare.Read);
                 buffer = new byte[sourceStream.Length];

                 int checkCounter = sourceStream.Read(buffer, 0, buffer.Length);
                 if (checkCounter != buffer.Length)
                 {
                     throw new ApplicationException();
                 }

                 // Open the FileStream to write to
                 destinationStream = new FileStream(arquivoDestinoCompactado, FileMode.OpenOrCreate, FileAccess.Write);

                 // Create a compression stream pointing to the destiantion stream
                 compressedStream = new GZipStream(destinationStream, CompressionMode.Compress, true);

                 // Now write the compressed data to the destination file
                 compressedStream.Write(buffer, 0, buffer.Length);
                 return "OK";
             }

            catch
            {
                return "ERRO NA COMPACTACAO";
            }

            finally

            {
                if (sourceStream != null)
                sourceStream.Close();
                if (compressedStream != null)
                compressedStream.Close();
                if (destinationStream != null)
                destinationStream.Close();
             }            
       }//fim metodo
       #endregion
 
        #region Descompactacao de Arquivos
        /// <summary>
        /// Descompacta um Arquivo em Formato .ZIP
        /// </summary>
        /// <param name="arquivoOrigemCompactado">Caminho do Arquivo de Origem</param>
        /// <param name="arquivoDestinoDescompactado">Caminho do Arquivo de Destino</param>
        /// <returns>Retorna "OK" se conseguir, 'ARQUIVO ORIGEM NÃO EXISTE' se não achar o arquivo de origem,
        /// e 'ERRO NA COMPACTACAO' caso aconteça algum problema dentro do método</returns>
        public string descompactarArquivo(string arquivoOrigemCompactado, string arquivoDestinoDescompactado)
        {
            //verifica se o arquivo de origem existe
            if (File.Exists(arquivoOrigemCompactado) == false)
            {
                return "ARQUIVO ORIGEM NÃO EXISTE!";
            }

            // cria um array para guardar o arquivo em memoria
            FileStream sourceStream = null;
            FileStream destinationStream = null;
            GZipStream decompressedStream = null;
            byte[] quartetBuffer = null;
            try
            {
                sourceStream = new FileStream(arquivoOrigemCompactado, FileMode.Open);
                decompressedStream = new GZipStream(sourceStream, CompressionMode.Decompress, true);
                quartetBuffer = new byte[4];
                int position = (int)sourceStream.Length - 4;
                sourceStream.Position = position;
                sourceStream.Read(quartetBuffer, 0, 4);
                sourceStream.Position = 0;
                int checkLength = BitConverter.ToInt32(quartetBuffer, 0);
                byte[] buffer = new byte[checkLength + 100];
                int offset = 0;
                int total = 0;

                while (true)
                {
                    int bytesRead = decompressedStream.Read(buffer, offset, 100);
                    if (bytesRead == 0)
                    break;
                    offset += bytesRead;
                    total += bytesRead;
                }

                destinationStream = new FileStream(arquivoDestinoDescompactado, FileMode.Create);
                destinationStream.Write(buffer, 0, total);
                destinationStream.Flush();
                return "OK";
            }

            catch
            {
                return "ERRO NA COMPACTACAO";
            }

            finally
            {
                //Destruimos todos os objetos da memória dentro do finally
                if (sourceStream != null)
                sourceStream.Close();
                if (decompressedStream != null)
                decompressedStream.Close();
                if (destinationStream != null)
                destinationStream.Close();
            }//fim finally
        }//fim metodo
        #endregion
    }//fim classe
}//fim namespace
