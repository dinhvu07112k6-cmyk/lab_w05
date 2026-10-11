/********************
Mã sinh viên: 202419021
Họ tên: Phạm Đình Vũ
********************/

using lab_w05.Interfaces;

namespace lab_w05.Models;

public class Computer : Device, INetworkable
{
    // 1.Thuộc tính Computer
    public int RamSize { get; set; } // Tính theo GB
    public string CpuType { get; set; } = string.Empty;
    public bool HasDiscreteGpu { get; set; }

    // 2.Thuộc tính từ INetworkable
    public string IpAddress { get; private set; } = string.Empty;
    public bool IsConnected { get; private set; }

    // 3.Constructor
    public Computer(string deviceId, string name, int yearOfUse, decimal price, DeviceStatus status,
                    int ramSize, string cpuType, bool hasDiscreteGpu)
        : base(deviceId, name, yearOfUse, price, status)
    {
        RamSize = ramSize;
        CpuType = cpuType;
        HasDiscreteGpu = hasDiscreteGpu;
        IsConnected = false;
        IpAddress = string.Empty;
    }

    // 4.Chi phí bảo trì hàng năm (Device)
    public override decimal CalculateAnnualMaintenanceCost()
    {
        decimal rate = 0.05m; // 5% cơ bản
        if (HasDiscreteGpu)
            rate += 0.02m;    // +2% nếu có GPU rời
        if (DateTime.Now.Year - YearOfUse > 5)
            rate += 0.01m;    // +1% nếu > 5 năm

        return Price * rate;
    }

    // 5.Thực thi các hành vi mạng từ INetworkable
    public void Connect(string ipAddress)
    {
        if (IsConnected)
            throw new InvalidOperationException($"Máy tính [{DeviceId}] đang kết nối mạng, không thể kết nối lại.");

        if (string.IsNullOrWhiteSpace(ipAddress))
            throw new ArgumentException("Địa chỉ IP không được để trống.", nameof(ipAddress));

        IpAddress = ipAddress.Trim();
        IsConnected = true;
    }

    public void Disconnect()
    {
        IsConnected = false;
        IpAddress = string.Empty; // Xóa IP hoạt động bằng chuỗi rỗng khi ngắt mạng
    }

    // 6.Ghi đè chuỗi
    public override string ToString()
    {
        string netInfo = IsConnected ? $"Đã kết nối ({IpAddress})" : "Chưa kết nối";
        return base.ToString() + $" | Máy tính: {CpuType}, {RamSize}GB RAM, GPU rời: {HasDiscreteGpu} | Mạng: {netInfo}";
    }
}