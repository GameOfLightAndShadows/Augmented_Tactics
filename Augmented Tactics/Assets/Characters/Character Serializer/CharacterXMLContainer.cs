using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Assets.Map.Creator;

namespace Assets.Characters.Character_Serializer
{
    [XmlRoot ("Characters Collection")]
    public class CharacterXMLContainer
    {
        [XmlAttribute("NumberCharacter")]
        public int number;

        [XmlArray("Humans")]
        [XmlArrayItem("Human")]
        public List<CharacterXML> humans = new List<CharacterXML>();

        [XmlArray("AI Characters")]
        [XmlArrayItem("AI Character")]
        public List<CharacterXML> ai = new List<CharacterXML>();
    }
}
