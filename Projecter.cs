/********************
Mã sinh viên: 202419021
Họ tên: Phạm Đình Vũ
********************/

namespace lab_w05;

public class Projecter : Device
{
    // Thuộc Tính
    public int Lumens { get; set; } // Độ sáng
    public int LampHoursUsed { get; set; } // Số giờ sử dụng bóng đèn

    // Constructor - khởi tạo
    public Projecter(string deviceId, string name, int yearOfUse, decimal price, DeviceStatus status,
                     int lumens, int lampHoursUsed)
        : base(deviceId, name, yearOfUse, price, status)
    {
        Lumens = lumens;
        LampHoursUsed = lampHoursUsed;
    }
    // Phương thức tính chi phí bảo trì
    public override decimal CalculateAnnualMaintenanceCost()
    {
        decimal cost = Price * 0.03m; // 3% cơ bản
        if (LampHoursUsed > 3000)
            cost += 1500000;    // + 1500000 nếu đã sử dụng > 3000 giờ
        return cost;
    }

    public override string ToString()
    {
        return base.ToString() + $" | Máy chiếu: {Lumens} Lumens, Giờ bóng đèn: {LampHoursUsed:N0}h";
    }
}
