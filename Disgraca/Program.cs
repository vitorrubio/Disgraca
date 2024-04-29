using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disgraca
{
    public static class Program
    {

        public static void Main(string[] args)
        {
            var op = new CommandArguments(args);
            if (!op.Valid)
            {
                return;
            }

            if (op.Operation == "convert")
            {
                Convert(op);
            }

            if (op.Operation == "list")
            {
                ListCharsets(op);
            }

        }

        private static void Convert(CommandArguments op)
        {
            foreach (var file in op.Files)
            {
                try
                {
                    foreach (var fullName in new DirectoryInfo(op.Path).GetFiles(file, SearchOption.AllDirectories).Select(x => x.FullName))
                    {
                        var fileEnc = Translate( GetEncoding(fullName));
                        if (fileEnc != null && !string.Equals(fileEnc, op.DestinationEncoding, StringComparison.OrdinalIgnoreCase))
                        {

                            var str = File.ReadAllText(fullName, Encoding.GetEncoding(fileEnc));
                            File.WriteAllText(fullName, str, Encoding.GetEncoding(op.DestinationEncoding));

                            Console.WriteLine($"Arquivo {fullName} convertido de {fileEnc} para {op.DestinationEncoding}");

                        }
                    }
                }
                catch (Exception err)
                {
                    Console.WriteLine("Ocorreu o seguinte erro" + err.Message);
                    return;
                }

            }
        }

        private static void ListCharsets(CommandArguments op)
        {
            foreach (var file in op.Files)
            {
                try
                {
                    foreach (var fullName in new DirectoryInfo(op.Path).GetFiles(file, SearchOption.AllDirectories).Select(x => x.FullName))
                    {
                        var fileEnc = ListEncoding(fullName);
                        Console.WriteLine($"Arquivo {fullName} é {Translate(fileEnc.Charset)} com {fileEnc.Confidence} de certeza");
                    }
                }
                catch (Exception err)
                {
                    Console.WriteLine("Ocorreu o seguinte erro" + err.Message);
                    return;
                }

            }
        }

        private static string GetEncoding(string filename)
        {
            using (var fs = File.OpenRead(filename))
            {
                var cdet = new Ude.CharsetDetector();
                cdet.Feed(fs);
                cdet.DataEnd();
                return cdet.Charset;
            }
        }


        private static (string Charset, string Confidence) ListEncoding(string filename)
        {
            using (var fs = File.OpenRead(filename))
            {
                var cdet = new Ude.CharsetDetector();
                cdet.Feed(fs);
                cdet.DataEnd();
                return (cdet.Charset, cdet.Confidence.ToString("P2"));
            }
        }


        private static readonly ImmutableDictionary<string, string> _standardEncodings = ImmutableDictionary.CreateRange(new Dictionary<string, string>
            {
                {"windows-1252", "ISO-8859-1" }
            });

        public static string Translate(string encoding)
        {
            if (!_standardEncodings.ContainsKey(encoding))
            {
                return encoding;
            }

            return _standardEncodings[encoding];
        }
    }
}
