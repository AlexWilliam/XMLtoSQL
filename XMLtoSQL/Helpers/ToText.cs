using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Web;

namespace XMLtoSQL.Helpers {
    public class ToText {

        public static string RemoveCaracteresEspeciaisBetrieve(string texto) {
            if (string.IsNullOrEmpty(texto))
                return string.Empty;
            try {
                texto = Regex.Replace(texto, @"'*", string.Empty);
                texto = Regex.Replace(texto, @"[^0-9a-zA-ZéúíóáÉÚÍÓÁèùìòàÈÙÌÒÀõãñÕÃÑêûîôâÊÛÎÔÂëÿüïöäËYÜÏÖÄçÇ\.\,\s]+?", string.Empty);
                //texto = HttpUtility.HtmlDecode(texto);
            } catch (Exception) { }
            return texto;
        }

        public static string RemoveCaracteresEspeciaisPrisma(string texto) {
            if (string.IsNullOrEmpty(texto))
                return string.Empty;
            try {
                texto = Regex.Replace(texto, @"'*", string.Empty);
                texto = Regex.Replace(texto, @"[^0-9a-zA-ZéúíóáÉÚÍÓÁèùìòàÈÙÌÒÀõãñÕÃÑêûîôâÊÛÎÔÂëÿüïöäËYÜÏÖÄçÇ\.\s]+?", string.Empty);
                //texto = HttpUtility.HtmlDecode(texto);
            } catch (Exception) { }
            return texto;
        }

        public static string RemoveCaracteresEspeciais(string texto, bool preservaEspacos) {
            if (string.IsNullOrEmpty(texto))
                return string.Empty;
            texto = HttpUtility.HtmlDecode(texto);
            try {

                texto = Regex.Replace(texto, @"'*", string.Empty);
                if (preservaEspacos)
                    texto = Regex.Replace(texto, @"[^0-9a-zA-ZéúíóáÉÚÍÓÁèùìòàÈÙÌÒÀõãñÕÃÑêûîôâÊÛÎÔÂëÿüïöäËYÜÏÖÄçÇ\s]+?", string.Empty);
                else
                    texto = Regex.Replace(texto, @"[^0-9a-zA-ZéúíóáÉÚÍÓÁèùìòàÈÙÌÒÀõãñÕÃÑêûîôâÊÛÎÔÂëÿüïöäËYÜÏÖÄçÇ]+?", string.Empty);
            } catch (Exception) { }
            return texto;
        }

        public static string RemoveAcentos(string texto) {
            if (string.IsNullOrEmpty(texto))
                return string.Empty;
            StringBuilder _sb = new StringBuilder();
            texto = HttpUtility.HtmlDecode(texto);
            try {
                texto = Regex.Replace(texto, @"'*", string.Empty);
                for (int i = 0; i < texto.Length; i++) {
                    if (texto[i] > 255)
                        _sb.Append(texto[i]);
                    else
                        _sb.Append(_diacriticos[texto[i]]);
                }
            } catch (Exception) { }
            return _sb.ToString();
        }

        private static readonly char[] _diacriticos = GetDiacriticos();

        private static char[] GetDiacriticos() {
            char[] _accentos = new char[256];

            for (int i = 0; i < 256; i++)
                _accentos[i] = (char)i;

            _accentos[(byte)'á'] = _accentos[(byte)'à'] = _accentos[(byte)'ã'] = _accentos[(byte)'â'] = _accentos[(byte)'ä'] = _accentos[unchecked((byte)'ă')] = 'a';
            _accentos[(byte)'Á'] = _accentos[(byte)'À'] = _accentos[(byte)'Ã'] = _accentos[(byte)'Â'] = _accentos[(byte)'Ä'] = 'A';

            _accentos[(byte)'é'] = _accentos[(byte)'è'] = _accentos[(byte)'ê'] = _accentos[(byte)'ë'] = 'e';
            _accentos[(byte)'É'] = _accentos[(byte)'È'] = _accentos[(byte)'Ê'] = _accentos[(byte)'Ë'] = 'E';

            _accentos[(byte)'í'] = _accentos[(byte)'ì'] = _accentos[(byte)'î'] = _accentos[(byte)'ï'] = 'i';
            _accentos[(byte)'Í'] = _accentos[(byte)'Ì'] = _accentos[(byte)'Î'] = _accentos[(byte)'Ï'] = 'I';

            _accentos[(byte)'ó'] = _accentos[(byte)'ò'] = _accentos[(byte)'ô'] = _accentos[(byte)'õ'] = _accentos[(byte)'ö'] = 'o';
            _accentos[(byte)'Ó'] = _accentos[(byte)'Ò'] = _accentos[(byte)'Ô'] = _accentos[(byte)'Õ'] = _accentos[(byte)'Ö'] = 'O';

            _accentos[(byte)'ú'] = _accentos[(byte)'ù'] = _accentos[(byte)'û'] = _accentos[(byte)'ü'] = 'u';
            _accentos[(byte)'Ú'] = _accentos[(byte)'Ù'] = _accentos[(byte)'Û'] = _accentos[(byte)'Ü'] = 'U';

            _accentos[(byte)'ç'] = 'c';
            _accentos[(byte)'Ç'] = 'C';

            _accentos[(byte)'ñ'] = 'n';
            _accentos[(byte)'Ñ'] = 'N';

            _accentos[(byte)'ÿ'] = _accentos[(byte)'ý'] = 'y';
            _accentos[(byte)'Ý'] = 'Y';

            return _accentos;
        }

    }
}
