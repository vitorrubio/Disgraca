using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disgraca
{
    public class CommandArguments
    {
        public CommandArguments(string[] args)
        {

            Valid = true;

            if (args == null || args.Length < 3)
            {
                Valid = false;
                Help();
                return;
            }

            Operation = args[0];

            if (!(new List<string>
                {
                    "convert",
                    "list"
                }).Contains(Operation.ToLower()))
            {
                Console.WriteLine($"Peração {Operation} Inválida");
                Valid = false;
                Help();
                return;
            }

            if (Operation == "list")
            {
                DestinationEncoding = "";
                Path = args[1];
                Files = new List<string>(args[2..]);
            }
            else if (Operation == "convert")
            {
                DestinationEncoding = args[1];
                Path = args[2];
                Files = new List<string>(args[3..]);
            }
            else
            {
                Valid = false;
                Help();
                return;
            }


            if (!Directory.Exists(Path) || ((File.GetAttributes(Path) & FileAttributes.Directory) != FileAttributes.Directory))
            {
                Console.WriteLine($"{Path} não é um diretório válido");
                Valid = false;
                Help();
            }


        }

        public List<string> Files { get; }
        public string Operation { get; }
        public string DestinationEncoding { get; }
        public string Path { get; }
        public bool Valid { get; }

        public static void Help()
        {
            Console.WriteLine("uso: Disgraca <operação (convert|list)> <encoding desejado> <diretorio> <extensão *.xxx> [<demais extensões *.xxx>] onde *.xxx é sua extensão com wildcards e sem <> []");
            Console.WriteLine("Exemplo: disgraca.exe convert utf-8 c:\\examples\\ *.html");
            Console.WriteLine("Espaços separam argumentos");
        }
    }
}
