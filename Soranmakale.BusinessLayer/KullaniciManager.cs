using Soranmakale.BusinessLayer.Abstract;
using Soranmakale.BusinessLayer.Result;
using Soranmakale.Entities;
using Soranmakale.Entities.ErrorMessageObject;
using Soranmakale.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soranmakale.BusinessLayer
{
    public class KullaniciManager : BaseManager<Kullanici>
    {
        public BusinessLayerResult<Kullanici> KullaniciKayit(RegisterViewModel data)
        {
            BusinessLayerResult<Kullanici> blr = new BusinessLayerResult<Kullanici>();

            Kullanici kullanici = Find(x => x.KullaniciAdi == data.KullaniciAdi || x.Email == data.Eposta);

            if(kullanici != null)
            {
                if(data.KullaniciAdi == kullanici.KullaniciAdi)
                {
                    blr.AddError("Kullanıcı adı sistemde kayıtlıdır.", ErrorMessageCode.KullaniciAdiKayitli);
                }
                if(data.Eposta == kullanici.Email)
                {
                    blr.AddError("E-posta adresi sistemde kayıtlıdır.", ErrorMessageCode.EpostaAdresiKayitli);
                }
            }
            else
            {
                int db_result = Insert(new Kullanici()
                {
                    KullaniciAdi = data.KullaniciAdi,
                    Sifre = data.Sifre,
                    Email = data.Eposta,
                    AdminMi = false,
                    YazarMi = false,
                    ProfilFotografi = "user_img.png",
                    KayitTarihi = DateTime.Now,
                    DogumTarihi = DateTime.Now
                });

                if(db_result > 0)
                {
                    blr.Result = Find(x => x.KullaniciAdi == data.KullaniciAdi && x.Email == data.Eposta);
                }

            }

            return blr;
        }

        public BusinessLayerResult<Kullanici> KullaniciGiris(LoginViewModel data)
        {
            BusinessLayerResult<Kullanici> blr = new BusinessLayerResult<Kullanici>();

            Kullanici kullanici = Find(x => x.KullaniciAdi == data.KullaniciAdi && x.Sifre == data.Sifre);

            if(kullanici == null)
            {
                blr.AddError("Kullanıcı adı veya şifre uyuşmuyor.",ErrorMessageCode.KullaniciAdiveyaSifreUyusmuyor);
            }
            else
            {
                blr.Result = kullanici;
            }

            return blr;
        }

        public BusinessLayerResult<Kullanici> KullaniciGetir(int id)
        {
            BusinessLayerResult<Kullanici> blr = new BusinessLayerResult<Kullanici>();

            blr.Result = Find(x => x.ID == id);

            if(blr.Result == null)
            {
                blr.AddError("Böyle bir kullanıcı yok.", ErrorMessageCode.KullaniciBulunamadi);
            }

            return blr;
        }

        public BusinessLayerResult<Kullanici> ProfileGuncelleme(Kullanici data)
        {
            Kullanici db_user = Find(x => x.ID == data.ID && (x.KullaniciAdi == data.KullaniciAdi || x.Email == data.Email));
            BusinessLayerResult<Kullanici> res = new BusinessLayerResult<Kullanici>();

            if (db_user != null && db_user.ID != data.ID)
            {
                if (db_user.KullaniciAdi == data.KullaniciAdi)
                {
                    res.AddError( "Kullanıcı adı kayıtlı.", ErrorMessageCode.KullaniciAdiKayitli);
                }

                if (db_user.Email == data.Email)
                {
                    res.AddError( "E-posta adresi kayıtlı.", ErrorMessageCode.EpostaAdresiKayitli);
                }

                return res;
            }

            res.Result = Find(x => x.ID == data.ID);
            res.Result.Email = data.Email;
            res.Result.Ad = data.Ad;
            res.Result.Soyad = data.Soyad;
            res.Result.Sifre = data.Sifre;
            res.Result.KullaniciAdi = data.KullaniciAdi;


            if (Update(res.Result) == 0)
            {
                res.AddError("Profil güncellenemedi.", ErrorMessageCode.ProfilGuncellenemedi);
            }

            return res;
        }
    }
}
