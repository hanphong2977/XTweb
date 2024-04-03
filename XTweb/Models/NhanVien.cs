using System;
using System.Collections.Generic;

namespace XTweb.Models;

public partial class NhanVien
{
    public int MaNhanVien { get; set; }

    public string TenNhanVien { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Sdt { get; set; } = null!;

    public string DiaChi { get; set; } = null!;

    public string MatKhau { get; set; } = null!;

    public string? AnhNhanVien { get; set; }
}
