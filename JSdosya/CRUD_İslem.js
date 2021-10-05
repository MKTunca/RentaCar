function musteriSil(ID) {

    bootbox.confirm('aracı silmek istiyor musunuz?', function (cevap) {

        if (cevap) {

            $.ajax({
                url: '/Museri/sil',
                data: { ID: ID },
                success: function (sonuc) {

                    bootbox.alert(sonuc);
                    $('#satir_' + ID).remove();
                },
                error: function () {

                }
            });

        }
        else {
            bootbox.alert('Silme işlemi iptal edildi.');
        }

    })
}

function aracSil(ID) {

    bootbox.confirm('aracı silmek istiyor musunuz?', function (cevap) {

        if (cevap) {

            $.ajax({
                url: '/Araclar/sil',
                data: { ID: ID },
                success: function (sonuc) {

                    bootbox.alert(sonuc);
                    $('#satir_' + ID).remove();
                },
                error: function () {

                }
            });

        }
        else {
            bootbox.alert('Silme işlemi iptal edildi.');
        }

    })
}

function musteriDegistir(id) {

    var adSoyad = document.getElementById('ad_' + id);
    var dogum = document.getElementById('dogum_' + id);
    var ehliyet = document.getElementById('ehliyet_' + id);
    var email = document.getElementById('email_' + id);
    var sifre = document.getElementById('sifre_' + id);
    var telefon = document.getElementById('telefon_' + id);
    var sehir = document.getElementById('sehir_' + id);
    var adres = document.getElementById('adres_' + id);
   


    if ((adSoyad.value != '') && (dogum.value != '') && (ehliyet.value != '') && (email.value != '') && (sifre.value != '') && (telefon.value != '') && (sehir.value != '') && (adres.value != '')) {

      
        $.ajax({

            url: '/Musteri/degisim',
            data: { id: id, adSoyad: adSoyad.value, dogum: dogum.value, ehliyet: ehliyet.value, email: email.value, sifre: sifre.value, telefon: telefon.value, sehir: sehir.value, adres: adres.value },
            type: 'POST',
            success: function (cevap) {


                if (cevap == "Yetkisiz giriş") {

                    Window.location = "/Admin/login";

                }
                else {
                   bootbox.alert(cevap);
                }
                bootbox.alert('Bilgilerinz Güncellendi..')
            },
            error: function () {

            }
            
        });
    }
    else {
        bootbox.alert('Alanları boş bırakmayınız!');
    }


}

function bilgiDegiss(id) {
    var marka = document.getElementById('marka_' + id);
    var model = document.getElementById('model_' + id);
    var durum = document.getElementById('durumlar_' + id);
    var plaka = document.getElementById('plaka_' + id);
    var yil = document.getElementById('yil_' + id);
    var vites = document.getElementById('vites_' + id);
    var yakit = document.getElementById('yakit_' + id);

    if ((marka.value != '') && (model.value != '') && (plaka.value != '') && (yakit.value != '') && (vites.value != '') && (yil.value != '')) {

        var dataBilgi = {};
        dataBilgi.aracyonetim = { ID: id, marka: marka.value, model: model.value, plaka: plaka.value, yakit: yakit.value, vites: vites.value };

        $.ajax({

            url: '/Araclar/degistir',
            data: dataBilgi,
            type: 'POST',
            success: function (cevap) {

           
                if (cevap == "Yetkisiz giriş") {

                    Window.location = "/Admin/login";

                }
                else {
                    bootbox.alert(cevap);
                }
                bootbox.alert('Bilgilerinz Güncellendi..')
            }
        });
    }
    else {
        bootbox.alert('Alanları boş bırakmayınız!');
        
    }


}

function imajYenile(id) {

    var formData = new FormData();
    var imaj = document.getElementById('imaj_' + id);
    var logo = document.getElementById('logo_' + id);


    formData.append(imaj.files[0].name, imaj.files[0]);

    formData.append("id", id);

    var xhr = new XMLHttpRequest();

    xhr.open('POST', '/Araclar/imaj_degistir');
    xhr.send(formData);
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {

            logo.src = '/imajlar/' + imaj.files[0].name;
            bootbox.alert("İmaj Güncellendi.");

        }
    }

}

function bilgiDegis(id) {

    var email = document.getElementById('email_' + id);
    var sifre = document.getElementById('sifre_' + id);
    var telefon = document.getElementById('telefon_' + id);

    if ((email.value != "") && (sifre.value != "") && (telefon.value != "")) {


        $.ajax({
            url: '/AdminCRUD/degisim',
            type: 'POST',
            data: {
                id: id, email: email.value, sifre: sifre.value, telefon: telefon.value            
            
            },
            success: function () {
                bootbox.alert('güncellendi')
            },
            error: function () {

            }
            
        })
    }

}