-- =============================================
-- OTO GALERÝ - VIEW TANIMLARI
-- Sýk kullanýlan sorgularý basitleþtirmek için
-- =============================================

USE OtoGaleriDB;
GO

-- =============================================
-- 1. VW_AktifIlanlar - Satýþta olan tüm ilanlar
-- =============================================
CREATE VIEW VW_AktifIlanlar AS
SELECT 
    i.IlanID,
    m.MarkaAdi,
    md.ModelAdi,
    m.MarkaAdi + ' ' + md.ModelAdi AS AracTamAdi,
    i.Fiyat,
    i.Yil,
    y.YakitTipiAdi,
    v.VitesTipiAdi,
    i.Kilometre,
    k.KasaTipiAdi,
    r.RenkAdi,
    CASE WHEN i.AgirHasarKayitli = 1 THEN 'Evet' ELSE 'Hayýr' END AS AgirHasar,
    i.Aciklama,
    i.IlanTarihi,
    DATEDIFF(DAY, i.IlanTarihi, GETDATE()) AS IlandanItibaren_Gun
FROM Ilan i
INNER JOIN Marka m ON i.MarkaID = m.MarkaID
INNER JOIN Model md ON i.ModelID = md.ModelID
INNER JOIN YakitTipi y ON i.YakitTipiID = y.YakitTipiID
INNER JOIN VitesTipi v ON i.VitesTipiID = v.VitesTipiID
INNER JOIN KasaTipi k ON i.KasaTipiID = k.KasaTipiID
INNER JOIN Renk r ON i.RenkID = r.RenkID
WHERE i.Durum = 1 AND i.Aktif = 1;
GO

-- Kullanýmý:
-- SELECT * FROM VW_AktifIlanlar ORDER BY IlanTarihi DESC;

-- =============================================
-- 2. VW_TumIlanlar - Tüm ilanlar (satýþta + satýlan)
-- =============================================
CREATE VIEW VW_TumIlanlar AS
SELECT 
    i.IlanID,
    m.MarkaAdi,
    md.ModelAdi,
    m.MarkaAdi + ' ' + md.ModelAdi AS AracTamAdi,
    i.Fiyat,
    i.Yil,
    y.YakitTipiAdi,
    v.VitesTipiAdi,
    i.Kilometre,
    k.KasaTipiAdi,
    r.RenkAdi,
    CASE WHEN i.AgirHasarKayitli = 1 THEN 'Evet' ELSE 'Hayýr' END AS AgirHasar,
    i.Aciklama,
    CASE WHEN i.Durum = 1 THEN 'Satýþta' ELSE 'Satýldý' END AS Durum,
    i.IlanTarihi,
    i.SatisTarihi,
    CASE 
        WHEN i.Durum = 0 THEN DATEDIFF(DAY, i.IlanTarihi, i.SatisTarihi)
        ELSE NULL 
    END AS SatilmaGunSayisi
FROM Ilan i
INNER JOIN Marka m ON i.MarkaID = m.MarkaID
INNER JOIN Model md ON i.ModelID = md.ModelID
INNER JOIN YakitTipi y ON i.YakitTipiID = y.YakitTipiID
INNER JOIN VitesTipi v ON i.VitesTipiID = v.VitesTipiID
INNER JOIN KasaTipi k ON i.KasaTipiID = k.KasaTipiID
INNER JOIN Renk r ON i.RenkID = r.RenkID
WHERE i.Aktif = 1;
GO

-- =============================================
-- 3. VW_SatilanIlanlar - Sadece satýlan ilanlar
-- =============================================
CREATE VIEW VW_SatilanIlanlar AS
SELECT 
    i.IlanID,
    m.MarkaAdi + ' ' + md.ModelAdi AS AracTamAdi,
    i.Fiyat,
    i.Yil,
    i.Kilometre,
    i.IlanTarihi,
    i.SatisTarihi,
    DATEDIFF(DAY, i.IlanTarihi, i.SatisTarihi) AS SatilmaGunSayisi
FROM Ilan i
INNER JOIN Marka m ON i.MarkaID = m.MarkaID
INNER JOIN Model md ON i.ModelID = md.ModelID
WHERE i.Durum = 0 AND i.Aktif = 1;
GO

-- =============================================
-- 4. VW_IlanFotograflar - Ýlanlar ve fotoðraflarý
-- =============================================
CREATE VIEW VW_IlanFotograflar AS
SELECT 
    i.IlanID,
    m.MarkaAdi + ' ' + md.ModelAdi AS AracTamAdi,
    i.Fiyat,
    i.Durum,
    f.FotografID,
    f.FotografYolu,
    f.Sira,
    CASE WHEN f.Sira = 1 THEN 1 ELSE 0 END AS VitrinFotografi
FROM Ilan i
INNER JOIN Marka m ON i.MarkaID = m.MarkaID
INNER JOIN Model md ON i.ModelID = md.ModelID
LEFT JOIN IlanFotograf f ON i.IlanID = f.IlanID
WHERE i.Aktif = 1;
GO

-- =============================================
-- 5. VW_StokDurumu - Stok analizi
-- =============================================
CREATE VIEW VW_StokDurumu AS
SELECT 
    m.MarkaAdi,
    COUNT(i.IlanID) AS ToplamAdet,
    SUM(CASE WHEN i.Durum = 1 THEN 1 ELSE 0 END) AS SatistakiAdet,
    SUM(CASE WHEN i.Durum = 0 THEN 1 ELSE 0 END) AS SatilanAdet,
    AVG(i.Fiyat) AS OrtalamFiyat,
    MIN(i.Fiyat) AS EnDusukFiyat,
    MAX(i.Fiyat) AS EnYuksekFiyat
FROM Marka m
LEFT JOIN Ilan i ON m.MarkaID = i.MarkaID AND i.Aktif = 1
GROUP BY m.MarkaAdi;
GO

-- =============================================
-- 6. VW_YillikSatisRaporu - Yýllýk satýþ analizi
-- =============================================
CREATE VIEW VW_YillikSatisRaporu AS
SELECT 
    YEAR(i.SatisTarihi) AS SatisYili,
    MONTH(i.SatisTarihi) AS SatisAyi,
    DATENAME(MONTH, i.SatisTarihi) AS AyAdi,
    COUNT(i.IlanID) AS SatilanAdet,
    SUM(i.Fiyat) AS ToplamCiro,
    AVG(i.Fiyat) AS OrtalamaSatisFiyati,
    AVG(DATEDIFF(DAY, i.IlanTarihi, i.SatisTarihi)) AS OrtSatilmaGunSayisi
FROM Ilan i
WHERE i.Durum = 0 AND i.Aktif = 1 AND i.SatisTarihi IS NOT NULL
GROUP BY YEAR(i.SatisTarihi), MONTH(i.SatisTarihi), DATENAME(MONTH, i.SatisTarihi);
GO

-- =============================================
-- 7. VW_PopulerMarkalar - En çok ilanlanan markalar
-- =============================================
CREATE VIEW VW_PopulerMarkalar AS
SELECT 
    m.MarkaAdi,
    COUNT(i.IlanID) AS IlanSayisi,
    SUM(CASE WHEN i.Durum = 1 THEN 1 ELSE 0 END) AS AktifIlanSayisi,
    AVG(i.Fiyat) AS OrtalamFiyat
FROM Marka m
LEFT JOIN Ilan i ON m.MarkaID = i.MarkaID AND i.Aktif = 1
GROUP BY m.MarkaAdi;
GO

-- =============================================
-- 8. VW_EnYeniIlanlar - Son eklenen ilanlar
-- =============================================
CREATE VIEW VW_EnYeniIlanlar AS
SELECT TOP 10
    i.IlanID,
    m.MarkaAdi + ' ' + md.ModelAdi AS AracTamAdi,
    i.Fiyat,
    i.Yil,
    i.Kilometre,
    i.IlanTarihi,
    DATEDIFF(DAY, i.IlanTarihi, GETDATE()) AS KacGunOnce
FROM Ilan i
INNER JOIN Marka m ON i.MarkaID = m.MarkaID
INNER JOIN Model md ON i.ModelID = md.ModelID
WHERE i.Durum = 1 AND i.Aktif = 1
ORDER BY i.IlanTarihi DESC;
GO

-- =============================================
-- KULLANIM ÖRNEKLERÝ
-- =============================================

-- Aktif ilanlarý listele
-- SELECT * FROM VW_AktifIlanlar;

-- Belirli bir markayý filtrele
-- SELECT * FROM VW_AktifIlanlar WHERE MarkaAdi = 'Volkswagen';

-- Stok durumunu görüntüle
-- SELECT * FROM VW_StokDurumu ORDER BY ToplamAdet DESC;

-- Bu ayýn satýþlarý
-- SELECT * FROM VW_YillikSatisRaporu WHERE SatisYili = YEAR(GETDATE()) AND SatisAyi = MONTH(GETDATE());

-- En yeni ilanlar
-- SELECT * FROM VW_EnYeniIlanlar;