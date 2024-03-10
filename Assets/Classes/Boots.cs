﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class Boots
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public int AdditionalArmour { get; set; }
        public int Rarity { get; set; }
        public string Url { get; set; }

        public Boots(int id, string name, int value, int additionalArmour, int rarity, string url)
        {
            Id = id;

            Name = name;
            Value = value;
            AdditionalArmour = additionalArmour;
            Rarity = rarity;
            Url = url;
        }
        public override string ToString()
        {
            return $"Boots Details:\n" +
                   $"Name: {Name}\n" +
                   $"Additional Armour: {AdditionalArmour}\n" +
                   $"Value: {Value}\n" +
                   $"Penetration: {Penetration}";
        }
    }
}
