﻿using System.Collections.Generic;

namespace FerryLegacy.domain
{
    public class TimeTable
    {
        public int Id { get; set; }
        public List<TimeTableEntry> Entries { get; set; }
    }
}