using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace XTweb.Models;

public partial class XuanTamDbContext : DbContext
{
    public XuanTamDbContext()
    {
    }

    public XuanTamDbContext(DbContextOptions<XuanTamDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChucNang> ChucNangs { get; set; }

    public virtual DbSet<Cthd> Cthds { get; set; }

    public virtual DbSet<CthdsanPham> CthdsanPhams { get; set; }

    public virtual DbSet<DanhMucSanPham> DanhMucSanPhams { get; set; }

    public virtual DbSet<DichVu> DichVus { get; set; }

    public virtual DbSet<HoaDonDichVu> HoaDonDichVus { get; set; }

    public virtual DbSet<HoaDonSanPham> HoaDonSanPhams { get; set; }

    public virtual DbSet<KhachHang> KhachHangs { get; set; }

    public virtual DbSet<LichHen> LichHens { get; set; }

    public virtual DbSet<NhanVien> NhanViens { get; set; }

    public virtual DbSet<PhanQuyen> PhanQuyens { get; set; }

    public virtual DbSet<SanPham> SanPhams { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=HIROT\\HIROT;Initial Catalog=XuanTamDB;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChucNang>(entity =>
        {
            entity.ToTable("ChucNang");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.MaChucNang).HasMaxLength(50);
            entity.Property(e => e.TenChucNang).HasMaxLength(50);
        });

        modelBuilder.Entity<Cthd>(entity =>
        {
            entity.HasKey(e => new { e.MaLichHen, e.MaHoaDonDv }).HasName("PK_CTHD_1");

            entity.ToTable("CTHD");

            entity.Property(e => e.MaHoaDonDv).HasColumnName("MaHoaDonDV");
            entity.Property(e => e.MaGd)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("MaGD");
            entity.Property(e => e.NgayDat).HasColumnType("datetime");
            entity.Property(e => e.Pttt)
                .HasMaxLength(50)
                .HasColumnName("PTTT");
            entity.Property(e => e.Sdt)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("SDT");
            entity.Property(e => e.TenKh)
                .HasMaxLength(50)
                .HasColumnName("TenKH");
            entity.Property(e => e.TienThanhToan).HasColumnType("decimal(10, 0)");
            entity.Property(e => e.TinhTrangTt)
                .HasMaxLength(50)
                .HasColumnName("TinhTrangTT");

            entity.HasOne(d => d.MaHoaDonDvNavigation).WithMany(p => p.Cthds)
                .HasForeignKey(d => d.MaHoaDonDv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CTHD_HoaDonDichVu");

            entity.HasOne(d => d.MaLichHenNavigation).WithMany(p => p.Cthds)
                .HasForeignKey(d => d.MaLichHen)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CTHD_LichHen");
        });

        modelBuilder.Entity<CthdsanPham>(entity =>
        {
            entity.HasKey(e => e.MaCthdsanPham);

            entity.ToTable("CTHDSanPham");

            entity.Property(e => e.MaCthdsanPham).HasColumnName("MaCTHDSanPham");
            entity.Property(e => e.Gia).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.MaHoaDonSanPhamNavigation).WithMany(p => p.CthdsanPhams)
                .HasForeignKey(d => d.MaHoaDonSanPham)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CTHDSanPham_HoaDonSanPham");

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.CthdsanPhams)
                .HasForeignKey(d => d.MaSanPham)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CTHDSanPham_SanPham");
        });

        modelBuilder.Entity<DanhMucSanPham>(entity =>
        {
            entity.HasKey(e => e.MaDanhMuc);

            entity.ToTable("DanhMucSanPham");

            entity.Property(e => e.TenDanhMuc).HasMaxLength(50);
        });

        modelBuilder.Entity<DichVu>(entity =>
        {
            entity.HasKey(e => e.MaDichVu);

            entity.ToTable("DichVu");

            entity.Property(e => e.AnhDichVu).IsUnicode(false);
            entity.Property(e => e.TenDichVu).HasMaxLength(50);
        });

        modelBuilder.Entity<HoaDonDichVu>(entity =>
        {
            entity.HasKey(e => e.MaHoaDon);

            entity.ToTable("HoaDonDichVu");

            entity.Property(e => e.MaHoaDon).ValueGeneratedNever();
        });

        modelBuilder.Entity<HoaDonSanPham>(entity =>
        {
            entity.HasKey(e => e.MaHoaDon);

            entity.ToTable("HoaDonSanPham");

            entity.Property(e => e.NgayMua).HasColumnType("datetime");

            entity.HasOne(d => d.MaKhachHangNavigation).WithMany(p => p.HoaDonSanPhams)
                .HasForeignKey(d => d.MaKhachHang)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HoaDonSanPham_KhachHang");
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.MaKhachHang);

            entity.ToTable("KhachHang");

       
            entity.Property(e => e.HoTen).HasMaxLength(250);
            entity.Property(e => e.MatKhau)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Sdt)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SDT");
        });

        modelBuilder.Entity<LichHen>(entity =>
        {
            entity.HasKey(e => e.MaLichHen);

            entity.ToTable("LichHen");

            entity.Property(e => e.NgayHen).HasColumnType("datetime");

            entity.HasOne(d => d.MaDichVuNavigation).WithMany(p => p.LichHens)
                .HasForeignKey(d => d.MaDichVu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LichHen_DichVu");

            entity.HasOne(d => d.MaKhachHangNavigation).WithMany(p => p.LichHens)
                .HasForeignKey(d => d.MaKhachHang)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LichHen_KhachHang");

            entity.HasOne(d => d.MaNhanVienNavigation).WithMany(p => p.LichHens)
                .HasForeignKey(d => d.MaNhanVien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LichHen_NhanVien");
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.HasKey(e => e.MaNhanVien);

            entity.ToTable("NhanVien");

            entity.Property(e => e.AnhNhanVien).IsUnicode(false);
            entity.Property(e => e.DiaChi).HasMaxLength(250);
            entity.Property(e => e.Email)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.MatKhau)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Sdt)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SDT");
            entity.Property(e => e.TenNhanVien).HasMaxLength(250);
        });

        modelBuilder.Entity<PhanQuyen>(entity =>
        {
            entity.HasKey(e => new { e.IdChucNang, e.IdNhanVien });

            entity.ToTable("PhanQuyen");

            entity.Property(e => e.GhiNang).HasMaxLength(50);

            entity.HasOne(d => d.IdChucNangNavigation).WithMany(p => p.PhanQuyens)
                .HasForeignKey(d => d.IdChucNang)
                .HasConstraintName("FK_PhanQuyen_ChucNang");

            entity.HasOne(d => d.IdNhanVienNavigation).WithMany(p => p.PhanQuyens)
                .HasForeignKey(d => d.IdNhanVien)
                .HasConstraintName("FK_PhanQuyen_NhanVien");
        });

        modelBuilder.Entity<SanPham>(entity =>
        {
            entity.HasKey(e => e.MaSanPham);

            entity.ToTable("SanPham");

            entity.Property(e => e.HinhAnh).IsUnicode(false);
            entity.Property(e => e.TenSanPham).HasMaxLength(50);

            entity.HasOne(d => d.MaDanhMucNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.MaDanhMuc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SanPham_DanhMucSanPham");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
