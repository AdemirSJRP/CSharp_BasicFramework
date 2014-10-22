/* --------------------------------------------------------------------------------------------------------------------
Fernando Passaia - https://www.linkedin.com/pub/fernando-passaia/24/622/285 - https://www.facebook.com/fernando.passaia
Blog: fernandopassaia.wordpress.com - Email/Skype: fernandopassaia@futuradata.com.br - Cel/Whatsapp: (11)98104-9080
Para feedbacks - favor utilizar o GitHub - ou enviar através dos contatos acima.

Classe para tratamento de compactação e redimensionamento de imagens... Nota: O método principal é Gera nova imagem, e
toda documentação - assim como os testes e valores obtidos - estão documentados no ínicio dele.

 * Nota: Todos podem colaborar subindo suas melhorias, novos métodos e correções para esse projeto totalmente Opensource
 * e livre para uso de quem quiser em qualquer tipo de aplicação. Nota2: Por padrão, compila em C:\CSharp_BasicFramework
--------------------------------------------------------------------------------------------------------------------- */

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Web.UI.HtmlControls;
using System.Threading;

namespace DllUtil.Grafico
{
    public class clsImagem
    { 
        #region Método Para gerar a Imagem em formato (GIF) Danfe
        /// <summary>
        /// Gera uma nova imagem através de um arquivo, e grava o destino com o novo tamanho
        /// </summary>
        /// <param name="larguraMaxima">Largura em Pixels (exemplo: 100 para 100px)</param>
        /// <param name="alturaMaxima">Altura em Pixels (exemplo: 100 para 100px)</param>
        /// <param name="arquivoOriginal">Caminho do original (ex: c:\imagem.jpg)</param>
        /// <param name="arquivoFinal">Caminho do Final (ex: c:\imagemFinal.jpg)</param>
        /// <returns>Retorna True se tudo der OK, False se não</returns>
        public bool gerarNovaImagemDanfe(int larguraMaxima, int alturaMaxima, string arquivoOriginal, string arquivoFinal)
        {
            try
            {
                Image imagemOriginal;
                imagemOriginal = Image.FromFile(arquivoOriginal);
                Bitmap backGround = new Bitmap(arquivoOriginal);
                Bitmap baseMap = new Bitmap(160, 160, PixelFormat.Format24bppRgb);
                Graphics graph = Graphics.FromImage(baseMap);

                graph.SmoothingMode = SmoothingMode.AntiAlias;
                graph.FillRectangle(new SolidBrush(Color.FromArgb(0, 0, 0, 0)), 0, 0, 160, 160);
                graph.DrawImage(imagemOriginal, new Rectangle(0, 0, 160, 160));
                graph.Dispose();

                baseMap.MakeTransparent(baseMap.GetPixel(0, 0));

                /* Grava a Imagem */
                baseMap.Save(arquivoFinal, ImageFormat.Gif);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
        
        #region Método "Ajuste do Novo Tamanho da Imagem"
        //Define o melhor tamanho para a nova imagem, de acordo com a
        //proporção entre largura e altura da imagem original
        private Size novoTamanho(Size Original, Size Nova)
        {

            decimal proporcaoOriginal = ((Original.Height * 100) / Original.Width) / 100;
            Size tamanho = new Size(); ;
            if (proporcaoOriginal > 1)
            {

                proporcaoOriginal = ((Original.Width * 100) / Original.Height) / 100;

                tamanho.Height = Nova.Height;

                tamanho.Width = Convert.ToInt32(Nova.Height * proporcaoOriginal);

                return tamanho;
            }
            else
            {

                tamanho.Width = Nova.Width;

                tamanho.Height = Convert.ToInt32(Nova.Width * proporcaoOriginal);
                return tamanho;
            }
            /*
            If Response.Write("<BR><BR><b>Tamanho original:</b>  " & Original.Width & "x" & Original.Height & "<BR>")

            Response.Write("<b>Novo Tamanho:</b> " & NovoTamanho.Width & "x" & NovoTamanho.Height & "<BR>")
            */
        }
        #endregion        

        #region Gera nova imagem - Altera a resoluçaõ e gera nova imagem (método principal)
        /// <summary>        
        ///A imagem original tem 1024x640 e 539KB. Todas as imagens serão reduzidas para 800x500...
        ///A primeira compactação foi 1 (qualidade baixa) com 10% de qualidade: Imagem de saída tem 14KB.
        ///A segunda compactação foi 1 (qualidade baixa) com 40% de qualidade: Imagem de saída tem 34KB.
        ///A terceira compactação foi 1 (qualidade baixa) com 70% de qualidade: Imagem de saída tem 54KB.
        ///A quarta compactação foi 1 (qualidade baixa) com 100% de qualidade: Imagem de saída tem 299KB.
        ///A quinta compactação foi 2 (qualidade alta) com 10% de qualidade: Imagem de saída tem 15KB.
        ///A sexta compactação foi 2 (qualidade alta) com 40% de qualidade: Imagem de saída tem 36KB.
        ///A setima compactação foi 2 (qualidade alta) com 70% de qualidade: Imagem de saída tem 57KB.
        ///A oitava compactação foi 2 (qualidade alta) com 100% de qualidade: Imagem de saída tem 310KB.
        /// </summary>
        /// <param name="larguraMaxima">Largura da Imagem Nova</param>
        /// <param name="alturaMaxima">Altura da Imagem Nova</param>
        /// <param name="arquivoOriginal">Arquivo original que será transformado</param>
        /// <param name="arquivoFinal">Arquivo final que será salvo já transformado</param>
        /// <param name="qualidade">Qualidade - 1 (Baixa) / 2 (Alta)</param>
        /// <param name="nivelQualidade">Define a porcentagem de compactação que será aplicada sobre a imagem - 0 mais baixa até 100 mais alta</param>
        /// <returns>Retorna o nome do arquivo final - que será o mesmo do arquivoFinal mas acrescido de '-comp.jpg' ao invés de apenas .jpg. Retorna "ERRO" se ocorrer excessão</returns>
        public string gerarNovaImagem(int larguraMaxima, int alturaMaxima, string arquivoOriginal, string arquivoFinal, int qualidade, int nivelQualidade)
        {
            try
            {
                if (File.Exists(arquivoFinal) == true)
                {
                    File.Delete(arquivoFinal);
                }
                if (File.Exists(arquivoFinal) == true)
                {
                    File.Delete(arquivoFinal.Replace(".jpg", "-comp.jpg"));
                }

                //Analise e Dados técnicos de conversão feitos por essa classe - Fernando 102014... Eu enviei uma imagem em 600x400.
                //As imagens aqui descritas foram salvas dentro do Namespace Grafico na Pasta Grafico dentro dessa DLL.
                
                //A imagem original tem 1024x640 e 539KB. Todas as imagens serão reduzidas para 800x500...
                //A primeira compactação foi 1 (qualidade baixa) com 10% de qualidade: Imagem de saída tem 14KB.
                //A segunda compactação foi 1 (qualidade baixa) com 40% de qualidade: Imagem de saída tem 34KB.
                //A terceira compactação foi 1 (qualidade baixa) com 70% de qualidade: Imagem de saída tem 54KB.
                //A quarta compactação foi 1 (qualidade baixa) com 100% de qualidade: Imagem de saída tem 299KB.
                //A quinta compactação foi 2 (qualidade alta) com 10% de qualidade: Imagem de saída tem 15KB.
                //A sexta compactação foi 2 (qualidade alta) com 40% de qualidade: Imagem de saída tem 36KB.
                //A setima compactação foi 2 (qualidade alta) com 70% de qualidade: Imagem de saída tem 57KB.
                //A oitava compactação foi 2 (qualidade alta) com 100% de qualidade: Imagem de saída tem 310KB.

                #region Altera a resolução da imagem
                Image imagemOriginal;
                Image imagemAlterada;

                Graphics grafico;       //Gráfico
                Size dimensaoFinal = new Size(); //Dimensão
                ImageFormat formato;    //Formato

                imagemOriginal = Image.FromFile(arquivoOriginal);
                dimensaoFinal = this.novoTamanho(imagemOriginal.Size, new Size(larguraMaxima, alturaMaxima));
                dimensaoFinal.Height = alturaMaxima; //precisei FORÇAR por que tava dando problema...
                dimensaoFinal.Width = larguraMaxima;

                //Define o novo formato da imagem
                formato = imagemOriginal.RawFormat;

                //Cria a nova imagem
                if (dimensaoFinal.Width == 0)
                {
                    dimensaoFinal.Width = 150;
                }

                if (dimensaoFinal.Height == 0)
                {
                    dimensaoFinal.Height = 150;
                }
                imagemAlterada = new Bitmap(dimensaoFinal.Width, dimensaoFinal.Height);

                grafico = Graphics.FromImage(imagemAlterada);

                //Opções relativas à qualidade da nova imagem
                if(qualidade == 1)
                {
                    grafico.CompositingQuality = CompositingQuality.HighSpeed;
                    grafico.SmoothingMode = SmoothingMode.HighSpeed;
                    grafico.InterpolationMode = InterpolationMode.Low;
                }
                if(qualidade == 2)
                {
                    grafico.CompositingQuality = CompositingQuality.HighQuality;
                    grafico.SmoothingMode = SmoothingMode.HighQuality;
                    grafico.InterpolationMode = InterpolationMode.High;
                }         

                //Desenha a imagem no objeto gráfico this.grafico
                grafico.DrawImage(imagemOriginal, new Rectangle(0, 0, dimensaoFinal.Width, dimensaoFinal.Height));
                
                imagemAlterada.Save(arquivoFinal);

                //inicia a compactação da imagem
                Image myImage = Image.FromFile(arquivoFinal);
                EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, nivelQualidade); 
                // Jpeg image codec 
                ImageCodecInfo   jpegCodec = GetEncoderInfo("image/jpeg"); 

                EncoderParameters encoderParams = new EncoderParameters(1); 
                encoderParams.Param[0] = qualityParam; 
                Thread.Sleep(500);

                myImage.Save(arquivoFinal.Replace(".jpg", "-comp.jpg"), jpegCodec, encoderParams);
                return arquivoFinal.Replace(".jpg", "-comp.jpg");
                #endregion                                
            }
            catch
            {
                return "ERRO";
            }
            finally
            {
                try
                {
                    //tenta excluir o arquivo gerado temporariamente antes da compactação
                    if (File.Exists(arquivoFinal) == true)
                    {
                        File.Delete(arquivoFinal);
                    }
                }
                catch
                {

                }
            }
        }
        #endregion

        #region GetCodecInfo
        /// <summary> 
        /// Returns the image codec with the given mime type 
        /// </summary> 
        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats 
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec 
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];
            return null;
        } 
        #endregion
    }//fim classe
}//fim namespace
