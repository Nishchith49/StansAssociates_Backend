using StansAssociates_Backend.Concrete.IServices;
using StansAssociates_Backend.Global;

namespace StansAssociates_Backend.Concrete.Services
{
    public class FileExtensionService : IFileExtensionService
    {
        public string GetExtension(string subString)
        {
            string extension;
            switch (subString.ToUpper())
            {
                case ExtensionConstants.PngSubString:
                    extension = ExtensionConstants.PngExt;
                    break;
                case ExtensionConstants.JpgSubString:
                    extension = ExtensionConstants.JpgExt;
                    break;
                case ExtensionConstants.mp4SubString:
                    extension = ExtensionConstants.mp4Ext;
                    break;
                case ExtensionConstants.pdfSubString:
                    extension = ExtensionConstants.pdfExt;
                    break;
                case ExtensionConstants.icoSubString:
                    extension = ExtensionConstants.icoExt;
                    break;
                case ExtensionConstants.rarSubString:
                    extension = ExtensionConstants.rarExt;
                    break;
                case ExtensionConstants.rtfSubString:
                    extension = ExtensionConstants.rtfExt;
                    break;
                case ExtensionConstants.txtSubString:
                    extension = ExtensionConstants.txtExt;
                    break;
                case ExtensionConstants.srtSubStringa:
                case ExtensionConstants.srtSubStringb:
                    extension = ExtensionConstants.srtExt;
                    break;
                default:
                    extension = string.Empty;
                    break;
            }

            return extension;
        }
    }
}
