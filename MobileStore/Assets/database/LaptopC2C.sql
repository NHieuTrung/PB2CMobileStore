---DROP DATABASE MobileStore---
--CREATE DATABASE--
CREATE DATABASE MobileStore
GO
--
USE MobileStore
GO

--TABLE MEMBER TYPE--
CREATE TABLE MemberType
(
	MemberTypeId int IDENTITY(1,1) PRIMARY KEY,
	RoleMember nvarchar(120)
)

CREATE TABLE GenderType
(
	GenderTypeId int IDENTITY(1,1) PRIMARY KEY,
	GenderName nvarchar(120)
)

--TABLE STORE--
CREATE TABLE Store
(
	StoreId int IDENTITY(1,1) PRIMARY KEY,
	StoreName nvarchar(50) NOT NULL,
	StoreAddress nvarchar(100),
	PhoneStore varchar(15) NOT NULL,
	EmailStore varchar(50) NOT NULL
)

--TABLE EMPLOYEE--
CREATE TABLE Member
(
	MemberId int IDENTITY(1,1) PRIMARY KEY,
	MemberEmail varchar(100),
	MemberPassword nvarchar(50) NOT NULL,
	FullName nvarchar(120) NOT NULL,
	PhoneNumber varchar(15) NOT NULL,
	GenderTypeId int  FOREIGN KEY REFERENCES GenderType(GenderTypeId),
	Birthday date,
	HomeAddress nvarchar(200) NOT NULL,
	RegDate date NOT NULL, --Khách hàng: Ngày đăng ký account ,Nhân viên: Ngày đăng ký vào làm--
	FileImage varchar(100), --File hình ảnh nhân viên--
	StoreId int FOREIGN KEY REFERENCES Store(StoreId),
	MemberTypeId int FOREIGN KEY REFERENCES MemberType(MemberTypeId) --0: AD, 1:NV 2:KH--
)


--TABLE VENDOR--
CREATE TABLE Vendor
(
	VendorId int IDENTITY(1,1) PRIMARY KEY,
	VendorName nvarchar(100) NOT NULL,
	BranchAddress nvarchar(200) NOT NULL,
	PhoneNumber varchar(15) NOT NULL,
	Email varchar(100) NOT NULL
)

--TABLE BRANDTYPE--
CREATE TABLE BrandType
(
	BrandTypeId int IDENTITY(1,1) PRIMARY KEY,
	BrandName nvarchar(20)
)

--TABLE PRODUCTYPE--
CREATE TABLE ProductType
(
	ProductTypeId int IDENTITY(1,1) PRIMARY KEY,
	ProductTypeName nvarchar(120)
)

--TABLE PRODUCT--
CREATE TABLE Product
(
	ProductId int IDENTITY(1,1) PRIMARY KEY,
	ProductName nvarchar(50) NOT NULL,
	FileImage varchar(100),
	BrandTypeId int FOREIGN KEY REFERENCES BrandType(BrandTypeId),
	ProductTypeId int FOREIGN KEY REFERENCES ProductType(ProductTypeId),
	RealeaseDate date
)

--TABLE PRODUCT_SMARTPHONE--
CREATE TABLE ProductPortableDevice
(
	ProductPortableDeviceId int IDENTITY(1,1) PRIMARY KEY,
	ProductId int FOREIGN KEY REFERENCES Product(ProductId),
	Color varchar(20) NOT NULL,
	MemorySize int NOT NULL,
	RamSize int NOT NULL,
	Display nvarchar(80) NOT NULL,
	Price money,
	FileImage varchar(100),
	Quantiny int NOT NULL
)

--TABLE STOREIMEI--
CREATE TABLE StoreImei
(
	ImeiPhone varchar(100) PRIMARY KEY,
	ProductPortableDeviceId int FOREIGN KEY REFERENCES ProductPortableDevice(ProductPortableDeviceId),
	DateActiveWarranty date,
	StatusPhone int NOT NULL --0: Chua ban, 1: Da ban--
)

--TABLE PRODUCT_ACCESSORIES--
CREATE TABLE ProductAccessory
(
	ProductAccessoryId int IDENTITY(1,1) PRIMARY KEY,
	ProductId int FOREIGN KEY REFERENCES Product(ProductId),
	Color varchar(20) NOT NULL,
	Price money,
	ImageFile varchar(100),
	Quantiny int NOT NULL
)

--TABLE PROMOTION--
CREATE TABLE Promotion
(
	PromotionId int IDENTITY(1,1) PRIMARY KEY,
	PromotionCode varchar(10) UNIQUE, --Mã giảm giá--
	Discount int
)

--TABLE ORDER PRODUCT--
CREATE TABLE ShoppingCart
(
	ShoppingCartId int IDENTITY(1,1) PRIMARY KEY,
	CustomerId int REFERENCES Member(MemberId),
	OrderDate datetime NOT NULL,
	ShippedDate date,
	EmployeeId int REFERENCES Member(MemberId),
	PromotionId int REFERENCES Promotion(PromotionId),
	PaymentType nvarchar(100),
	StatusCart int --0:Chưa thanh toán/1:Đã thanh toán/2:Đang giao hàng/3:Đã huỷ--
)

--TABLE ORDER DETAIL--
CREATE TABLE ShoppingCartItem
(
	ShoppingCartItemId int IDENTITY(1,1) PRIMARY KEY,
	ShoppingCartId int FOREIGN KEY REFERENCES ShoppingCart(ShoppingCartId),
	ProductPortableDeviceId int REFERENCES ProductPortableDevice(ProductPortableDeviceId),
	ProductAccessoryId int REFERENCES ProductAccessory(ProductAccessoryId),
	Quantiny int,
	Note nvarchar(200),
	Discount int,
	Price money
)

--TABLE IMPORT--
CREATE TABLE Import
(
	ImportId int IDENTITY(1,1) PRIMARY KEY,
	VendorId int FOREIGN KEY REFERENCES Vendor(VendorId),
	ImportDate datetime,
	EmployeeId int REFERENCES Member(MemberId),
	Note nvarchar(200)
)

--TABLE IMPORT DETAIL--
CREATE TABLE ImportItem
(
	ImportItemId int IDENTITY(1,1) PRIMARY KEY,
	ImportId int REFERENCES Import(ImportId),
	ProductPortableDeviceId int REFERENCES ProductPortableDevice(ProductPortableDeviceId),
	ProductAccessoriesId int REFERENCES ProductAccessory(ProductAccessoryId),
	Quantiny int NOT NULL,
	Price money NOT NULL
)

----------------------------INSERT GENDERTYPE---------------------------------------
INSERT into GenderType(GenderName)
VALUES (N'Nam'),
	   (N'Nữ'),
	   (N'Không muốn nói')

----------------------------INSERT MEMBERTYPE---------------------------------------
INSERT into MemberType(RoleMember)
VALUES (N'Admin'),
	   (N'Nhân viên'),
	   (N'Khách hàng')

----------------------------INSERT COMPANY---------------------------------------
INSERT into Store (StoreName,StoreAddress,PhoneStore,EmailStore)
VALUES (N'TNT Lý Tự Trọng',N'65 Lý Tự Trọng, P.Bến Nghé, Q.1, Hồ Chí Minh, Việt Nam','01222456789','tntlttservice@gmail.com'),
	   ( N'TNT Nguyễn Trãi',N'265 Nguyễn Trãi, P.Nguyễn Cư Trinh, Q.1, Hồ Chí Minh, Việt Nam','01222635478','tntntservice@gmail.com'),
	   (N'TNT Trần Hưng Đạo',N'462 Trần Hưng Đạo, P.2, Q.5, Hồ Chí Minh, Việt Nam','01222965326','tntthdservice@gmail.com'),
	   (N'TNT Điện Biên Phủ',N'643 Điện Biên Phủ, P.1, Q.3, Hồ Chí Minh, Việt Nam','01222412578','tntdbpservice@gmail.com'),
	   (N'TNT Ba Tháng Hai',N'349 Ba Tháng Hai, P.14, Q.10, Hồ Chí Minh, Việt Nam','01222326549','tntbthservice@gmail.com')

----------------------------INSERT MEMBERTYPE---------------------------------------
INSERT into Member(FullName,MemberEmail,MemberPassword,PhoneNumber,HomeAddress,StoreId,GenderTypeId,FileImage,Birthday,RegDate,MemberTypeId)
VALUES (N'Nguyễn Hiếu Trung','nguyenhieutrung@gmail.com','c3b18cd75e6c3f1669f3a2a32ce8f5e2','01686501020','1300 Phạm Thế Hiển P5 Q8',1,1,'nguyenhieutrung.png','1997-07-07','2018-07-20',1),

	   (N'Nguyễn Ánh','nguyenanh@gmail.com','bb602d34f5634a1322c5fb5e9465a3db','098756478','130 An Dương Vương P6 Q5',1,2,'nguyenanh.png','1997-6-6','2018-2-7',2),
	   (N'Châu Tuyết','chautuyet@gmail.com','d3a7d49ee31def9d72a482ccdc3890b8','012547896','23 Hồng Bàng P5 Q5',1,2,'chautuyet.png','1995-5-5','2018-2-7',2),

	   (N'Châu Khoa','chaukhoa@gmail.com','d3a7d49ee31def9d72a482ccdc3890b8','0245789545','23 Hồng Bàng P5 Q5',1,1,'chautuyet.png','1994-5-15','2018-2-7',3),
	   (N'Nguyễn Thùy','nguyenthuy@gmail.com','5a6a358db34b11ca7b1c0ea4697b9a1f','01456987','25 Hồng Bàng P5 Q5',1,2,'nguyenthuy.png','1995-2-15','2018-2-7',3)


----------------------------INSERT VENDOR---------------------------------------
INSERT into Vendor(VendorName,BranchAddress,PhoneNumber,Email)
	VALUES( N'FPT Trading', N'	Tòa nhà FPT, Lô L.29b-31b-33b, đường Tân Thuận, KCX Tân Thuận, Phường Tân Thuận Đông, Quận 7, TP Hồ Chí Minh', '08.7300 6666', 'fpt_trading@fpt.com.vn'),
		  ( N'Digiworld', N'201-203, CMT8, P.4, Q.3, Hồ Chí Minh, Việt Nam', '028.3929 0059', 'comms@dgw.com.vn'),
		  (N'Petrosetco','Lầu 6, Tòa nhà PetroVietNam, Số 1 - 5 Lê Duẩn, P. Bến Nghé, Quận 1, TP. Hồ Chí Minh','(028) 3911 7777','info@petrosetco.com.vn')


----------------------------INSERT PRODUCTTYPE---------------------------------------
INSERT into ProductType(ProductTypeName)
VALUES('Smartphone'),
	  ('Accessory')

----------------------------INSERT BRANDTYPE---------------------------------------
INSERT into BrandType(BrandName)
VALUES('Apple'),
	  ('Samsung'),
	  ('Sony'),
	  ('Oppo'),
	  ('Xiaomi'),
	  ('Huawei'),
	  ('Motorola'),
	  ('Nokia')

----------------------------INSERT PRODUCT/Phone---------------------------------------
--Apple--
INSERT into Product(ProductName,BrandTypeId,FileImage,RealeaseDate,ProductTypeId)
VALUES('Iphone X',1,'iphonex.png','2017-11-03',1),
	  ('Iphone 8',1,'iphone8.png','2017-09-15',1),
	  ('Iphone 8 Plus',1,'iphone8p.png','2017-09-15',1)
--Samsung--
INSERT into Product(ProductName,BrandTypeId,FileImage,RealeaseDate,ProductTypeId)
VALUES('Galaxy S8',2,'sss8.png','2017-04-04',1),
	  ('Galaxy S8 Plus',2,'sss8plus.png','2017-04-04',1),
	  ('Galaxy S9',2,'sss9.png','2018-03-16',1),
	  ('Galaxy S9 Plus',2,'sss9plus.png','2018-03-16',1),
	  ('Galaxy Note 8',2,'ssn8.png','2018-09-15',1)
--Sony--
INSERT into Product(ProductName,BrandTypeId,FileImage,RealeaseDate,ProductTypeId)
VALUES('Xperia XZ1',3,'snxz1.png','2017-09-19',1),
	  ('Xperia XZ2',3,'snxz2.png','2018-04-01',1),
	  ('Xperia XZ Premium',3,'snxzpre.png','2017-04-01',1),
	  ('Xperia XZS',3,'snxzs.png','2017-05-22',1),
	  ('Xperia XA2',3,'snxa2.png','2018-02-01',1)
--select * from Product	  
----------------------------INSERT PRODUCT SMARTPHONE---------------------------------------
--Apple--
INSERT into ProductPortableDevice(ProductId, Color, MemorySize, RamSize, Display, Price, FileImage, Quantiny)
	VALUES(1,'Silver', 64, 3, N'5.8 inch', 29990000, 'iphonex_silver.png', 5),
		  (1,'Black', 64, 3, N'5.8 inch', 29990000, 'iphonex_black.png', 4),
		  (1,'Silver', 256, 3, N'5.8 inch', 34790000, 'iphonex_silver.png', 6),
		  (1,'Black', 256, 3, N'5.8 inch', 34790000, 'iphonex_black.png', 8)

INSERT into ProductPortableDevice(ProductId, Color, MemorySize, RamSize, Display, Price, FileImage, Quantiny)
	VALUES(2,'Silver', 64, 2, N'4.7 inch', 20990000, 'iphone8_silver.png', 2),
		  (2,'Black', 64, 2, N'4.7 inch', 20990000, 'iphone8_black.png', 1),
		  (2,'Copper', 64, 2, N'4.7 inch', 20990000, 'iphone8_copper.png', 2),
		  (2,'Silver', 256, 2, N'4.7 inch', 25790000, 'iphone8_silver.png', 2),
		  (2,'Black', 256, 2, N'4.7 inch', 25790000, 'iphone8_black.png', 2),
		  (2,'Copper', 256, 2, N'4.7 inch', 25790000, 'iphone8_copper.png', 2)

INSERT into ProductPortableDevice(ProductId, Color, MemorySize, RamSize, Display, Price, FileImage, Quantiny)
	VALUES(3,'Copper', 64, 3, N'5.5 inch', 23990000, 'iphone8p_copper.png', 1),
		  (3,'Black', 64, 3, N'5.5 inch', 23990000, 'iphone8p_black.png', 1),
		  (3,'Copper', 256, 3, N'5.5 inch', 28790000, 'iphone8p_copper.png', 3),
		  (3,'Black', 256, 3, N'5.5 inch', 28790000, 'iphone8p_black.png', 2)

--Samsung--
INSERT into ProductPortableDevice( ProductId, Color, MemorySize, RamSize, Display, Price, FileImage, Quantiny)
VALUES(4,'Black',64,4,N'5.8 inch',15990000, 'sss8_black.png',8),
	  (4,'Blue',64,4,N'5.8 inch',15990000, 'sss8_blue.png',8)

INSERT into ProductPortableDevice(ProductId, Color, MemorySize, RamSize, Display, Price, FileImage, Quantiny)
VALUES(5,'Black',64,4,N'6.2 inch',17990000,'sss8plus_black.png',6),
	  (5,'Blue',64,4,N'6.2 inch',17990000,'sss8plus_blue.png',6),
	  (5,'Purple',64,4,N'6.2 inch',17990000,'sss8plus_purple.png',6)

INSERT into ProductPortableDevice(ProductId, Color, MemorySize, RamSize, Display, Price, FileImage, Quantiny)
VALUES(6,'Black',64,4,N'5.8 inch',19990000,'sss9_black.png',8),
	  (6,'Purple',64,4,N'5.8 inch',19990000,'sss9_purple.png',8)
INSERT into ProductPortableDevice(ProductId, Color, MemorySize, RamSize, Display, Price, FileImage, Quantiny)
VALUES(7,'Black',64,4,N'6.2 inch',23490000,'sss9plus_black.png',6),
	  (7,'Purple',64,4,N'6.2 inch',23490000,'sss9plus_purple.png',6)
INSERT into ProductPortableDevice(ProductId, Color, MemorySize, RamSize, Display, Price, FileImage, Quantiny)
VALUES(7,'Black',128,4,N'6.2 inch',24490000,'sss9plus_black.png',5)

INSERT into ProductPortableDevice(ProductId, Color, MemorySize, RamSize, Display, Price, FileImage, Quantiny)
VALUES(8,'Black',64,6,N'6.3 inch',22490000,'ssn8_black.png',7),
	  (8,'Gold',64,6,N'6.3 inch',22490000,'ssn8_gold.png',7) --select * from  ProductPortableDevice inner join Product on ProductPortableDevice.ProductId=Product.ProductId--
--Sony--
INSERT into ProductPortableDevice(ProductId, Color, MemorySize, RamSize, Display, Price, FileImage, Quantiny)
VALUES(9,'Black', 64, 4, N'5.2 inch', 13990000, 'snxz1_black.png', 8),
	  (9,'Silver', 64, 4, N'5.2 inch', 13990000, 'snxz1_silver.png', 9),
	  (9,'Blue', 64, 4, N'5.2 inch', 13990000,'snxz1_blue.png',  5),
	  (9,'Pink', 64, 4, N'5.2 inch', 13990000, 'snxz1_pink.png', 2)

INSERT into ProductPortableDevice(ProductId, Color, MemorySize, RamSize, Display, Price, FileImage, Quantiny)
VALUES(10,'Black', 64, 4, N'5.7 inch', 17000000,'snxz2_black.png',  8),
	  (10,'Silver', 64, 4, N'5.7 inch', 17000000,'snxz2_silver.png',  3),
	  (10,'Green', 64, 4, N'5.7 inch', 17000000,'snxz2_green.png',  4),
	  (10,'Pink', 64, 4, N'5.7 inch', 17000000,'snxz2_pink.png',  2)

INSERT into ProductPortableDevice(ProductId, Color, MemorySize, RamSize, Display, Price, FileImage, Quantiny)
VALUES(11,'Black', 64, 4, N'5.5 inch', 15990000, 'snxzpre_black.png', 9),
	  (11,'Pink', 64, 4, N'5.5 inch', 15990000, 'snxzpre_pink.png', 2),
	  (11,'Red', 64, 4, N'5.5 inch', 15990000, 'snxzpre_red.png', 1),
	  (11,'Chrome', 64, 4, N'5.5 inch', 15990000, 'snxzpre_chrome.png', 6)

INSERT into ProductPortableDevice(ProductId, Color, MemorySize, RamSize, Display, Price, FileImage, Quantiny)
VALUES(12,'Black', 64, 4, N'5.2 inch', 9990000, 'snxzs_black.png', 7),
	  (12,'Silver', 64, 4, N'5.2 inch', 9990000, 'snxzs_silver.png', 3),
	  (12,'Blue', 64, 4, N'5.2 inch', 9990000, 'snxzs_blue.png', 8)

INSERT into ProductPortableDevice(ProductId, Color, MemorySize, RamSize, Display, Price, FileImage, Quantiny)
VALUES(13,'Black', 32, 3, N'5.2 inch', 6590000, 'snxa2_black.png', 5),
	  (13,'Silver', 32, 3, N'5.2 inch', 6590000, 'snxa2_silver.png', 7),
	  (13,'Blue', 32, 3, N'5.2 inch', 6590000, 'snxa2_blue.png', 6),
	  (13,'Pink', 32, 3, N'5.2 inch', 6590000, 'snxa2_pink.png', 5)

--------------------------INSERT STORE IMEI------------------------------------------
--Samsung--
INSERT into StoreImei(ImeiPhone,ProductPortableDeviceId,StatusPhone)
VALUES('SSS810001B',15,1),
	  ('SSS810002B',15,1),
	  ('SSS810003B',15,1),
	  ('SSS810004B',15,1),
	  ('SSS810005B',15,1),
	  ('SSS810006B',15,1),
	  ('SSS810007B',15,1),
	  ('SSS810008B',15,1),
	  ('SSS810001BL',16,1),
	  ('SSS810002BL',16,1),
	  ('SSS810003BL',16,1),
	  ('SSS810004BL',16,1),
	  ('SSS810005BL',16,1),
	  ('SSS810006BL',16,1),
	  ('SSS810007BL',16,1),
	  ('SSS810008BL',16,1)

INSERT into StoreImei(ImeiPhone,ProductPortableDeviceId,StatusPhone)
VALUES('SSS8+10001B',17,1),
	  ('SSS8+10002B',17,1),
	  ('SSS8+10003B',17,1),
	  ('SSS8+10004B',17,1),
	  ('SSS8+10005B',17,1),
	  ('SSS8+10006B',17,1),
	  ('SSS8+10001BL',18,1),
	  ('SSS8+10002BL',18,1),
	  ('SSS8+10003BL',18,1),
	  ('SSS8+10004BL',18,1),
	  ('SSS8+10005BL',18,1),
	  ('SSS8+10006BL',18,1),
	  ('SSS8+10001P',19,1),
	  ('SSS8+10002P',19,1),
	  ('SSS8+10003P',19,1),
	  ('SSS8+10004P',19,1),
	  ('SSS8+10005P',19,1),
	  ('SSS8+10006P',19,1)

INSERT into StoreImei(ImeiPhone,ProductPortableDeviceId,StatusPhone)
VALUES('SSS910001B',20,1),
	  ('SSS910002B',20,1),
	  ('SSS910003B',20,1),
	  ('SSS910004B',20,1),
	  ('SSS910005B',20,1),
	  ('SSS910006B',20,1),
	  ('SSS910007B',20,1),
	  ('SSS910008B',20,1)

INSERT into StoreImei(ImeiPhone,ProductPortableDeviceId,StatusPhone)
VALUES('SSS910001P',21,1),
	  ('SSS910002P',21,1),
	  ('SSS910003P',21,1),
	  ('SSS910004P',21,1),
	  ('SSS910005P',21,1),
	  ('SSS910006P',21,1),
	  ('SSS910007P',21,1),
	  ('SSS910008P',21,1)

INSERT into StoreImei(ImeiPhone,ProductPortableDeviceId,StatusPhone)
VALUES('SSS9+10001B',22,1),
	  ('SSS9+10002B',22,1),
	  ('SSS9+10003B',22,1),
	  ('SSS9+10004B',22,1),
	  ('SSS9+10005B',22,1),
	  ('SSS9+10006B',22,1),
	  ('SSS9+10007B',22,1),
	  ('SSS9+10008B',22,1)

INSERT into StoreImei(ImeiPhone,ProductPortableDeviceId,StatusPhone)
VALUES('SSS9+10001P',23,1),
	  ('SSS9+10002P',23,1),
	  ('SSS9+10003P',23,1),
	  ('SSS9+10004P',23,1),
	  ('SSS9+10005P',23,1),
	  ('SSS9+10006P',23,1),
	  ('SSS9+10007P',23,1),
	  ('SSS9+10008P',23,1)

INSERT into StoreImei(ImeiPhone,ProductPortableDeviceId,StatusPhone)
VALUES('SSS9+(128)10001B',22,1),
	  ('SSS9+(128)10002B',22,1),
	  ('SSS9+(128)10003B',22,1),
	  ('SSS9+(128)10004B',22,1),
	  ('SSS9+(128)10005B',22,1),
	  ('SSS9+(128)10006B',22,1),
	  ('SSS9+(128)10007B',22,1)

INSERT into StoreImei(ImeiPhone,ProductPortableDeviceId,StatusPhone)
VALUES('SSN810001B',25,1),
	  ('SSN810002B',25,1),
	  ('SSN810003B',25,1),
	  ('SSN810004B',25,1),
	  ('SSN810005B',25,1),
	  ('SSN810006B',25,1),
	  ('SSN810007B',25,1),
	  ('SSN810008B',25,1)

INSERT into StoreImei(ImeiPhone,ProductPortableDeviceId,StatusPhone)
VALUES('SSN810001G',26,1),
	  ('SSN810002G',26,1),
	  ('SSN810003G',26,1),
	  ('SSN810004G',26,1),
	  ('SSN810005G',26,1),
	  ('SSN810006G',26,1),
	  ('SSN810007G',26,1),
	  ('SSN810008G',26,1)
--Sony--
INSERT into StoreImei(ImeiPhone,ProductPortableDeviceId,StatusPhone)
VALUES('SNXZ2BK000001',31, 1),
	  ('SNXZ2BK000002',31, 1),
	  ('SNXZ2BK000003',31, 1),
	  ('SNXZ2BK000004',31, 1),
	  ('SNXZ2BK000005',31, 1),
	  ('SNXZ2BK000006',31, 1),
	  ('SNXZ2BK000007',31, 1),
	  ('SNXZ2BK000008',31, 1),
	  ('SNXZ2SR000001',32, 1),
	  ('SNXZ2SR000002',32, 1),
	  ('SNXZ2SR000003',32, 1),
	  ('SNXZ2GN000001',33, 1),
	  ('SNXZ2GN000002',33, 1),
	  ('SNXZ2GN000003',33, 1),
	  ('SNXZ2GN000004',33, 1),
	  ('SNXZ2PK000001',34, 1),
	  ('SNXZ2PK000002',34, 1)

INSERT into StoreImei(ImeiPhone,ProductPortableDeviceId,StatusPhone)
VALUES('SNXZ1BK000001',27, 1),
	  ('SNXZ1BK000002',27, 1),
	  ('SNXZ1BK000003',27, 1),
	  ('SNXZ1BK000004',27, 1),
	  ('SNXZ1BK000005',27, 1),
	  ('SNXZ1BK000006',27, 1),
	  ('SNXZ1BK000007',27, 1),
	  ('SNXZ1BK000008',27, 1),
	  ('SNXZ1SR000001',28, 1),
	  ('SNXZ1SR000002',28, 1),
	  ('SNXZ1SR000003',28, 1),
	  ('SNXZ1SR000004',28, 1),
	  ('SNXZ1SR000005',28, 1),
	  ('SNXZ1SR000006',28, 1),
	  ('SNXZ1SR000007',28, 1),
	  ('SNXZ1SR000008',28, 1),
	  ('SNXZ1SR000009',28, 1),
	  ('SNXZ1BE000001',29, 1),
	  ('SNXZ1BE000002',29, 1),
	  ('SNXZ1BE000003',29, 1),
	  ('SNXZ1BE000004',29, 1),
	  ('SNXZ1BE000005',29, 1),
	  ('SNXZ1PK000001',30, 1),
	  ('SNXZ1PK000002',30, 1)

INSERT into StoreImei(ImeiPhone,ProductPortableDeviceId,StatusPhone)
VALUES('SNXZSBK000001',35, 1),
	  ('SNXZSBK000002',35, 1),
	  ('SNXZSBK000003',35, 1),
	  ('SNXZSBK000004',35, 1),
	  ('SNXZSBK000005',35, 1),
	  ('SNXZSBK000006',35, 1),
	  ('SNXZSBK000007',35, 1),
	  ('SNXZSSR000001',36, 1),
	  ('SNXZSSR000002',36, 1),
	  ('SNXZSSR000003',36, 1),
	  ('SNXZSBE000001',37, 1),
	  ('SNXZSBE000002',37, 1),
	  ('SNXZSBE000003',37, 1),
	  ('SNXZSBE000004',37, 1),
	  ('SNXZSBE000005',37, 1),
	  ('SNXZSBE000006',37, 1),
	  ('SNXZSBE000007',37, 1),
	  ('SNXZSBE000008',37, 1)

INSERT into StoreImei(ImeiPhone,ProductPortableDeviceId,StatusPhone)
VALUES('SNXZPREBK000001',38, 1),
	  ('SNXZPREBK000002',38, 1),
	  ('SNXZPREBK000003',38, 1),
	  ('SNXZPREBK000004',38, 1),
	  ('SNXZPREBK000005',38, 1),
	  ('SNXZPREBK000006',38, 1),
	  ('SNXZPREBK000007',38, 1),
	  ('SNXZPREBK000008',38, 1),
	  ('SNXZPREBK000009',38, 1),
	  ('SNXZPREPK000001',39, 1),
	  ('SNXZPREPK000002',39, 1),
	  ('SNXZPRERD000001',40, 1),
	  ('SNXZPRECE000001',41, 1),
	  ('SNXZPRECE000002',41, 1),
	  ('SNXZPRECE000003',41, 1),
	  ('SNXZPRECE000004',41, 1),
	  ('SNXZPRECE000005',41, 1),
	  ('SNXZPRECE000006',41, 1)

INSERT into StoreImei(ImeiPhone,ProductPortableDeviceId,StatusPhone)
VALUES('SNXA2BK000001',42, 1),
	  ('SNXA2BK000002',42, 1),
	  ('SNXA2BK000003',42, 1),
	  ('SNXA2BK000004',42, 1),
	  ('SNXA2BK000005',42, 1),
	  ('SNXA2SR000001',43, 1),
	  ('SNXA2SR000002',43, 1),
	  ('SNXA2SR000003',43, 1),
	  ('SNXA2SR000004',43, 1),
	  ('SNXA2SR000005',43, 1),
	  ('SNXA2SR000006',43, 1),
	  ('SNXA2SR000007',43, 1),
	  ('SNXA2BE000001',44, 1),
	  ('SNXA2BE000002',44, 1),
	  ('SNXA2BE000003',44, 1),
	  ('SNXA2BE000004',44, 1),
	  ('SNXA2BE000005',44, 1),
	  ('SNXA2BE000006',44, 1),
	  ('SNXA2PK000001',45, 1),
	  ('SNXA2PK000002',45, 1),
	  ('SNXA2PK000003',45, 1),
	  ('SNXA2PK000004',45, 1),
	  ('SNXA2PK000005',45, 1)
--Apple--
INSERT into StoreImei(ImeiPhone,ProductPortableDeviceId,StatusPhone)
	VALUES('APIXSR64000001',1, 1),
		  ('APIXSR64000002',1, 1),
		  ('APIXSR64000003',1, 1),
		  ('APIXSR64000004',1, 1),
		  ('APIXSR64000005',1, 1),
		  ('APIXGY64000001',2, 1),
		  ('APIXGY64000002',2, 1),
		  ('APIXGY64000003',2, 1),
		  ('APIXGY64000004',2, 1),
		  ('APIXSR256000001',3, 1),
		  ('APIXSR256000002',3, 1),
		  ('APIXSR256000003',3, 1),
		  ('APIXSR256000004',3, 1),
		  ('APIXSR256000005',3, 1),
		  ('APIXSR256000006',3, 1),
		  ('APIXGY256000001',4, 1),
		  ('APIXGY256000002',4, 1),
		  ('APIXGY256000003',4, 1),
		  ('APIXGY256000004',4, 1),
		  ('APIXGY256000005',4, 1),
		  ('APIXGY256000006',4, 1),
		  ('APIXGY256000007',4, 1),
		  ('APIXGY256000008',4, 1)

INSERT into StoreImei(ImeiPhone,ProductPortableDeviceId,StatusPhone)
	VALUES('API8SR64000001',5, 1),
		  ('API8SR64000002',5, 1),
		  ('API8GY64000001',6, 1),
		  ('API8CR64000001',7, 1),
		  ('API8CR64000002',7, 1),
		  ('API8SR256000001',8, 1),
		  ('API8SR256000002',8, 1),
		  ('API8GY256000001',9, 1),
		  ('API8GY256000002',9, 1),
		  ('API8CR256000001',10, 1),
		  ('API8CR256000002',10, 1)

INSERT into StoreImei(ImeiPhone,ProductPortableDeviceId,StatusPhone)
	VALUES('API8PCR64000001',11, 1),
		  ('API8PGY64000001',12, 1),
		  ('API8PCR256000001',13, 1),
		  ('API8PCR256000002',13, 1),
		  ('API8PCR256000003',13, 1),
		  ('API8PGY256000001',14, 1),
		  ('API8PGY256000002',14, 1)

----------------------------INSERT PRODUCT/Acessories---------------------------------------
---Samsung---
INSERT into Product(ProductName,ProductTypeId,FileImage,BrandTypeId,RealeaseDate)
	VALUES('Tai nghe Mono Bluetooth',2,'hpmonoblt.png',2,'2014/11/02'),
		  ('Tai nghe có dây EG920',2,'hpeg920.png',2,'2015/11/04'),
		  ('Tai nghe có dây IG935',2,'hpig935.png',2,'2016/05/01'),
		  ('Tai nghe Level U Pro',2,'hplevelupro.png',2,'2016/09/01'),
		  ('Tai nghe AKG S8',2,'hpakgs8.png',2,'2017/04/28'),
		  ('Loa Bluetooth Scoop',2,'spscoop.png',2,'2016/09/21'),
		  ('Loa Bluetooth Bottle',2,'spbottle.png',2,'2017/01/01')

--Sony--
INSERT into Product(ProductName,ProductTypeId,FileImage,BrandTypeId,RealeaseDate)
	VALUES('Tai nghe bluetooth SBH90C',2,'snsbh90c.png',3,'2018/03/02'),
		  ('Tai nghe bluetooth MBH22',2,'snmbh22.png',3,'2018/01/24'),
		  ('Sony Xperia Touch',2,'sntouch.png',3,'2017/10/25')

----------------------------INSERT PRODUCT ACCESSORIES---------------------------------------
--Samsung--
INSERT into ProductAccessory(ProductId,Color,Price,Quantiny,ImageFile)
	VALUES(14,'Black','850000',2,'hpmonoblt_black.png'),
		  (14,'White','850000',2,'hpmonoblt_white.png'),
		  (15,'Black','280000',2,'hpeg920_black.png'),
		  (15,'Red','280000',2,'hpeg920_red.png'),
		  (16,'Black','300000',2,'hpig935_black.png'),
		  (16,'White','300000',2,'hpig935_white.png'),
		  (17,'Black','1450000',2,'hplevelupro_black.png'),
		  (17,'Gold','1450000',2,'hplevelupro_gold.png'),
		  (18,'Black','2190000',2,'hpakgs8_black.png'),
		  (19,'White','850000',2,'spscoop_white.png'),
		  (20,'White','1500000',2,'spbottle_white.png')

--Sony--
INSERT into ProductAccessory(ProductId,Color,Price,Quantiny,ImageFile)
	VALUES(21,'Black','4490000',2,'snsbh90c_black.png'),
		  (21,'White','4490000',2,'snsbh90c_white.png'),
		  (22,'Black','890000',2,'snmbh22_black.png'),
		  (22,'White','890000',2,'snmbh22_white.png'),
		  (23,'Black','39990000',2,'sntouch.png')

select * from Product
select * from ProductAccessory where ProductId=20

SELECT *
FROM ProductPortableDevice
INNER JOIN Product ON ProductPortableDevice.ProductId=Product.ProductId;