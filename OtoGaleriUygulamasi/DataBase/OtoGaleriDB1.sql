-- =============================================
-- OTO GALERÝ VERÝTABANI ÞEMASI (MSSQL)
-- Normalize Edilmiþ Tasarým
-- =============================================

-- Veritabaný oluþturma
CREATE DATABASE OtoGaleriDB;
GO

USE OtoGaleriDB;
GO

-- =============================================
-- LOOKUP TABLOLAR (Referans Veriler)
-- =============================================

-- Marka Tablosu
CREATE TABLE Marka (
    MarkaID INT IDENTITY(1,1) PRIMARY KEY,
    MarkaAdi NVARCHAR(50) NOT NULL UNIQUE,
    Aktif BIT DEFAULT 1,
    OlusturmaTarihi DATETIME DEFAULT GETDATE()
);

-- Model Tablosu
CREATE TABLE Model (
    ModelID INT IDENTITY(1,1) PRIMARY KEY,
    MarkaID INT NOT NULL,
    ModelAdi NVARCHAR(50) NOT NULL,
    Aktif BIT DEFAULT 1,
    OlusturmaTarihi DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (MarkaID) REFERENCES Marka(MarkaID),
    CONSTRAINT UK_Marka_Model UNIQUE (MarkaID, ModelAdi)
);

-- Yakýt Tipi Tablosu
CREATE TABLE YakitTipi (
    YakitTipiID INT IDENTITY(1,1) PRIMARY KEY,
    YakitTipiAdi NVARCHAR(30) NOT NULL UNIQUE,
    Aktif BIT DEFAULT 1
);

-- Vites Tipi Tablosu
CREATE TABLE VitesTipi (
    VitesTipiID INT IDENTITY(1,1) PRIMARY KEY,
    VitesTipiAdi NVARCHAR(30) NOT NULL UNIQUE,
    Aktif BIT DEFAULT 1
);

-- Kasa Tipi Tablosu
CREATE TABLE KasaTipi (
    KasaTipiID INT IDENTITY(1,1) PRIMARY KEY,
    KasaTipiAdi NVARCHAR(30) NOT NULL UNIQUE,
    Aktif BIT DEFAULT 1
);

-- Renk Tablosu
CREATE TABLE Renk (
    RenkID INT IDENTITY(1,1) PRIMARY KEY,
    RenkAdi NVARCHAR(30) NOT NULL UNIQUE,
    Aktif BIT DEFAULT 1
);

-- =============================================
-- ANA TABLOLAR
-- =============================================

-- Ýlan Tablosu (Ana Tablo)
CREATE TABLE Ilan (
    IlanID INT IDENTITY(1,1) PRIMARY KEY,
    MarkaID INT NOT NULL,
    ModelID INT NOT NULL,
    Fiyat DECIMAL(18,2) NOT NULL,
    Yil INT NOT NULL,
    YakitTipiID INT NOT NULL,
    VitesTipiID INT NOT NULL,
    Kilometre INT NOT NULL,
    KasaTipiID INT NOT NULL,
    RenkID INT NOT NULL,
    AgirHasarKayitli BIT NOT NULL DEFAULT 0,
    Aciklama NVARCHAR(MAX),
    Durum BIT NOT NULL DEFAULT 1, -- 1: Satýþta, 0: Satýldý
    IlanTarihi DATETIME NOT NULL DEFAULT GETDATE(),
    GuncellemeTarihi DATETIME,
    SatisTarihi DATETIME,
    Aktif BIT DEFAULT 1,
    FOREIGN KEY (MarkaID) REFERENCES Marka(MarkaID),
    FOREIGN KEY (ModelID) REFERENCES Model(ModelID),
    FOREIGN KEY (YakitTipiID) REFERENCES YakitTipi(YakitTipiID),
    FOREIGN KEY (VitesTipiID) REFERENCES VitesTipi(VitesTipiID),
    FOREIGN KEY (KasaTipiID) REFERENCES KasaTipi(KasaTipiID),
    FOREIGN KEY (RenkID) REFERENCES Renk(RenkID),
    CONSTRAINT CHK_Yil CHECK (Yil >= 1900 AND Yil <= YEAR(GETDATE()) + 1),
    CONSTRAINT CHK_Kilometre CHECK (Kilometre >= 0),
    CONSTRAINT CHK_Fiyat CHECK (Fiyat >= 0)
);

-- Fotoðraf Tablosu (Bir ilana birden fazla fotoðraf)
CREATE TABLE IlanFotograf (
    FotografID INT IDENTITY(1,1) PRIMARY KEY,
    IlanID INT NOT NULL,
    FotografYolu NVARCHAR(500) NOT NULL,
    Sira INT DEFAULT 0, -- Fotoðraf sýrasý (1. fotoðraf vitrin fotoðrafý olabilir)
    YuklemeTarihi DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (IlanID) REFERENCES Ilan(IlanID) ON DELETE CASCADE
);

-- =============================================
-- ÝNDEXLER (Performans için)
-- =============================================

CREATE INDEX IX_Ilan_MarkaID ON Ilan(MarkaID);
CREATE INDEX IX_Ilan_ModelID ON Ilan(ModelID);
CREATE INDEX IX_Ilan_Durum ON Ilan(Durum);
CREATE INDEX IX_Ilan_IlanTarihi ON Ilan(IlanTarihi);
CREATE INDEX IX_Ilan_Fiyat ON Ilan(Fiyat);
CREATE INDEX IX_Model_MarkaID ON Model(MarkaID);
CREATE INDEX IX_IlanFotograf_IlanID ON IlanFotograf(IlanID);

-- =============================================
-- ÖRNEK VERÝ EKLEME
-- =============================================

-- Markalar
INSERT INTO Marka (MarkaAdi) VALUES 
('Abarth'), ('Aion'), ('Alfa Romeo'), ('Alpine'), ('Anadol'), ('Arora'), ('Aston Martin'), 
('Audi'), ('Bentley'), ('BMW'), ('Buick'), ('BYD'), ('Cadillac'), ('Cenntro'), 
('Chery'), ('Chevrolet'), ('Chrysler'), ('Citroen'), ('Cupra'), ('Dacia'), ('Daewoo'), 
('Daihatsu'), ('Dodge'), ('DS Automobiles'), ('Eagle'), ('Ferrari'), ('Fiat'), ('Ford'), 
('Geely'), ('Honda'), ('Hyundai'), ('I-GO'), ('Ikco'), ('Infiniti'), ('Jaguar'), 
('Joyce'), ('Kia'), ('Kuba'), ('Lada'), ('Lamborghini'), ('Lancia'), ('Leapmotor'), 
('Lexus'), ('Lincoln'), ('Lotus'), ('Luqi'), ('Marcos'), ('Maserati'), ('Mazda'), 
('McLaren'), ('Mercedes-Benz'), ('MG'), ('Micro'), ('Mini'), ('Mitsubishi'), ('Morgan'), 
('Nieve'), ('Niðmer'), ('Nissan'), ('Opel'), ('Orti'), ('Peugeot'), ('Plymouth'), 
('Polestar'), ('Pontiac'), ('Porsche'), ('Proton'), ('Rainwoll'), ('Reeder'), 
('Regal Raptor'), ('Relive'), ('Renault'), ('RKS'), ('Rolls-Royce'), ('Rover'), 
('Saab'), ('Saipa'), ('Seat'), ('Skoda'), ('Smart'), ('Subaru'), ('Suzuki'), 
('Tata'), ('Tesla'), ('The London Taxi'), ('Tofaþ'), ('TOGG'), ('Toyota'), 
('Vanderhall'), ('Volkswagen'), ('Volta'), ('Volvo'), ('XEV'), ('Yuki');

-- Modeller (Örnek)
INSERT INTO Model (MarkaID, ModelAdi) VALUES
(3, 'Giulia'), (3, 'Giuletta'), (3, 'MiTo'), 
(8, 'A1'), (8, 'A2'), (8, 'A3'), (8, 'A4'), (8, 'A5'), (8, 'A6'), (8, 'A7'), (8, 'A8'),
(10, '116d'), (10, '316i'), (10, '318i'), (10, '320d'), (10, '320i ED'), (10, '520d'), (10, '520i'),
(16, 'Aveo'), (16, 'Cruze'), (16, 'Kalos'), (16, 'Lacetti'), (16, 'Spark'),
(18, 'C1'), (18, 'C2'), (18, 'C3'), (18, 'C4'), (18, 'C5'), (18, 'C-Elysee'), (18, 'Saxo'),
(20, 'Logan'), (20, 'Sandero'), (20, 'Stepway'), (20, 'Duster'), 
(27, 'Albea'), (27, 'Brava'), (27, 'Bravo'), (27, 'Egea'), (27, 'Linea'), (27, 'Marea'), (27, 'Palio'), (27, 'Panda'), (27, 'Punto'), 
(28, 'Fiesta'), (28, 'Focus'), (28, 'Escort'), (28, 'Fusion'),
(30, 'Accord'), (30, 'City'), (30, 'Civic'), (30, 'Jazz'),
(31, 'Accent'), (31, 'Accent Era'), (31, 'Accent Blue'), (31, 'Atos'), (31, 'Elantra'), (31, 'Getz'), (31, 'i10'), (31, 'i20'), (31, 'i30'),
(37, 'Rio'), (37, 'Picanto'),
(51, 'A-Serisi'), (51, 'C-Serisi'), (51, 'E-Serisi'),
(59, 'MÝcra'), (59, 'Note'), (59, 'Juke'), (59, 'Qashqai'), (59, 'Primera'),
(60, 'Astra'), (60, 'Corsa'), (60, 'Insignia'), (60, 'Tigra'),
(62, '106'), (62, '206'), (62, '206+'), (62, '207'), (62, '208'), (62, '307'), (62, '308'), 
(72, 'Clio'), (72, 'Fluence'), (72, 'Laguna'), (72, 'Latitude'), (72, 'Megane'), (72, 'Scenic'), (72, 'Symbol'), (72, 'Taliant'), (72, 'Talisman'), (72, 'Twingo'),
(78, 'Cordoba'), (78, 'Ibiza'), (78, 'Leon'), (78, 'Toledo'),
(79, 'Fabia'), (79, 'Octavia'), (79, 'Superb'),
(82, 'Swift'),
(86, 'Doðan'), (86, 'Kartal'), (86, 'Þahin'),
(87, 'T10-F'), (87, 'T10-X'),
(88, 'Auris'), (88, 'Corolla'), (88, 'Yaris'),
(90, 'Bora'), (90, 'Golf'), (90, 'Jetta'), (90, 'Passat'), (90, 'Polo'), (90, 'Scirocco');

-- Yakýt Tipleri
INSERT INTO YakitTipi (YakitTipiAdi) VALUES 
('Benzin'), ('Dizel'), ('Elektrik'), ('Hybrid'), ('Benzin + LPG');

-- Vites Tipleri
INSERT INTO VitesTipi (VitesTipiAdi) VALUES 
('Manuel'), ('Otomatik');

-- Kasa Tipleri
INSERT INTO KasaTipi (KasaTipiAdi) VALUES 
('Sedan'), ('Hatchback'), ('SUV'), ('Station Wagon'), 
('Coupe'), ('Cabrio'), ('MPV'), ('Pick-up');

-- Renkler
INSERT INTO Renk (RenkAdi) VALUES 
('Bej'), ('Beyaz'), ('Bordo'), ('Füme'), ('Gri'), ('Gümüþ Gri'), ('Kahverengi'), ('Kýrmýzý'), ('Lacivert'), ('Mavi'), ('Mor'), ('Pembe'),
('Sarý'), ('Siyah'), ('Þampanya'), ('Turkuaz'), ('Turuncu'), ('Yeþil');

-- Örnek Ýlanlar
INSERT INTO Ilan 
(MarkaID, ModelID, Fiyat, Yil, YakitTipiID, VitesTipiID, Kilometre, KasaTipiID, RenkID, AgirHasarKayitli, Aciklama, Durum) 
VALUES 
-- 1. Ýlan: Volkswagen Golf (Marka 90, Model Golf, Fiyat 1.2M, 2022 Yýlý, Hasar Kaydý Yok)
(90, (SELECT ModelID FROM Model WHERE ModelAdi = 'Golf' AND MarkaID = 90), 1250000.00, 2022, 1, 1, 15000, 2, 2, 0, 'Ýlk sahibinden temiz araç.', 1),

-- 2. Ýlan: BMW 320d (Marka 10, Model 320d, Fiyat 2.1M, 2020 Yýlý, Hasar Kaydý Yok)
(10, (SELECT ModelID FROM Model WHERE ModelAdi = '320d' AND MarkaID = 10), 2100000.00, 2020, 2, 2, 85000, 1, 14, 0, 'Yetkili servis bakýmlý.', 1),

-- 3. Ýlan: Fiat Egea (Marka 27, Model Egea, Fiyat 850bin, 2023 Yýlý, Hasar Kaydý Yok)
(27, (SELECT ModelID FROM Model WHERE ModelAdi = 'Egea' AND MarkaID = 27), 850000.00, 2023, 5, 1, 5000, 1, 5, 0, 'Sýfýr ayarýnda, masrafsýz.', 1);

-- Örnek Fotoðraflar
/*
INSERT INTO IlanFotograf (IlanID, FotografYolu, Sira) VALUES
(1, 'C:\OtoGaleri\Fotograflar\golf_1_on.jpg', 1),
(1, 'C:\OtoGaleri\Fotograflar\golf_1_ic.jpg', 2),
(1, 'C:\OtoGaleri\Fotograflar\golf_1_arka.jpg', 3),
(2, 'C:\OtoGaleri\Fotograflar\corolla_1_on.jpg', 1),
(2, 'C:\OtoGaleri\Fotograflar\corolla_1_ic.jpg', 2);
*/

GO