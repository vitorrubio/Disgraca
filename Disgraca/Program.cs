using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disgraca
{
    class Program
    {

        static void Main(string[] args)
        {
            if(args.Length < 3)
            {
                Console.WriteLine("**** T� ERRADO PORRA ****");
                Console.WriteLine("Voc� precisa de 3 argumentos");
                Console.WriteLine("use Disgraca <encoding desejado> <diretorio> <extens�o *.xxx> [<demais extens�es *.xxx>] onde *.xxx � sua extens�o com wildcards e sem <> [] bicho animal");
                Console.WriteLine("Aqui espa�os separam argumentos, ent�o seu arquivo porco n�o pode ter espa�os no nome. N�o gostou faz melhor");
                return;
            }



            string targetEncoding = args[0];
            string diretorio = args[1];

            if(!Directory.Exists(diretorio) || ! ((File.GetAttributes(diretorio) & FileAttributes.Directory) == FileAttributes.Directory))
            {
                Console.WriteLine("**** MANO, VOC� � BURRO? ****");
                Console.WriteLine("* VOC� � BURRO? *");
                Console.WriteLine("* � LIM�TROFE? *");

                Console.WriteLine($"DESDE QUANDO {diretorio} � UM DIRET�RIO V�LIDO?!?!");

                Console.WriteLine("S� SE FOR NA TUA TERRA!");

                Console.WriteLine("use Disgraca <encoding desejado> <diretorio> <extens�o *.xxx> [<demais extens�es *.xxx>] onde *.xxx � sua extens�o com wildcards e sem <> [] bicho animal");
                Console.WriteLine("Aqui espa�os separam argumentos, ent�o seu arquivo porco n�o pode ter espa�os no nome. N�o gostou faz melhor");
                return;
            }

            for(int i=2; i<args.Length; i++)
            {
                try
                {
                    foreach (var f in new DirectoryInfo(diretorio).GetFiles(args[i], SearchOption.AllDirectories))
                    {
                        var fileEnc = GetEncoding(f.FullName);
                        if (fileEnc != null && !string.Equals(fileEnc, targetEncoding, StringComparison.OrdinalIgnoreCase))
                        {

                            var str = File.ReadAllText(f.FullName, Encoding.GetEncoding(fileEnc));
                            File.WriteAllText(f.FullName, str, Encoding.GetEncoding(targetEncoding));

                            Console.WriteLine($"Arquivo {f.FullName} convertido de {fileEnc} para {targetEncoding}");

                        }
                    }
                }
                catch(Exception err)
                {
                    Console.WriteLine("Meua migo, alguma merda n�s fizemos (provavelmente s� voc�), l� a� e v� que que deu: " + err.Message);
                    return;
                }

            }


            Console.WriteLine("Feito!");
        }

        private static string GetEncoding(string filename)
        {
            using (var fs = File.OpenRead(filename))
            {
                var cdet = new Ude.CharsetDetector();
                cdet.Feed(fs);
                cdet.DataEnd();
                //if (cdet.Charset != null)
                //    Console.WriteLine("Charset: {0}, confidence: {1} : " + filename, cdet.Charset, cdet.Confidence);
                //else
                //    Console.WriteLine("Detection failed: " + filename);
                return cdet.Charset;
            }
        }
    }
}
