using System;
using System.Collections.Generic;

namespace XTweb.Models;

public partial class ChucNang
{
    public int Id { get; set; }

    public string? TenChucNang { get; set; }

    public string? MaChucNang { get; set; }

    public virtual ICollection<PhanQuyen> PhanQuyens { get; set; } = new List<PhanQuyen>();
}
