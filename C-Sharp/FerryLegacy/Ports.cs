﻿using System;
using System.Collections.Generic;
using System.IO;
using FerryLegacy.domain;
using ServiceStack;

namespace FerryLegacy
{
    public class Ports
    {
        private readonly List<Port> _ports = new List<Port>();

        public Ports()
        {
            var reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\data\\ports.txt");
            _ports = reader.ReadToEnd().FromJson<List<Port>>();
        }

        public List<Port> All()
        {
            return _ports;
        }
    }
}