/********************
Mã sinh viên: 202419021
Họ tên: Phạm Đình Vũ
********************/

using lab_w05.Interfaces;

namespace lab_w05.Models;

public class NetworkPrinter : Printer, INetworkable
{
    // Thuộc tính từ INetworkable
    public string IpAddress { get; private set; } = string.Empty;
    public bool IsConnected { get; private set; }

    // Constructor: Lấy toàn bộ thông số máy in lên lớp cha Printer
    public NetworkPrinter(string deviceId, string name, int yearOfUse, decimal price, DeviceStatus status,
                          string printerType, int printedPagesCount, bool isColor)
        : base(deviceId, name, yearOfUse, price, status, printerType, printedPagesCount, isColor)
    {
        IsConnected = false;
        IpAddress = string.Empty;
    }

    // Thực thi các hành vi mạng từ INetworkable
    public void Connect(string ipAddress)
    {
        if (IsConnected)
            throw new InvalidOperationException($"Máy in [{DeviceId}] đang kết nối mạng, không thể kết nối lại.");

        if (string.IsNullOrWhiteSpace(ipAddress))
            throw new ArgumentException("Địa chỉ IP không được để trống.", nameof(ipAddress));

        IpAddress = ipAddress.Trim();
        IsConnected = true;
    }

    public void Disconnect()
    {
        IsConnected = false;
        IpAddress = string.Empty; // Xóa IP khi ngắt kết nối
    }

    // Ghi đè chuỗi
    public override string ToString()
    {
        string netInfo = IsConnected ? $"Đã kết nối ({IpAddress})" : "Chưa kết nối";
        // Tận dụng ToString() của Printer và bổ sung thông tin mạng
        return base.ToString() + $" | Mạng: Có hỗ trợ ({netInfo})";
    }
}