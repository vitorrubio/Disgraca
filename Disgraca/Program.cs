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
                Console.WriteLine("**** TÁ ERRADO PORRA ****");
                Console.WriteLine("Você precisa de 3 argumentos");
                Console.WriteLine("use Disgraca <encoding desejado> <diretorio> <extensão *.xxx> [<demais extensões *.xxx>] onde *.xxx é sua extensão com wildcards e sem <> [] bicho animal");
                Console.WriteLine("Aqui espaços separam argumentos, então seu arquivo porco não pode ter espaços no nome. Não gostou faz melhor");
                return;
            }



            string targetEncoding = args[0];
            string diretorio = args[1];

            if(!Directory.Exists(diretorio) || ! ((File.GetAttributes(diretorio) & FileAttributes.Directory) == FileAttributes.Directory))
            {
                Console.WriteLine("**** MANO, VOCÊ É BURRO? ****");
                Console.WriteLine("* VOCÊ É BURRO? *");
                Console.WriteLine("* É LIMÍTROFE? *");

                Console.WriteLine($"DESDE QUANDO {diretorio} É UM DIRETÓRIO VÁLIDO?!?!");

                Console.WriteLine("SÓ SE FOR NA TUA TERRA!");

                Console.WriteLine("use Disgraca <encoding desejado> <diretorio> <extensão *.xxx> [<demais extensões *.xxx>] onde *.xxx é sua extensão com wildcards e sem <> [] bicho animal");
                Console.WriteLine("Aqui espaços separam argumentos, então seu arquivo porco não pode ter espaços no nome. Não gostou faz melhor");
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
                    Console.WriteLine("Meua migo, alguma merda nós fizemos (provavelmente só você), lê aí e vê que que deu: " + err.Message);
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
