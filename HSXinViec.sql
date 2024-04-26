USE XinViec
GO

CREATE TABLE HoSoXinViec (
	tenHoSo nvarchar(100) NOT NULL,
	ngayCapNhat DateTime NOT NULL,
	viTriUngTuyen nvarchar(50) NOT NULL,
	mucTieuNgheNghiep nvarchar(1000) NOT NULL,
	kinhNghiemLamViec nvarchar(1000)NOT NULL,
	kyNang nvarchar(1000) NOT NULL,
	soThich nvarchar(1000) NOT NULL,
	EmailDangNhap varchar(50) NOT NULL,
	FOREIGN KEY (EmailDangNhap) REFERENCES UngVien(EmailDangNhap),
	PRIMARY KEY (tenHoSo, EmailDangNhap)

);
INSERT INTO HoSoXinViec (tenHoSo, NgayCapNhat, viTriUngTuyen, mucTieuNgheNghiep, 
						kinhNghiemLamViec, kyNang, soThich, EmailDangNhap)
VALUES 
	(N'Hồ sơ xin việc 1', '2024-3-21 00:00:00', N'Kế toán', N'Học hỏi nhiều hơn', N'Hoạt động trong câu lạc bộ của trường', N'Tự học tốt', N'Đàn, hát', 'Kiet@gmail.com'),
	(N'Hồ sơ xin việc 1', '2024-3-21 00:00:00', N'Kế toán', N'Học hỏi nhiều hơn', N'Hoạt động trong câu lạc bộ của trường', N'Tự học tốt', N'Đàn, hát', 'Anh@gmail.com')

