using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace txstudio.iTextSharp.SampleCode.Secured
{
    class Program
    {
        static void Main(string[] args)
        {
            var _path = @"../../NET Platform Guide.pdf";
            var _buffer = File.ReadAllBytes(_path);
            var _secured = new byte[] { };

            _secured = SecuredPdf(_buffer);

            _path = _path.Replace(".pdf", "-secured.pdf");

            File.WriteAllBytes(_path, _secured);
        }

        static byte[] SecuredPdf(byte[] content)
        {
            var _result = new byte[] { };

            using (MemoryStream _stream = new MemoryStream())
            {
                using (PdfReader _reader = new PdfReader(content))
                {
                    Dictionary<string, string> info = _reader.Info;

                    PdfStamper stamper = new PdfStamper(_reader, _stream);

                    stamper.MoreInfo = info;
                    stamper.SetEncryption(PdfWriter.STRENGTH40BITS
                                        , null
                                        , null
                                        , PdfWriter.AllowScreenReaders);

                    stamper.Close();
                }

                _result = _stream.ToArray();
            }

            return _result;
        }
    }
}
