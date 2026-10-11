/********************
Mã sinh viên: 202419021
Họ tên: Phạm Đình Vũ
********************/
namespace lab_w05;

public class Computer : Device
{
    public int RamSize { get; set; } // Tính theo GB
    public string CpuType { get; set; }
    public bool HasDiscreteGpu { get; set; }

    public Computer(string deviceId, string name, int yearOfUse, decimal price, DeviceStatus status,
                    int ramSize, string cpuType, bool hasDiscreteGpu)
        : base(deviceId, name, yearOfUse, price, status)
    {
        RamSize = ramSize;
        CpuType = cpuType;
        HasDiscreteGpu = hasDiscreteGpu;
    }

    public override decimal CalculateAnnualMaintenanceCost()
    {
        decimal rate = 0.05m; // 5% cơ bản
        if (HasDiscreteGpu)
            rate += 0.02m;    // 2% nếu có GPU rời
        if (DateTime.Now.Year - YearOfUse > 5)
            rate += 0.01m;    // 1% nếu > 5 năm

        return Price * rate;
    }

    public override string ToString()
    {
        return base.ToString() + $" | Máy tính: {CpuType}, {RamSize}GB RAM, GPU rời: {HasDiscreteGpu}";
    }
}