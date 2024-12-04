# NT106P13LopLyThuyet
Repo dành cho môn LTMCB - lớp lý thuyết
- Thành phần GUI nằm trong folder project ClientSide.
- Thành phần Console Server nằm trong folder ConsoleServer, bind vào địa chỉ loopback "127.0.0.1".
- Để tạm thời "đi đường tắt" bỏ qua bước đăng kí tài khoản, có thể sử dụng Username là "11", để trống password.
- Phần lớn các static method thực hiện việc liên lạc thông qua giao thức TCP nằm trong class Station, và các static method tương tự qua HTTP Google Books API nằm trong class GoogleBooksWorker.
- Vì static method GoogleBooksWorker.GetUserCredentialAsync() đặt canceled time span hơi ngắn (29 giây) nên khi chạy thử, nếu đồng ý cho phép app truy cập kệ sách thì cần thao tác nhanh tay.
- ĐỪNG XÓA file ClientSide/bin/Debug/net8.0-windows/credentials.json , thiếu nó app không chạy được.
- Tính năng mail SMTP (Server.cs -> SendTemporaryPasswordEmail() ) cần có 1 địa chỉ Gmail và App Password.
