using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace SprintZero1.XMLFiles
{
    public class XMLParser
    {
        public XMLParser(Game1 game)
        { 
        
            

        }

        public void Parse(String fileName) 
        {
            XmlReader xmlReader = new XmlTextReader(fileName);
            while (xmlReader.Read()) 
            {
                String nodeType = xmlReader.NodeType.ToString();
                switch (nodeType)
                {
                    case "Blocks":

                        break;
                    case "Items":

                        break;
                    case "Enemies":

                        break;
                }
            }

          
        }


    }
}
