
//# Public API Tool for Click2Mail
//
//This file is part ofPublic API Tool for Click2Mail, Developed by Vincent D. Senese.
//
//Public API Tool for Click2Mail is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.
//
//Public API Tool for Click2Mail is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
//
//You should have received a copy of the GNU General Public License
//along with Click2Mail Too.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;



namespace ConvertedClick2Mail
{
    public static class Utils
    {
        public static string Left(this string str, int length)
        {
            return str.Substring(0, Math.Min(length, str.Length));
        }
        public static void Merge(string file, string OutFile, ref SetupStationaryFields.addresscollection aic)
        {
            using (FileStream stream = new FileStream(OutFile, FileMode.Create))
            {
                using (Document doc = new Document())
                {
                    using (PdfCopy pdf = new PdfCopy(doc, stream))
                    {
                        doc.Open();

                        PdfReader reader = null;
                        PdfImportedPage page = null;

                        //fixed typo
                        int ii = 0;
                        int count = 0;

                        foreach (SetupStationaryFields.addressitem ai in aic)
                        {

                            if ((!ai.ommitted))
                            {

                                reader = new PdfReader(file);
                                PdfReader.unethicalreading = true;
                                count = reader.NumberOfPages;
                                for (int i = 0; i <= reader.NumberOfPages - 1; i++)
                                {
                                    page = pdf.GetImportedPage(reader, i + 1);
                                    pdf.AddPage(page);
                                }

                                pdf.FreeReader(reader);
                                reader.Close();



                                ai.startpage = ((ii) * count) + 1;
                                ai.endpage = (ii * count) + count;
                                ii = ii + 1;

                            }
                        }
                    }
                }
                stream.Close();
            }
        }

    }
}
