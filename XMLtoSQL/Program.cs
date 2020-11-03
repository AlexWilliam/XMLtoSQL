using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using XMLtoSQL.Helpers;

namespace XMLtoSQL {
    class Program {

        static void Main(string[] args) {

            string _nomeArquivo;
            string _nomeTabela;

            string camposTabelas;
            string values;

            int linha = 0;
            int i = 0;
            int c = 0;

            try {

                foreach (string filePath in Directory.GetFiles(@"E:\310", "*.xml", SearchOption.AllDirectories)) {

                    camposTabelas = "";

                    Console.WriteLine("Ciando arquivo SQL de " + filePath);

                    XmlDocument _doc = new XmlDocument();

                    _doc.Load(filePath);

                    _nomeArquivo = filePath.Replace(".xml", ".sql");

                    if (File.Exists(_nomeArquivo)) {
                        File.Delete(_nomeArquivo);
                    }

                    _nomeTabela = filePath.Replace(@"E:\310\", "").Replace(".xml", "");

                    i = 0;
                    linha = 0;

                    using (StreamWriter _ws = new StreamWriter(_nomeArquivo, true, Encoding.Unicode)) {

                        foreach(XmlNode _linha in _doc.SelectNodes("CLIENTES/CLIENTE")) {
                        //foreach (XmlNode _linha in _doc.SelectNodes("anotacoes/anotacao")) {
                            if (linha == 0) {

                                _ws.WriteLine("CREATE TABLE "+ _nomeTabela + " (");

                                foreach (XmlNode _campo in _linha) {
                                    if (_linha.LastChild.Name == _campo.Name) {
                                        _ws.WriteLine(ToText.RemoveAcentos( ToText.RemoveCaracteresEspeciais(_campo.Name, false)) + " ntext");
                                        camposTabelas += ToText.RemoveAcentos(ToText.RemoveCaracteresEspeciais(_campo.Name, false)) + "";
                                    } else {
                                        _ws.WriteLine(ToText.RemoveAcentos(ToText.RemoveCaracteresEspeciais(_campo.Name, false)) + " ntext,");
                                        camposTabelas += ToText.RemoveAcentos(ToText.RemoveCaracteresEspeciais(_campo.Name, false)) + ",";
                                    }

                                    i++;
                                }

                                _ws.WriteLine(")");

                                _ws.WriteLine("");
                                _ws.WriteLine("");

                                linha++;
                            }

                            values = "";

                            for (c=0; c< i; c++) {
                                if (c == i-1) {
                                    values += "'" + _linha.ChildNodes[c].InnerText.Replace("'","")+ "'";
                                } else{
                                    values += "'" + _linha.ChildNodes[c].InnerText.Replace("'", "") + "', ";
                                }
                            }

                            _ws.WriteLine("INSERT INTO "+_nomeTabela+"("+camposTabelas+") VALUES ("+values+");");

                        }

                    }
                }

                Console.WriteLine("");

                Console.WriteLine("Conversão executada com sucesso! Pressione qualquer tecla para sair...");

                Console.ReadKey();

            } catch (Exception e) {
                throw e;
            }

        }
    }
}
