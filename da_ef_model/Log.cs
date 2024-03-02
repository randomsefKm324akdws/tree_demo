using System;
using System.Collections.Generic;

namespace da_ef_model;

public partial class Log
{
    public int Id { get; set; }

    public string Type { get; set; }

    public string Data { get; set; }

    public DateTime EventTime { get; set; }
}
