using System;
using System.Collections.Generic;

namespace XTweb.Models;

public partial class PhanQuyen
{
    public int IdChucNang { get; set; }

    public int IdNhanVien { get; set; }

    public string? GhiNang { get; set; }

    public virtual ChucNang IdChucNangNavigation { get; set; } = null!;

    public virtual NhanVien IdNhanVienNavigation { get; set; } = null!;
}
