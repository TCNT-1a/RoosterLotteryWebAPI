# Phần 1: Cài đặt môi trường

## 1 Tải source code

```shell
 git clone https://github.com/TCNTrading-lab/RoosterLottery.git
```

```shell
 git clone https://github.com/TCNTrading-lab/RoosterLotteryWebAPI.git
```

## 2 Tạo cơ sở dữ liệu

- Vào Project: RoosterLotteryWebAPI
- Tạo cơ sở dữ liệu
- Phục hồi lại với tên là RoosterLottery
  ![alt](Aspose.Words.76af8797-d44a-41eb-b6e8-2c545a2c5a82.001.png)

## 3 Cấu hình backend

![alt](Aspose.Words.76af8797-d44a-41eb-b6e8-2c545a2c5a82.002.png)

- **Port** backend, nếu không có thay đổi gì thì giử nguyên.
- Chú ý **connectionStrings: có thể giử nguyên nó không hoạt động**, do là thông số **Server = .\\SQLEXPRESS** có thể có giá trị khác nhau ở những máy tính khác nhau, tuỳ vào người cài đặt ban đầu. Cần điều chỉnh cho thích hợp.

![alt](Aspose.Words.76af8797-d44a-41eb-b6e8-2c545a2c5a82.003.png)

## 4 Khởi động Backend

- Vào địa chỉ này xem danh sách API: <http://localhost:5000/swagger/index.html>

## 5 Mở project RoosterLottery

- Config tại client cho biết địa chỉ API
- Port Sever

![alt](Aspose.Words.76af8797-d44a-41eb-b6e8-2c545a2c5a82.005.png)

- Sau đó khởi động phía **Project**

# Phần 2: Hướng dẫn sử dụng

## 1 Player đã có tồn tại trong CSDL

![alt](Aspose.Words.76af8797-d44a-41eb-b6e8-2c545a2c5a82.006.png)

Nhập vào một số điện thoại có tồn tại (1000000001->1000000015)

![alt](player.png)

Bạn có thể nhập vào số từ 0 đến 9 và bấm cược, app đã có validate giá trị cược từ 0 đến 9, nếu nhập số ngoài khoảng này thì app thông báo không cho cược.

Trong ván cược kéo dài 1 giờ, thì chỉ có thể cược 1 lần, sau khi cược thì không thể thay đổi. Sau khi cược rồi, cược nữa thì app báo lỗi.

## 2 Player không có trong CSDL

![alt](create-player.png)

- Nhập vào sđt 1000000016 - (Lưu ý cần nhập đúng định dạng số điện thoại)
- Nhập vào họ và tên
- Nhập vào ngày sinh
- Lúc này chức năng tạo user kích hoạt
- Nhập vào các thông tin user: họ và tên, ngày sinh, sau đó bấm Tạo

![alt](create-player1.png)

- Lúc này bạn có thể cược, khi bạn cược, bạn có thể xem con số mà các player khác cược chung ván với bạn

- Nhập vào 1 con số và bấm cược

![alt](cuoc-thanhcong.png)

- Bấm tìm để xem các player khác

![alt](tim-player.png)

- Cột **betID** đại diện cho ván

- Cột **betNumber** là đại diện con số mà user cược

- Cột **drawTime** là thời gian mở ván

- Cột **resultNumber** là con số hệ thống random

- Nếu mà **betNumber** bằng với **resultNumber** vào khoảng thời gian mở ván tiếp theo thì **isWinner** là true, ngược lại **isWinner** là false

# Phần 3 KIẾN TRÚC HỆ THỐNG

![alt](Aspose.Words.76af8797-d44a-41eb-b6e8-2c545a2c5a82.013.png)

![alt](Aspose.Words.76af8797-d44a-41eb-b6e8-2c545a2c5a82.014.png)

- Vì 1 ván cược kéo dài 1 giờ nên khó khăn trong việc test, nên có thể vào function **Fn_CaculateNextTime**, thay đổi giá trị **@NextTime = @NextMinute**

- **CRON schedule** là một chuổi dùng để cấu hình các tác vụ thực hiện có tính chất lặp đi lặp lại. Khái niệm này có ở nhiều ngôn ngữ lập trình.

- Store Procedure: **CreateInitialBet, PerformLotteryDraw, UpdatePlayerBetIsWinner** Được chạy ngầm theo CRON schedule

![alt](Aspose.Words.76af8797-d44a-41eb-b6e8-2c545a2c5a82.015.png)

- Bạn có thể sửa **CRON.H** thành **CRON.M** để giảm thời gian test app.

### Các Procedure còn lại được sử dụng để xữ lý nghiệp vụ ở API SERVER

![alt](api.png)

### Các API theo thứ tự từ trên xuống dưới có công dụng:

- Tìm Người chơi dựa theo SĐT
- Tạo Người chơi
- Cho phép người chơi cá cược
- Lấy thông tin các người chơi khác cược chung ván với mình.

### Về phần table để dùng để lưu trử thông tin

![alt](Aspose.Words.76af8797-d44a-41eb-b6e8-2c545a2c5a82.017.png)

- PLAYER

![alt](player1.png)

- BET

![alt](Aspose.Words.76af8797-d44a-41eb-b6e8-2c545a2c5a82.019.png)

- Phiên cuối cột ResultNumber thường là NULL do là phiên hiện tại chưa đến thời điểm sổ số

-PLAYER_BET

![alt](Aspose.Words.76af8797-d44a-41eb-b6e8-2c545a2c5a82.020.png)

- Cột **BetNumber** là cột mà player cược.

- Nếu cột **isWinner** là NULL là chưa tới phiên kiểm tra thắng thua, còn cột **isWinner** là 1 là thắng, 0 là thua.

# Phần 4 TRIỂN KHAI MONITORING

## **Metric 1**: Dùng để đánh giá hành vi người chơi, và thói quen người chơi, đánh giá được ứng dụng đang phát triển, hay suy giảm qua từng tuần, tháng , từ đó có được chiến lượt thay đổi.

- Số lượng người cược mổi ngày, số lượng trung bình của tháng, lượng vào cuối tuần
- Thống kê độ tuổi người chơi, người chơi đa số ở độ tuổi nào

## **Metric 2**: có thể tùy chỉnh sức mạnh phần cứng vào những khoảng thời gian khác nhau tối ưu hạ tầng động theo thời gian. Có thể giảm chi phí thuê cloud hoặc điện năng vận hành,…

- Khoảng thời gian nào thì có lượng user cao nhất, khoảng thời gian nào có lượng user thấp nhất, xem mức tải hệ thống.

## **Metric 2**: Tài chính

- Số lượng vé bán ra, số lượng người chơi trúng, tổng số người chơi, doanh thu, chi phí, số vé trung bình 1 ngày,…
- Winrate: % người chơi trúng thưởng. % người thua

# Phần 5 Một số bug (nếu còn thời gian sẻ sửa)

- Khi nhập số âm trong khi player cược thì bị set về 0
