using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Assets.Characters.Character_Serializer
{
    public class CharacterXML
    {
        [XmlAttribute("Type")]
        public CharacterType type;

        [XmlAttribute("Role")]
        public CharacterRole role;

        [XmlAttribute("Health")]
        public Health health;

        [XmlAttribute("Stats")]
        public CharacterStats stats;

        [XmlAttribute("Current Position")]
        public Cell currentPosition;

        [XmlAttribute("Old Position")]
        public Cell oldPosition;

        [XmlAttribute("Direction")]
        public PlayerDirection direction;

        [XmlAttribute("Observers")]
        public ArrayList observers;
    }
}
