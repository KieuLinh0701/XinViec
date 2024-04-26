USE XinViec;
GO

CREATE TABLE ThongTinCTy(
	MaCongTy int PRIMARY KEY,
	Ten nvarchar(50),
	NgayThanhLap Date,
	QuyMo nvarchar(50),
	SDT varchar(11),
	Email varchar(50),
	DiaChi nvarchar(100),
	MoTa text,
);
GO
