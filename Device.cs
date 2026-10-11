/********************
Mã sinh viên: 202419021
Họ tên: Phạm Đình Vũ
********************/
namespace lab_w05;

// Mã Thiết bị và Tên Thiết bị
public abstract class Device
{
    public string DeviceId { get; init; }             // init chỉ cho phép gán giá trị trong quá trình khởi tạo đối tượng, không thể thay đổi sau đó

    private string _name = string.Empty;
    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))

                throw new ArgumentException("Tên thiết bị không được để trống", nameof(value));
                _name = value.Trim();                   // .Trim() để loại bỏ khoảng trắng ở đầu và cuối chuỗi @@
           
        }
    }


    // Năm đưa vào sử dụng
    private int _yearOfUse;
    public int YearOfUse
     {
             get => _yearOfUse;
             set
        {
             if (value > DateTime.Now.Year)
                 throw new ArgumentOutOfRangeException(nameof(value), "Năm sử dụng không được lớn hơn năm hiện tại.");
             _yearOfUse = value;
        }
     }

    // Giá
             private decimal _price;
             public decimal Price
    {
        get => _price;
        set
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Giá mua phải lớn hơn 0.");
            _price = value;
        }
    }

    // Trạng thái thiết bị

    public DeviceStatus Status { get; set; }

    protected Device(string deviceId, string name, int yearOfUse, decimal price, DeviceStatus status)
    {
        if (string.IsNullOrWhiteSpace(deviceId))
            throw new ArgumentException("Mã thiết bị không được để trống.", nameof(deviceId));

        DeviceId = deviceId.Trim();
        Name = name;
        YearOfUse = yearOfUse;
        Price = price;
        Status = status;
    }

    // Phương thức trừu tượng: lớp con bắt buộc phải tự định nghĩa
    public abstract decimal CalculateAnnualMaintenanceCost();

    public override string ToString()
    {
        return $"[{DeviceId}] {Name} | Năm SD: {YearOfUse} | Giá: {Price:N0} VNĐ | Trạng thái: {Status}";
    }
}