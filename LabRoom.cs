/********************
Mã sinh viên: 202419021
Họ tên: Phạm Đình Vũ
********************/

namespace lab_w05.Models;

public class LabRoom
{
    // 1.Thuộc tính 
    public string RoomId { get; private set; } = string.Empty;
    public string RoomName { get; set; } = string.Empty;
    public int Capacity { get; set; }

    // 2.Danh sách thiết bị kết tập (dùng List nội bộ, bên ngoài chỉ đọc qua IReadOnlyList để bảo vệ dữ liệu)
    private readonly List<Device> _devices = new();
    public IReadOnlyList<Device> Devices => _devices.AsReadOnly();

    // 3.Constructor 
    public LabRoom(string roomId, string roomName, int capacity)
    {
        if (string.IsNullOrWhiteSpace(roomId))
            throw new ArgumentException("Mã phòng không được để trống.", nameof(roomId));

        if (string.IsNullOrWhiteSpace(roomName))
            throw new ArgumentException("Tên phòng không được để trống.", nameof(roomName));

        if (capacity <= 0)
            throw new ArgumentOutOfRangeException(nameof(capacity), "Sức chứa của phòng > 0.");

        RoomId = roomId.Trim();
        RoomName = roomName.Trim();
        Capacity = capacity;
    }

    // 4.Thêm thiết bị vào phòng
    public void AddDevice(Device device)
    {
        // QT 2: Không chấp nhận đối tượng thiết bị null
        if (device == null)
            throw new ArgumentNullException(nameof(device), "Không thể thêm thiết bị rỗng vào phòng.");

        // QT 1: Không thêm hai thiết bị có cùng mã vào một phòng
        if (_devices.Any(d => d.DeviceId.Equals(device.DeviceId, StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationException($"Lỗi: Thiết bị có mã '{device.DeviceId}' đã tồn tại trong phòng {RoomId}.");

        _devices.Add(device);
    }

    // 5.Xóa thiết bị theo mã
    public bool RemoveDevice(string deviceId)
    {
        var device = FindDevice(deviceId);
        if (device != null)
        {
            return _devices.Remove(device);
        }
        return false;
    }

    // 6.Tìm kiếm thiết bị theo mã 
    public Device? FindDevice(string deviceId)
    {
        if (string.IsNullOrWhiteSpace(deviceId))
            return null;

        return _devices.FirstOrDefault(d => d.DeviceId.Equals(deviceId.Trim(), StringComparison.OrdinalIgnoreCase));
    }

    // 7 Tính tổng chi phí bảo trì dự kiến trong năm
    // QT 3: Tận dụng tính đa hình thông qua lớp cơ sở Device, tuyệt đối không dùng if/switch kiểm tra kiểu
    public decimal CalculateAnnualMaintenanceCost()
    {
        decimal totalCost = 0;
        foreach (var device in _devices)
        {
            // Runtime sẽ tự động phân giải và gọi hàm tính phí của đúng loại: Computer, Printer, hoặc Projector
            totalCost += device.CalculateAnnualMaintenanceCost();
        }
        return totalCost;
    }

    // 8. Lọc các thiết bị cần bảo trì
    // QT 4: Thiết bị đang ở trạng thái UnderMaintenance HOẶC đã sử dụng trên 5 năm
    public List<Device> GetDevicesRequiringMaintenance()
    {
        int currentYear = DateTime.Now.Year;
        return _devices
            .Where(d => d.Status == DeviceStatus.UnderMaintenance || (currentYear - d.YearOfUse > 5))
            .ToList();
    }

    public override string ToString()
    {
        return $"Phòng: {RoomName} [{RoomId}] | Sức chứa: {Capacity} chỗ | Số lượng thiết bị hiện có: {_devices.Count}";
    }
}