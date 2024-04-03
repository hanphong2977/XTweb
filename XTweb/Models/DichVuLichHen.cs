using System;
using System.Collections.Generic;

namespace XTweb.Models;

public partial class DichVuLichHen
{
    public int IdDichVu { get; set; }

    public int IdLichHen { get; set; }

    public virtual DichVu IdDichVuNavigation { get; set; } = null!;

    public virtual LichHen IdLichHenNavigation { get; set; } = null!;
}
