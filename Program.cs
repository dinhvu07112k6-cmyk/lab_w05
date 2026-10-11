/********************
Mã sinh viên: 202419021
Họ tên: Phạm Đình Vũ
********************/

using System;
using System.Collections.Generic;
using lab_w05.Interfaces;
using lab_w05.Models;

namespace lab_w05;

internal class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // 1. Khoi tao doi tuong theo de bai yeu cau
        // - 2 may tinh (1 may co GPU roi)
        Computer pc1 = new Computer("PC01", "Dell OptiPlex", 2024, 15000000m, DeviceStatus.Active, 16, "Core i5", false);
        Computer pc2 = new Computer("PC02", "HP Workstation", 2018, 28000000m, DeviceStatus.Active, 32, "Core i7", true); // dung > 5 nam, co GPU

        // - 2 may in (1 may in > 100.000 trang, 1 may in mang)
        Printer pr1 = new Printer("PR01", "Canon LBP 2900", 2022, 3500000m, DeviceStatus.Active, "Laser", 120000, false);
        NetworkPrinter pr2 = new NetworkPrinter("PR02", "Epson L8050", 2023, 7500000m, DeviceStatus.Active, "Phun", 20000, true);

        // - 1 may chieu (bong den > 3000 gio)
        Projector pj1 = new Projector("PJ01", "Sony VPL", 2021, 16000000m, DeviceStatus.UnderMaintenance, 3500, 3200);

        // - 2 phong thuc hanh
        LabRoom room1 = new LabRoom("LAB1", "Phong May Tinh 1", 40);
        LabRoom room2 = new LabRoom("LAB2", "Phong Da Phuong Tien", 30);

        // -----------------------------------------------------------------
        // THAO TAC 1: Them thiet bi vao phong
        // -----------------------------------------------------------------
        Console.WriteLine("--- 1. Them thiet bi vao cac phong ---");
        room1.AddDevice(pc1);
        room1.AddDevice(pr1);
        room1.AddDevice(pj1);

        room2.AddDevice(pc2);
        room2.AddDevice(pr2);
        Console.WriteLine($"Da them {room1.Devices.Count} thiet bi vao {room1.RoomName}");
        Console.WriteLine($"Da them {room2.Devices.Count} thiet bi vao {room2.RoomName}\n");

        // -----------------------------------------------------------------
        // THAO TAC 2: Thu them thiet bi bi trung ma
        // -----------------------------------------------------------------
        Console.WriteLine("--- 2. Thu them thiet bi trung ma PC01 vao LAB1 ---");
        try
        {
            Computer pcTrung = new Computer("PC01", "Dell Trung Ma", 2024, 12000000m, DeviceStatus.Active, 8, "Core i3", false);
            room1.AddDevice(pcTrung);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Bat loi thanh cong: {ex.Message}\n");
        }

        // -----------------------------------------------------------------
        // THAO TAC 3: In danh sach thiet bi trong tung phong
        // -----------------------------------------------------------------
        Console.WriteLine("--- 3. Danh sach thiet bi tung phong ---");
        Console.WriteLine($"[{room1.RoomId}] {room1.RoomName}:");
        foreach (var dev in room1.Devices)
        {
            Console.WriteLine($"  + {dev}");
        }

        Console.WriteLine($"[{room2.RoomId}] {room2.RoomName}:");
        foreach (var dev in room2.Devices)
        {
            Console.WriteLine($"  + {dev}");
        }
        Console.WriteLine();

        // -----------------------------------------------------------------
        // THAO TAC 4: Tinh tong chi phi bao tri moi phong (Da hinh)
        // -----------------------------------------------------------------
        Console.WriteLine("--- 4. Tong chi phi bao tri tung phong ---");
        Console.WriteLine($"Chi phi phong 1 ({room1.RoomName}): {room1.CalculateAnnualMaintenanceCost():N0} VND");
        Console.WriteLine($"Chi phi phong 2 ({room2.RoomName}): {room2.CalculateAnnualMaintenanceCost():N0} VND\n");

        // -----------------------------------------------------------------
        // THAO TAC 5: Liet ke thiet bi can bao tri
        // -----------------------------------------------------------------
        Console.WriteLine("--- 5. Cac thiet bi can bao tri (Trang thai dang bao tri hoac > 5 nam) ---");
        Console.WriteLine($"Phong {room1.RoomName}:");
        var listBt1 = room1.GetDevicesRequiringMaintenance();
        foreach (var dev in listBt1)
        {
            Console.WriteLine($"  * [{dev.DeviceId}] {dev.Name} - Nam SD: {dev.YearOfUse} - Trạng thái: {dev.Status}");
        }

        Console.WriteLine($"Phong {room2.RoomName}:");
        var listBt2 = room2.GetDevicesRequiringMaintenance();
        foreach (var dev in listBt2)
        {
            Console.WriteLine($"  * [{dev.DeviceId}] {dev.Name} - Nam SD: {dev.YearOfUse} - Trạng thái: {dev.Status}");
        }
        Console.WriteLine();

        // -----------------------------------------------------------------
        // THAO TAC 6 & 7: Ket noi mang va duyet qua kieu INetworkable
        // -----------------------------------------------------------------
        Console.WriteLine("--- 6 & 7. Ket noi va duyet danh sach thiet bi qua Interface INetworkable ---");

        // Tao danh sach gom cac thiet bi thuc thi INetworkable
        List<INetworkable> netDevices = new List<INetworkable>();
        netDevices.Add(pc1);
        netDevices.Add(pc2);
        netDevices.Add(pr2);

        // Ket noi mang
        pc1.Connect("192.168.1.10");
        pc2.Connect("192.168.1.11");
        pr2.Connect("192.168.1.20");

        // Duyet qua INetworkable, khong quan tam la Computer hay NetworkPrinter
        foreach (INetworkable nd in netDevices)
        {
            Console.WriteLine($"- Da ket noi: {nd.IsConnected} | Dia chi IP: {nd.IpAddress}");
        }

        // Test ngat ket noi
        Console.WriteLine("\nThu ngat ket noi may in PR02:");
        pr2.Disconnect();
        Console.WriteLine($"- PR02 sau khi ngat: IsConnected = {pr2.IsConnected}, IP = '{pr2.IpAddress}'");
    }
}