using System;
using System.Collections.Generic;

namespace XTweb.Models;

public partial class DichVu
{
    public int MaDichVu { get; set; }

    public string TenDichVu { get; set; } = null!;

    public double Gia { get; set; }

    public string? AnhDichVu { get; set; }

    public virtual ICollection<LichHen> LichHens { get; set; } = new List<LichHen>();
}
