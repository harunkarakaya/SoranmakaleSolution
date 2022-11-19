using Soranmakale.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soranmakale.DataAccessLayer.EntityFramework
{
    public class MyInitializer : CreateDatabaseIfNotExists<DatabaseContext>
    {
        public DateTime Fakedata { get; private set; }

        protected override void Seed(DatabaseContext context)
        {
            Kullanici admin = new Kullanici()
            {
                Ad = "Harun",
                Soyad = "karakaya",
                KullaniciAdi = "harun67",
                Sifre = "123456",
                AdminMi = true,
                DogumTarihi = DateTime.Now,
                KayitTarihi = DateTime.Now,
                ProfilFotografi = "user_img.png",
                Email = "karakayaharun67@gmail.com",
                YazarMi = true

            };

            Kullanici admin2 = new Kullanici()
            {
                Ad = "Yusuf",
                Soyad = "karakaya",
                KullaniciAdi = "yusuf67",
                Sifre = "654321",
                AdminMi = true,
                DogumTarihi = DateTime.Now,
                KayitTarihi = DateTime.Now,
                ProfilFotografi = "user_img.png",
                Email = "yusufkarakaya67@gmail.com",
                YazarMi = true

            };

            context.Kullanici.Add(admin);
            context.Kullanici.Add(admin2);

            //Kullanıcılar
            for (int i = 0; i < 10; i++)
            {
                Kullanici user = new Kullanici()
                {
                    Ad = FakeData.NameData.GetFirstName(),
                    Soyad = FakeData.NameData.GetSurname(),
                    KullaniciAdi = $"user{i}",
                    Sifre = "654321",
                    AdminMi = false,
                    DogumTarihi = DateTime.Now,
                    KayitTarihi = DateTime.Now,
                    ProfilFotografi = "user_img.png",
                    Email = $"user{i}@gmail.com",
                    YazarMi = false
                };
                context.Kullanici.Add(user);
            }
            context.SaveChanges();

            //Oluşturulan kullanıcılar
            List<Kullanici> kullanicilar = context.Kullanici.ToList();

            //Kategoriler - İçerikler
            for (int i = 0; i < 10; i++)
            {


                Kategori kat = new Kategori()
                {
                    KategoriAdi = FakeData.PlaceData.GetStreetName(),
                    OlusturulmaTarihi = DateTime.Now
                };

                context.Kategori.Add(kat);

                for (int u = 0; u < 4; u++)
                {
                    //Makale,makale yorumu,makale beğenisi
                    for (int j = 0; j < FakeData.NumberData.GetNumber(1, 7); j++)
                    {
                        Kullanici sahip = kullanicilar[FakeData.NumberData.GetNumber(0, 11)];

                        Makale makale = new Makale()
                        {
                            Baslik = FakeData.PlaceData.GetStreetName(),
                            BegenilmeSayisi = 0 ,
                            DuzenlenmeTarihi = DateTime.Now,
                            Icerik = FakeData.TextData.GetSentences(300),
                            OkunmaSuresi = 10,
                            OlusturulmaTarihi = DateTime.Now,
                            Sahibi = sahip,
                            GoruntulenmeSayisi = FakeData.NumberData.GetNumber(60),
                            Kategori = kat
                        };

                        kat.Makaleleri.Add(makale);

                        for (int k = 0; k < FakeData.NumberData.GetNumber(3, 8); k++)
                        {
                            Kullanici yorum_sahip = kullanicilar[FakeData.NumberData.GetNumber(0, 11)];

                            MakaleninYorumu mak_yor = new MakaleninYorumu()
                            {
                                Icerik = FakeData.TextData.GetAlphaNumeric(50),
                                DuzenlenmeTarihi = DateTime.Now,
                                OlusturulmaTarihi = DateTime.Now,
                                Sahibi = yorum_sahip,
                                Makale = makale,
                                SahibiAdi = yorum_sahip.Ad
                            };

                            makale.Yorumlari.Add(mak_yor);
                        }

                        for (int l = 0; l < FakeData.NumberData.GetNumber(2, 12); l++)
                        {
                            MakaleninBegenisi makale_begeni = new MakaleninBegenisi()
                            {
                                BegenenKisi = kullanicilar[l],
                                Makale = makale
                            };

                            makale.Begenileri.Add(makale_begeni);
                            makale.BegenilmeSayisi = makale.BegenilmeSayisi + 1;
                        }

                        for (int d = 0; d < FakeData.NumberData.GetNumber(3, 7); d++)
                        {
                            FavlananMakale fav = new FavlananMakale()
                            {
                                FavKisi = kullanicilar[FakeData.NumberData.GetNumber(0, 11)],
                                Makale = makale
                            };

                            makale.FavlananMak.Add(fav);
                        }

                    }

                    ////Soru,sorunun yorumu, sorunun yorumunun beğenisi
                    for (int k = 0; k < FakeData.NumberData.GetNumber(2, 5); k++)
                    {
                        Kullanici sahip = kullanicilar[FakeData.NumberData.GetNumber(0, 11)];

                        Soru soru = new Soru()
                        {
                            Baslik = FakeData.PlaceData.GetState(),
                            DuzenlenmeTarihi = DateTime.Now,
                            GoruntulenmeSayisi = FakeData.NumberData.GetNumber(20, 60),
                            Icerik = FakeData.TextData.GetSentences(50),
                            Kategori = kat,
                            Sahibi = sahip,
                            OlusturulmaTarihi = DateTime.Now

                        };

                        kat.Sorulari.Add(soru);

                        for (int m = 0; m < FakeData.NumberData.GetNumber(5, 12); m++)
                        {
                            Kullanici yorum_sahibi = kullanicilar[FakeData.NumberData.GetNumber(0, 11)];

                            SorununYorumu sorunun_yorumu = new SorununYorumu()
                            {
                                Icerik = FakeData.TextData.GetAlphaNumeric(50),
                                BegenilmeSayisi = 0,
                                DuzenlenmeTarihi = DateTime.Now,
                                OlusturulmaTarihi = DateTime.Now,
                                Sahibi = yorum_sahibi,
                                SahibiAdi = yorum_sahibi.Ad,
                                Soru = soru
                            };

                            soru.Yorumlari.Add(sorunun_yorumu);


                            for (int n = 0; n < FakeData.NumberData.GetNumber(6, 12); n++)
                            {

                                Kullanici begenen_kisi = kullanicilar[n];

                                SorununYorumununBegenisi syb = new SorununYorumununBegenisi()
                                {
                                    BegenenKisi = begenen_kisi,
                                    Yorum = sorunun_yorumu
                                };

                                sorunun_yorumu.BegenilmeSayisi = sorunun_yorumu.BegenilmeSayisi + 1;
                                sorunun_yorumu.Begenileri.Add(syb);
                            }
                        }

                        for (int e = 0; e < FakeData.NumberData.GetNumber(3, 7); e++)
                        {
                            FavlananSoru fav = new FavlananSoru()
                            {
                                FavKisi = kullanicilar[FakeData.NumberData.GetNumber(0, 11)],
                                Soru = soru
                            };
                            soru.FavlananSoru.Add(fav);
                        }

                    }

                    ////Anket, Anketin Yorumu, anketin cevabı
                    for (int a = 0; a < FakeData.NumberData.GetNumber(1, 6); a++)
                    {
                        Kullanici sahip = kullanicilar[FakeData.NumberData.GetNumber(0, 11)];

                        Anket anket = new Anket()
                        {
                            Baslik = FakeData.PlaceData.GetCountry(),
                            GoruntulenmeSayisi = FakeData.NumberData.GetNumber(20, 60),
                            Kategori = kat,
                            KatilimSayisi = FakeData.NumberData.GetNumber(20, 60),
                            OlusturulmaTarihi = DateTime.Now,
                            Sahibi = sahip,
                            SonOylamaTarihi = DateTime.Now.AddDays(10)
                        };

                        kat.Anketleri.Add(anket);

                        for (int b = 0; b < 4; b++)
                        {
                            AnketinCevabi ac = new AnketinCevabi()
                            {
                                SecenekAdi = FakeData.PlaceData.GetCity(),
                                Anketi = anket,
                                AnketinAdi = anket.Baslik
                            };
                            anket.Cevaplari.Add(ac);


                            for (int h = 0; h < FakeData.NumberData.GetNumber(5, 10); h++)
                            {
                                Kullanici cevabi_secen = kullanicilar[FakeData.NumberData.GetNumber(0, 11)];

                                AnketinCevabiniSecenler acs = new AnketinCevabiniSecenler()
                                {
                                    SecenKisi = cevabi_secen,
                                    KisininAdi = cevabi_secen.Ad,
                                    AnketinCevabi = ac,
                                    Anket = anket,
                                    SeceneginAdi = ac.SecenekAdi
                                };

                                ac.Secenler.Add(acs);
                            }
                        }

                        for (int c = 0; c < FakeData.NumberData.GetNumber(5, 10); c++)
                        {
                            Kullanici yorum_sahib = kullanicilar[FakeData.NumberData.GetNumber(0, 11)];

                            AnketinYorumu ay = new AnketinYorumu()
                            {
                                DuzenlenmeTarihi = DateTime.Now.AddDays(1),
                                Anketi = anket,
                                Icerik = FakeData.TextData.GetAlphaNumeric(50),
                                OlusturulmaTarihi = DateTime.Now,
                                Sahibi = yorum_sahib,
                                SahibiAdi = yorum_sahib.Ad
                            };

                            anket.Yorumu.Add(ay);
                        }


                        for (int g = 0; g < FakeData.NumberData.GetNumber(3, 7); g++)
                        {
                            Kullanici fav_kisi = kullanicilar[FakeData.NumberData.GetNumber(0, 11)];

                            FavlananAnket fav = new FavlananAnket()
                            {
                                FavKisi = fav_kisi,
                                Anket = anket
                            };
                            anket.FavlananAnket.Add(fav);
                        }
                    }
                }

            }
            context.SaveChanges();
        }
    }
}
