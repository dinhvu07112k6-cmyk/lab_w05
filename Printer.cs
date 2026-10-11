/********************
Mã sinh viên: 202419021
Họ tên: Phạm Đình Vũ
********************/

namespace lab_w05;

public class Printer : Device
{
    // Thuộc Tính
    public string PrinterType { get; set; } // Ví dụ: Laser or Phun
    public int PrintedPagesCount { get; set; } // Tính theo trang đã in
    public bool IsColor { get; set; } // In màu hay không

    // Constructor - khởi tạo
    public Printer(string deviceId, string name, int yearOfUse, decimal price, DeviceStatus status,
                   string printerType, int printedPagesCount, bool isColor)
        : base(deviceId, name, yearOfUse, price, status)
    {
        PrinterType = printerType;
        PrintedPagesCount = printedPagesCount;
        IsColor = isColor;
    }

    // Phương thức tính chi phí bảo trì 
    public override decimal CalculateAnnualMaintenanceCost()
    {
        decimal cost = Price * 0.04m; // 4% cơ bản
        if (PrintedPagesCount > 100000)
            cost += 500000;    // + 500000 nếu in màu
        if (IsColor)
            cost += 300000;    // + 300000 nếu in màu
        return cost;
    }

    // Có hỗ trợ mạng hay không. ta sẽ giả sử rằng máy in này không hỗ trợ mạng, nên phương thức này sẽ trả về false.
    // Tách riêng ra một phương thức để kiểm tra hỗ trợ mạng, vì có thể trong tương lai sẽ có các loại máy in khác nhau.
    public override string ToString()
    {
        string colorInfo = IsColor ? "In màu" : "In đen trắng";
        return base.ToString() + $" | Máy in: {PrinterType}, {colorInfo}, Đã in: {PrintedPagesCount:N0} trang | Mạng: Không hỗ trợ";
    }
}

